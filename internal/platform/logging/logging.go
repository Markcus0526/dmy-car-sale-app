// Package logging provides the application logger.
//
// Deliberately replaces CommonMisc.LogErrors, whose catch block called itself
// (GO_MIGRATION_PLAN.md 10.4): if the log file was unwritable the recursion was
// unbounded and killed the process with a StackOverflowException .NET cannot
// catch. slog writes to stderr and cannot recurse -- there is no fallback path
// that re-enters the logger.
package logging

import (
	"log/slog"
	"os"
	"strings"
)

// New returns a structured logger. Format is JSON in prod, text in dev.
func New(level, env string) *slog.Logger {
	opts := &slog.HandlerOptions{Level: parseLevel(level)}

	var h slog.Handler
	if env == "prod" {
		h = slog.NewJSONHandler(os.Stderr, opts)
	} else {
		h = slog.NewTextHandler(os.Stderr, opts)
	}
	return slog.New(h)
}

func parseLevel(s string) slog.Level {
	switch strings.ToLower(strings.TrimSpace(s)) {
	case "debug":
		return slog.LevelDebug
	case "warn", "warning":
		return slog.LevelWarn
	case "error":
		return slog.LevelError
	default:
		return slog.LevelInfo
	}
}
