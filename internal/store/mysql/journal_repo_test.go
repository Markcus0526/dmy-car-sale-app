package mysql

import (
	"context"
	"database/sql"
	"errors"
	"testing"
)

func freshJournal(t *testing.T, db *sql.DB) *JournalRepo {
	t.Helper()
	if _, err := db.ExecContext(context.Background(), "DELETE FROM tbl_log"); err != nil {
		t.Fatalf("clearing tbl_log: %v", err)
	}
	return NewJournalRepo(db)
}

func TestJournalCreateAndList(t *testing.T) {
	db := testDB(t)
	repo := freshJournal(t, db)
	ctx := context.Background()

	if _, err := repo.Create(ctx, "2026-07-01", "旧记录", "内容甲"); err != nil {
		t.Fatal(err)
	}
	if _, err := repo.Create(ctx, "2026-08-01", "新记录", "内容乙"); err != nil {
		t.Fatal(err)
	}

	entries, truncated, err := repo.List(ctx)
	if err != nil {
		t.Fatalf("list: %v", err)
	}
	if truncated {
		t.Error("two rows should not truncate")
	}
	// Newest first. Unlike every other list here, which is insertion-ordered:
	// a journal is read from the top.
	if len(entries) != 2 || entries[0].Title != "新记录" {
		t.Fatalf("got %+v, want the newest entry first", entries)
	}
	if entries[0].Date != "2026-08-01" {
		t.Errorf("date = %q, want 2026-08-01", entries[0].Date)
	}
}

func TestJournalUpdateRejectsStaleVersion(t *testing.T) {
	db := testDB(t)
	repo := freshJournal(t, db)
	ctx := context.Background()

	uid, err := repo.Create(ctx, "2026-08-01", "标题", "内容")
	if err != nil {
		t.Fatal(err)
	}
	if err := repo.Update(ctx, uid, 1, "2026-08-01", "标题", "第一次修改"); err != nil {
		t.Fatalf("first update: %v", err)
	}
	if err := repo.Update(ctx, uid, 1, "2026-08-01", "标题", "第二次修改"); !errors.Is(err, ErrVersionConflict) {
		t.Fatalf("stale update: err = %v, want ErrVersionConflict", err)
	}

	entries, _, _ := repo.List(ctx)
	if len(entries) != 1 || entries[0].Content != "第一次修改" {
		t.Errorf("content = %+v — the stale write overwrote the newer row", entries)
	}
}

func TestJournalDelete(t *testing.T) {
	db := testDB(t)
	repo := freshJournal(t, db)
	ctx := context.Background()

	uid, err := repo.Create(ctx, "2026-08-01", "标题", "内容")
	if err != nil {
		t.Fatal(err)
	}
	if err := repo.Delete(ctx, uid); err != nil {
		t.Fatalf("delete: %v", err)
	}
	if err := repo.Delete(ctx, uid); !errors.Is(err, ErrNotFound) {
		t.Errorf("second delete: err = %v, want ErrNotFound", err)
	}
}

// title and cont are NULL-able, but the legacy new-row handler defaults both
// to "" (FrmSpecJournal.cs:169-171). Existing rows may hold either, so both
// must read back as the empty string rather than crashing the scan.
func TestJournalNullColumnsReadAsEmpty(t *testing.T) {
	db := testDB(t)
	repo := freshJournal(t, db)
	ctx := context.Background()

	if _, err := db.ExecContext(ctx,
		`INSERT INTO tbl_log (logdate, title, cont) VALUES ('2026-08-01', NULL, NULL)`); err != nil {
		t.Fatal(err)
	}
	entries, _, err := repo.List(ctx)
	if err != nil {
		t.Fatalf("list: %v", err)
	}
	if len(entries) != 1 || entries[0].Title != "" || entries[0].Content != "" {
		t.Errorf("got %+v, want empty strings for the NULL columns", entries)
	}
}
