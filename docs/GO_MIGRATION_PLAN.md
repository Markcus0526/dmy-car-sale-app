# CarSaleMan → Go + MySQL Migration Plan

**Target:** Go HTTP API + web frontend, MySQL backend.
**Source:** C# / .NET Framework 4.0 / WinForms MDI / ComponentOne / SQL Server 2005.

Companion artefacts:
- **[docs/DEVELOPMENT_PLAN.md](DEVELOPMENT_PLAN.md) — the execution plan: locked decisions, the
  161-session day-by-day schedule, gates and exit criteria. Work from that one daily; this
  document is the analysis behind it.**
- [docs/mysql/schema.sql](mysql/schema.sql) — generated MySQL DDL + the 28 proc / 5 view contracts
- [docs/mssql-export.sql](mssql-export.sql) — Phase 0 extraction script

**Decisions since this analysis was written** (detail in the development plan): backend Go,
frontend TypeScript + React, bilingual UI (English/Chinese) with the 11 formal reports staying
Chinese-only, and the web UI built per-slice rather than after the API.

---

## Table of contents

1. [Inventory](#1-inventory)
2. [How the current system is built](#2-how-the-current-system-is-built)
3. [Target architecture](#3-target-architecture)
4. [API surface](#4-api-surface)
5. [Schema conversion](#5-schema-conversion-mssql--mysql)
6. [Business logic to port](#6-business-logic-to-port)
7. [Reports catalogue](#7-reports-catalogue)
8. [Phased plan](#8-phased-plan)
9. [Testing strategy](#9-testing-strategy)
10. [Defects and behaviour decisions found during analysis](#10-defects-and-behaviour-decisions-found-during-analysis)
11. [Risks](#11-risks)
12. [Effort](#12-effort)

---

## 1. Inventory

| Metric | Value |
|---|---|
| C# source | 133 files, ~101k lines |
| …generated typed-DataSet | `CmsDB.Designer.cs` = **55,352 lines** (55%) — discard entirely |
| …WinForms layout | 64 `*.Designer.cs` + 64 `.resx` — discard, replaced by web UI |
| **Hand-written logic to port** | **~28k lines across 63 forms** |
| Base tables | 15 (`tbl_*`) |
| Views | 5 (`vw_*`) |
| Stored procedures | **28** (`stor_*`) — all reporting/statistics/chart logic |
| Reports | 11 (programmatic C1Preview `RenderTable`) |
| Charts | 5 (C1Chart) |
| Statistics screens | 7 |
| 3rd-party UI deps | C1FlexGrid, C1Chart, C1Report/C1Preview, C1Excel, C1Pdf, C1List, C1InputPanel |
| Source encoding | **Mixed** — GB18030 *and* UTF-8-with-BOM (corrected, see §11.7) |
| UI strings | **1,156 literals in `.cs` source**, across 123 of 130 files — *not* in the `.resx` files (corrected, see §11.7) |

### 1.1 Complete form inventory

Sorted by size. `→ target` is the web screen or module it becomes.

| Form | Lines | → target |
|---|---:|---|
| `FrmStoreIn` | 909 | store-in screen (dual grid: on-road + stored) |
| `FrmStatisCarSaleQuarter` | 898 | quarterly-target editable grid **(own slice)** |
| `FrmStoreOut` | 867 | store-out screen (dual grid) |
| `FrmFinanceStore` | 671 | finance inventory screen + interest engine |
| `FrmMDIMain` | 670 | app shell / nav / permission gating |
| `FrmSearchStorein` | 624 | store-in query screen |
| `FrmSearchOnroadCar` | 624 | on-road query screen |
| `FrmStatisRemainAmount` | 620 | remaining-amount statistics |
| `FrmSearchStoreout` | 550 | store-out query screen |
| `FrmOnRoadCar` | 542 | on-road vehicles list |
| `FrmSearch` | 491 | **shared** filter-builder component |
| `FrmStatisCartypeColor` | 473 | car-type/colour statistics |
| `FrmStatisHandler` | 467 | handler statistics |
| `FrmStoreInAddMan` | 463 | manual batch store-in |
| `FrmUserPermission` | 455 | permission matrix admin |
| `FrmStatisSaleCartype` | 442 | sales-by-car-type statistics |
| `FrmStatisCarSaleRegion` | 442 | sales-by-region statistics |
| `FrmStatisCustsJob` | 440 | customer-occupation statistics |
| `FrmStoreChange` | 418 | stock-transfer screen |
| `FrmBaseData` | 409 | reference-data admin |
| `FrmSpecCar` | 382 | special vehicles list |
| `FrmFinanceParam` | 346 | finance parameters form |
| `FrmChartSaleRegion` … `FrmChartBuyTotal` | 327 ×5 | 5 chart endpoints + client render |
| `FrmCarType` / `FrmCarCompany` | 321 ×2 | car-type / company admin lists |
| `FrmOnRoadCarEdit` | 312 | on-road modal |
| `FrmStoreOutAdd` | 302 | store-out create modal |
| `FrmStoreInAdd` | 273 | store-in create modal |
| `FrmStoreChangeEdit` | 271 | transfer modal |
| `FrmReport*` × 11 | 196–260 | `internal/report` (see §7) |
| `FrmImportExcel` | 229 | on-road Excel import |
| `FrmAppendRepair` / `FrmAppendRepairEdit` | 221 / 194 | repair orders |
| `FrmCarTypeEdit` | 214 | car-type modal |
| `FrmUserPermissionEdit` | 211 | permission modal |
| `FrmLogon` | 207 | login |
| `FrmCarCompanyEdit` | 207 | company modal |
| `FrmMessage` | 201 | toast/dialog component — **no port needed** |
| `FrmSpecJournal` | 181 | service journal |
| `FrmStoreOutEdit` | 179 | store-out modal |
| `FrmPassChange` | 176 | change password |
| `FrmStoreInEdit` | 159 | store-in modal |
| `FrmSpecCarEdit` | 138 | special-vehicle modal |
| `FrmBaseDataEdit` | 124 | reference-data modal |
| `FrmEnvSet` | 108 | **drop** — DB connection config becomes server env |
| `FrmSplash` | 106 | **drop** |
| `FrmActionHis` | 75 | audit-trail viewer (`tbl_log`) |
| `FrmDateSetting` | 69 | date-range picker component |
| `FrmBaseDataImport` | 58 | reference-data import |

**63 forms → ~20 screens.** ~25 are `*Add`/`*Edit` modal variants of a parent list and
collapse into modals over a shared list component; `FrmMessage`, `FrmSplash`, `FrmEnvSet`
disappear entirely.

---

## 2. How the current system is built

```
WinForms MDI (FrmMDIMain)
   └── 63 forms, each holding its own TableAdapter instances
          └── CmsDB typed DataSet (in-memory client-side cache)
                 └── SqlClient → SQL Server "csm"
                        ├── 15 tables (CRUD via generated adapters)
                        ├── 5 views (read models with carseries joined in)
                        └── 28 stored procs (ALL report/statistic/chart math)
```

Seven structural facts that shape the migration:

**2.1 There is no service or repository layer.** Business logic lives directly in form
event handlers. `DBProvider` in
[CommonMisc.cs:277](../CarSaleMan/CarSaleMan/CommonMisc.cs#L277) is only a
connection-string holder — it never executes a query. `Global` is a bag of mutable static
state (`LOGIN_USERID`, `PERMISSION_LIST`, `MENU_LIST`), which is exactly what a
per-request context replaces.

**2.2 All data access goes through generated TableAdapters.** Grepping for
`SqlCommand`/`SqlConnection`/`SqlDataAdapter` outside `CmsDB.Designer.cs` returns **zero
hits**. Every SQL statement the app can issue is therefore recoverable from `CmsDB.xsd`
(130 `CommandText` blocks). There are no hidden query strings. This is the single biggest
piece of good news in the whole analysis.

**2.3 Reports are code, not report definitions.** Each `FrmReport*.cs` assembles a
`RenderTable` cell-by-cell — headers, column stretching, group row-spanning, subtotal and
grand-total rows — see
[FrmReportStoreDetail.cs:57-120](../CarSaleMan/CarSaleMan/FrmReportStoreDetail.cs#L57-L120).
No `.rpx`/`.xml` layouts exist to reverse-engineer. The layout *is* readable C# we can
re-express directly.

**2.4 The DataSet is used as a client-side query engine.** Forms `SELECT *` an entire
table, then filter in memory:

```csharp
// FrmLogon.cs:72 — loads every user row, then filters client-side
DataRow[] rows = cmsDB.tbl_userinfo.Select("username = '" + cbUsername.Text + "'");
```

In Go these become real parameterised `WHERE` clauses. This *removes* a scaling problem
rather than porting one — but note the string concatenation, which the port must not
reproduce (§10.6).

**2.5 Concurrency control is optimistic and whole-row.** Every generated `UPDATE`/`DELETE`
compares *all* original column values plus `@IsNull_*` flags:

```sql
DELETE FROM [dbo].[tbl_storechange]
WHERE (([uid] = @Original_uid) AND ([changeid] = @Original_changeid)
   AND ([batchno] = @Original_batchno) AND ... 12 more columns ...
   AND ((@IsNull_remark = 1 AND [remark] IS NULL) OR ([remark] = @Original_remark)))
```

Reproduce this deliberately or lost writes appear silently (§11.4).

**2.6 `FrmSearch` is a shared filter-builder used by 10 screens.** It builds a DataSet
filter expression and dispatches via a `searchKind` discriminator
(`Global.SEARCH_*`, 14 constants at
[CommonMisc.cs:446-460](../CarSaleMan/CarSaleMan/CommonMisc.cs#L446-L460)) into each
caller's `SearchByCondition(int searchKind, string cond)`. Port it **once** as a
structured filter DTO + a query builder, not ten times.

**2.7 Multi-table movements are not transactional.** Store-in/out/change each mutate
`tbl_onroad`, `tbl_storein`/`tbl_storeout` and `tbl_storechange` through *separate*
adapter `Update()` calls (see
[FrmStoreInAddMan.cs:318-395](../CarSaleMan/CarSaleMan/FrmStoreInAddMan.cs#L318-L395)).
A failure mid-sequence leaves inconsistent state today.

---

## 3. Target architecture

```
web/                        React SPA (or Go templates + HTMX)
  │  REST/JSON
cmd/carsaleman/main.go      wiring, config, graceful shutdown
cmd/migrate-data/main.go    one-shot MSSQL → MySQL data migration
internal/
  http/
    router.go               chi routes
    middleware/             auth, permission, request-id, recover, logging
    handler_*.go            one file per resource
    dto/                    request/response types (decimal-as-string)
  auth/
    session.go              login, session/JWT issue+verify
    password.go             bcrypt + legacy-DES verification path (§6.5)
    permission.go           replaces Permission.GetFuncPermission / SetMenuPermission
  domain/                   pure Go, no SQL, unit-testable
    onroad/  storein/  storechange/  storeout/  speccar/  repair/
    cartype/ carcompany/ basedata/ user/
    finance/                interest engine (§6.1) — highest-value unit tests
    quarterstats/           quarterly target math (§6.3)
    movement/               the shared store-in/out/change transaction (§6.2)
  query/
    filter.go               structured filter DTO replacing FrmSearch (§2.6)
  store/mysql/
    *_repo.go               one file per aggregate
    queries.sql             sqlc source; all SQL lives here, versioned
  stats/                    the 28 procs' logic + golden-file tests (§8, Phase 2)
  report/
    render.go               shared table/group/subtotal renderer
    report_*.go             11 reports, each (data) → []byte
    excel.go                excelize export
  platform/                 config, logging, errors, decimal helpers
migrations/                 golang-migrate .up/.down
testdata/golden/            per-report/per-proc expected output
```

### 3.1 Dependency choices

| Concern | Library | Note |
|---|---|---|
| MySQL driver | `go-sql-driver/mysql` | `parseTime=true&loc=Asia/Shanghai&charset=utf8mb4` |
| Query layer | `sqlc` (fallback `sqlx`) | `sqlc` fits: the SQL is already known and static |
| Migrations | `golang-migrate/migrate` | |
| Router | `go-chi/chi/v5` | |
| Decimal | `shopspring/decimal` | storage + sums; see §11.2 for the interest-calc nuance |
| Excel | `xuri/excelize/v2` | `.xlsx` only — see §11.6 |
| PDF | `go-pdf/fpdf`, or HTML→PDF via headless Chrome | HTML→PDF reuses the web table styling |
| Charts | Recharts / ECharts, client-side | server returns JSON only |
| Passwords | `x/crypto/bcrypt` | replaces DES (§6.5) |
| Chinese collation | `x/text/collate` | for `zh` display ordering (§5.4) |

### 3.2 Why API + web for this app specifically

The README states multi-user concurrent access, and today every desktop client connects
straight to SQL Server with the `sa` password shipped in
[app.config](../CarSaleMan/CarSaleMan/app.config). Putting the database behind a server
isn't architectural taste here — it's what allows the `sa` credential to stop being
distributed to every workstation, and it's where the missing transactions (§2.7) and
server-side filtering (§2.4) naturally land.

---

## 4. API surface

Roughly 20 resources. Permission names are the **Chinese menu labels** already stored in
`tbl_permission.fieldname` — keep them as the authorization keys so existing permission
rows migrate unchanged (§6.4).

```
POST   /api/auth/login                  → session; loads permissions
POST   /api/auth/logout
POST   /api/auth/password               change own password
GET    /api/auth/me                     user + permission map (drives nav gating)

GET    /api/departments                 vw_department

CRUD   /api/cartypes                    tbl_cartype   (soft delete: deleted=1)
CRUD   /api/carcompanies                tbl_carcompany
CRUD   /api/basedata                    tbl_basedata  (?type=&name=)
CRUD   /api/users                       tbl_userinfo
CRUD   /api/users/{id}/permissions      tbl_permission

CRUD   /api/onroad                      vw_onroad / tbl_onroad
POST   /api/onroad/import               Excel import (§11.6)

GET    /api/storein                     vw_storein
POST   /api/storein                     movement txn: onroad.inflag=1 + insert (§6.2)
POST   /api/storein/batch               FrmStoreInAddMan
PATCH  /api/storein/{id}
DELETE /api/storein/{id}                movement txn: reverses inflag

CRUD   /api/storechange                 tbl_storechange
CRUD   /api/storeout                    vw_storeout; POST/DELETE are movement txns
CRUD   /api/speccar                     vw_speccar
CRUD   /api/repairs                     append-repair orders
CRUD   /api/journal                     tbl_fit (service journal)

GET    /api/finance/store               interest engine output (§6.1)
GET   /PUT /api/finance/params          tbl_env finance keys (§5.5)

GET    /api/quarterstats?year=&quarter= tbl_quarterstats
PUT    /api/quarterstats                editable grid write-back (§6.3)

GET    /api/stats/{name}?startdate=&enddate=      7 statistics screens
GET    /api/charts/{name}?startdate=&enddate=     5 charts → JSON
GET    /api/reports/{name}?startdate=&enddate=&format=pdf|xlsx|json   11 reports

GET    /api/log                         tbl_log audit trail
```

**Cross-cutting conventions**

- All list endpoints accept the structured filter DTO from §2.6 (`field`, `op`, `value`,
  `conjunction`) — never a raw expression string.
- All decimals serialize as JSON **strings** (§11.2).
- Writes to `tbl_*` carry `row_version`; mismatch → `409 Conflict` (§11.4).
- Every mutation writes `tbl_log` (the audit trail the README claims).

---

## 5. Schema conversion (MSSQL → MySQL)

Full generated DDL: **[docs/mysql/schema.sql](mysql/schema.sql)** (757 lines).

### 5.1 Type mapping (as it actually applies here)

| SQL Server (from `CmsDB.xsd`) | MySQL | Occurrences |
|---|---|---|
| `int IDENTITY(1,1)` | `INT NOT NULL AUTO_INCREMENT` | 15 (every PK) |
| `nvarchar(N)` | `VARCHAR(N)` utf8mb4 | 370 params |
| `decimal(10,2)` | `DECIMAL(10,2)` | 140 params |
| `decimal(10,3)` | `DECIMAL(10,3)` | `outstoreprice`, `profitval` |
| `datetime` | `DATETIME` | 84 params |
| `smallint` | `SMALLINT` | `companyno`, `inflag`, `inkind`, `outflag`, `deleted` |
| `tinyint` | `TINYINT UNSIGNED` | `tbl_quarterstats.type` |

Every string column is **NVARCHAR** → the whole database is `utf8mb4` /
`utf8mb4_unicode_ci`. No `latin1` anywhere.

### 5.2 Statements needing rewrite

| MSSQL | Occurrences | MySQL |
|---|---:|---|
| `SCOPE_IDENTITY()` | 13 | driver `LastInsertId()` — drops the re-`SELECT` round-trip |
| `[dbo].[tbl_x]` | all DML | `` `tbl_x` `` — strip the `dbo.` prefix |
| `@param` | all | `?` positional |
| `SELECT TOP n` | 0 | none present |
| `GETDATE`/`DATEDIFF`/`ISNULL` | 0 in app SQL | **expect them inside the 28 procs** → `NOW()`/`DATEDIFF()`/`IFNULL()` |

Note the argument-order trap: T-SQL `DATEDIFF(day, a, b)` = `b - a`; MySQL
`DATEDIFF(a, b)` = `a - b`. Every occurrence inside the dumped procs must be re-checked
individually — this is the most likely source of silent off-by-sign errors in Phase 2.

### 5.3 Foreign keys

Only four are declared; a fifth is implied by the `vw_*` joins but never enforced:

```
tbl_permission.userinfoid  → tbl_userinfo.uid
tbl_onroad.cartypeid       → tbl_cartype.uid
tbl_storein.onroadid       → tbl_onroad.uid
tbl_storeout.onroadid      → tbl_onroad.uid
tbl_storechange.onroadid   → tbl_onroad.uid   (implied, not declared today)
```

Validate for orphans *before* adding the constraints — years of unconstrained operation
make dangling references likely. `cmd/migrate-data` should report orphan counts per FK and
refuse to proceed silently.

### 5.4 Collation / sort-order caveat

The app orders Chinese text (`ORDER BY carseries`, `ORDER BY companyno`) under
`Chinese_PRC_CI_AS`. `utf8mb4_unicode_ci` will **not** reproduce that sequence. Where
display order is visible (dropdowns, report grouping, `carseries` grouping in every
report), sort in Go with `x/text/collate` for `zh`, or add an explicit sort-key column.
Decide once, centrally, in Phase 1.

### 5.5 `tbl_env` — the finance parameter store

Eight keys, all read as strings and converted at use site
([FrmFinanceParam.cs:71-94](../CarSaleMan/CarSaleMan/FrmFinanceParam.cs#L71-L94)):

| key | type | meaning |
|---|---|---|
| `nointerestdates` | int | interest-free days from bill date |
| `extenddates` | int | extension-period length (days), applied twice |
| `interestrate` | decimal | base annual rate (%) |
| `extendrate` | decimal | extension annual rate (%) |
| `nointerest5color` | int (ARGB) | grid highlight, ≤5 days to interest |
| `nointerest10color` | int (ARGB) | grid highlight, 6–10 days |
| `extend5color` | int (ARGB) | grid highlight, ≤5 days to extension end |
| `extend10color` | int (ARGB) | grid highlight, 6–10 days |

Model these as a typed `finance.Params` struct with validation on write. The four colour
keys are presentation concerns — move them to frontend config and keep the *thresholds*
(5/10 days) in the API response as a severity enum rather than shipping ARGB integers to a
browser.

### 5.6 `tbl_basedata` — 17 reference-data domains

`tbl_basedata(type, name, keyname, value)` is a generic key-value store backing every
dropdown. The `name` values in use, recovered from binding-source filters across the forms:

| `name` | domain |
|---|---|
| `车系列` | car series |
| `车型大类` | car-type major category |
| `进货途径` | purchase channel |
| `进车状态` | inbound vehicle state |
| `状态名称` | state name |
| `库位` | storage location |
| `地区` | region |
| `行业` | customer industry/occupation |
| `销售方式` | sales method |
| `销售顾问` | sales consultant |
| `经手人` | handler |
| `批复人` | approver |
| `结算方式` (`资金情况`) | settlement / funding status |
| `返利状态` | rebate status |
| `特种车类型` | special-vehicle type |
| `是否上报` / `是否开票` / `是否付款` / `是否提车` | yes/no flags: reported, invoiced, paid, collected |

`type` is `1` or `2` (branching at
[FrmBaseData.cs:93-98](../CarSaleMan/CarSaleMan/FrmBaseData.cs#L93-L98)) — confirm the
distinction against live data during Phase 0; `CmsDB.xsd` doesn't record it.

The four `是否*` domains are booleans stored as Chinese strings in `varchar(50)` columns
(`isbill`, `ispayment`, `issend`, `isreport`). Keep them as strings through the migration
to preserve report equality, and normalise only in a later, separate change.

---

## 6. Business logic to port

This is the part that no tool can mechanically translate. Each subsection is a
`internal/domain` package with unit tests.

### 6.1 Finance interest engine — `domain/finance` *(highest complexity)*

`stor_finance_store` returns base columns; the client then **recomputes and overwrites**
`nointerestdate`, `noextenddate`, `noallmoneydate`, `interestdates`,
`distnointerestdates`, `distextenddates`, `distallmoney`, `interestrate`, `totalinterest`
per row ([FrmFinanceStore.cs:139-226](../CarSaleMan/CarSaleMan/FrmFinanceStore.cs#L139-L226)).
This moves server-side. The algorithm, exactly as it stands today:

```
DAYS_PER_YEAR = 360                       // banker's year, hardcoded

nointerestdate  = billdate + nointerestdates
noextenddate    = nointerestdate + extenddates
noallmoneydate  = noextenddate  + extenddates      // extenddates applied twice

goodprice       = inprice * 0.9                    // hardcoded, NOT in tbl_env
goodremainprice = inprice * 0.68                   // hardcoded, NOT in tbl_env

interestdates        = today - nointerestdate      // days past interest-free end
distnointerestdates  = nointerestdate - today
distextenddates      = today - noextenddate
distallmoney         = today - noallmoneydate

if interestdates > 0:
    if interestdates <= nointerestdates:
        totalinterest = goodprice * (interestrate/100) / 360 * interestdates
        rate = interestrate
    elif distextenddates > 0:
        totalinterest = goodprice       * (interestrate/100) / 360 * nointerestdates
                      + goodremainprice * (extendrate/100)   / 360 * distextenddates
        rate = extendrate
    # else: falls through with totalinterest = 0  ← see §10.2
else:
    totalinterest = 0

totalinterest = max(totalinterest, 0)
```

Three things to carry across deliberately:

- **Stored sign convention.** The output columns are negated *and* the two `*nointerest*`
  values are cross-assigned: column `interestdates` receives `-distnointerestdates`, and
  column `distnointerestdates` receives `-interestdates`
  ([lines 195-196](../CarSaleMan/CarSaleMan/FrmFinanceStore.cs#L195-L196)). Preserve it
  exactly, or every finance figure flips sign. Add a comment in the Go code pointing here.
- **`0.9` and `0.68` are magic numbers**, not configurable. Lift them into
  `finance.Params` with those defaults so they become tunable without another rewrite.
- **The legacy math is `double`, not `decimal`.** See §11.2 — this determines whether the
  Go port can be bit-equal to the old numbers.

### 6.2 Vehicle movement transaction — `domain/movement` *(highest business value)*

The lifecycle is a state machine over `tbl_onroad.inflag` and `tbl_storein.outflag`,
labelled by four constants at
[CommonMisc.cs:438-443](../CarSaleMan/CarSaleMan/CommonMisc.cs#L438-L443):

```
车辆采购 purchase → tbl_onroad (inflag=0)
车辆入库 store-in → tbl_storein + onroad.inflag=1
车辆转库 transfer → tbl_storechange (changeid groups a transfer batch)
车辆出库 store-out→ tbl_storeout + storein.outflag=1
```

Today each transition is 2–3 independent adapter `Update()` calls with **no transaction**
([FrmStoreInAddMan.cs:318-395](../CarSaleMan/CarSaleMan/FrmStoreInAddMan.cs#L318-L395)) —
a crash between them orphans a vehicle in a half-moved state.

**Port each transition as one `movement` service method inside a single MySQL
transaction**, with the flag update guarded (`WHERE uid=? AND inflag=0`) so a
double-submit fails loudly instead of double-storing. This is the one place the port
should intentionally *not* be faithful.

### 6.3 Quarterly targets — `domain/quarterstats`

`FrmStatisCarSaleQuarter` (898 lines) is an editable grid over `tbl_quarterstats`, split
into "general" and "special" car-type blocks (`gencount` / `speccount`), each with a
subtotal row. `CalculateAmounts()`
([lines 244-300+](../CarSaleMan/CarSaleMan/FrmStatisCarSaleQuarter.cs#L244-L300)):

- `col5 = col2 + col3 + col4` per row (three-month sum → quarter total)
- column-wise sums `a1..a7` into the block subtotal row
- achievement `%` = `sum / col1 * 100`, formatted `"0.00%"`, with an explicit
  divide-by-zero guard yielding `"0.00%"`
- the same computation runs twice, once per block, with a shifted `colbase`

Writes go back to `tbl_quarterstats` columns `m1..m12`, `total1..4`, `remain1..4`, keyed
by `(year, quarter)` — `SaveChange(oldYear, oldQuarter)` fires on year/quarter switch,
which is where the current code is most likely to lose edits.

Port the arithmetic as a pure function over a typed grid, unit-test the percentage and
zero cases, and make the API a whole-quarter `PUT` so partial-save races disappear.

### 6.4 Permissions — `auth/permission`

Two-level, string-keyed by **Chinese menu label**:

- `Permission.SetMenuPermission(menu)`
  ([CommonMisc.cs:807](../CarSaleMan/CarSaleMan/CommonMisc.cs#L807)) walks the MenuStrip,
  strips any `"(...)"` suffix from each label, registers it in `Global.MENU_LIST`, and
  enables/disables the item by looking up `Global.PERMISSION_LIST[label]`. Absent key →
  disabled (deny-by-default — keep that).
- `Permission.GetFuncPermission(func)` returns `true` only when the value is `读写`
  (read-write); `不可用` (unavailable) disables the menu entry.

So permission values are the literal strings `读写` / `只读` / `不可用`. `tbl_permission`
rows are `(userinfoid, fieldname=menu label, permission=one of those strings)`.

Keep the Chinese labels as authorization keys so existing rows migrate with no data
transformation, but define them as a Go `const` enum in one file rather than comparing
inline literals in 63 places. Enforce on the **server** (middleware per route) — the
current implementation only greys out menu items, which is not a security boundary.

### 6.5 Password migration — `auth/password`

Today: DES-CBC, key = first 8 chars of the hardcoded `Global.STR_DES_KEY` (`"123456AB"`),
fixed IV, Base64 — i.e. **reversible encryption, not hashing**
([CommonMisc.cs:526-564](../CarSaleMan/CarSaleMan/CommonMisc.cs#L526-L564)). Login compares
ciphertext strings ([FrmLogon.cs:70-76](../CarSaleMan/CarSaleMan/FrmLogon.cs#L70-L76)).

Migration path:

1. Add `password_bcrypt VARCHAR(60) NULL`; keep the legacy `password` column.
2. On login: if `password_bcrypt` is set, verify with bcrypt. Otherwise DES-verify against
   `password`, and on success write `password_bcrypt` and null out `password`.
3. After one release, drop the DES path and force-reset any account still unmigrated.

Go's stdlib has DES in `crypto/des` for step 2 — no third-party dependency needed.

### 6.6 Batch numbering — `domain/movement`

`batchno` is generated client-side as
`"R" + DateTime.Now.ToString("yyyyMMddhhmmss")`
([FrmStoreInAddMan.cs:425-437](../CarSaleMan/CarSaleMan/FrmStoreInAddMan.cs#L425-L437)).

Two defects to fix rather than port: `hh` is **12-hour** format in .NET, so 09:00 and
21:00 collide; and generation is per-client with no uniqueness check, so two users in the
same second collide. Generate server-side with `HH` and a `UNIQUE` index on `batchno`.

### 6.7 Environment/config

`CommonMisc.ReadEnvironment`/`WriteEnvironment` persist server address, DES-encrypted DB
password, username and an auto-logon flag to a local `CSMEnv.ini` XML file. In the target
architecture this whole mechanism disappears: DB credentials become server-side env vars,
and `FrmEnvSet`/`FrmSplash` are dropped.

---

## 7. Reports catalogue

All 11 are programmatic `RenderTable` builds with `carseries` group headers, row-spanning
group cells, per-group subtotals and a grand total. Titles are the exact Chinese strings
in the source.

| # | Form | Title | Cols | Proc | Params |
|---:|---|---|---:|---|---|
| 1 | `FrmReportStoreDetail` | 到货明细报表 | 10 | `stor_report_storeindetail` | date range |
| 2 | `FrmReportStoreCarTypeDetail` | 分销明细报表 | 10 | `stor_report_storeintypetotalperiod` | date range |
| 3 | `FrmReportOnroadDetail` | 在途/未提车辆明细报表 | 10 | `stor_report_onroaddetail` | date range |
| 4 | `FrmReportProfitDetail` | 进货返利报表 | 11 | `stor_report_profitdetail` | date range |
| 5 | `FrmReportStoreTotal` | 总库存明细报表 | 10 | `stor_report_storeintotal` | none |
| 6 | `FrmReportStoreCartypeTotal` | 库存车型颜色统计报表 | 6 | `stor_report_storeintypetotal` | none |
| 7 | `FrmReportReserveSale` | 预售明细报表 | 17 | `stor_report_storeoutreserve` | none |
| 8 | `FrmReportSaleTotal` | 总销售明细报表 | 17 | `stor_report_storeouttotal` | none |
| 9 | `FrmReportSaleCountTotal` | 销售数量汇总报表 | 9 | `stor_report_storeoutcount` | date range |
| 10 | `FrmReportWholeSaleTotal` | 销售批发零售汇总报表 | 12 | `stor_report_wholesaletotal` | date range |
| 11 | `FrmReportCarseriesTotal` | 进销存汇总报表 | 9 | `stor_report_carseriestotal` + `…carseriesdetail` | companyno + date range |

Report 11 is the only one driving **two** procs (16 TableAdapter references vs 8 in the
others) — a totals row plus a per-car-type detail expansion. Budget it as the hardest.

**Build one shared renderer.** All 11 share: title block, date-range subtitle
(`从：… - 到：…`), header row, `carseries` group column with `SpanRows`, subtotal rows,
grand total, and `AutoSizeSpecificCols`. Extract that once into `report/render.go`; each
report then declares columns, group key and aggregates as data. This turns ~2,500 lines of
repetitive C# into roughly one renderer plus 11 short declarations.

---

## 8. Phased plan

Phases 0–2 are strictly sequential. Phases 3–4 parallelise by slice once 0–2 land.

### Phase 0 — Recover ground truth *(hard gate)*

Run [docs/mssql-export.sql](mssql-export.sql) against the live `csm` database. It dumps,
in 10 steps: proc bodies, view definitions, functions/triggers, authoritative
`INFORMATION_SCHEMA` columns (incl. defaults and identity/computed flags), computed-column
expressions, PK/unique/index definitions, foreign keys, default/check constraints, per-table
row counts, and the database collation.

Then reconcile against [mysql/schema.sql](mysql/schema.sql), which is *inferred* from
`CmsDB.xsd` and cannot see defaults, indexes, checks, triggers or collation.

**Exit criteria:** step 1 returns 28 rows, step 2 returns 5, and the reconciled DDL is
committed. Fewer rows means procs/views were dropped from that instance and another source
is needed.

### Phase 1 — Foundation

1. `go mod init`; layout per §3; config from env — the hardcoded `sa` password goes.
2. Convert all 133 sources GB18030 → UTF-8 into a reference tree so Chinese literals are
   readable during the port (working copy only; the C# app is untouched).
3. `migrations/0001_init.up.sql` from the reconciled schema. **Decide `row_version` here**
   (§11.4) — it changes every table's DDL.
4. `cmd/migrate-data`: MSSQL → MySQL, table-by-table, with per-table row counts, decimal
   sum checksums, and an orphan report per FK (§5.3). Load order:
   `tbl_cartype`, `tbl_userinfo`, `tbl_carcompany`, `tbl_basedata`, `tbl_env` →
   `tbl_onroad` → `tbl_storein`, `tbl_storeout`, `tbl_storechange` → remainder.
5. CI: build, `go vet`, `-race` tests, MySQL service container, `golangci-lint`.
6. Decide the Chinese collation strategy (§5.4) once, centrally.

### Phase 2 — Translate views and procedures

1. **Views** — `vw_onroad`, `vw_storein`, `vw_storeout`, `vw_speccar`, `vw_department`.
   Keep them as MySQL views so read models stay column-for-column identical to today
   (exact column lists are in [schema.sql](mysql/schema.sql) §3). `vw_storeout` has a
   column literally named `Expr1` — preserve the name; rename only once nothing reads it.
2. **Procedures** — translate each `stor_*` into plain SQL in `internal/stats/queries.sql`
   rather than MySQL stored procedures: versioned with the code, testable, and it avoids
   MySQL's procedural dialect entirely. Reserve actual procs for genuinely imperative
   bodies, if the dump reveals any.
   Watch `DATEDIFF` argument order (§5.2) on every occurrence.
3. **Golden-file equivalence tests** — for each of the 28, run the old proc on MSSQL and
   the new query on migrated MySQL over identical parameters; assert identical result
   sets. This is what makes "the reports still match" a fact rather than a hope (§9).

Signatures recovered from `CmsDB.xsd`:

| Params | Procedures |
|---|---|
| none (9) | `stor_statis_onroad`, `stor_finance_store`, `stor_statis_storein`, `stor_statis_storeout`, `stor_statis_remainamount`, `stor_report_storeouttotal`, `stor_report_storeintypetotal`, `stor_report_storeintotal`, `stor_report_storeoutreserve` |
| `@startdate`,`@enddate` (17) | `stor_storeout_region`, `stor_storeout_custsjob`, `stor_statis_cartypecolor`, `stor_statis_handler`, `stor_statis_salecartype`, `stor_report_onroaddetail`, `stor_report_storeindetail`, `stor_report_storeintypetotalperiod`, `stor_report_profitdetail`, `stor_report_storeoutcount`, `stor_report_wholesaletotal`, `stor_chart_salecartype`, `stor_chart_salekindcount`, `stor_chart_salecount`, `stor_chart_saleregion`, `stor_chart_salecustsjob` |
| `@companyno`,`@startdate`,`@enddate` (2) | `stor_report_carseriesdetail`, `stor_report_carseriestotal` |

### Phase 3 — Domain + CRUD

Each slice = domain package + repository + REST handlers + tests, in dependency order.

| # | Slice | Source forms | ~Lines | Notes |
|---:|---|---|---:|---|
| 1 | auth, users, permissions | `FrmLogon`, `FrmUserPermission*`, `FrmPassChange` | 1,050 | §6.4, §6.5; server-side enforcement |
| 2 | reference data | `FrmBaseData*`, `FrmCarType*`, `FrmCarCompany*`, `FrmBaseDataImport` | 1,450 | §5.6; 17 domains |
| 3 | shared query filter | `FrmSearch` | 490 | §2.6 — **do this before slices 4-8** |
| 4 | on-road vehicles | `FrmOnRoadCar*`, `FrmImportExcel` | 1,080 | §11.6 Excel |
| 5 | store-in | `FrmStoreIn`, `FrmStoreInAdd`, `FrmStoreInAddMan`, `FrmStoreInEdit` | 1,800 | §6.2, §6.6 |
| 6 | store-change | `FrmStoreChange*` | 690 | §6.2 |
| 7 | store-out | `FrmStoreOut`, `FrmStoreOutAdd`, `FrmStoreOutEdit` | 1,350 | §6.2 |
| 8 | special cars, repair, journal | `FrmSpecCar*`, `FrmAppendRepair*`, `FrmSpecJournal` | 900 | |
| 9 | finance | `FrmFinanceStore`, `FrmFinanceParam` | 1,020 | §6.1 — most test-heavy |
| 10 | audit trail | `FrmActionHis` | 75 | write path added across all slices |

Slice 3 gates 4–8: porting `FrmSearch` once avoids re-implementing filter logic in five
list screens.

### Phase 4 — Statistics, reports, charts

1. `report/render.go` — the shared renderer (§7). Build this first; it's the leverage point.
2. 11 reports as declarations over that renderer, each with a golden-file test.
3. 7 statistics endpoints (`FrmStatis*`) — mostly thin wrappers over Phase 2 queries,
   except `FrmStatisCarSaleQuarter` (§6.3) and `FrmStatisRemainAmount` (620 lines,
   percentage columns computed client-side today).
4. 5 chart endpoints returning JSON; rendering moves to the browser.
5. Excel export via `excelize` — 10 screens currently export through `C1XLBook`
   (`SaveSheet`/`StyleFromFlex` patterns repeat across `FrmStatis*` and `FrmSearch*`;
   extract one exporter).

### Phase 5 — Web UI

- One reusable data-grid component replaces C1FlexGrid across every list screen.
- Modal pattern for the ~25 `*Add`/`*Edit` forms.
- Nav gating from `/api/auth/me`, mirroring `SetMenuPermission` — with the real check on
  the server.
- Menu structure carries over unchanged from `FrmMDIMain` (README §Menu Structure).
- Extract UI strings from the **`.cs` / `.Designer.cs` source** (1,156 literals — *not* the `.resx`
  files, which hold no strings; see §11.7) into `locales/zh-CN.json` as keys.
- Dual-block editable grid for quarterly targets (§6.3).

### Phase 6 — Cutover

1. Both systems against the same migrated data; diff every report and statistic (§9).
2. Dry-run `cmd/migrate-data` against a production copy; record duration and orphan report.
3. Freeze → final migrate → verify → switch.
4. MSSQL kept read-only for a defined rollback window.
5. Rotate the `sa` credential; give the Go service a least-privilege MySQL account.

---

## 9. Testing strategy

| Layer | What | How |
|---|---|---|
| Proc equivalence | 28 procs | Phase 2.3 — old vs new result sets over identical params, on real migrated data |
| Report equivalence | 11 reports | golden files in `testdata/golden/`, compared as normalised JSON (row/column values), not PDF bytes |
| Finance engine | §6.1 | table-driven unit tests: each branch, the `max(…,0)` clamp, the sign convention, and the §10.2 fall-through |
| Quarter math | §6.3 | unit tests incl. divide-by-zero → `"0.00%"` and both blocks |
| Movement txn | §6.2 | integration tests against MySQL: happy path, mid-transaction failure rolls back, double-submit rejected |
| Migration tool | Phase 1.4 | per-table row counts + decimal sum checksums + FK orphan report |
| Permissions | §6.4 | one test per route asserting deny-by-default |

Compare reports as **normalised data**, not rendered bytes — PDF output will never be
byte-identical, and the numbers are what matter.

Pick the comparison dataset deliberately: a date range containing at least one vehicle in
each lifecycle state, plus one past its extension window (which is where §10.2 bites).

---

## 10. Defects and behaviour decisions found during analysis

Each needs an explicit *port faithfully* or *fix* decision. Defaults suggested; all are
the user's call.

**10.1 Colour-threshold branch assigns the wrong style.**
[FrmFinanceStore.cs:211-214](../CarSaleMan/CarSaleMan/FrmFinanceStore.cs#L211-L214):
`remainDates <= 5` → `extend5Color`, and `remainDates > 5 && <= 10` → `extend5Color`
**again** — `extend10Color` is created and styled but never applied. The 6–10 day
extension warning has never been visually distinct. *Fix.*

**10.2 Interest is silently zero past the extension window.**
[FrmFinanceStore.cs:176-184](../CarSaleMan/CarSaleMan/FrmFinanceStore.cs#L176-L184): when
`interestdates > nointerestdates` **and** `distextenddates <= 0`, no branch assigns
`totalinterest`, so it stays `0`. Vehicles beyond the extension period report zero
interest — plausibly the largest financial impact of any item here. *Confirm intent with
the business before changing.* Whichever way it goes, the golden-file dataset must include
such a vehicle so the behaviour is pinned by a test.

**10.3 `tbl_env` keys are read with inconsistent casing.**
Written lowercase (`extend10color`) at
[FrmFinanceParam.cs:79-91](../CarSaleMan/CarSaleMan/FrmFinanceParam.cs#L79-L91), read
mixed-case (`extend10Color`, `nointerest5Color`) at
[FrmFinanceStore.cs:115-125](../CarSaleMan/CarSaleMan/FrmFinanceStore.cs#L115-L125). This
only works because `DataTable.Select` is case-insensitive by default. A Go `map[string]`
lookup **is** case-sensitive and will return zero values — silently producing zero rates.
*Normalise keys on read; add a startup check that all 8 keys resolve.*

**10.4 `LogErrors` recurses infinitely on failure.**
[CommonMisc.cs:142-156](../CarSaleMan/CarSaleMan/CommonMisc.cs#L142-L156): the `catch`
block around the file write calls `LogErrors(ex.ToString())` — itself. If the log file is
unwritable (permissions, disk full, file locked by another instance), this is unbounded
recursion → `StackOverflowException`, which .NET cannot catch: the process dies. *Fix;* use
structured logging with a non-recursive fallback.

**10.5 Batch numbers collide.** `hh` (12-hour) instead of `HH`, plus client-side
generation with no uniqueness constraint — see §6.6. *Fix.*

**10.6 Filter expressions are built by string concatenation.**
e.g. [FrmLogon.cs:72](../CarSaleMan/CarSaleMan/FrmLogon.cs#L72) and the `FrmSearch`
filter path. These target `DataTable.Select`, not the database, so this is expression
injection rather than SQL injection — a crafted username breaks or subverts the filter
rather than reaching SQL Server. Nonetheless the Go port must use parameterised queries
and the structured filter DTO (§2.6, §4) throughout. *Fix by construction.*

**10.7 No transactions on multi-table movements.** §2.7 / §6.2. *Fix.*

**10.8 Permission is UI-only.** Menu items are greyed out; nothing prevents a client from
issuing the operation. In a fat client against SQL Server with `sa`, there was no server to
enforce it. *Fix — enforce in middleware.*

**10.9 Empty branch.**
[FrmFinanceStore.cs:204-207](../CarSaleMan/CarSaleMan/FrmFinanceStore.cs#L204-L207):
`if (distextenddates >= 0) { // }` — dead code, or a lost requirement. *Confirm, then delete.*

**10.10 Cross-assigned sign convention.** §6.1 — columns `interestdates` and
`distnointerestdates` receive each other's negated values. Looks deliberate; verify against
a known-good finance screenshot before "fixing" it. *Port faithfully.*

---

## 11. Risks

### 11.1 The stored procedures *(highest — now mitigated)*
28 procs hold all report/statistics/chart math and are absent from this repo (`Database/`
removed in commit `8f19f54`). You confirmed you can dump them from live MSSQL — Phase 0
does that and is a hard gate. Until the bodies exist, Phase 2 and 4 estimates are guesses:
a proc could be 5 lines or 500.

### 11.2 Decimal vs the legacy `double` — a fidelity decision, not just a bug
Two different concerns that pull in opposite directions:

- **Storage, sums, API boundary:** use `DECIMAL` in MySQL and `shopspring/decimal` in Go.
  148 decimal columns carry prices, profits and interest; `float64` silently corrupts
  totals in the profit and finance reports. Serialize as JSON **strings** so JavaScript's
  `float64` doesn't reintroduce the problem at the boundary.
- **The interest calculation itself already uses `double`** ([§6.1](#61-finance-interest-engine--domainfinance);
  see the `(double)` casts at
  [FrmFinanceStore.cs:159-181](../CarSaleMan/CarSaleMan/FrmFinanceStore.cs#L159-L181)),
  including `inprice * 0.9` and `* 0.68`. A faithful decimal port will therefore differ
  from legacy output in the last cents.

So the equivalence tests for `stor_finance_store` **cannot** demand exact equality against
the old screen. Choose one, explicitly, and write it down:

- **(a) Recommended** — compute in `decimal`, and compare against legacy with a documented
  tolerance (e.g. ±0.01 per row, ±0.05 per report total). More correct going forward;
  requires sign-off that small differences from the old numbers are expected.
- **(b)** Reproduce `float64` arithmetic exactly in the interest engine to stay bit-equal,
  and keep `decimal` everywhere else. Maximum fidelity, but it ports a known imprecision.

This choice belongs to the business, not the port. Raise it in Phase 0.

### 11.3 Reporting correctness is the acceptance criterion
This is a financial system; the reports are the product. Golden-file equivalence on real
data (§9) is how the port is judged — not whether the UI renders.

### 11.4 Optimistic-concurrency semantics
Generated `UPDATE`/`DELETE` compare every original column (§2.5). A naive
`WHERE uid = ?` converts today's concurrency violations into silent overwrites. Add
`row_version` (or an `updated_at` check) and return `409 Conflict`. **Decide in Phase 1** —
it affects every table's DDL.

### 11.5 Password storage
DES encryption, not hashing, with a key hardcoded at
[CommonMisc.cs:435](../CarSaleMan/CarSaleMan/CommonMisc.cs#L435). Migration path in §6.5.
Separately: the production `sa` password is committed in plaintext in
[app.config](../CarSaleMan/CarSaleMan/app.config) — rotate it during cutover and give the
Go service a least-privilege MySQL account.

### 11.6 Excel `.xls` vs `.xlsx`
`FrmImportExcel` reads legacy **`.xls`** (BIFF8) via `C1XLBook`; `excelize` handles
`.xlsx` only. Either require `.xlsx`, or add a converter. Confirm what format suppliers
actually send before deciding — this is a live workflow dependency, and it also affects
the 10 export screens.

### 11.7 Source encoding and UI-string location *(corrected — measured, not inferred)*

The original text of this section claimed all 133 files were GB18030 and that UI strings lived in
the 64 `.resx` files. **Both were wrong.** Measured against the source:

| Claim | Reality |
|---|---|
| All files GB18030 | **Mixed — three encodings.** Across 201 source files: GB18030 84, UTF-8-with-BOM 64, plain UTF-8 53. Detect per file; a blanket `iconv -f GB18030` mojibakes the 117 that are already UTF-8 |
| UI strings in 64 `.resx` | **Partly true, and the first correction of this section was itself wrong.** The `.resx` files hold no ordinary *string resources* — only bitmaps, icons, colors and byte arrays. But **27 of 63 embed a serialized C1FlexGrid `ColumnInfo` blob containing 144 Chinese column captions** |
| — | The bulk of UI text is **hardcoded in `.cs` / `.Designer.cs`**: ~1,150 quoted literals containing Chinese, across 123 of 130 files |

Both `.cs` literals and `.resx` `ColumnInfo` captions must be extracted. Missing the
latter means every list screen renders Chinese column headers inside an English UI —
144 captions, 116 of which appear nowhere in the `.cs` sources.

The captions look like this inside the `<value>` blob:

```
Columns:0{Name:"vin";Caption:"VIN码";Visible:True}1{Name:"inprice";Caption:"进价"…}
```

The `Name` field is a good key stem — `common.col_inprice` beats anything derived from
the caption text.

Example, `FrmLogon.Designer.cs`:

```csharp
this.labelCode.Text = "编　码";
this.labelName.Text = "姓　名";
this.labelPass.Text = "口　令";
```

Reading a GB18030 file as UTF-8 still mojibakes every Chinese literal — including report titles
(§7), the 17 reference-data domain names (§5.6) and the permission strings `读写`/`不可用` (§6.4),
which are compared by value. The conversion remains a Phase 1 task; only its *input* changes.

**Tooling caveat:** `grep -P '[\x80-\xff]'` silently returns **0** on these files even where high
bytes exist — it reported 0 matches in a file where `perl` found 3. Any extraction script must use
`perl`, `python`, or `iconv`, never `grep -P`, for byte-range detection. This is how the original
claim came to be wrong.

### 11.8 `tbl_stats`, `tbl_dbbackup_log`, `tbl_fit` have unclear ownership
`tbl_stats` (bonus1..7 + total) has full CRUD generated but no form references it in the
grep above — possibly dead, possibly driven by a report whose proc we haven't seen.
`tbl_dbbackup_log` **does have a UI** — corrected: `数据备份` is a live entry under the
`系统设置` menu in `FrmMDIMain`, so the backup feature is reachable and carries its own
permission key. Resolve `tbl_stats` during Phase 0 from live row counts before porting or
dropping it.

### 11.9 Scope
~28k lines of hand-written logic, 15 tables, 28 procs, 11 reports, 5 charts, 7 statistics
screens, 63 forms. This is a rewrite, not a translation.

---

## 12. Effort

Phase 2 and 4 cannot be sized until the proc bodies land (§11.1). Sequencing, and relative
weight, are firm:

| Phase | Scope | Relative weight | Blocked by |
|---|---|---|---|
| 0 | dump + reconcile schema | small | live DB access |
| 1 | foundation, migrations, data migration tool | medium | 0 |
| 2 | 5 views + 28 procs + equivalence harness | **unknown until 0** | 0, 1 |
| 3 | 10 domain/CRUD slices | large — parallelisable after slice 3 | 1 |
| 4 | renderer + 11 reports + 7 stats + 5 charts | large | 2, 3 |
| 5 | web UI, ~20 screens | large — parallel with 3–4 once the API shape is fixed | 4 (API contracts) |
| 6 | cutover | small | all |

Highest-leverage single tasks: the shared report renderer (§7, collapses ~2,500 lines), the
shared query filter (§2.6, gates five screens), and the movement transaction (§6.2, the
correctness core).

---

## 13. What's already done

- Full inventory and dependency analysis of the C# codebase (§1, §2).
- Complete table/column/type extraction → [docs/mysql/schema.sql](mysql/schema.sql).
- All 28 proc signatures + exact result-column contracts recovered from `CmsDB.xsd`
  (in `schema.sql` §4) — these are what the Phase 2 translations must satisfy.
- 5 view column contracts recovered (`schema.sql` §3).
- Finance interest algorithm reconstructed from source (§6.1).
- 17 reference-data domains and 8 finance parameter keys recovered (§5.5, §5.6).
- 11-report catalogue with titles, column counts and backing procs (§7).
- 10 latent defects identified with decisions attached (§10).
- Phase 0 extraction script → [docs/mssql-export.sql](mssql-export.sql).

## 14. Next step

Run [docs/mssql-export.sql](mssql-export.sql) against the live `csm` database and commit
the output. In the same pass, get business answers on the two questions that change the
work: **§10.2** (is zero interest past the extension window intended?) and **§11.2**
(decimal-with-tolerance, or bit-equal float?).
