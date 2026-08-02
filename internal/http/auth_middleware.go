package http

import (
	"context"
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"net/http"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/http/apierr"
)

const ctxKeyUser ctxKey = iota + 1

type authContext struct {
	UserID      int64
	Permissions auth.Set
}

// UserFrom returns the authenticated caller, if any.
func UserFrom(ctx context.Context) (authContext, bool) {
	a, ok := ctx.Value(ctxKeyUser).(authContext)
	return a, ok
}

// requireAuth rejects unauthenticated requests and attaches the caller's
// permissions to the context.
//
// Permissions are loaded per request rather than carried in the session, so an
// administrator's revocation takes effect on the very next call. That is the
// property D17 exists to provide, and caching it here would give it away.
func (s *Server) requireAuth(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		reqID := RequestIDFrom(r.Context())

		// Without a database there is no way to authenticate anyone. Say so
		// explicitly: dereferencing a nil service would panic into a generic
		// 500 and look like a bug rather than a missing DSN.
		if s.Sessions == nil || s.auth == nil {
			s.log.Error("authenticated route reached with no database configured",
				"path", r.URL.Path, "request_id", reqID)
			apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
			return
		}

		c, err := r.Cookie(s.sessionCookieName())
		if err != nil || c.Value == "" {
			apierr.Write(w, apierr.CodeUnauthorized, reqID, nil)
			return
		}

		userID, err := s.Sessions.Validate(r.Context(), c.Value)
		if err != nil {
			if errors.Is(err, auth.ErrSessionInvalid) {
				// The cookie is dead; clear it so the browser stops sending it.
				s.clearSessionCookie(w)
				apierr.Write(w, apierr.CodeUnauthorized, reqID, nil)
				return
			}
			s.log.Error("session validation failed", "err", err, "request_id", reqID)
			apierr.Write(w, apierr.CodeInternal, reqID, nil)
			return
		}

		perms, err := s.auth.Permissions(r.Context(), userID)
		if err != nil {
			s.log.Error("loading permissions failed", "err", err, "request_id", reqID)
			apierr.Write(w, apierr.CodeInternal, reqID, nil)
			return
		}

		ctx := context.WithValue(r.Context(), ctxKeyUser,
			authContext{UserID: userID, Permissions: perms})
		next.ServeHTTP(w, r.WithContext(ctx))
	})
}

// requirePermission gates a route on a permission key.
//
// This is the fix for §10.8. The legacy application only greyed out menu
// items; nothing stopped a client from issuing the operation anyway. Nav
// gating on the client is a convenience, and this is the boundary.
func (s *Server) requirePermission(key string, write bool, next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		a, ok := UserFrom(r.Context())
		if !ok {
			apierr.Write(w, apierr.CodeUnauthorized, RequestIDFrom(r.Context()), nil)
			return
		}

		allowed := a.Permissions.CanRead(key)
		if write {
			allowed = a.Permissions.CanWrite(key)
		}
		if !allowed {
			apierr.Write(w, apierr.CodeForbidden, RequestIDFrom(r.Context()), nil)
			return
		}
		next.ServeHTTP(w, r)
	})
}

// decodeJSON reads a JSON body, rejecting unknown fields and oversized input.
func decodeJSON(r *http.Request, dst any) error {
	// 1 MiB is far more than any request here needs; the point is to bound
	// what an unauthenticated caller can make the server allocate.
	dec := json.NewDecoder(io.LimitReader(r.Body, 1<<20))
	dec.DisallowUnknownFields()
	if err := dec.Decode(dst); err != nil {
		return fmt.Errorf("decoding body: %w", err)
	}
	return nil
}
