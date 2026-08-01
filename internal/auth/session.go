package auth

import (
	"context"
	"crypto/rand"
	"crypto/sha256"
	"encoding/base64"
	"encoding/hex"
	"errors"
	"fmt"
	"time"
)

// Session lifetimes.
//
// Two clocks, because they answer different questions:
//
//	IdleTimeout     how long an unattended session survives. Bumped on use, so
//	                someone working through the afternoon is never logged out
//	                mid-task.
//	AbsoluteTimeout the hard ceiling. A session dies here however active it has
//	                been, bounding the value of a stolen cookie.
//
// Tuned for an internal system where users stay logged in for a working day.
const (
	IdleTimeout     = 8 * time.Hour
	AbsoluteTimeout = 24 * time.Hour
)

// sessionTokenBytes is the raw entropy per token. 256 bits: the token is the
// entire credential for an authenticated request, so it must be infeasible to
// guess and there is no reason to be frugal.
const sessionTokenBytes = 32

// ErrSessionInvalid covers every reason a session cannot be used: unknown
// token, idle timeout, absolute timeout, revoked. Deliberately one error --
// the client gets 401 and re-authenticates regardless, and distinguishing the
// cases only helps someone probing for valid tokens.
var ErrSessionInvalid = errors.New("auth: session invalid or expired")

// Session is a server-side session record.
//
// The plaintext token is NEVER stored here or in the database -- only its
// hash. It exists exactly once, in the response that creates it.
type Session struct {
	ID         int64
	UserID     int64
	TokenHash  string
	CreatedAt  time.Time
	LastSeenAt time.Time
	ExpiresAt  time.Time
	IP         string
	UserAgent  string
}

// SessionRepository is the storage this package needs.
type SessionRepository interface {
	Create(ctx context.Context, s Session) (int64, error)
	// FindByTokenHash returns nil (nil error) when no such session exists.
	FindByTokenHash(ctx context.Context, tokenHash string) (*Session, error)
	Touch(ctx context.Context, id int64, lastSeen time.Time) error
	Delete(ctx context.Context, id int64) error
	DeleteForUser(ctx context.Context, userID int64) error
	DeleteExpired(ctx context.Context, now time.Time) (int64, error)
}

// NewSessionToken returns a fresh opaque token and its storage hash.
//
// The token is URL-safe base64 so it is cookie-safe without further encoding.
func NewSessionToken() (token, hash string, err error) {
	raw := make([]byte, sessionTokenBytes)
	if _, err := rand.Read(raw); err != nil {
		return "", "", fmt.Errorf("auth: generating session token: %w", err)
	}
	token = base64.RawURLEncoding.EncodeToString(raw)
	return token, HashSessionToken(token), nil
}

// HashSessionToken maps a token to its stored form.
//
// SHA-256, not bcrypt: the input is 256 bits of CSPRNG output, so there is no
// low-entropy secret to slow an attacker down over, and this runs on every
// authenticated request. bcrypt here would add latency to every call and buy
// nothing.
func HashSessionToken(token string) string {
	sum := sha256.Sum256([]byte(token))
	return hex.EncodeToString(sum[:])
}

// SessionService issues and validates sessions.
type SessionService struct {
	repo SessionRepository
	// now is injectable so timeout behaviour is testable without sleeping.
	now func() time.Time
}

func NewSessionService(repo SessionRepository) *SessionService {
	return &SessionService{repo: repo, now: time.Now}
}

// Issue creates a session and returns the plaintext token for the cookie.
//
// This is the only moment the plaintext exists; it is not recoverable
// afterwards, by us or by anyone reading the database.
func (s *SessionService) Issue(ctx context.Context, userID int64, ip, userAgent string) (token string, err error) {
	token, hash, err := NewSessionToken()
	if err != nil {
		return "", err
	}

	now := s.now()
	sess := Session{
		UserID:     userID,
		TokenHash:  hash,
		CreatedAt:  now,
		LastSeenAt: now,
		ExpiresAt:  now.Add(AbsoluteTimeout),
		IP:         ip,
		UserAgent:  truncate(userAgent, 255),
	}

	if _, err := s.repo.Create(ctx, sess); err != nil {
		return "", err
	}
	return token, nil
}

// Validate resolves a token to its user, enforcing both timeouts.
//
// On success the session's last-seen time is refreshed. A failure to refresh
// is not fatal: the user is authenticated, and the only consequence is that
// the idle clock did not advance.
func (s *SessionService) Validate(ctx context.Context, token string) (userID int64, err error) {
	if token == "" {
		return 0, ErrSessionInvalid
	}

	sess, err := s.repo.FindByTokenHash(ctx, HashSessionToken(token))
	if err != nil {
		return 0, err
	}
	if sess == nil {
		return 0, ErrSessionInvalid
	}

	now := s.now()

	// Absolute ceiling.
	if !now.Before(sess.ExpiresAt) {
		_ = s.repo.Delete(ctx, sess.ID)
		return 0, ErrSessionInvalid
	}

	// Idle timeout.
	if now.Sub(sess.LastSeenAt) >= IdleTimeout {
		_ = s.repo.Delete(ctx, sess.ID)
		return 0, ErrSessionInvalid
	}

	// Refresh at most once a minute: on a busy screen this would otherwise be
	// a write on every request, for a clock that only needs minute resolution.
	if now.Sub(sess.LastSeenAt) > time.Minute {
		_ = s.repo.Touch(ctx, sess.ID, now)
	}

	return sess.UserID, nil
}

// Revoke ends one session (logout).
func (s *SessionService) Revoke(ctx context.Context, token string) error {
	sess, err := s.repo.FindByTokenHash(ctx, HashSessionToken(token))
	if err != nil {
		return err
	}
	if sess == nil {
		return nil // already gone; logout is idempotent
	}
	return s.repo.Delete(ctx, sess.ID)
}

// RevokeAllForUser ends every session a user holds.
//
// Call this when an account is disabled or its password changes -- otherwise
// a revoked user keeps working until their cookie happens to expire, which is
// the JWT problem this design exists to avoid.
func (s *SessionService) RevokeAllForUser(ctx context.Context, userID int64) error {
	return s.repo.DeleteForUser(ctx, userID)
}

// Sweep removes expired sessions. Intended for a periodic job; expiry is
// already enforced on read, so this is housekeeping rather than security.
func (s *SessionService) Sweep(ctx context.Context) (int64, error) {
	return s.repo.DeleteExpired(ctx, s.now())
}

func truncate(s string, n int) string {
	if len(s) <= n {
		return s
	}
	return s[:n]
}
