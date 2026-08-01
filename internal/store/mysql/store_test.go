package mysql

import (
	"context"
	"database/sql"
	"os"
	"testing"
	"time"

	"github.com/Markcus0526/carsaleman/internal/auth"
)

// Integration tests. They need a real MySQL because the things most likely to
// be wrong -- charset negotiation, NULL handling, DATETIME round-tripping,
// constraint behaviour -- are exactly what a fake would paper over.
//
//	CARSALEMAN_TEST_DSN=root:test@tcp(127.0.0.1:13306)/csm go test ./internal/store/mysql/
//
// Skipped when the variable is unset, so `go test ./...` stays runnable
// without Docker.
func testDB(t *testing.T) *sql.DB {
	t.Helper()

	dsn := os.Getenv("CARSALEMAN_TEST_DSN")
	if dsn == "" {
		t.Skip("CARSALEMAN_TEST_DSN not set; skipping MySQL integration tests")
	}

	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancel()

	db, err := Open(ctx, dsn)
	if err != nil {
		t.Fatalf("Open: %v", err)
	}
	t.Cleanup(func() { db.Close() })

	// Order matters: sessions and permissions reference users.
	for _, tbl := range []string{"tbl_session", "tbl_permission", "tbl_userinfo"} {
		if _, err := db.ExecContext(ctx, "DELETE FROM "+tbl); err != nil {
			t.Fatalf("clearing %s: %v", tbl, err)
		}
	}
	return db
}

func insertUser(t *testing.T, db *sql.DB, username, legacy, bcryptHash string) int64 {
	t.Helper()
	res, err := db.Exec(
		`INSERT INTO tbl_userinfo (departmentcode, departmentname, username, password, password_bcrypt)
		 VALUES ('D1','销售部',?,?,?)`,
		username, nullIfEmpty(legacy), nullIfEmpty(bcryptHash))
	if err != nil {
		t.Fatalf("insert user: %v", err)
	}
	id, _ := res.LastInsertId()
	return id
}

// ---------------------------------------------------------------------------

// The charset check exists because getting this wrong is silent: a narrower
// connection charset mangles 4-byte characters with no error.
func TestOpenRejectsNonUtf8mb4(t *testing.T) {
	db := testDB(t)

	var client, conn string
	if err := db.QueryRow("SELECT @@character_set_client, @@character_set_connection").Scan(&client, &conn); err != nil {
		t.Fatal(err)
	}
	if client != "utf8mb4" || conn != "utf8mb4" {
		t.Errorf("charset client=%s connection=%s, want utf8mb4", client, conn)
	}
}

func TestUserRepoRoundTripsChineseAndNulls(t *testing.T) {
	db := testDB(t)
	repo := NewUserRepo(db)
	ctx := context.Background()

	id := insertUser(t, db, "张三", "SrIyfqk3hjo=", "")

	got, err := repo.FindByUsername(ctx, "张三")
	if err != nil {
		t.Fatalf("FindByUsername: %v", err)
	}
	if got == nil {
		t.Fatal("user not found")
	}
	if got.ID != id || got.Username != "张三" || got.DepartmentName != "销售部" {
		t.Errorf("got %+v", got)
	}
	if got.Credential.Legacy != "SrIyfqk3hjo=" {
		t.Errorf("legacy credential = %q", got.Credential.Legacy)
	}
	// NULL password_bcrypt must arrive as "", not as a scan error.
	if got.Credential.Bcrypt != "" {
		t.Errorf("bcrypt should be empty, got %q", got.Credential.Bcrypt)
	}
}

// An unknown user is not an error: the caller must not be able to tell it
// apart from a wrong password.
func TestUserRepoUnknownUserIsNotAnError(t *testing.T) {
	repo := NewUserRepo(testDB(t))

	got, err := repo.FindByUsername(context.Background(), "nobody")
	if err != nil {
		t.Fatalf("err = %v, want nil", err)
	}
	if got != nil {
		t.Errorf("got %+v, want nil", got)
	}
}

// The username index is deliberately non-unique (0003), so duplicates are
// possible until the data is cleaned. Resolution must be deterministic rather
// than failing a login.
func TestUserRepoDuplicateUsernamePicksLowestUID(t *testing.T) {
	db := testDB(t)
	repo := NewUserRepo(db)

	first := insertUser(t, db, "dup", "a", "")
	insertUser(t, db, "dup", "b", "")

	got, err := repo.FindByUsername(context.Background(), "dup")
	if err != nil {
		t.Fatal(err)
	}
	if got.ID != first {
		t.Errorf("uid = %d, want the lowest (%d)", got.ID, first)
	}
}

func TestUserRepoUpgradePasswordIsAtomic(t *testing.T) {
	db := testDB(t)
	repo := NewUserRepo(db)
	ctx := context.Background()

	id := insertUser(t, db, "zhangsan", "SrIyfqk3hjo=", "")

	hash, err := auth.HashPassword("s3cret")
	if err != nil {
		t.Fatal(err)
	}
	if err := repo.UpgradePassword(ctx, id, hash); err != nil {
		t.Fatalf("UpgradePassword: %v", err)
	}

	got, _ := repo.FindByUsername(ctx, "zhangsan")
	if got.Credential.Bcrypt != hash {
		t.Error("bcrypt hash was not stored")
	}
	if got.Credential.Legacy != "" {
		t.Error("the legacy password must be cleared in the same statement")
	}

	// row_version must advance so concurrent editors see the change.
	var rv int
	if err := db.QueryRow("SELECT row_version FROM tbl_userinfo WHERE uid=?", id).Scan(&rv); err != nil {
		t.Fatal(err)
	}
	if rv != 2 {
		t.Errorf("row_version = %d, want 2", rv)
	}
}

func TestUserRepoUpgradeMissingUserErrors(t *testing.T) {
	repo := NewUserRepo(testDB(t))
	if err := repo.UpgradePassword(context.Background(), 999999, "x"); err == nil {
		t.Error("upgrading a non-existent user should fail loudly")
	}
}

// Permissions are keyed by the literal Chinese menu label, and an unknown
// level must deny rather than default to anything permissive.
func TestUserRepoLoadPermissions(t *testing.T) {
	db := testDB(t)
	repo := NewUserRepo(db)
	id := insertUser(t, db, "zhangsan", "x", "")

	for _, p := range []struct{ field, level string }{
		{auth.PermStoreIn, string(auth.LevelReadWrite)},
		{auth.PermStoreOut, string(auth.LevelReadOnly)},
		{auth.PermFinance, string(auth.LevelUnavailable)},
		{auth.PermReports, "something unexpected"},
	} {
		if _, err := db.Exec(
			`INSERT INTO tbl_permission (userinfoid, fieldname, permission) VALUES (?,?,?)`,
			id, p.field, p.level); err != nil {
			t.Fatal(err)
		}
	}

	perms, err := repo.LoadPermissions(context.Background(), id)
	if err != nil {
		t.Fatal(err)
	}
	if !perms.CanWrite(auth.PermStoreIn) {
		t.Error("读写 should grant write")
	}
	if perms.CanWrite(auth.PermStoreOut) || !perms.CanRead(auth.PermStoreOut) {
		t.Error("只读 should grant read but not write")
	}
	if perms.CanRead(auth.PermFinance) {
		t.Error("不可用 should grant nothing")
	}
	if perms.CanRead(auth.PermReports) {
		t.Error("an unrecognised level must deny, not default to permissive")
	}
	if perms.CanRead(auth.PermCarType) {
		t.Error("an absent key must deny")
	}
}

func TestLoadPermissionsReturnsEmptyNotNil(t *testing.T) {
	db := testDB(t)
	id := insertUser(t, db, "nopermissions", "x", "")

	perms, err := NewUserRepo(db).LoadPermissions(context.Background(), id)
	if err != nil {
		t.Fatal(err)
	}
	if perms == nil {
		t.Fatal("want an empty set, not nil — a nil map invites being read as unrestricted")
	}
	if len(perms) != 0 {
		t.Errorf("len = %d, want 0", len(perms))
	}
}

// ---------------------------------------------------------------------------

func TestSessionRepoLifecycle(t *testing.T) {
	db := testDB(t)
	users := NewUserRepo(db)
	_ = users
	sessions := NewSessionRepo(db)
	ctx := context.Background()

	uid := insertUser(t, db, "zhangsan", "x", "")

	svc := auth.NewSessionService(sessions)
	token, err := svc.Issue(ctx, uid, "2001:db8::1", "Firefox")
	if err != nil {
		t.Fatalf("Issue: %v", err)
	}

	// The plaintext token must not be recoverable from storage.
	var stored string
	if err := db.QueryRow("SELECT token_hash FROM tbl_session").Scan(&stored); err != nil {
		t.Fatal(err)
	}
	if stored == token {
		t.Fatal("the plaintext token was persisted")
	}
	if stored != auth.HashSessionToken(token) {
		t.Error("stored hash does not correspond to the issued token")
	}

	gotUID, err := svc.Validate(ctx, token)
	if err != nil {
		t.Fatalf("Validate: %v", err)
	}
	if gotUID != uid {
		t.Errorf("userID = %d, want %d", gotUID, uid)
	}

	if err := svc.Revoke(ctx, token); err != nil {
		t.Fatal(err)
	}
	if _, err := svc.Validate(ctx, token); err == nil {
		t.Error("a revoked session must not validate")
	}
	if err := svc.Revoke(ctx, token); err != nil {
		t.Errorf("revoking twice should be a no-op: %v", err)
	}
}

// The property JWT cannot provide (D17): revocation is immediate.
func TestSessionRepoRevokeAllForUser(t *testing.T) {
	db := testDB(t)
	sessions := NewSessionRepo(db)
	svc := auth.NewSessionService(sessions)
	ctx := context.Background()

	target := insertUser(t, db, "target", "x", "")
	other := insertUser(t, db, "other", "x", "")

	a, _ := svc.Issue(ctx, target, "", "desktop")
	b, _ := svc.Issue(ctx, target, "", "laptop")
	c, _ := svc.Issue(ctx, other, "", "")

	if err := svc.RevokeAllForUser(ctx, target); err != nil {
		t.Fatal(err)
	}
	for _, tok := range []string{a, b} {
		if _, err := svc.Validate(ctx, tok); err == nil {
			t.Error("every session for the user should be gone")
		}
	}
	if _, err := svc.Validate(ctx, c); err != nil {
		t.Errorf("another user's session must survive: %v", err)
	}
}

func TestSessionRepoDeleteExpired(t *testing.T) {
	db := testDB(t)
	repo := NewSessionRepo(db)
	ctx := context.Background()
	uid := insertUser(t, db, "zhangsan", "x", "")

	now := time.Now()
	mk := func(hash string, expires time.Time) {
		if _, err := repo.Create(ctx, auth.Session{
			UserID: uid, TokenHash: hash,
			CreatedAt: now.Add(-time.Hour), LastSeenAt: now, ExpiresAt: expires,
		}); err != nil {
			t.Fatal(err)
		}
	}
	mk("expired-------------------------------------------------------a", now.Add(-time.Minute))
	mk("live----------------------------------------------------------b", now.Add(time.Hour))

	n, err := repo.DeleteExpired(ctx, now)
	if err != nil {
		t.Fatal(err)
	}
	if n != 1 {
		t.Errorf("deleted %d, want 1", n)
	}

	var left int
	if err := db.QueryRow("SELECT COUNT(*) FROM tbl_session").Scan(&left); err != nil {
		t.Fatal(err)
	}
	if left != 1 {
		t.Errorf("%d sessions left, want 1", left)
	}
}

// DATETIME has no timezone, so a value must come back as the same wall clock
// it went in as. A mismatch here is how every migrated date ends up 8 hours
// out (plan 9 report equivalence).
func TestSessionRepoDatetimeRoundTrip(t *testing.T) {
	db := testDB(t)
	repo := NewSessionRepo(db)
	ctx := context.Background()
	uid := insertUser(t, db, "zhangsan", "x", "")

	shanghai, err := time.LoadLocation("Asia/Shanghai")
	if err != nil {
		t.Skip("tzdata unavailable")
	}
	want := time.Date(2026, 8, 1, 14, 30, 45, 0, shanghai)

	if _, err := repo.Create(ctx, auth.Session{
		UserID: uid, TokenHash: "dt------------------------------------------------------------",
		CreatedAt: want, LastSeenAt: want, ExpiresAt: want.Add(24 * time.Hour),
	}); err != nil {
		t.Fatal(err)
	}

	got, err := repo.FindByTokenHash(ctx, "dt------------------------------------------------------------")
	if err != nil {
		t.Fatal(err)
	}
	if !got.CreatedAt.Equal(want) {
		t.Errorf("CreatedAt = %v, want %v (wall clock must survive the round trip)",
			got.CreatedAt, want)
	}
}
