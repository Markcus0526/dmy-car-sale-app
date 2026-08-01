# CarSaleMan — Development Plan

**Companion to [GO_MIGRATION_PLAN.md](GO_MIGRATION_PLAN.md).** That document is the *analysis*:
what the legacy system is, how it works, what defects it carries, what the risks are. This
document is the *execution plan*: locked decisions, a day-by-day schedule, gates, and exit
criteria.

Read the analysis once. Work from this one daily.

---

## Table of contents

1. [Locked decisions](#1-locked-decisions)
2. [Open decisions](#2-open-decisions)
3. [Assumptions and totals](#3-assumptions-and-totals)
4. [Phase 0 — Recover ground truth](#phase-0--recover-ground-truth-2-sessions)
5. [Phase 1 — Foundation](#phase-1--foundation-16-sessions)
6. [Phase 2 — Views and procedures](#phase-2--views-and-procedures-22-sessions)
7. [Phase 3 — Vertical slices](#phase-3--vertical-slices-73-sessions)
8. [Phase 4 — Reports, statistics, charts](#phase-4--reports-statistics-charts-40-sessions)
9. [Phase 5 — Cutover](#phase-5--cutover-11-sessions)
10. [Risk register](#10-risk-register)
11. [Session protocol](#11-session-protocol)

---

## 1. Locked decisions

These are settled. Changing one after its phase begins is expensive; the cost column says how
expensive.

| # | Decision | Rationale | Cost to reverse later |
|---|---|---|---|
| D1 | **Backend: Go** | Workload is CRUD + SQL + arithmetic + table rendering. `sqlc` fits because all 130 SQL statements are already known and static (§2.2). Single static binary removes the distributed `sa` password | Total rewrite |
| D2 | **Frontend: TypeScript + React** | The quarterly-target grid (§6.3) is genuinely stateful; charts are already client-side by design (§3.1); TS catches the two hazards below | ~20 screens |
| D3 | **Decimals cross the wire as JSON strings** | 148 decimal columns. A branded `type Decimal = string` makes `price * qty` a compile error, preventing reintroduction of the float corruption §11.2 exists to prevent | Every DTO + every screen |
| D4 | **Permission keys stay Chinese, defined once as a Go const enum** | Existing `tbl_permission` rows migrate with no transformation (§6.4). One file, not 63 inline literals | Data migration |
| D5 | **Grid: TanStack Table (headless)** | Replaces C1FlexGrid. AG Grid's editable-grid features are paid — that swaps one commercial dependency for another, which is part of what this migration escapes | The shared grid component |
| D6 | **PDF: HTML → PDF via headless Chrome** | Reuses web table styling and sidesteps CJK font embedding, which is the unpleasant part of Go's PDF ecosystem | Report export layer |
| D7 | **UI languages: English + Chinese, key-first from day 5** | See §1.1. Extracting `.resx` as keys costs +1 day now, ~15 days later | 20+ screens |
| D8 | **Web UI is built per-slice, not after the API** | Deviation from the analysis doc's Phase 5. Solo at 4 hrs/day, sequencing UI last means nothing demoable until month 6 and UI-shaped API mistakes surface months after they're made | Rework, not redesign |
| D9 | **Movement transitions are transactional and flag-guarded** | The one place the port intentionally departs from legacy behaviour (§6.2). `WHERE uid=? AND inflag=0` so a double-submit fails loudly | The movement service |
| D10 | **`cmd/migrate-data` reports orphans and refuses to proceed silently** | Years of unconstrained operation make dangling FK references likely (§5.3) | Migration tool |
| D11 | **The 11 formal reports render in Chinese only** | Business answer. Their consumers are Chinese-language; English report output would be unread. Screen-level Excel exports still follow the viewer's locale | 11 report declarations |
| D12 | **The UI serves internal staff *and* external users** | Business answer. Full translation coverage — no skipping rarely-seen admin screens — and a higher quality bar on the English strings | Translation scope |
| D13 | **§10.2: port the zero-interest behaviour faithfully, behind `finance.Params.ChargeInterestPastExtension` (default `false`)** | See §2.1. Assessed as a bug, but silently fixing it breaks equivalence testing on exactly the rows that matter. Config flag, not a code change | One config default |
| D14 | **§11.2: compute in `decimal`; equivalence tolerance ±0.01/row, ±0.05/report total** | See §2.2. `0.9` and `0.68` are exact in decimal and inexact in binary float, so decimal is more correct at the multiplication step. Migrated historical values are preserved as-is so issued invoices still reconcile | Finance engine + test assertions |
| D15 | **§11.4: `row_version INT UNSIGNED NOT NULL DEFAULT 1` on every table** | A counter is unambiguous where `updated_at` collides inside one clock tick and suffers clock skew. Whole-row comparison is fragile around NULL and DECIMAL equality. **No `created_at`/`updated_at`** — there is no source data, so every migrated row would carry a fabricated timestamp | Every table's DDL |
| D16 | **§5.4: storage collation `utf8mb4_unicode_ci`; display order sorted in Go via `x/text/collate`** | `utf8mb4_zh_0900_as_cs` matches legacy pinyin order but is accent- and case-**sensitive**, silently changing equality for `读写` and the `是否*` columns. Changing sort order is a display bug; changing equality is a correctness bug | Every table's DDL + all list queries |
| D18 | **No database i18n — `tbl_basedata` values display as stored; locale in `localStorage`** | Scoped out by the user. Accepts English chrome around Chinese dropdown options. Removes `tbl_i18n` and its admin screen (−3 sessions) and keeps `tbl_basedata` byte-identical to source for the Phase 1 checksums | Lookup layer only, not screens |
| D17 | **Opaque server-side sessions in `tbl_session`, HttpOnly cookie — not JWT** | This system has a permission-admin screen (`权限设定`). With a JWT, revoking access does not take effect until the token expires — the same §10.8 failure, moved from the client to the token. Token stored **hashed**; permissions loaded per request; HttpOnly cookie rather than `localStorage` avoids the XSS→takeover path that matters more now external users are in scope (D12) | Session layer + auth middleware |

### 1.1 Internationalisation — the split that matters

Chinese text in this system divides into two categories needing **opposite** treatment. Getting
this wrong produces an authorization bug, not a translation bug.

**Translatable — UI chrome.** The 64 `.resx` files, menu labels, column headers, buttons,
validation messages, report titles. ~1,500–2,500 strings. Ordinary i18n.

**Not translatable — data and keys:**

| What | Why |
|---|---|
| Permission values `读写` / `只读` / `不可用` (§6.4) | Authorization **keys**, compared by value. Translate the display, never the key |
| `tbl_permission.fieldname` = Chinese menu labels (§6.4) | Same. An English-locale user whose permission lookup misses is silently denied every screen |
| `isbill` / `ispayment` / `issend` / `isreport` (§5.6) | Booleans stored as Chinese strings in `varchar(50)`. Display "Yes"/"No", store `是`/`否` |

**`tbl_basedata` is not homogeneous.** The 17 domains (§5.6) split three ways:

| Type | Domains | Treatment |
|---|---|---|
| **Enums — *would* be translatable** | `进货途径`, `进车状态`, `返利状态`, `销售方式`, `地区`, `行业`, `特种车类型`, the four `是否*` flags | **Not translated (D18).** Displayed as stored |
| **Proper nouns — never translate** | `销售顾问`, `经手人`, `批复人` | These are **people's names**. Pass through verbatim in both locales |
| **Judgement call** | `车系列`, `车型大类`, `库位` | Manufacturer/site terms. Default to keeping Chinese; accept a supplied English label if the business has one |

> **D18 — no database i18n.** Scoped out by the user. `tbl_basedata` values are displayed
> exactly as stored, so an English-locale user sees English chrome around Chinese options
> (`地区`, `行业`, `销售方式`). Accepted deliberately. This removes the `tbl_i18n` table and
> its admin screen — **3 sessions off slice 2** — and keeps `tbl_basedata` byte-identical to
> source, which the Phase 1 checksums depend on.
>
> Reversible: the allowlist indirection in `internal/query` and the key-based catalogue mean
> adding a translation table later touches the lookup, not the screens.

**Mechanics.** `react-i18next` with `locales/zh-CN.json` + `locales/en.json`. Locale lives in
`localStorage` (D18 — no `tbl_userinfo.locale` column), defaulting to `Accept-Language`. It
therefore does not roam between devices. Backend stays locale-agnostic
except where it renders text: **error responses return codes, not prose**; report/Excel/PDF
endpoints take a `lang` parameter. The shared renderer (§7) takes a locale and each of the 11
reports declares header *keys* — one renderer change covers all eleven.

**Reports are exempt (D11).** The 11 formal reports (§7) keep their exact Chinese titles and
headers in both locales — the renderer needs no locale parameter for them. Screen-level Excel
exports are *not* exempt: they mirror the grid the user is looking at, so their headers follow the
viewer's locale.

This has a testing benefit worth naming: report chrome is byte-stable across locales, so there is
no locale dimension in the golden-file suite. Every report is tested once, not twice.

**Golden files are unaffected.** §9 already compares normalised data rather than rendered bytes,
so translated chrome could not have broken report equivalence anyway.

**External users raise the English bar (D12).** Internal staff tolerate awkward phrasing; external
users read it as a quality signal. The day 18 glossary pass and the native review that follows are
load-bearing, not a formality — budget real reviewer attention there rather than accepting a
machine-drafted `en.json`.

---

## 2. Open decisions

### Blocking — needed before the phase named

| # | Question | Needed by | Impact if wrong |
|---|---|---|---|
| Q3 | **§11.6** — do suppliers send `.xls` or `.xlsx`? | Day 66 (slice 4) | `.xls` adds a converter, ~+3 days. Free to ask now |
| Q4 | **§5.6** — is `tbl_basedata.type` 1 vs 2 meaningful? | Day 50 (slice 2) | `CmsDB.xsd` doesn't record it. Answer from live row counts in Phase 0 |
| Q5 | Which `tbl_basedata` domains get English labels? | Day 50 (slice 2) | Default per §1.1 table; confirm the judgement-call row |

### Non-blocking — decide when convenient

| # | Question | Default if unanswered |
|---|---|---|
| Q8 | **§11.8** — are `tbl_stats` and `tbl_dbbackup_log` live? | Resolve from Phase 0 row counts. If dead, drop rather than port |
| Q9 | **§10.9** — the empty `if (distextenddates >= 0) { }` branch | Delete. Confirm nobody remembers a lost requirement |

### Answered

| # | Question | Answer | Effect |
|---|---|---|---|
| Q1 | §10.2 — zero interest past the extension window? | **Assessed as a bug; ported faithfully behind a flag** | D13. See §2.1 |
| Q2 | §11.2 — decimal or bit-equal float? | **decimal, ±0.01/row tolerance** | D14. See §2.2 |
| Q6 | Reports — English or always Chinese? | **Always Chinese** | D11. Reclaims 3 sessions from Phase 4 |
| Q7 | English for internal or external users? | **Both** | D12. No sessions reclaimed — full translation coverage stays, English quality bar rises |

> **Q1 and Q2 were determined by the development team, not the business.** They are
> recorded here as engineering decisions with the reasoning attached, and both are built
> to be reversed cheaply if the business later disagrees. Treat §2.1 and §2.2 as the
> briefing document for that conversation whenever it happens.

### 2.1 Q1 — §10.2, zero interest past the extension window

**Determination: this is a bug — but the port reproduces it faithfully by default.**

Evidence it is an omission rather than policy:

1. **The code shape.** `if / elif` with no `else`; `totalinterest` retains its initialised
   `0` by falling through. A deliberate write-off would be an explicit
   `else { totalinterest = 0; }` with a comment.
2. **It inverts the economics.** The structure escalates pressure over time — interest-free
   period, then base rate, then extension rate — and then drops to zero at the point of
   greatest exposure. No inventory-financing arrangement works that way.
3. **§10.9 is adjacent.** `if (distextenddates >= 0) { // }` — an empty branch on the same
   variable, immediately nearby. Someone began handling this case and stopped.

**Why we still port it faithfully.** Correcting it silently would change every historical
figure and fail equivalence tests on precisely the rows that matter most. So:

- `finance.Params.ChargeInterestPastExtension`, default **`false`** (legacy behaviour).
- The golden dataset **must** contain a past-extension vehicle, pinning the behaviour by
  test in either configuration.
- Phase 2 emits a **quantification report** — affected vehicle count and total money — so
  the business decision, when it happens, is informed by numbers rather than hypotheticals.

Flipping the default is a config change, not a code change.

### 2.2 Q2 — §11.2, decimal vs bit-equal float

**Determination: compute in `decimal`. Equivalence tolerance ±0.01 per row, ±0.05 per
report total.**

- Storage, sums and the API boundary were always going to be `DECIMAL` / `shopspring/decimal`
  — 148 columns. That part was never in question.
- **`0.9` and `0.68` are exact in decimal and inexact in binary floating point.** Decimal is
  therefore *more correct at the multiplication step*, not merely tidier.
- Option (b) is technically achievable — Go `float64` and .NET `double` are both IEEE-754
  binary64, so preserving operation order would reproduce results bit-for-bit — but it buys
  bit-equality only for historical reconciliation while permanently porting a known
  imprecision.

**Guard:** migrated historical values are preserved exactly as they are in the source data.
Only *new* calculations use decimal, so invoices already issued continue to reconcile.

The 360-day banker's year (§6.1) is preserved either way.

---

## 3. Assumptions and totals

**4-hour sessions, 5 days per week, solo.** No parallelism — everything the analysis marks
"parallelisable" runs sequentially here. Throughput assumed at ~520 lines of reviewed, tested code
per session, which is conservative once review, debugging, and context reload are counted.

| | |
|---|---|
| **Total sessions** | **158** (161 − 3 for D18) |
| **Elapsed** | ~32 weeks ≈ **7.5 months** at 5 days/week (≈6 months at 6 days/week) |
| **Estimated deliverable** | ~78,000 lines (Go backend + tests, React frontend + tests, migrations, tooling) |
| **Estimated API cost** | **$6,000–7,000**, range $5,000–10,000 (see §10, R1) |

### Phase summary

| Phase | Sessions | Days | Gate |
|---|---:|---|---|
| 0 — Recover ground truth | 2 | 1–2 | **Hard gate.** Live DB access |
| 1 — Foundation | 16 | 3–18 | Phase 0 |
| 2 — Views and procedures | 22 | 19–40 | Phase 0, 1 + answers to Q1, Q2 |
| 3 — Vertical slices | 70 | 41–110 | Phase 1 (Phase 2 only for slice 9) |
| 4 — Reports, statistics, charts | 37 | 114–150 | Phase 2, 3 |
| 5 — Cutover | 11 | 151–161 | All |

---

## Phase 0 — Recover ground truth (2 sessions)

| Day | Work |
|---:|---|
| 1 | Run [mssql-export.sql](mssql-export.sql) against live `csm`. Commit the output |
| 2 | Reconcile against [mysql/schema.sql](mysql/schema.sql) — it is *inferred* from `CmsDB.xsd` and cannot see defaults, indexes, checks, triggers, or collation. Commit reconciled DDL. Answer Q4 and Q8 from row counts. Put Q1 and Q2 to the business |

**Exit criteria:** step 1 returns **28** rows, step 2 returns **5**, reconciled DDL committed.
Fewer rows means procs or views were dropped from that instance and another source is needed.

**Do not start Phase 1 without this.** Phase 2's 22 sessions are sized on procs averaging ~50
lines; that number is a guess until day 1 completes.

---

## Phase 1 — Foundation (16 sessions)

| Days | Work |
|---:|---|
| 3 | `go mod init`, §3 directory layout, config from environment (the hardcoded `sa` password goes), `docker-compose` MySQL |
| 4 | Convert all 201 sources to UTF-8 into a reference tree. Encoding is **three-way** — GB18030 84, UTF-8-BOM 64, UTF-8 53 — detect per file (§11.7). **DONE**: **Verify** report titles (§7), the 17 domain names (§5.6), and `读写`/`不可用` (§6.4) survive intact |
| 5 | Extract **1,447 Chinese literals** → 472 keys. Sources are `.cs`/`.Designer.cs` **and** `.resx` `ColumnInfo` grid captions (144, of which 116 appear nowhere in `.cs`) — the earlier "resx hold no strings" claim was wrong, see §11.7. Keys, not raw literals (D7). Do not use `grep -P` for byte-range detection; it silently returns 0 on these files. **DONE** |
| 6–7 | `migrations/0001_init.up.sql` from reconciled DDL. **Decide `row_version` (§11.4) and the Chinese collation strategy (§5.4)** — both change every table's DDL |
| 8–11 | `cmd/migrate-data`: table-by-table with per-table row counts, decimal sum checksums, and an orphan report per FK. Load order per §8 Phase 1.4 |
| 12 | Run against a production copy. Resolve orphans |
| 13 | Local check script (`./scripts/check.sh`, `--db` for the MySQL checks). GitHub Actions removed by request — nothing gates a push, so run this before committing |
| 14–15 | `internal/platform`: config, structured logging with a **non-recursive** fallback (fixes §10.4), errors, decimal helpers |
| 16–18 | Draft `locales/en.json` from `zh-CN.json`. Day 18 is a glossary pass on domain terms — send for native review, which can proceed asynchronously |

**Exit criteria:** migrations apply clean; `migrate-data` runs against a production copy with row
counts and checksums matching and a zero-orphan report; CI green; both locale files exist with
every key present.

---

## Phase 2 — Views and procedures (22 sessions)

No longer blocked — Q1 and Q2 are settled by D13 and D14. Two consequences for this phase:

- Equivalence assertions use the **±0.01/row, ±0.05/report tolerance** (D14), not exact
  equality, for anything downstream of `stor_finance_store`.
- Add a **§10.2 quantification query** to the harness: affected vehicle count and total
  money, using the column names step 16 of the export recovers. That output is the
  briefing note for the business conversation, and it costs about half a session.

| Days | Work |
|---:|---|
| 19–20 | 5 views as MySQL views, column-for-column identical to today. **Preserve `vw_storeout.Expr1` verbatim** — rename only once nothing reads it |
| 21–22 | Golden-file equivalence harness: run the old proc on MSSQL and the new query on migrated MySQL over identical parameters, assert identical result sets |
| 23–40 | 28 procs → `internal/stats/queries.sql` as plain SQL, ~1.5 per session, each with its equivalence test |

**The `DATEDIFF` trap.** T-SQL `DATEDIFF(day, a, b)` = `b - a`; MySQL `DATEDIFF(a, b)` = `a - b`.
Check **every** occurrence individually. This is the most likely source of silent sign errors in
the entire project.

**Comparison dataset:** pick deliberately — at least one vehicle in each lifecycle state, plus one
past its extension window, which is where §10.2 bites.

**Exit criteria:** all 28 procs and 5 views have passing equivalence tests on real migrated data.

---

## Phase 3 — Vertical slices (73 sessions)

Each slice ships domain package + repository + REST handlers + tests **and its screens**. Working
software from slice 1 (D8).

| Slice | Days | Split | Notes |
|---|---:|---|---|
| 1 — auth, users, permissions | 41–49 | BE 4 / UI 3 / i18n scaffold 2 | §6.4 server-side enforcement, §6.5 DES→bcrypt dual path. i18n provider + switcher (locale in `localStorage`, D18). **DONE** |
| 2 — reference data | 50–56 | BE 4 / UI 3 | 17 domains (§5.6). Answers Q4, Q5. **−3 sessions**: no `tbl_i18n`, no translation admin screen (D18) |
| 3 — **shared filter + grid** | 60–65 | 6 | Structured filter DTO replacing `FrmSearch` (§2.6) + the reusable grid. **Gates slices 4–8.** Highest-leverage work in the project |
| 4 — on-road vehicles | 66–73 | BE 4 / UI 3 / i18n 1 | Excel import (§11.6). Q3 must be answered by day 66 |
| 5 — store-in | 74–83 | BE 6 / UI 3 / i18n 1 | Largest slice. Movement transaction (§6.2) + server-side `batchno` with `HH` and a `UNIQUE` index (§6.6) |
| 6 — store-change | 84–88 | BE 3 / UI 2 | §6.2 |
| 7 — store-out | 89–96 | BE 4 / UI 3 / i18n 1 | §6.2 |
| 8 — special cars, repair, journal | 97–102 | BE 3 / UI 3 | |
| 9 — finance | 103–111 | BE 5 / UI 3 / i18n 1 | §6.1. Most test-heavy slice. Needs Phase 2 complete |
| 10 — audit trail | 112–113 | 2 | Viewer only; the write path is added per-slice throughout |

**Three things to carry deliberately into slice 9** (§6.1):

- The **cross-assigned sign convention** — column `interestdates` receives `-distnointerestdates`
  and vice versa. Port faithfully; comment the Go code pointing at the source lines.
- `0.9` and `0.68` are magic numbers. Lift them into `finance.Params` with those defaults.
- **Normalise `tbl_env` keys on read** and add a startup check that all 8 resolve (§10.3) — Go map
  lookups are case-sensitive where `DataTable.Select` was not, and the silent failure mode is zero
  interest rates.

**Exit criteria per slice:** domain unit tests pass; movement integration tests cover happy path,
mid-transaction rollback, and double-submit rejection; one permission test per route asserting
deny-by-default; screens work in both locales.

---

## Phase 4 — Reports, statistics, charts (37 sessions)

| Days | Work |
|---:|---|
| 114–117 | `internal/report/render.go` — the shared renderer. Title block, date subtitle, `carseries` group column with row spanning, subtotals, grand total, autosize. Build this first; it collapses ~2,500 lines of repetitive C#. **No locale parameter** (D11) |
| 118–133 | 11 reports as declarations over the renderer, ~1.5 sessions each, each with a golden-file test. Chinese chrome throughout. Budget days 132–133 for report 11 (`进销存汇总报表`) — the only one driving two procs |
| 134–141 | 7 statistics endpoints. Mostly thin wrappers over Phase 2 queries, except the quarterly-target grid (§6.3, ~3 days) and remaining-amount (~2 days). Screens are bilingual |
| 142–144 | 5 chart endpoints returning JSON; rendering in the browser. Bilingual axis and legend labels |
| 145–148 | One shared `excelize` exporter replacing the `C1XLBook` pattern repeated across 10 screens. **Locale-aware headers** — these mirror the on-screen grid, so they follow the viewer's locale (D11) |
| 149–150 | PDF via headless Chrome (D6) |

**Quarterly grid specifics** (§6.3): port `CalculateAmounts()` as a pure function over a typed
grid. Unit-test the divide-by-zero case yielding `"0.00%"` and both blocks. Make the API a
whole-quarter `PUT` so the partial-save race in `SaveChange(oldYear, oldQuarter)` disappears.

**Exit criteria:** every report and statistic has a golden-file test comparing normalised JSON
(not PDF bytes) against legacy output on real data.

---

## Phase 5 — Cutover (11 sessions)

| Days | Work |
|---:|---|
| 151–153 | End-to-end testing in both locales. External users (D12) means the English pass is a real review, not a smoke test |
| 154–156 | Parallel run: both systems against the same migrated data. Diff **every** report and statistic |
| 157–158 | Dry-run `cmd/migrate-data` against a production copy. Record duration and the orphan report |
| 159 | Freeze → final migrate → verify |
| 160 | Switch. MSSQL kept **read-only** for a defined rollback window |
| 161 | Rotate the `sa` credential (§11.5); give the Go service a least-privilege MySQL account |

**Exit criteria:** zero unexplained differences in the parallel-run diff. §11.3 is explicit — this
is a financial system, the reports are the product, and report equivalence is how the port is
judged.

---

## 10. Risk register

| # | Risk | Exposure | Mitigation |
|---|---|---:|---|
| R1 | **Phase 2 is unsized.** 28 proc bodies are absent from this repo (`Database/` dropped in `8f19f54`). Sized on ~50-line procs | ±20 sessions, ±$3k | Phase 0 is a hard gate. Re-size the whole plan on day 2 |
| R2 | **`DATEDIFF` sign errors** pass tests written from the same misreading | Silent wrong numbers | Equivalence tests run against the *original proc*, not a re-derivation. That's the point of the harness |
| R3 | **§10.2 / §11.2 determined by the dev team, not the business** (D13, D14) | Config flip + re-run of the finance golden files if the business disagrees — roughly 1 session, not 4–6 | Legacy behaviour is the default; the flag is config, not code; the golden dataset pins both configurations. Phase 2's quantification report is the trigger for confirming |
| R4 | **i18n retrofitted** rather than key-first | +15 sessions | D7. Day 5 is non-negotiable |
| R5 | **Excel is `.xls`** (§11.6) | +3 sessions | Q3 — ask now, it's free |
| R6 | **Solo bus factor.** No second reviewer on a financial system | Project | Golden-file tests are the safety net. Keep them passing, never skip one |
| R7 | **Context reload tax** at 4 hrs/day | ~5% throughput | §11 session protocol |

---

## 11. Session protocol

A 4-hour session that reliably produces working code:

| | |
|---|---|
| **0:00–0:20** | Read the last entry in **[SESSION_LOG.md](SESSION_LOG.md)**. Re-orient. Run the test suite before touching anything |
| **0:20–2:50** | Build and review. One slice item at a time |
| **2:50–3:35** | Tests, lint, cleanup. Commit |
| **3:35–3:50** | Update the day's row in this plan: done / partial / blocked |
| **3:50–4:00** | **Append an entry to [SESSION_LOG.md](SESSION_LOG.md)** — done, verified, half-finished, blocked, and exactly one next action |

That last 15 minutes matters more than it sounds. At 4 hrs/day the context reload is a real tax,
and a specific handoff note roughly halves it. "Continue store-in" is not a handoff note;
"`movement.StoreIn` txn works, double-submit guard untested, `batchno` unique index not yet in
migrations" is.

**Never end a session with a failing test suite.** Revert instead. The next session starts with
20 minutes of re-orientation — starting it with a debugging session on code you can no longer
remember is the most expensive way to lose a day.

---

## 12. Progress

*Updated end of Day 13. Work has run out of plan order because Phase 0 is blocked — see
[SESSION_LOG.md](SESSION_LOG.md) for the per-session detail.*

| Phase | Sessions | Status |
|---|---:|---|
| 0 — Ground truth | 2 | **BLOCKED** — needs the live `csm` database |
| 1 — Foundation | 16 | **Partial.** Days 3–7, 13–18 done. Days 8–12 (`cmd/migrate-data`) blocked on Phase 0 |
| 2 — Views and procedures | 22 | Not started — blocked on Phase 0 |
| 3 — Vertical slices | 70 | **Slice 1 done.** Slice 3 backend done (`internal/query`); its grid/filter UI remains |
| 4 — Reports, statistics, charts | 37 | Not started |
| 5 — Cutover | 11 | Not started |

**Total is now 158 sessions** (was 161): D18 removes 3 from slice 2.

**Done out of order, because Phase 0 blocks the schema-dependent work:**

| Built | Where |
|---|---|
| Go + React skeleton, i18n, nav, error-code envelope | `cmd/`, `internal/http`, `web/` |
| UTF-8 reference tree + string extraction + verification | `tools/convert-encoding`, `tools/extract-strings` |
| Migrations 0001–0004, verified against MySQL 8.4 | `migrations/`, `tools/gen-migration` |
| Legacy DES → bcrypt password migration | `internal/auth/password.go` |
| Sessions (D17), login service, `store/mysql` | `internal/auth`, `internal/store/mysql` |
| Working end-to-end login with permission-filtered nav | all of the above |
| Parameterised filter replacing `FrmSearch` | `internal/query` |
| `./scripts/check.sh` (replaces CI) | `scripts/` |

**Next action:** the React data grid + filter UI on top of `internal/query`, finishing slice 3
and unblocking slices 4–8.

**Still blocked:** Phase 0. Fourteen sessions have routed around it. Phase 2's 22 sessions stay
unsized, and the missing non-PK indexes in `0001_init.up.sql` remain unknown.
