package auth

import (
	"context"
	"errors"
	"strings"
	"testing"
	"time"
)

type fakeSessionRepo struct {
	sessions map[int64]*Session
	nextID   int64
	touched  []int64
	deleted  []int64
	findErr  error
}

func newFakeSessionRepo() *fakeSessionRepo {
	return &fakeSessionRepo{sessions: map[int64]*Session{}, nextID: 1}
}

func (f *fakeSessionRepo) Create(_ context.Context, s Session) (int64, error) {
	id := f.nextID
	f.nextID++
	s.ID = id
	f.sessions[id] = &s
	return id, nil
}

func (f *fakeSessionRepo) FindByTokenHash(_ context.Context, hash string) (*Session, error) {
	if f.findErr != nil {
		return nil, f.findErr
	}
	for _, s := range f.sessions {
		if s.TokenHash == hash {
			return s, nil
		}
	}
	return nil, nil
}

func (f *fakeSessionRepo) Touch(_ context.Context, id int64, t time.Time) error {
	f.touched = append(f.touched, id)
	if s, ok := f.sessions[id]; ok {
		s.LastSeenAt = t
	}
	return nil
}

func (f *fakeSessionRepo) Delete(_ context.Context, id int64) error {
	f.deleted = append(f.deleted, id)
	delete(f.sessions, id)
	return nil
}

func (f *fakeSessionRepo) DeleteForUser(_ context.Context, userID int64) error {
	for id, s := range f.sessions {
		if s.UserID == userID {
			delete(f.sessions, id)
		}
	}
	return nil
}

func (f *fakeSessionRepo) DeleteExpired(_ context.Context, now time.Time) (int64, error) {
	var n int64
	for id, s := range f.sessions {
		if !now.Before(s.ExpiresAt) {
			delete(f.sessions, id)
			n++
		}
	}
	return n, nil
}

// clocked returns a service whose clock the test controls.
func clocked(repo SessionRepository, at *time.Time) *SessionService {
	s := NewSessionService(repo)
	s.now = func() time.Time { return *at }
	return s
}

// ---------------------------------------------------------------------------

func TestNewSessionTokenIsUniqueAndHashed(t *testing.T) {
	seen := map[string]bool{}
	for range 100 {
		tok, hash, err := NewSessionToken()
		if err != nil {
			t.Fatal(err)
		}
		if seen[tok] {
			t.Fatal("token collision — the CSPRNG is not being used correctly")
		}
		seen[tok] = true

		if hash != HashSessionToken(tok) {
			t.Error("returned hash does not match HashSessionToken")
		}
		if strings.Contains(hash, tok) {
			t.Error("the hash must not contain the token")
		}
		if len(hash) != 64 {
			t.Errorf("hash length = %d, want 64 hex chars", len(hash))
		}
		// base64 of 32 bytes, unpadded.
		if len(tok) != 43 {
			t.Errorf("token length = %d, want 43", len(tok))
		}
	}
}

// The plaintext token must never be persisted — a database leak should not
// hand over usable sessions.
func TestIssueStoresOnlyTheHash(t *testing.T) {
	repo := newFakeSessionRepo()
	now := time.Date(2026, 8, 1, 9, 0, 0, 0, time.UTC)

	token, err := clocked(repo, &now).Issue(context.Background(), 7, "10.0.0.1", "Firefox")
	if err != nil {
		t.Fatal(err)
	}

	for _, s := range repo.sessions {
		if s.TokenHash == token {
			t.Fatal("the plaintext token was stored")
		}
		if s.TokenHash != HashSessionToken(token) {
			t.Error("stored hash does not correspond to the issued token")
		}
		if !s.ExpiresAt.Equal(now.Add(AbsoluteTimeout)) {
			t.Errorf("ExpiresAt = %v, want now+%v", s.ExpiresAt, AbsoluteTimeout)
		}
	}
}

func TestValidateHappyPath(t *testing.T) {
	repo := newFakeSessionRepo()
	now := time.Date(2026, 8, 1, 9, 0, 0, 0, time.UTC)
	svc := clocked(repo, &now)

	token, _ := svc.Issue(context.Background(), 7, "", "")

	uid, err := svc.Validate(context.Background(), token)
	if err != nil {
		t.Fatalf("Validate: %v", err)
	}
	if uid != 7 {
		t.Errorf("userID = %d, want 7", uid)
	}
}

func TestValidateRejectsUnknownAndEmptyTokens(t *testing.T) {
	svc := NewSessionService(newFakeSessionRepo())

	for _, tok := range []string{"", "not-a-real-token"} {
		if _, err := svc.Validate(context.Background(), tok); !errors.Is(err, ErrSessionInvalid) {
			t.Errorf("token %q: err = %v, want ErrSessionInvalid", tok, err)
		}
	}
}

// Idle timeout: an unattended session dies even though its absolute deadline
// is still far away.
func TestValidateEnforcesIdleTimeout(t *testing.T) {
	repo := newFakeSessionRepo()
	now := time.Date(2026, 8, 1, 9, 0, 0, 0, time.UTC)
	svc := clocked(repo, &now)

	token, _ := svc.Issue(context.Background(), 7, "", "")

	now = now.Add(IdleTimeout + time.Minute) // still inside AbsoluteTimeout
	if _, err := svc.Validate(context.Background(), token); !errors.Is(err, ErrSessionInvalid) {
		t.Fatalf("err = %v, want ErrSessionInvalid", err)
	}
	if len(repo.deleted) == 0 {
		t.Error("an idle-expired session should be deleted, not left to accumulate")
	}
}

// Absolute timeout: continuous activity must not extend a session forever,
// or a stolen cookie is good indefinitely.
func TestValidateEnforcesAbsoluteTimeoutDespiteActivity(t *testing.T) {
	repo := newFakeSessionRepo()
	now := time.Date(2026, 8, 1, 9, 0, 0, 0, time.UTC)
	svc := clocked(repo, &now)

	token, _ := svc.Issue(context.Background(), 7, "", "")

	// Stay active: validate every hour, well inside the idle window.
	for range int(AbsoluteTimeout/time.Hour) - 1 {
		now = now.Add(time.Hour)
		if _, err := svc.Validate(context.Background(), token); err != nil {
			t.Fatalf("session died early at %v: %v", now, err)
		}
	}

	now = now.Add(2 * time.Hour) // past the absolute ceiling
	if _, err := svc.Validate(context.Background(), token); !errors.Is(err, ErrSessionInvalid) {
		t.Error("activity must not extend a session past its absolute deadline")
	}
}

// Touch is throttled: a busy screen would otherwise write on every request.
func TestValidateThrottlesTouch(t *testing.T) {
	repo := newFakeSessionRepo()
	now := time.Date(2026, 8, 1, 9, 0, 0, 0, time.UTC)
	svc := clocked(repo, &now)

	token, _ := svc.Issue(context.Background(), 7, "", "")

	now = now.Add(10 * time.Second)
	_, _ = svc.Validate(context.Background(), token)
	if len(repo.touched) != 0 {
		t.Error("a request seconds after the last should not write")
	}

	now = now.Add(2 * time.Minute)
	_, _ = svc.Validate(context.Background(), token)
	if len(repo.touched) != 1 {
		t.Errorf("touched %d times, want 1 after crossing the throttle", len(repo.touched))
	}
}

func TestRevokeIsIdempotent(t *testing.T) {
	repo := newFakeSessionRepo()
	now := time.Date(2026, 8, 1, 9, 0, 0, 0, time.UTC)
	svc := clocked(repo, &now)

	token, _ := svc.Issue(context.Background(), 7, "", "")

	if err := svc.Revoke(context.Background(), token); err != nil {
		t.Fatal(err)
	}
	if _, err := svc.Validate(context.Background(), token); !errors.Is(err, ErrSessionInvalid) {
		t.Error("a revoked session must not validate")
	}
	if err := svc.Revoke(context.Background(), token); err != nil {
		t.Errorf("revoking twice should be a no-op, got %v", err)
	}
}

// This is the property JWT cannot provide, and the reason for D17: an
// administrator revoking access takes effect on the very next request.
func TestRevokeAllForUserIsImmediate(t *testing.T) {
	repo := newFakeSessionRepo()
	now := time.Date(2026, 8, 1, 9, 0, 0, 0, time.UTC)
	svc := clocked(repo, &now)

	a, _ := svc.Issue(context.Background(), 7, "", "desktop")
	b, _ := svc.Issue(context.Background(), 7, "", "laptop")
	other, _ := svc.Issue(context.Background(), 99, "", "")

	if err := svc.RevokeAllForUser(context.Background(), 7); err != nil {
		t.Fatal(err)
	}

	for _, tok := range []string{a, b} {
		if _, err := svc.Validate(context.Background(), tok); !errors.Is(err, ErrSessionInvalid) {
			t.Error("every session for the user should be gone")
		}
	}
	if _, err := svc.Validate(context.Background(), other); err != nil {
		t.Error("another user's session must be untouched")
	}
}

func TestSweepRemovesOnlyExpired(t *testing.T) {
	repo := newFakeSessionRepo()
	now := time.Date(2026, 8, 1, 9, 0, 0, 0, time.UTC)
	svc := clocked(repo, &now)

	old, _ := svc.Issue(context.Background(), 7, "", "")
	now = now.Add(AbsoluteTimeout + time.Hour)
	fresh, _ := svc.Issue(context.Background(), 8, "", "")

	n, err := svc.Sweep(context.Background())
	if err != nil {
		t.Fatal(err)
	}
	if n != 1 {
		t.Errorf("swept %d, want 1", n)
	}
	if _, err := svc.Validate(context.Background(), old); !errors.Is(err, ErrSessionInvalid) {
		t.Error("the expired session should be gone")
	}
	if _, err := svc.Validate(context.Background(), fresh); err != nil {
		t.Error("the fresh session should survive")
	}
}

func TestValidatePropagatesRepositoryErrors(t *testing.T) {
	boom := errors.New("connection refused")
	repo := newFakeSessionRepo()
	repo.findErr = boom

	_, err := NewSessionService(repo).Validate(context.Background(), "whatever")
	if !errors.Is(err, boom) {
		t.Errorf("err = %v, want the repository error", err)
	}
	if errors.Is(err, ErrSessionInvalid) {
		t.Error("an infrastructure failure must not present as an invalid session")
	}
}
