// Command carsaleman is the CarSaleMan HTTP API server.
package main

import (
	"context"
	"errors"
	"net/http"
	"os"
	"os/signal"
	"syscall"
	"time"

	"github.com/Markcus0526/carsaleman/internal/auth"
	apphttp "github.com/Markcus0526/carsaleman/internal/http"
	"github.com/Markcus0526/carsaleman/internal/platform/config"
	"github.com/Markcus0526/carsaleman/internal/platform/logging"
	storemysql "github.com/Markcus0526/carsaleman/internal/store/mysql"
)

func main() {
	if err := run(); err != nil {
		// Config may have failed before a logger exists, so use stderr directly.
		os.Stderr.WriteString("fatal: " + err.Error() + "\n")
		os.Exit(1)
	}
}

func run() error {
	cfg, err := config.Load()
	if err != nil {
		return err
	}

	log := logging.New(cfg.LogLevel, cfg.Env)
	log.Info("starting", "addr", cfg.Addr, "env", cfg.Env)

	// Authentication needs storage. In dev without a DSN the server still
	// starts and serves /api/health, so the frontend can be worked on without
	// a database -- but every authenticated route will fail loudly rather than
	// silently granting access.
	var (
		authSvc  *auth.Service
		sessions *auth.SessionService
	)
	if cfg.MySQLDSN != "" {
		dbCtx, cancelDB := context.WithTimeout(context.Background(), 15*time.Second)
		db, err := storemysql.Open(dbCtx, cfg.MySQLDSN)
		cancelDB()
		if err != nil {
			return err
		}
		defer db.Close()

		authSvc = auth.NewService(storemysql.NewUserRepo(db), log)
		sessions = auth.NewSessionService(storemysql.NewSessionRepo(db))
		log.Info("database connected")
	} else {
		log.Warn("no CARSALEMAN_MYSQL_DSN: authenticated routes are unavailable")
	}

	srv := &http.Server{
		Addr:              cfg.Addr,
		Handler:           apphttp.NewServer(cfg, log, authSvc, sessions).Handler(),
		ReadHeaderTimeout: 10 * time.Second,
	}

	// Graceful shutdown on SIGINT/SIGTERM.
	ctx, stop := signal.NotifyContext(context.Background(), os.Interrupt, syscall.SIGTERM)
	defer stop()

	errCh := make(chan error, 1)
	go func() {
		if err := srv.ListenAndServe(); err != nil && !errors.Is(err, http.ErrServerClosed) {
			errCh <- err
		}
	}()

	select {
	case err := <-errCh:
		return err
	case <-ctx.Done():
		log.Info("shutting down", "timeout", cfg.ShutdownTimeout)
		shutdownCtx, cancel := context.WithTimeout(context.Background(), cfg.ShutdownTimeout)
		defer cancel()
		return srv.Shutdown(shutdownCtx)
	}
}
