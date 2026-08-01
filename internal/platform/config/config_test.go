package config

import "testing"

// The legacy app shipped the SQL Server `sa` password to every workstation in
// app.config. The replacement must fail loudly rather than fall back to a
// built-in default, so this is a security test, not a config test.
func TestProdRequiresDSN(t *testing.T) {
	t.Setenv("CARSALEMAN_ENV", "prod")
	t.Setenv("CARSALEMAN_MYSQL_DSN", "")

	if _, err := Load(); err == nil {
		t.Fatal("prod without CARSALEMAN_MYSQL_DSN must fail to start")
	}
}

func TestDevAllowsMissingDSN(t *testing.T) {
	t.Setenv("CARSALEMAN_ENV", "dev")
	t.Setenv("CARSALEMAN_MYSQL_DSN", "")

	if _, err := Load(); err != nil {
		t.Fatalf("dev should start without a DSN: %v", err)
	}
}

func TestRejectsUnknownEnv(t *testing.T) {
	t.Setenv("CARSALEMAN_ENV", "staging")

	if _, err := Load(); err == nil {
		t.Fatal("an unrecognised CARSALEMAN_ENV must be rejected, not silently accepted")
	}
}

func TestDefaults(t *testing.T) {
	t.Setenv("CARSALEMAN_ENV", "dev")
	for _, k := range []string{
		"CARSALEMAN_ADDR", "CARSALEMAN_LOG_LEVEL",
		"CARSALEMAN_SHUTDOWN_TIMEOUT", "CARSALEMAN_CORS_ORIGINS",
	} {
		t.Setenv(k, "")
	}

	c, err := Load()
	if err != nil {
		t.Fatalf("Load: %v", err)
	}
	if c.Addr != ":8080" {
		t.Errorf("Addr = %q, want :8080", c.Addr)
	}
	if len(c.CORSOrigins) != 1 || c.CORSOrigins[0] != "http://localhost:5173" {
		t.Errorf("CORSOrigins = %v, want the Vite dev origin", c.CORSOrigins)
	}
}

func TestDurationAcceptsBareSeconds(t *testing.T) {
	t.Setenv("CARSALEMAN_ENV", "dev")
	t.Setenv("CARSALEMAN_SHUTDOWN_TIMEOUT", "30")

	c, err := Load()
	if err != nil {
		t.Fatalf("Load: %v", err)
	}
	if c.ShutdownTimeout.Seconds() != 30 {
		t.Errorf("ShutdownTimeout = %v, want 30s", c.ShutdownTimeout)
	}
}
