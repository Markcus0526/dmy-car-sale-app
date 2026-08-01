package auth

import (
	"context"
	"errors"
	"io"
	"log/slog"
	"testing"
)

type fakeRepo struct {
	user        *User
	findErr     error
	permsErr    error
	upgradeErr  error
	upgraded    []string // bcrypt hashes written
	permsCalled int
}

func (f *fakeRepo) FindByUsername(context.Context, string) (*User, error) {
	return f.user, f.findErr
}

func (f *fakeRepo) FindByID(context.Context, int64) (*User, error) {
	return f.user, f.findErr
}

func (f *fakeRepo) UpgradePassword(_ context.Context, _ int64, hash string) error {
	if f.upgradeErr != nil {
		return f.upgradeErr
	}
	f.upgraded = append(f.upgraded, hash)
	return nil
}

func (f *fakeRepo) LoadPermissions(context.Context, int64) (Set, error) {
	f.permsCalled++
	if f.permsErr != nil {
		return nil, f.permsErr
	}
	return Set{PermStoreIn: LevelReadWrite}, nil
}

func newService(r *fakeRepo) *Service {
	return NewService(r, slog.New(slog.NewTextHandler(io.Discard, nil)))
}

func legacyUser(t *testing.T, password string) *User {
	t.Helper()
	ct, err := legacyDESEncode(password)
	if err != nil {
		t.Fatal(err)
	}
	return &User{ID: 7, Username: "zhangsan", Credential: Credential{Legacy: ct}}
}

func bcryptUser(t *testing.T, password string) *User {
	t.Helper()
	h, err := HashPassword(password)
	if err != nil {
		t.Fatal(err)
	}
	return &User{ID: 7, Username: "zhangsan", Credential: Credential{Bcrypt: h}}
}

// ---------------------------------------------------------------------------

func TestLoginBcryptPath(t *testing.T) {
	repo := &fakeRepo{user: bcryptUser(t, "s3cret")}

	got, err := newService(repo).Login(context.Background(), "zhangsan", "s3cret")
	if err != nil {
		t.Fatalf("Login: %v", err)
	}
	if got.Upgraded {
		t.Error("a bcrypt credential must not report an upgrade")
	}
	if len(repo.upgraded) != 0 {
		t.Error("no upgrade write should have occurred")
	}
	if !got.Permissions.CanWrite(PermStoreIn) {
		t.Error("permissions were not loaded")
	}
}

func TestLoginLegacyPathUpgradesInPlace(t *testing.T) {
	repo := &fakeRepo{user: legacyUser(t, "s3cret")}

	got, err := newService(repo).Login(context.Background(), "zhangsan", "s3cret")
	if err != nil {
		t.Fatalf("Login: %v", err)
	}
	if !got.Upgraded {
		t.Error("legacy authentication should report Upgraded")
	}
	if len(repo.upgraded) != 1 {
		t.Fatalf("expected exactly one upgrade write, got %d", len(repo.upgraded))
	}

	// The stored hash must verify against the same password the user typed.
	if ok, _ := Verify("s3cret", Credential{Bcrypt: repo.upgraded[0]}); !ok {
		t.Error("the written hash does not verify against the original password")
	}
}

// A storage failure during the upgrade must not cost the user their session:
// they proved they know the password, and the next login retries the upgrade.
func TestLoginSucceedsWhenUpgradeWriteFails(t *testing.T) {
	repo := &fakeRepo{
		user:       legacyUser(t, "s3cret"),
		upgradeErr: errors.New("database is down"),
	}

	got, err := newService(repo).Login(context.Background(), "zhangsan", "s3cret")
	if err != nil {
		t.Fatalf("login should succeed despite the failed upgrade write: %v", err)
	}
	if got.Upgraded {
		t.Error("Upgraded must be false when the write failed")
	}
}

func TestLoginWrongPassword(t *testing.T) {
	for _, tc := range []struct {
		name string
		user *User
	}{
		{"bcrypt", bcryptUser(t, "right")},
		{"legacy", legacyUser(t, "right")},
	} {
		t.Run(tc.name, func(t *testing.T) {
			repo := &fakeRepo{user: tc.user}
			_, err := newService(repo).Login(context.Background(), "zhangsan", "wrong")
			if !errors.Is(err, ErrInvalidCredentials) {
				t.Errorf("err = %v, want ErrInvalidCredentials", err)
			}
			if repo.permsCalled != 0 {
				t.Error("permissions must not be loaded for a failed login")
			}
		})
	}
}

// An unknown user and a wrong password must be indistinguishable, or the
// endpoint becomes a username oracle.
func TestLoginUnknownUserIsIndistinguishable(t *testing.T) {
	repo := &fakeRepo{user: nil}

	_, err := newService(repo).Login(context.Background(), "nobody", "whatever")
	if !errors.Is(err, ErrInvalidCredentials) {
		t.Errorf("err = %v, want ErrInvalidCredentials", err)
	}
}

// A user row with neither credential populated must never authenticate --
// including on an empty password, which is the case most likely to slip
// through a naive implementation.
func TestLoginUserWithNoCredential(t *testing.T) {
	repo := &fakeRepo{user: &User{ID: 7, Username: "zhangsan"}}

	for _, pw := range []string{"", "anything"} {
		if _, err := newService(repo).Login(context.Background(), "zhangsan", pw); !errors.Is(err, ErrInvalidCredentials) {
			t.Errorf("password %q: err = %v, want ErrInvalidCredentials", pw, err)
		}
	}
}

// Repository errors are infrastructure failures, not authentication failures,
// and must not be reported to the client as bad credentials.
func TestLoginPropagatesRepositoryErrors(t *testing.T) {
	boom := errors.New("connection refused")

	t.Run("lookup", func(t *testing.T) {
		_, err := newService(&fakeRepo{findErr: boom}).Login(context.Background(), "x", "y")
		if !errors.Is(err, boom) {
			t.Errorf("err = %v, want the repository error", err)
		}
		if errors.Is(err, ErrInvalidCredentials) {
			t.Error("an infrastructure failure must not surface as invalid credentials")
		}
	})

	t.Run("permissions", func(t *testing.T) {
		repo := &fakeRepo{user: bcryptUser(t, "s3cret"), permsErr: boom}
		_, err := newService(repo).Login(context.Background(), "zhangsan", "s3cret")
		if !errors.Is(err, boom) {
			t.Errorf("err = %v, want the repository error", err)
		}
	})
}
