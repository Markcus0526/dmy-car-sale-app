// Package mysql implements the repository interfaces defined by the domain
// packages, against MySQL.
package mysql

import (
	"context"
	"database/sql"
	"fmt"
	"time"

	"github.com/go-sql-driver/mysql"
)

// Open connects and verifies the connection is usable.
//
// The DSN is validated rather than trusted: getting the charset wrong here is
// silent, not loud. A connection that negotiates a narrower charset mangles
// 4-byte characters on the way in with no error, and the damage surfaces
// months later in a report (see the CI migrations job).
func Open(ctx context.Context, dsn string) (*sql.DB, error) {
	cfg, err := mysql.ParseDSN(dsn)
	if err != nil {
		return nil, fmt.Errorf("mysql: parsing DSN: %w", err)
	}

	// Force the settings the rest of the system assumes, rather than hoping
	// the operator supplied them.
	cfg.Params = ensureParams(cfg.Params)
	cfg.ParseTime = true // DATETIME -> time.Time

	// MySQL DATETIME carries no timezone. Every legacy value was written by a
	// Windows client in Chinese local time, so that is how they must be read:
	// interpreting them as UTC would shift every migrated date by 8 hours and
	// silently break report equivalence (plan 9), which is the acceptance
	// criterion for the whole port.
	//
	// This affects parsing and formatting only. time.Time is absolute, so
	// session expiry arithmetic is unaffected either way.
	loc, err := time.LoadLocation("Asia/Shanghai")
	if err != nil {
		return nil, fmt.Errorf("mysql: loading Asia/Shanghai (is tzdata present?): %w", err)
	}
	cfg.Loc = loc

	db, err := sql.Open("mysql", cfg.FormatDSN())
	if err != nil {
		return nil, fmt.Errorf("mysql: open: %w", err)
	}

	// Modest pool: this is an internal system with a few dozen users, and an
	// unbounded pool against a single MySQL is a way to turn a slow query into
	// a connection-exhaustion outage.
	db.SetMaxOpenConns(25)
	db.SetMaxIdleConns(5)
	db.SetConnMaxLifetime(30 * time.Minute)

	if err := db.PingContext(ctx); err != nil {
		db.Close()
		return nil, fmt.Errorf("mysql: ping: %w", err)
	}

	if err := verifyCharset(ctx, db); err != nil {
		db.Close()
		return nil, err
	}
	return db, nil
}

func ensureParams(p map[string]string) map[string]string {
	if p == nil {
		p = map[string]string{}
	}
	// Every string column in the source is NVARCHAR (plan 5.1). utf8mb4 is not
	// negotiable.
	p["charset"] = "utf8mb4"
	p["collation"] = "utf8mb4_unicode_ci"
	return p
}

// verifyCharset fails fast if the connection is not utf8mb4.
//
// Belt and braces over ensureParams: a proxy, a server-side default or a
// future DSN edit could still land us on utf8/latin1, and the failure mode is
// silent data loss rather than an error.
func verifyCharset(ctx context.Context, db *sql.DB) error {
	var client, conn string
	err := db.QueryRowContext(ctx,
		"SELECT @@character_set_client, @@character_set_connection").Scan(&client, &conn)
	if err != nil {
		return fmt.Errorf("mysql: reading connection charset: %w", err)
	}
	if client != "utf8mb4" || conn != "utf8mb4" {
		return fmt.Errorf(
			"mysql: connection charset is client=%s connection=%s, want utf8mb4 — "+
				"4-byte characters would be silently corrupted", client, conn)
	}
	return nil
}
