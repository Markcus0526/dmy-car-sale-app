// Package apierr defines the error envelope returned by every endpoint.
//
// Errors carry a machine-readable CODE, never human prose. The client resolves
// the code to a localised message via its i18n catalogue -- that is what keeps
// the backend locale-agnostic and stops English/Chinese strings leaking into
// Go source. See DEVELOPMENT_PLAN.md 1.1.
//
// Codes map to i18n keys on the client by convention: AUTH_INVALID_CREDENTIALS
// resolves to error.AUTH_INVALID_CREDENTIALS.
package apierr

import (
	"encoding/json"
	"net/http"
)

type Code string

const (
	CodeBadRequest         Code = "BAD_REQUEST"
	CodeUnauthorized       Code = "UNAUTHORIZED"
	CodeForbidden          Code = "FORBIDDEN"
	CodeNotFound           Code = "NOT_FOUND"
	CodeConflict           Code = "CONFLICT" // optimistic concurrency, 11.4
	CodeValidationFailed   Code = "VALIDATION_FAILED"
	CodeInvalidCredentials Code = "AUTH_INVALID_CREDENTIALS"
	CodeInternal           Code = "INTERNAL"
	// CodeUnavailable: the route needs a dependency that is not configured
	// (e.g. no database). Distinct from INTERNAL so an operator can tell a
	// misconfiguration from a bug.
	CodeUnavailable Code = "SERVICE_UNAVAILABLE"
)

// Error is the JSON body of every non-2xx response.
type Error struct {
	Code Code `json:"code"`
	// Fields carries per-field validation codes, e.g. {"username": "REQUIRED"}.
	Fields map[string]string `json:"fields,omitempty"`
	// RequestID lets a user quote something actionable to support.
	RequestID string `json:"requestId,omitempty"`
}

var statusFor = map[Code]int{
	CodeBadRequest:         http.StatusBadRequest,
	CodeUnauthorized:       http.StatusUnauthorized,
	CodeForbidden:          http.StatusForbidden,
	CodeNotFound:           http.StatusNotFound,
	CodeConflict:           http.StatusConflict,
	CodeValidationFailed:   http.StatusUnprocessableEntity,
	CodeInvalidCredentials: http.StatusUnauthorized,
	CodeInternal:           http.StatusInternalServerError,
	CodeUnavailable:        http.StatusServiceUnavailable,
}

// Status returns the HTTP status for a code, defaulting to 500.
func Status(c Code) int {
	if s, ok := statusFor[c]; ok {
		return s
	}
	return http.StatusInternalServerError
}

// Write emits the error envelope. requestID may be empty.
func Write(w http.ResponseWriter, code Code, requestID string, fields map[string]string) {
	w.Header().Set("Content-Type", "application/json; charset=utf-8")
	w.WriteHeader(Status(code))
	_ = json.NewEncoder(w).Encode(Error{Code: code, Fields: fields, RequestID: requestID})
}
