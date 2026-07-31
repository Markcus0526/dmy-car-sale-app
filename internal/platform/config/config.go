// Package config loads runtime configuration from the environment.
//
// The legacy app shipped the SQL Server `sa` password in app.config to every
// workstation (GO_MIGRATION_PLAN.md 3.2, 11.5). Nothing here has a credential
// default: a missing DSN is a startup failure, not a silent fallback.
package config

import (
	"fmt"
	"os"
	"strconv"
	"strings"
	"time"
)

type Config struct {
	Addr            string
	MySQLDSN        string
	Env             string // "dev" | "prod"
	LogLevel        string // "debug" | "info" | "warn" | "error"
	ShutdownTimeout time.Duration
	CORSOrigins     []string
}

// Load reads configuration from the environment.
//
// Required in prod: CARSALEMAN_MYSQL_DSN.
func Load() (Config, error) {
	c := Config{
		Addr:            env("CARSALEMAN_ADDR", ":8080"),
		MySQLDSN:        os.Getenv("CARSALEMAN_MYSQL_DSN"),
		Env:             env("CARSALEMAN_ENV", "dev"),
		LogLevel:        env("CARSALEMAN_LOG_LEVEL", "info"),
		ShutdownTimeout: envDuration("CARSALEMAN_SHUTDOWN_TIMEOUT", 15*time.Second),
		CORSOrigins:     envList("CARSALEMAN_CORS_ORIGINS", []string{"http://localhost:5173"}),
	}

	if c.Env != "dev" && c.Env != "prod" {
		return c, fmt.Errorf("CARSALEMAN_ENV must be dev or prod, got %q", c.Env)
	}
	if c.Env == "prod" && c.MySQLDSN == "" {
		return c, fmt.Errorf("CARSALEMAN_MYSQL_DSN is required when CARSALEMAN_ENV=prod")
	}
	return c, nil
}

func env(key, def string) string {
	if v := os.Getenv(key); v != "" {
		return v
	}
	return def
}

func envDuration(key string, def time.Duration) time.Duration {
	v := os.Getenv(key)
	if v == "" {
		return def
	}
	if d, err := time.ParseDuration(v); err == nil {
		return d
	}
	if secs, err := strconv.Atoi(v); err == nil {
		return time.Duration(secs) * time.Second
	}
	return def
}

func envList(key string, def []string) []string {
	v := os.Getenv(key)
	if v == "" {
		return def
	}
	parts := strings.Split(v, ",")
	out := make([]string, 0, len(parts))
	for _, p := range parts {
		if p = strings.TrimSpace(p); p != "" {
			out = append(out, p)
		}
	}
	if len(out) == 0 {
		return def
	}
	return out
}
