package mysql

import (
	"context"
	"database/sql"
	"errors"
	"fmt"
	"strings"
)

// tbl_basedata is the generic key/value store behind every dropdown in the
// system -- 17 domains (§5.6), each a group of rows sharing a `name`.
//
// WHAT `type` MEANS (answers Q4, from the source rather than from live data)
//
// FrmBaseData.cs:93-101 reads `type` from the FIRST row of the selected group
// and uses it to show or hide the `keyname` column for the whole group:
//
//	type 1 — value-only. keyname is hidden and written as "".
//	type 2 — key/value pair. keyname is shown, and bulk import splits each
//	         pasted line on ":" (FrmBaseData.cs:300-318).
//
// So `type` is a property of the DOMAIN, not of the row: it is chosen once
// when the domain is created (FrmBaseDataEdit's radio buttons) and applied to
// every row underneath it. This repository enforces that invariant, which the
// legacy code assumed but never checked.

const (
	// TypeValueOnly is a domain of plain values -- 地区, 行业, 销售方式.
	TypeValueOnly = 1
	// TypeKeyValue is a domain of code/label pairs -- keyname is meaningful.
	TypeKeyValue = 2
)

// ErrDuplicateDomain is returned when creating a domain whose name exists.
// The legacy app checked this in the client against its cached DataSet, which
// two users could pass simultaneously.
var ErrDuplicateDomain = errors.New("mysql: a domain with that name already exists")

// BaseDomain is one dropdown's worth of reference data.
type BaseDomain struct {
	// Name is the Chinese domain name as stored, e.g. 车系列. It is data and
	// an identifier at once -- rows are grouped by it -- so it is never
	// translated in storage. The UI resolves a display label separately and
	// falls back to this string.
	Name string `json:"name"`
	Type int    `json:"type"`
	// Count includes the blank placeholder row, if any. See CreateDomain.
	Count int `json:"count"`
}

// BaseValue is one row of a domain.
type BaseValue struct {
	UID        int64  `json:"uid"`
	Name       string `json:"name"`
	Type       int    `json:"type"`
	KeyName    string `json:"keyname"`
	Value      string `json:"value"`
	RowVersion int    `json:"rowVersion"`
}

type BaseDataRepo struct{ db *sql.DB }

func NewBaseDataRepo(db *sql.DB) *BaseDataRepo { return &BaseDataRepo{db: db} }

// Domains lists every domain with its type and row count.
//
// MIN(type) rather than an arbitrary pick: if a domain's rows ever disagree
// about their type -- which the legacy app permitted, since it never wrote the
// type back on edit -- this is deterministic instead of depending on row order.
// DomainTypeConflicts reports the disagreement separately rather than hiding it.
func (r *BaseDataRepo) Domains(ctx context.Context) ([]BaseDomain, error) {
	const q = `
		SELECT name, MIN(type), COUNT(*)
		FROM tbl_basedata
		GROUP BY name
		ORDER BY name`

	rows, err := r.db.QueryContext(ctx, q)
	if err != nil {
		return nil, fmt.Errorf("mysql: list basedata domains: %w", err)
	}
	defer rows.Close()

	domains := []BaseDomain{}
	for rows.Next() {
		var d BaseDomain
		if err := rows.Scan(&d.Name, &d.Type, &d.Count); err != nil {
			return nil, fmt.Errorf("mysql: list basedata domains: %w", err)
		}
		domains = append(domains, d)
	}
	return domains, rows.Err()
}

// DomainTypeConflicts returns domains whose rows disagree about `type`.
//
// The legacy schema cannot express "type is constant within a name", so the
// data may violate it. Surfaced rather than silently normalised: a domain that
// half-thinks it has keynames is a data problem for a human to look at, and
// picking a winner would destroy the evidence.
func (r *BaseDataRepo) DomainTypeConflicts(ctx context.Context) ([]string, error) {
	const q = `
		SELECT name FROM tbl_basedata
		GROUP BY name HAVING COUNT(DISTINCT type) > 1
		ORDER BY name`

	rows, err := r.db.QueryContext(ctx, q)
	if err != nil {
		return nil, fmt.Errorf("mysql: basedata type conflicts: %w", err)
	}
	defer rows.Close()

	names := []string{}
	for rows.Next() {
		var n string
		if err := rows.Scan(&n); err != nil {
			return nil, fmt.Errorf("mysql: basedata type conflicts: %w", err)
		}
		names = append(names, n)
	}
	return names, rows.Err()
}

// Values returns one domain's rows in stored order.
//
// Ordering is by uid, not by value: Chinese display order needs x/text/collate
// in Go (D16), because utf8mb4_unicode_ci does not reproduce the legacy
// Chinese_PRC_CI_AS sequence.
func (r *BaseDataRepo) Values(ctx context.Context, name string) ([]BaseValue, error) {
	const q = `
		SELECT uid, name, type, COALESCE(keyname,''), COALESCE(value,''), row_version
		FROM tbl_basedata WHERE name = ? ORDER BY uid`

	rows, err := r.db.QueryContext(ctx, q, name)
	if err != nil {
		return nil, fmt.Errorf("mysql: list basedata values: %w", err)
	}
	defer rows.Close()

	values := []BaseValue{}
	for rows.Next() {
		var v BaseValue
		if err := rows.Scan(&v.UID, &v.Name, &v.Type, &v.KeyName, &v.Value, &v.RowVersion); err != nil {
			return nil, fmt.Errorf("mysql: list basedata values: %w", err)
		}
		values = append(values, v)
	}
	return values, rows.Err()
}

// Lookup returns a domain's non-blank values, for populating a dropdown.
//
// Blank values are filtered out here but NOT in Values: CreateDomain has to
// insert a placeholder row to make an empty domain exist at all (a domain is
// only a group of rows), and that placeholder must be visible in the admin
// screen while never appearing in a dropdown.
func (r *BaseDataRepo) Lookup(ctx context.Context, name string) ([]BaseValue, error) {
	all, err := r.Values(ctx, name)
	if err != nil {
		return nil, err
	}
	out := make([]BaseValue, 0, len(all))
	for _, v := range all {
		if strings.TrimSpace(v.Value) != "" {
			out = append(out, v)
		}
	}
	return out, nil
}

// CreateDomain adds a new domain with a single blank placeholder row.
//
// The placeholder is not decoration. A domain has no existence of its own in
// this schema -- it is precisely "the set of rows sharing a name" -- so
// creating an empty one means creating a row. The legacy app does the same
// (FrmBaseData.cs:170-176). Lookup filters it back out.
func (r *BaseDataRepo) CreateDomain(ctx context.Context, name string, typ int) error {
	tx, err := r.db.BeginTx(ctx, nil)
	if err != nil {
		return fmt.Errorf("mysql: create domain: %w", err)
	}
	defer tx.Rollback() //nolint:errcheck // no-op after a successful Commit

	// FOR UPDATE, so two concurrent creates cannot both see "not present".
	// The legacy check ran client-side against a cached DataSet and had no
	// defence against that at all.
	var exists bool
	if err := tx.QueryRowContext(ctx,
		`SELECT EXISTS(SELECT 1 FROM tbl_basedata WHERE name = ? FOR UPDATE)`, name,
	).Scan(&exists); err != nil {
		return fmt.Errorf("mysql: create domain: %w", err)
	}
	if exists {
		return ErrDuplicateDomain
	}

	if _, err := tx.ExecContext(ctx,
		`INSERT INTO tbl_basedata (type, name, keyname, value) VALUES (?,?,'','')`,
		typ, name); err != nil {
		return fmt.Errorf("mysql: create domain: %w", err)
	}
	return tx.Commit()
}

// RenameDomain renames every row in a domain.
//
// It does NOT rewrite the values stored on transaction tables. That is
// correct: tbl_onroad.carstate and friends store the VALUE (红色), never the
// domain name (颜色), so renaming a domain cannot orphan anything. Deleting a
// value has the same property -- existing records keep displaying their stored
// string; it just stops being offered in new dropdowns.
func (r *BaseDataRepo) RenameDomain(ctx context.Context, oldName, newName string) error {
	tx, err := r.db.BeginTx(ctx, nil)
	if err != nil {
		return fmt.Errorf("mysql: rename domain: %w", err)
	}
	defer tx.Rollback() //nolint:errcheck

	var exists bool
	if err := tx.QueryRowContext(ctx,
		`SELECT EXISTS(SELECT 1 FROM tbl_basedata WHERE name = ? FOR UPDATE)`, newName,
	).Scan(&exists); err != nil {
		return fmt.Errorf("mysql: rename domain: %w", err)
	}
	if exists {
		return ErrDuplicateDomain
	}

	res, err := tx.ExecContext(ctx,
		`UPDATE tbl_basedata SET name = ?, row_version = row_version + 1 WHERE name = ?`,
		newName, oldName)
	if err != nil {
		return fmt.Errorf("mysql: rename domain: %w", err)
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return ErrNotFound
	}
	return tx.Commit()
}

// DeleteDomain removes every row in a domain. See RenameDomain on why this
// does not orphan stored values.
func (r *BaseDataRepo) DeleteDomain(ctx context.Context, name string) error {
	res, err := r.db.ExecContext(ctx, `DELETE FROM tbl_basedata WHERE name = ?`, name)
	if err != nil {
		return fmt.Errorf("mysql: delete domain: %w", err)
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return ErrNotFound
	}
	return nil
}

// domainType reads a domain's type, so a value inherits it rather than
// accepting one from the caller. A row whose type disagrees with its domain is
// the corruption DomainTypeConflicts exists to report; there is no reason to
// let the write path create more of it.
func (r *BaseDataRepo) domainType(ctx context.Context, q queryer, name string) (int, error) {
	// sql.NullInt64, not int. A bare aggregate over zero rows returns ONE row
	// holding NULL rather than no rows at all, so scanning into an int would
	// surface a missing domain as a driver conversion error instead of
	// ErrNotFound -- a 500 where the answer is 404.
	var typ sql.NullInt64
	err := q.QueryRowContext(ctx,
		`SELECT MIN(type) FROM tbl_basedata WHERE name = ?`, name).Scan(&typ)
	if err != nil {
		return 0, fmt.Errorf("mysql: domain type: %w", err)
	}
	if !typ.Valid {
		return 0, ErrNotFound
	}
	return int(typ.Int64), nil
}

// queryer is the subset shared by *sql.DB and *sql.Tx.
type queryer interface {
	QueryRowContext(ctx context.Context, q string, args ...any) *sql.Row
}

// AddValue appends a row to a domain, inheriting the domain's type.
func (r *BaseDataRepo) AddValue(ctx context.Context, name, keyname, value string) (int64, error) {
	typ, err := r.domainType(ctx, r.db, name)
	if err != nil {
		return 0, err
	}
	if typ == TypeValueOnly {
		keyname = "" // type 1 has no keyname; the legacy app writes "" too
	}

	res, err := r.db.ExecContext(ctx,
		`INSERT INTO tbl_basedata (type, name, keyname, value) VALUES (?,?,?,?)`,
		typ, name, keyname, value)
	if err != nil {
		return 0, fmt.Errorf("mysql: add basedata value: %w", err)
	}
	return res.LastInsertId()
}

// UpdateValue edits one row, guarded by row_version.
//
// name and type are deliberately not updatable: moving a row between domains
// through an edit box is not a thing the UI offers, and allowing it here would
// be a way to break the domain/type invariant from the outside.
func (r *BaseDataRepo) UpdateValue(ctx context.Context, uid int64, version int, keyname, value string) error {
	res, err := r.db.ExecContext(ctx, `
		UPDATE tbl_basedata
		SET keyname = IF(type = ?, '', ?), value = ?, row_version = row_version + 1
		WHERE uid = ? AND row_version = ?`,
		TypeValueOnly, keyname, value, uid, version)
	if err != nil {
		return fmt.Errorf("mysql: update basedata value: %w", err)
	}
	if n, _ := res.RowsAffected(); n == 1 {
		return nil
	}

	var exists bool
	if err := r.db.QueryRowContext(ctx,
		`SELECT EXISTS(SELECT 1 FROM tbl_basedata WHERE uid = ?)`, uid).Scan(&exists); err != nil {
		return fmt.Errorf("mysql: update basedata value: %w", err)
	}
	if !exists {
		return ErrNotFound
	}
	return ErrVersionConflict
}

// DeleteValue removes one row.
func (r *BaseDataRepo) DeleteValue(ctx context.Context, uid int64) error {
	res, err := r.db.ExecContext(ctx, `DELETE FROM tbl_basedata WHERE uid = ?`, uid)
	if err != nil {
		return fmt.Errorf("mysql: delete basedata value: %w", err)
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return ErrNotFound
	}
	return nil
}

// MaxImportRows bounds a single bulk import.
//
// A paste box with no limit is a way to turn a stray clipboard into a table
// full of garbage, and this is a lookup table every screen reads.
const MaxImportRows = 500

// ImportValues bulk-inserts pasted lines, reproducing the legacy parse.
//
// Line handling follows FrmBaseData.cs:291-318 exactly:
//
//	type 1 — the whole line is the value.
//	type 2 — split on the FIRST ':' into keyname and value; a line with no
//	         colon becomes a value with an empty keyname rather than being
//	         rejected.
//
// Blank lines are skipped, as in the original. Reproduced rather than improved:
// operators have muscle memory for this format, and a stricter parser would
// reject the files they already have.
func (r *BaseDataRepo) ImportValues(ctx context.Context, name, content string) (int, error) {
	typ, err := r.domainType(ctx, r.db, name)
	if err != nil {
		return 0, err
	}

	lines := strings.FieldsFunc(content, func(c rune) bool { return c == '\n' || c == '\r' })
	if len(lines) > MaxImportRows {
		return 0, fmt.Errorf("%w: %d lines exceeds the %d-line limit",
			ErrTooManyRows, len(lines), MaxImportRows)
	}

	tx, err := r.db.BeginTx(ctx, nil)
	if err != nil {
		return 0, fmt.Errorf("mysql: import basedata: %w", err)
	}
	defer tx.Rollback() //nolint:errcheck

	stmt, err := tx.PrepareContext(ctx,
		`INSERT INTO tbl_basedata (type, name, keyname, value) VALUES (?,?,?,?)`)
	if err != nil {
		return 0, fmt.Errorf("mysql: import basedata: %w", err)
	}
	defer stmt.Close()

	n := 0
	for _, line := range lines {
		line = strings.TrimSpace(line)
		if line == "" {
			continue
		}
		keyname, value := "", line
		if typ == TypeKeyValue {
			if k, v, found := strings.Cut(line, ":"); found {
				keyname, value = k, v
			}
		}
		if _, err := stmt.ExecContext(ctx, typ, name, keyname, value); err != nil {
			return 0, fmt.Errorf("mysql: import basedata: %w", err)
		}
		n++
	}

	// One transaction, so a failure halfway through leaves nothing behind.
	// The legacy import added rows to a client DataSet and relied on the user
	// remembering to press save -- a partial import was the normal outcome.
	if err := tx.Commit(); err != nil {
		return 0, fmt.Errorf("mysql: import basedata: %w", err)
	}
	return n, nil
}

// ErrTooManyRows is returned when a bulk operation exceeds its limit.
var ErrTooManyRows = errors.New("mysql: too many rows")
