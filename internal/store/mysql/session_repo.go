package mysql

import (
	"context"
	"database/sql"
	"errors"
	"fmt"
	"time"

	"github.com/Markcus0526/carsaleman/internal/auth"
)

// SessionRepo implements auth.SessionRepository.
type SessionRepo struct{ db *sql.DB }

func NewSessionRepo(db *sql.DB) *SessionRepo { return &SessionRepo{db: db} }

var _ auth.SessionRepository = (*SessionRepo)(nil)

func (r *SessionRepo) Create(ctx context.Context, s auth.Session) (int64, error) {
	const q = `
		INSERT INTO tbl_session
			(userinfoid, token_hash, created_at, last_seen_at, expires_at, ip, user_agent)
		VALUES (?, ?, ?, ?, ?, ?, ?)`

	res, err := r.db.ExecContext(ctx, q,
		s.UserID, s.TokenHash, s.CreatedAt, s.LastSeenAt, s.ExpiresAt,
		nullIfEmpty(s.IP), nullIfEmpty(s.UserAgent),
	)
	if err != nil {
		return 0, fmt.Errorf("mysql: create session: %w", err)
	}
	id, err := res.LastInsertId()
	if err != nil {
		return 0, fmt.Errorf("mysql: create session: %w", err)
	}
	return id, nil
}

// FindByTokenHash returns nil, nil when the token is unknown. The caller turns
// that into ErrSessionInvalid; distinguishing "no such session" from "expired"
// only helps someone probing for valid tokens.
func (r *SessionRepo) FindByTokenHash(ctx context.Context, hash string) (*auth.Session, error) {
	const q = `
		SELECT uid, userinfoid, token_hash, created_at, last_seen_at, expires_at,
		       COALESCE(ip, ''), COALESCE(user_agent, '')
		FROM tbl_session
		WHERE token_hash = ?`

	var s auth.Session
	err := r.db.QueryRowContext(ctx, q, hash).Scan(
		&s.ID, &s.UserID, &s.TokenHash, &s.CreatedAt, &s.LastSeenAt, &s.ExpiresAt,
		&s.IP, &s.UserAgent,
	)
	if errors.Is(err, sql.ErrNoRows) {
		return nil, nil
	}
	if err != nil {
		return nil, fmt.Errorf("mysql: find session: %w", err)
	}
	return &s, nil
}

func (r *SessionRepo) Touch(ctx context.Context, id int64, lastSeen time.Time) error {
	const q = `UPDATE tbl_session SET last_seen_at = ? WHERE uid = ?`
	if _, err := r.db.ExecContext(ctx, q, lastSeen, id); err != nil {
		return fmt.Errorf("mysql: touch session: %w", err)
	}
	return nil
}

// Delete is idempotent: deleting an already-gone session is success, because
// logout must not fail on a double-submit.
func (r *SessionRepo) Delete(ctx context.Context, id int64) error {
	const q = `DELETE FROM tbl_session WHERE uid = ?`
	if _, err := r.db.ExecContext(ctx, q, id); err != nil {
		return fmt.Errorf("mysql: delete session: %w", err)
	}
	return nil
}

// DeleteForUser ends every session a user holds.
//
// This is the operation a JWT cannot provide, and the reason for D17. Call it
// when an account is disabled or its password changes.
func (r *SessionRepo) DeleteForUser(ctx context.Context, userID int64) error {
	const q = `DELETE FROM tbl_session WHERE userinfoid = ?`
	if _, err := r.db.ExecContext(ctx, q, userID); err != nil {
		return fmt.Errorf("mysql: delete sessions for user: %w", err)
	}
	return nil
}

// DeleteExpired removes sessions past their absolute deadline.
//
// Housekeeping only -- expiry is already enforced on read, so a table that has
// not been swept is not a security problem, just an untidy one.
func (r *SessionRepo) DeleteExpired(ctx context.Context, now time.Time) (int64, error) {
	const q = `DELETE FROM tbl_session WHERE expires_at <= ?`
	res, err := r.db.ExecContext(ctx, q, now)
	if err != nil {
		return 0, fmt.Errorf("mysql: delete expired sessions: %w", err)
	}
	n, err := res.RowsAffected()
	if err != nil {
		return 0, fmt.Errorf("mysql: delete expired sessions: %w", err)
	}
	return n, nil
}

func nullIfEmpty(s string) any {
	if s == "" {
		return nil
	}
	return s
}
