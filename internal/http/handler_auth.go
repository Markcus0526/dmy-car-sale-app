package http

import (
	"errors"
	"net"
	"net/http"
	"strings"

	"github.com/Markcus0526/carsaleman/internal/auth"
	"github.com/Markcus0526/carsaleman/internal/http/apierr"
)

// SessionCookieName is the cookie carrying the opaque session token (D17).
//
// The __Host- prefix is a browser-enforced guarantee: the cookie must be
// Secure, Path=/, and carry no Domain attribute, which stops a subdomain from
// setting or overwriting it. Only usable over HTTPS, so dev falls back to the
// plain name.
const (
	SessionCookieName       = "__Host-carsaleman_session"
	SessionCookieNameDev    = "carsaleman_session"
	sessionCookieMaxAgeSecs = int(auth.AbsoluteTimeout / 1e9)
)

func (s *Server) sessionCookieName() string {
	if s.cfg.Env == "prod" {
		return SessionCookieName
	}
	return SessionCookieNameDev
}

func (s *Server) setSessionCookie(w http.ResponseWriter, token string) {
	http.SetCookie(w, &http.Cookie{
		Name:  s.sessionCookieName(),
		Value: token,
		Path:  "/",
		// HttpOnly is the reason this is a cookie rather than a header: a token
		// in localStorage is readable by any injected script (D17).
		HttpOnly: true,
		Secure:   s.cfg.Env == "prod",
		// Lax, not Strict: Strict would drop the cookie when a user arrives via
		// an external link, logging them out for no security gain here. Lax
		// still blocks the cross-site POST that CSRF depends on.
		SameSite: http.SameSiteLaxMode,
		MaxAge:   sessionCookieMaxAgeSecs,
	})
}

func (s *Server) clearSessionCookie(w http.ResponseWriter) {
	http.SetCookie(w, &http.Cookie{
		Name:     s.sessionCookieName(),
		Value:    "",
		Path:     "/",
		HttpOnly: true,
		Secure:   s.cfg.Env == "prod",
		SameSite: http.SameSiteLaxMode,
		MaxAge:   -1,
	})
}

type loginRequest struct {
	Username string `json:"username"`
	Password string `json:"password"`
}

func (s *Server) handleLogin(w http.ResponseWriter, r *http.Request) {
	reqID := RequestIDFrom(r.Context())

	if s.auth == nil || s.sessions == nil {
		s.log.Error("login attempted with no database configured", "request_id", reqID)
		apierr.Write(w, apierr.CodeUnavailable, reqID, nil)
		return
	}

	var req loginRequest
	if err := decodeJSON(r, &req); err != nil {
		apierr.Write(w, apierr.CodeBadRequest, reqID, nil)
		return
	}

	fields := map[string]string{}
	if strings.TrimSpace(req.Username) == "" {
		fields["username"] = "REQUIRED"
	}
	if req.Password == "" {
		fields["password"] = "REQUIRED"
	}
	if len(fields) > 0 {
		apierr.Write(w, apierr.CodeValidationFailed, reqID, fields)
		return
	}

	result, err := s.auth.Login(r.Context(), req.Username, req.Password)
	if err != nil {
		if errors.Is(err, auth.ErrInvalidCredentials) {
			// One code for unknown-user and wrong-password alike; the service
			// already equalises timing.
			apierr.Write(w, apierr.CodeInvalidCredentials, reqID, nil)
			return
		}
		s.log.Error("login failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}

	token, err := s.sessions.Issue(r.Context(), result.User.ID, clientIP(r), r.UserAgent())
	if err != nil {
		s.log.Error("issuing session failed", "err", err, "request_id", reqID)
		apierr.Write(w, apierr.CodeInternal, reqID, nil)
		return
	}
	s.setSessionCookie(w, token)

	if result.Upgraded {
		s.log.Info("credential upgraded to bcrypt on login", "user_id", result.User.ID)
	}

	writeJSON(w, http.StatusOK, s.meBody(result.User, result.Permissions))
}

func (s *Server) handleLogout(w http.ResponseWriter, r *http.Request) {
	// Revoke server-side first, then set headers, then write the status.
	//
	// NOT `defer s.clearSessionCookie(w)`: a deferred call runs after
	// WriteHeader has already flushed the headers, and http.SetCookie is
	// silently a no-op at that point. The cookie would never be cleared and
	// nothing would report an error.
	if s.sessions != nil {
		if c, err := r.Cookie(s.sessionCookieName()); err == nil && c.Value != "" {
			// Revocation is what actually ends the session. Clearing the cookie
			// only tidies the browser; a stolen token must die server-side.
			if err := s.sessions.Revoke(r.Context(), c.Value); err != nil {
				s.log.Error("revoking session failed", "err", err,
					"request_id", RequestIDFrom(r.Context()))
			}
		}
	}

	s.clearSessionCookie(w)
	w.WriteHeader(http.StatusNoContent)
}

// meBody builds the /api/auth/me payload.
//
// Permissions are sent so the client can hide what a user cannot reach. That
// is convenience only -- every route is checked server-side, because the
// legacy system's greyed-out menus were never a security boundary (§10.8).
func (s *Server) meBody(u auth.User, perms auth.Set) meResponse {
	out := make(map[string]string, len(perms))
	for k, v := range perms {
		out[k] = string(v)
	}
	name := u.Username
	if name == "" {
		name = u.DepartmentCode
	}
	return meResponse{
		Username:    u.Username,
		DisplayName: name,
		Permissions: out,
		Menu:        filterMenu(menuTree, perms),
	}
}

// clientIP prefers X-Forwarded-For's first entry, then RemoteAddr.
//
// Recorded for the audit trail only, never for authorization -- the header is
// client-supplied and trivially forged.
func clientIP(r *http.Request) string {
	if xff := r.Header.Get("X-Forwarded-For"); xff != "" {
		if first, _, ok := strings.Cut(xff, ","); ok {
			return strings.TrimSpace(first)
		}
		return strings.TrimSpace(xff)
	}
	if host, _, err := net.SplitHostPort(r.RemoteAddr); err == nil {
		return host
	}
	return r.RemoteAddr
}
