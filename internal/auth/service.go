package auth

import (
	"context"
	"errors"
	"log/slog"
)

// ErrInvalidCredentials is returned for every authentication failure.
//
// One error for all cases, deliberately: an unknown username and a wrong
// password must be indistinguishable to the caller, or the login endpoint
// becomes a username oracle. Verify() already equalises the timing.
var ErrInvalidCredentials = errors.New("auth: invalid credentials")

// User is the subset of tbl_userinfo that authentication needs.
type User struct {
	ID             int64
	Username       string
	DepartmentCode string
	DepartmentName string
	Credential     Credential
}

// UserRepository is the storage this package needs. Defined here, implemented
// in store/mysql -- so the service is testable without a database, and the
// dependency points inward.
type UserRepository interface {
	// FindByUsername returns nil (with a nil error) when no such user exists.
	FindByUsername(ctx context.Context, username string) (*User, error)

	// FindByID returns nil (with a nil error) when no such user exists.
	FindByID(ctx context.Context, id int64) (*User, error)

	// UpgradePassword writes the bcrypt hash and clears the legacy password
	// column, atomically.
	UpgradePassword(ctx context.Context, userID int64, bcryptHash string) error

	// LoadPermissions returns the user's tbl_permission rows.
	LoadPermissions(ctx context.Context, userID int64) (Set, error)
}

// Authenticated is a successful login result.
type Authenticated struct {
	User        User
	Permissions Set
	// Upgraded reports that the credential was migrated from DES to bcrypt
	// during this login. Useful for telling operators how much of the user
	// base has moved before the legacy path is deleted.
	Upgraded bool
}

type Service struct {
	repo UserRepository
	log  *slog.Logger
}

func NewService(repo UserRepository, log *slog.Logger) *Service {
	return &Service{repo: repo, log: log}
}

// Login authenticates a user and returns their resolved permissions.
func (s *Service) Login(ctx context.Context, username, password string) (*Authenticated, error) {
	user, err := s.repo.FindByUsername(ctx, username)
	if err != nil {
		return nil, err
	}

	if user == nil {
		// Still do the work a real verification would, so a probe cannot
		// distinguish "no such user" from "wrong password" by latency.
		Verify(password, Credential{})
		return nil, ErrInvalidCredentials
	}

	ok, needsUpgrade := Verify(password, user.Credential)
	if !ok {
		return nil, ErrInvalidCredentials
	}

	upgraded := false
	if needsUpgrade {
		upgraded = s.upgrade(ctx, user, password)
	}

	perms, err := s.repo.LoadPermissions(ctx, user.ID)
	if err != nil {
		return nil, err
	}

	return &Authenticated{User: *user, Permissions: perms, Upgraded: upgraded}, nil
}

// UserByID loads the user behind an authenticated session.
//
// A session whose user has been deleted is treated as invalid rather than as
// an internal error: the row is gone, so the session should be too.
func (s *Service) UserByID(ctx context.Context, id int64) (User, error) {
	u, err := s.repo.FindByID(ctx, id)
	if err != nil {
		return User{}, err
	}
	if u == nil {
		return User{}, ErrInvalidCredentials
	}
	return *u, nil
}

// Permissions loads a user's permissions.
//
// Called on every authenticated request rather than cached in the session, so
// an administrator's change takes effect on the next call (D17).
func (s *Service) Permissions(ctx context.Context, userID int64) (Set, error) {
	return s.repo.LoadPermissions(ctx, userID)
}

// upgrade re-hashes a just-verified legacy credential with bcrypt.
//
// The plaintext is required and is only available here, at the one moment the
// user has proven they know it. It is not retained beyond this call.
//
// A failure does NOT fail the login. The user authenticated correctly;
// refusing them because a follow-up write failed would turn a storage blip
// into an outage. The next login simply retries the upgrade.
func (s *Service) upgrade(ctx context.Context, user *User, plaintext string) bool {
	hash, err := HashPassword(plaintext)
	if err != nil {
		s.log.Error("password upgrade: hashing failed",
			"user_id", user.ID, "err", err)
		return false
	}

	if err := s.repo.UpgradePassword(ctx, user.ID, hash); err != nil {
		s.log.Error("password upgrade: write failed; login still succeeds",
			"user_id", user.ID, "err", err)
		return false
	}

	s.log.Info("password upgraded from legacy DES to bcrypt", "user_id", user.ID)
	return true
}
