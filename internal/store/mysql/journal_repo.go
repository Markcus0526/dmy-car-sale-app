package mysql

import (
	"context"
	"database/sql"
	"fmt"
	"time"
)

// tbl_log is the 特殊日记 ("special journal") notes table -- NOT an audit
// trail, despite what the README and GO_MIGRATION_PLAN §11.3 said before day
// 23. Its only user in the entire legacy codebase is FrmSpecJournal, an
// editable grid of date/title/body rows that staff write by hand.
//
// FrmActionHis, which the plan called the audit-trail viewer, reads a
// List<StoreChange> its caller passes in and never touches this table.
//
// Whether to ADD a real audit trail is Q10. It is new scope, not migration.

// JournalEntry is one note.
type JournalEntry struct {
	UID  int64  `json:"uid"`
	Date string `json:"logdate"` // yyyy-mm-dd
	// Title is VARCHAR(50) NULL; Content is VARCHAR(200) NULL. The legacy new-row
	// handler defaults both to "" (FrmSpecJournal.cs:169-171), so blank rather
	// than NULL is the shape this table actually holds.
	Title      string `json:"title"`
	Content    string `json:"cont"`
	RowVersion int    `json:"rowVersion"`
}

type JournalRepo struct{ db *sql.DB }

func NewJournalRepo(db *sql.DB) *JournalRepo { return &JournalRepo{db: db} }

// MaxJournalRows bounds one page of notes.
const MaxJournalRows = 500

// List returns notes newest first.
//
// Newest first, unlike every other list in this system: the legacy grid was
// insertion-ordered, but a journal is read at the top. Ordering by uid as the
// tiebreak keeps same-day entries stable rather than shuffling on each load.
func (r *JournalRepo) List(ctx context.Context) (entries []JournalEntry, truncated bool, err error) {
	const q = `
		SELECT uid, logdate, COALESCE(title,''), COALESCE(cont,''), row_version
		FROM tbl_log
		ORDER BY logdate DESC, uid DESC
		LIMIT ?`

	rows, err := r.db.QueryContext(ctx, q, MaxJournalRows+1)
	if err != nil {
		return nil, false, fmt.Errorf("mysql: list journal: %w", err)
	}
	defer rows.Close()

	entries = []JournalEntry{}
	for rows.Next() {
		var e JournalEntry
		var d time.Time
		if err := rows.Scan(&e.UID, &d, &e.Title, &e.Content, &e.RowVersion); err != nil {
			return nil, false, fmt.Errorf("mysql: list journal: %w", err)
		}
		e.Date = d.Format("2006-01-02")
		entries = append(entries, e)
	}
	if err := rows.Err(); err != nil {
		return nil, false, fmt.Errorf("mysql: list journal: %w", err)
	}

	if len(entries) > MaxJournalRows {
		return entries[:MaxJournalRows], true, nil
	}
	return entries, false, nil
}

func (r *JournalRepo) Create(ctx context.Context, date, title, content string) (int64, error) {
	res, err := r.db.ExecContext(ctx,
		`INSERT INTO tbl_log (logdate, title, cont) VALUES (?,?,?)`, date, title, content)
	if err != nil {
		return 0, fmt.Errorf("mysql: create journal entry: %w", err)
	}
	return res.LastInsertId()
}

// Update edits one note, guarded by row_version.
func (r *JournalRepo) Update(ctx context.Context, uid int64, version int, date, title, content string) error {
	res, err := r.db.ExecContext(ctx, `
		UPDATE tbl_log SET logdate = ?, title = ?, cont = ?, row_version = row_version + 1
		WHERE uid = ? AND row_version = ?`,
		date, title, content, uid, version)
	if err != nil {
		return fmt.Errorf("mysql: update journal entry: %w", err)
	}
	if n, _ := res.RowsAffected(); n == 1 {
		return nil
	}

	var exists bool
	if err := r.db.QueryRowContext(ctx,
		`SELECT EXISTS(SELECT 1 FROM tbl_log WHERE uid = ?)`, uid).Scan(&exists); err != nil {
		return fmt.Errorf("mysql: update journal entry: %w", err)
	}
	if !exists {
		return ErrNotFound
	}
	return ErrVersionConflict
}

func (r *JournalRepo) Delete(ctx context.Context, uid int64) error {
	res, err := r.db.ExecContext(ctx, `DELETE FROM tbl_log WHERE uid = ?`, uid)
	if err != nil {
		return fmt.Errorf("mysql: delete journal entry: %w", err)
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return ErrNotFound
	}
	return nil
}
