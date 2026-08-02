package mysql

import (
	"context"
	"database/sql"
	"errors"
	"strings"
	"testing"
)

func freshBaseData(t *testing.T, db *sql.DB) *BaseDataRepo {
	t.Helper()
	if _, err := db.ExecContext(context.Background(), "DELETE FROM tbl_basedata"); err != nil {
		t.Fatalf("clearing tbl_basedata: %v", err)
	}
	return NewBaseDataRepo(db)
}

func TestBaseDataCreateDomainAndList(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if err := repo.CreateDomain(ctx, "地区", TypeValueOnly); err != nil {
		t.Fatalf("create 地区: %v", err)
	}
	if err := repo.CreateDomain(ctx, "车系列", TypeKeyValue); err != nil {
		t.Fatalf("create 车系列: %v", err)
	}

	domains, err := repo.Domains(ctx)
	if err != nil {
		t.Fatalf("domains: %v", err)
	}
	if len(domains) != 2 {
		t.Fatalf("got %d domains, want 2", len(domains))
	}
	byName := map[string]BaseDomain{}
	for _, d := range domains {
		byName[d.Name] = d
	}
	if byName["地区"].Type != TypeValueOnly {
		t.Errorf("地区 type = %d, want 1", byName["地区"].Type)
	}
	if byName["车系列"].Type != TypeKeyValue {
		t.Errorf("车系列 type = %d, want 2", byName["车系列"].Type)
	}
	// A new domain carries exactly one blank placeholder row: a domain has no
	// existence apart from its rows in this schema.
	if byName["地区"].Count != 1 {
		t.Errorf("地区 count = %d, want 1 (the placeholder)", byName["地区"].Count)
	}
}

// The placeholder must be visible to the admin screen and invisible to every
// dropdown. A blank option in 地区 is a bug users would hit on day one.
func TestBaseDataLookupHidesPlaceholder(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if err := repo.CreateDomain(ctx, "地区", TypeValueOnly); err != nil {
		t.Fatal(err)
	}
	if _, err := repo.AddValue(ctx, "地区", "", "沈阳"); err != nil {
		t.Fatal(err)
	}

	all, err := repo.Values(ctx, "地区")
	if err != nil {
		t.Fatal(err)
	}
	if len(all) != 2 {
		t.Errorf("Values returned %d rows, want 2 (placeholder + 沈阳)", len(all))
	}

	lookup, err := repo.Lookup(ctx, "地区")
	if err != nil {
		t.Fatal(err)
	}
	if len(lookup) != 1 || lookup[0].Value != "沈阳" {
		t.Errorf("Lookup returned %+v, want only 沈阳", lookup)
	}
}

func TestBaseDataDuplicateDomainRejected(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if err := repo.CreateDomain(ctx, "地区", TypeValueOnly); err != nil {
		t.Fatal(err)
	}
	err := repo.CreateDomain(ctx, "地区", TypeKeyValue)
	if !errors.Is(err, ErrDuplicateDomain) {
		t.Fatalf("err = %v, want ErrDuplicateDomain", err)
	}
}

// A value inherits its domain's type rather than accepting one from the
// caller, and a type-1 domain stores "" for keyname whatever was submitted.
// This is the invariant the legacy schema cannot express and the legacy code
// never checked.
func TestBaseDataValueInheritsDomainType(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if err := repo.CreateDomain(ctx, "地区", TypeValueOnly); err != nil {
		t.Fatal(err)
	}
	if _, err := repo.AddValue(ctx, "地区", "IGNORED", "沈阳"); err != nil {
		t.Fatal(err)
	}

	values, err := repo.Values(ctx, "地区")
	if err != nil {
		t.Fatal(err)
	}
	for _, v := range values {
		if v.Type != TypeValueOnly {
			t.Errorf("row %d type = %d, want 1", v.UID, v.Type)
		}
		if v.KeyName != "" {
			t.Errorf("row %d keyname = %q, want empty on a type-1 domain", v.UID, v.KeyName)
		}
	}
}

func TestBaseDataAddValueToMissingDomain(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)

	_, err := repo.AddValue(context.Background(), "不存在的域", "", "x")
	if !errors.Is(err, ErrNotFound) {
		t.Fatalf("err = %v, want ErrNotFound", err)
	}
}

func TestBaseDataRenameDomainMovesEveryRow(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if err := repo.CreateDomain(ctx, "地区", TypeValueOnly); err != nil {
		t.Fatal(err)
	}
	for _, v := range []string{"沈阳", "大连", "鞍山"} {
		if _, err := repo.AddValue(ctx, "地区", "", v); err != nil {
			t.Fatal(err)
		}
	}

	if err := repo.RenameDomain(ctx, "地区", "销售区域"); err != nil {
		t.Fatalf("rename: %v", err)
	}
	if old, _ := repo.Values(ctx, "地区"); len(old) != 0 {
		t.Errorf("%d rows left under the old name", len(old))
	}
	if moved, _ := repo.Values(ctx, "销售区域"); len(moved) != 4 {
		t.Errorf("%d rows under the new name, want 4", len(moved))
	}
}

func TestBaseDataRenameOntoExistingNameRejected(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	for _, n := range []string{"地区", "行业"} {
		if err := repo.CreateDomain(ctx, n, TypeValueOnly); err != nil {
			t.Fatal(err)
		}
	}
	if err := repo.RenameDomain(ctx, "地区", "行业"); !errors.Is(err, ErrDuplicateDomain) {
		t.Fatalf("err = %v, want ErrDuplicateDomain", err)
	}
	// The refused rename must not have moved anything.
	if rows, _ := repo.Values(ctx, "地区"); len(rows) != 1 {
		t.Errorf("地区 has %d rows after a refused rename, want 1", len(rows))
	}
}

func TestBaseDataDeleteDomainRemovesEveryRow(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if err := repo.CreateDomain(ctx, "地区", TypeValueOnly); err != nil {
		t.Fatal(err)
	}
	for _, v := range []string{"沈阳", "大连"} {
		if _, err := repo.AddValue(ctx, "地区", "", v); err != nil {
			t.Fatal(err)
		}
	}
	if err := repo.DeleteDomain(ctx, "地区"); err != nil {
		t.Fatalf("delete: %v", err)
	}
	if rows, _ := repo.Values(ctx, "地区"); len(rows) != 0 {
		t.Errorf("%d rows survived the delete", len(rows))
	}
	if err := repo.DeleteDomain(ctx, "地区"); !errors.Is(err, ErrNotFound) {
		t.Errorf("second delete: err = %v, want ErrNotFound", err)
	}
}

func TestBaseDataUpdateValueRejectsStaleVersion(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if err := repo.CreateDomain(ctx, "地区", TypeValueOnly); err != nil {
		t.Fatal(err)
	}
	uid, err := repo.AddValue(ctx, "地区", "", "沈阳")
	if err != nil {
		t.Fatal(err)
	}

	if err := repo.UpdateValue(ctx, uid, 1, "", "沈阳市"); err != nil {
		t.Fatalf("first update: %v", err)
	}
	if err := repo.UpdateValue(ctx, uid, 1, "", "别的"); !errors.Is(err, ErrVersionConflict) {
		t.Fatalf("stale update: err = %v, want ErrVersionConflict", err)
	}

	values, _ := repo.Values(ctx, "地区")
	for _, v := range values {
		if v.UID == uid && v.Value != "沈阳市" {
			t.Errorf("value = %q — the stale write overwrote the newer row", v.Value)
		}
	}
}

// Import reproduces the legacy parse (FrmBaseData.cs:291-318) exactly:
// type 2 splits on the first colon; a line without one becomes a value with an
// empty keyname rather than being rejected. Operators have files in this
// format already, so a stricter parser would reject work that used to load.
func TestBaseDataImportKeyValue(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if err := repo.CreateDomain(ctx, "车系列", TypeKeyValue); err != nil {
		t.Fatal(err)
	}
	n, err := repo.ImportValues(ctx, "车系列", "A1:轿车系列\r\nB2:越野系列\n\n没有冒号的行\nC3:带:冒号的值\n")
	if err != nil {
		t.Fatalf("import: %v", err)
	}
	if n != 4 {
		t.Fatalf("imported %d, want 4 (blank lines skipped)", n)
	}

	got := map[string]string{}
	values, _ := repo.Values(ctx, "车系列")
	for _, v := range values {
		if v.Value != "" || v.KeyName != "" {
			got[v.KeyName] = v.Value
		}
	}
	want := map[string]string{
		"A1": "轿车系列",
		"B2": "越野系列",
		"":   "没有冒号的行",
		"C3": "带:冒号的值", // split on the FIRST colon only
	}
	for k, v := range want {
		if got[k] != v {
			t.Errorf("keyname %q = %q, want %q", k, got[k], v)
		}
	}
}

func TestBaseDataImportValueOnlyKeepsWholeLine(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if err := repo.CreateDomain(ctx, "地区", TypeValueOnly); err != nil {
		t.Fatal(err)
	}
	if _, err := repo.ImportValues(ctx, "地区", "沈阳:市区\n大连"); err != nil {
		t.Fatal(err)
	}

	lookup, _ := repo.Lookup(ctx, "地区")
	found := false
	for _, v := range lookup {
		if v.Value == "沈阳:市区" {
			found = true
		}
		if v.KeyName != "" {
			t.Errorf("type-1 row got keyname %q", v.KeyName)
		}
	}
	if !found {
		t.Error("a colon in a type-1 domain must stay part of the value, not become a key")
	}
}

// A failure partway through must leave nothing behind. The legacy import added
// rows to a client DataSet and relied on the user pressing save, so a partial
// import was the normal outcome.
func TestBaseDataImportIsAtomic(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if err := repo.CreateDomain(ctx, "地区", TypeValueOnly); err != nil {
		t.Fatal(err)
	}
	// value is VARCHAR(100); the long line fails mid-import under strict mode.
	content := "沈阳\n大连\n" + strings.Repeat("超", 200) + "\n鞍山"
	if _, err := repo.ImportValues(ctx, "地区", content); err == nil {
		t.Fatal("expected the over-long value to fail the import")
	}
	if lookup, _ := repo.Lookup(ctx, "地区"); len(lookup) != 0 {
		t.Errorf("%d rows survived a failed import; it must be all or nothing", len(lookup))
	}
}

func TestBaseDataImportRespectsRowLimit(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if err := repo.CreateDomain(ctx, "地区", TypeValueOnly); err != nil {
		t.Fatal(err)
	}
	content := strings.Repeat("行\n", MaxImportRows+1)
	if _, err := repo.ImportValues(ctx, "地区", content); !errors.Is(err, ErrTooManyRows) {
		t.Fatalf("err = %v, want ErrTooManyRows", err)
	}
}

// Legacy data can violate "type is constant within a name" because the schema
// cannot express it. That must be reported, not silently normalised: picking a
// winner would destroy the evidence of a real data problem.
func TestBaseDataReportsTypeConflicts(t *testing.T) {
	db := testDB(t)
	repo := freshBaseData(t, db)
	ctx := context.Background()

	if _, err := db.ExecContext(ctx, `
		INSERT INTO tbl_basedata (type, name, keyname, value) VALUES
			(1,'混乱域','','甲'), (2,'混乱域','B','乙'), (1,'正常域','','丙')`); err != nil {
		t.Fatal(err)
	}

	conflicts, err := repo.DomainTypeConflicts(ctx)
	if err != nil {
		t.Fatal(err)
	}
	if len(conflicts) != 1 || conflicts[0] != "混乱域" {
		t.Errorf("conflicts = %v, want [混乱域]", conflicts)
	}

	// MIN(type), so the reported type is deterministic rather than dependent
	// on row order.
	domains, _ := repo.Domains(ctx)
	for _, d := range domains {
		if d.Name == "混乱域" && d.Type != 1 {
			t.Errorf("混乱域 type = %d, want the deterministic MIN of 1", d.Type)
		}
	}
}
