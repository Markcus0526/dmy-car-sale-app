package mysql

import (
	"context"
	"database/sql"
	"errors"
	"fmt"

	"github.com/Markcus0526/carsaleman/internal/auth"
)

// UserRepo implements auth.UserRepository.
type UserRepo struct{ db *sql.DB }

func NewUserRepo(db *sql.DB) *UserRepo { return &UserRepo{db: db} }

var _ auth.UserRepository = (*UserRepo)(nil)

// FindByUsername returns nil, nil when no such user exists -- an unknown user
// is not an error condition, and the caller must not be able to distinguish it
// from a wrong password.
//
// LIMIT 1 is deliberate: username is indexed but NOT unique, because the
// legacy data has never been constrained (migration 0003). If duplicates
// exist, this picks the lowest uid deterministically rather than erroring at
// login time. cmd/migrate-data reports duplicates so they can be resolved.
func (r *UserRepo) FindByUsername(ctx context.Context, username string) (*auth.User, error) {
	const q = `
		SELECT uid, username, departmentcode, departmentname, password, password_bcrypt
		FROM tbl_userinfo
		WHERE username = ?
		ORDER BY uid
		LIMIT 1`
	return r.scanUser(r.db.QueryRowContext(ctx, q, username))
}

// FindByID resolves the user behind a session.
func (r *UserRepo) FindByID(ctx context.Context, id int64) (*auth.User, error) {
	const q = `
		SELECT uid, username, departmentcode, departmentname, password, password_bcrypt
		FROM tbl_userinfo
		WHERE uid = ?`
	return r.scanUser(r.db.QueryRowContext(ctx, q, id))
}

func (r *UserRepo) scanUser(row *sql.Row) (*auth.User, error) {
	var (
		u                      auth.User
		name, deptName         sql.NullString
		legacyPass, bcryptPass sql.NullString
	)
	err := row.Scan(&u.ID, &name, &u.DepartmentCode, &deptName, &legacyPass, &bcryptPass)
	if errors.Is(err, sql.ErrNoRows) {
		return nil, nil
	}
	if err != nil {
		return nil, fmt.Errorf("mysql: find user: %w", err)
	}

	u.Username = name.String
	u.DepartmentName = deptName.String
	u.Credential = auth.Credential{
		Bcrypt: bcryptPass.String,
		Legacy: legacyPass.String,
	}
	return &u, nil
}

// UpgradePassword writes the bcrypt hash and clears the legacy column in one
// statement, so a crash cannot leave a user with both or neither.
//
// The row_version guard is omitted intentionally: this is an idempotent
// credential upgrade triggered by a successful login, not a user edit. Two
// concurrent logins racing here both write a valid hash for the same password,
// and returning 409 to one of them would be a worse outcome than the last
// write winning.
func (r *UserRepo) UpgradePassword(ctx context.Context, userID int64, bcryptHash string) error {
	const q = `
		UPDATE tbl_userinfo
		SET password_bcrypt = ?, password = NULL, row_version = row_version + 1
		WHERE uid = ?`

	res, err := r.db.ExecContext(ctx, q, bcryptHash, userID)
	if err != nil {
		return fmt.Errorf("mysql: upgrade password: %w", err)
	}
	n, err := res.RowsAffected()
	if err != nil {
		return fmt.Errorf("mysql: upgrade password: %w", err)
	}
	if n == 0 {
		return fmt.Errorf("mysql: upgrade password: user %d not found", userID)
	}
	return nil
}

// LoadPermissions reads the user's tbl_permission rows.
//
// Returns an empty (non-nil) set when the user has no rows, so callers get
// deny-by-default rather than a nil map they might mistake for "unrestricted".
func (r *UserRepo) LoadPermissions(ctx context.Context, userID int64) (auth.Set, error) {
	const q = `SELECT fieldname, permission FROM tbl_permission WHERE userinfoid = ?`

	rows, err := r.db.QueryContext(ctx, q, userID)
	if err != nil {
		return nil, fmt.Errorf("mysql: load permissions: %w", err)
	}
	defer rows.Close()

	perms := auth.Set{}
	for rows.Next() {
		var field, level sql.NullString
		if err := rows.Scan(&field, &level); err != nil {
			return nil, fmt.Errorf("mysql: load permissions: %w", err)
		}
		if field.Valid {
			// The stored value is the literal Chinese level (读写 / 只读 /
			// 不可用). Anything unrecognised maps to a Level that neither
			// CanRead nor CanWrite accepts -- deny-by-default, no special case.
			perms[field.String] = auth.Level(level.String)
		}
	}
	if err := rows.Err(); err != nil {
		return nil, fmt.Errorf("mysql: load permissions: %w", err)
	}
	return perms, nil
}
