# Session Log

Working history and end-of-session state. **Read the most recent entry before starting
work; append a new one before finishing.** See [DEVELOPMENT_PLAN.md](DEVELOPMENT_PLAN.md)
§11 — this is the 3:50–4:00 slot of every session.

Newest entry first.

---

## Where things stand

*Updated end of Day 5. Read this first; the entries below are the detail.*

| | |
|---|---|
| **Phase** | Phase 1 (foundation) in progress. **Phase 0 not yet run** |
| **Sessions logged** | 6 (Day 0–5) |
| **Plan** | [DEVELOPMENT_PLAN.md](DEVELOPMENT_PLAN.md) — 161 sessions, 14 locked decisions |
| **Next action** | Convert the 130 C# sources to UTF-8 into a reference tree (plan day 4) |

**Green — verified and repeatable**

- **Go backend compiles, vets and tests clean under `-race`.** Go 1.26.5 installed to the
  scratchpad (no sudo needed): `export PATH="$SP/go/bin:$PATH"`.
- `web/` builds under strict TS; dev server serves; app catalogue 90/90.
- String extraction: 1,154 literals → 1,023 TRANSLATE (370 keys) / 86 NEVER / 45 DROP /
  **0 REVIEW**. Re-runnable: `node tools/extract-strings/extract.mjs`.
- English catalogue: 370/370 translated, `verify.mjs` exit=0 on both catalogues.
- 49 permission keys match legacy `FrmMDIMain` exactly.
- **CI** (`.github/workflows/ci.yml`) — 3 jobs, all 9 steps verified locally.

**Amber — known debt, carried deliberately**

- The 370-key extracted catalogue is reference material; it merges into
  `web/src/locales/` per-slice as screens are built, not in bulk.
- 8 concatenation call sites need merging into interpolated keys at port time
  (`docs/i18n/CONCATENATIONS.md`) — affects 6 of the 11 reports.
- Go is installed to the **scratchpad**, which is session-scoped. CI has its own
  toolchain, so this only affects local runs — re-extract if it disappears.

**Red — blocked on someone else**

- **Phase 0 day 1**: run [mssql-export.sql](mssql-export.sql) against `csm` on
  `R-SEVEN64`. The only external blocker. Gates Phase 2's sizing, which is the
  ±20-session unknown in the whole plan. Gate: **28 proc files, 5 view files**.

**Decisions made without business input** — reversible, see DEVELOPMENT_PLAN §2.1/§2.2

- **D13** (§10.2 zero interest past extension): assessed a bug, ported faithfully behind a
  default-`false` flag.
- **D14** (§11.2): decimal, ±0.01/row tolerance.

**Rules that make this worth keeping:**

- Record verification *results*, not intentions. "Frontend builds, 90/90 locale parity"
  beats "worked on i18n".
- Name exactly **one** next action. If there are three, pick the one that unblocks the
  others.
- Be specific about what is half-finished. "Continue store-in" is not a handoff note;
  "`movement.StoreIn` txn works, double-submit guard untested, `batchno` unique index
  not yet in migrations" is.
- Never end a session with a failing test suite. Revert instead, and say so here.

### Entry template

```markdown
## Day N — YYYY-MM-DD — <phase / slice>

**Done**
-

**Verified** (commands run and their results)
-

**Half-finished**
-

**Blocked**
-

**Next action**
-
```

---

## Day 5 — 2026-08-01 — Go verified + CI (plan day 13)

Cleared the three-session "never compiled" debt first, because CI needs a working Go build
anyway and shipping an unvalidated workflow would have been the same mistake twice.

**Done**

- Installed **Go 1.26.5** locally — extracted to the scratchpad, **no sudo required**.
- Compiled, vetted, gofmt'd and **tested** the backend for the first time.
- Added test coverage: `internal/auth`, `internal/http` (menu + router),
  `internal/platform/config`.
- Wrote `.github/workflows/ci.yml` — three jobs: Go, Web, i18n.

**Verified — every CI step run locally before committing the workflow**

```
Go     gofmt  PASS   build  PASS   vet  PASS   test -race  PASS
Web    typecheck  PASS   build  PASS
i18n   app catalogue  PASS   extracted catalogue  PASS   reproducible  PASS
```

Server exercised end to end on :18080 — `/api/health` 200,
`/api/auth/me` returns 48 permissions across 7 menu sections with the
`permissionKey` / `labelKey` separation intact on the wire.

**Bug found by running it, not by reading it**

`GET /api/nope` returned Go's stdlib `404 page not found` **as plaintext**. The client
parses JSON to read the error code, so a plaintext body made `res.json()` throw and
degraded *every* unmatched route to `error.UNKNOWN` instead of `error.NOT_FOUND`.

The code-not-prose contract has to hold for misses as well as hits. Fixed with a catch-all
route returning the standard envelope; pinned by `TestNotFoundReturnsErrorCodeNotProse`.

Worth noting the compiler had nothing to say about this. It compiled, vetted and gofmt'd
clean for three sessions with the bug present — only running it surfaced it.

**Tests worth calling out**

- `TestSetDenyByDefault` — an absent permission key grants neither read nor write (§9's
  deny-by-default requirement, previously untested).
- `TestFilterMenuDropsChildlessParents` — a section whose children are all denied
  disappears, rather than expanding onto nothing.
- `TestMenuKeysAreDistinctInKind` — `LabelKey` must be ASCII under `menu.`, `PermissionKey`
  must not equal it. Fails loudly if anyone ever swaps the two.
- `TestProdRequiresDSN` — prod without a DSN refuses to start. A security test, not a
  config test: the legacy app shipped the `sa` password to every workstation.

**CI design note**

The i18n job re-runs the extractor and fails on a diff. That catches two different things
with one check: the legacy C# source changed, or someone hand-edited generated output.
Both need a human; neither should reach a screen silently.

**Half-finished**

- Go lives in the **scratchpad**, which is session-scoped. CI is unaffected (it provisions
  its own), but a future local session may need to re-extract it.
- CI has **not run on GitHub yet** — validated locally only. First push to `develop` will
  be the real test.

**Blocked**

- **Phase 0 day 1** — export against `csm` on `R-SEVEN64`. Still the only external blocker.

**Next action**

Plan day 4: convert the 130 C# sources to UTF-8 into a reference tree, detecting encoding
per file (84 GB18030 / 39 UTF-8-BOM / 7 UTF-8).

---

## Day 4 — 2026-07-31 — Phase 1 days 16–18 (English catalogue)

**Done**

- Drafted `docs/i18n/en.extracted.json` — **all 370 keys** translated from the extracted
  zh-CN catalogue.
- Built `tools/extract-strings/verify.mjs` — four checks, ordered by how badly each fails
  at runtime: placeholder integrity, key parity, empty values, untranslated values.
- Added **concatenation-fragment detection** to the extractor →
  `docs/i18n/CONCATENATIONS.md`.

**Verified**

```
zh-CN.extracted.json -> en.extracted.json   370 keys
  PASS placeholders  PASS parity  PASS empty  PASS untranslated   exit=0
web/src/locales/zh-CN.json -> en.json        90 keys
  PASS all four                                                   exit=0
```

**The verifier immediately caught a real error of mine**

`statisCarSaleQuarter.tsbTitleText` = `"年 "` — I had translated it to a bare space.
Chasing it down found the actual problem:

```csharp
tsbTitleText.Text = "     " + year + "年 " + quarter + "任务汇总";
```

That is one sentence assembled from fragments. Translating fragments independently cannot
work in general — word order differs between languages. So the fix was not the value; it
was detecting **every** occurrence of the pattern.

**Concatenation audit — 59 call sites, 18 unique fragments**

| Shape | Sites | Risk |
|---|---:|---|
| `"label：" + value` — label is a complete string | 51 | Safe. Translate independently |
| **Sentence split across 2+ fragments** | **8** | **Reordering risk — must merge** |

The 8 risky sites are two distinct bugs-in-waiting:

- `common.rDate` + `common.rDate_2` (`"从："` / `" - 到："`) — the date-range subtitle on
  **6 of the 11 reports**.
- `statisCarSaleQuarter.tsbTitleText` + `_2` — the quarterly-target title, ×2 sites.

Each must become a single interpolated key at port time, e.g.
`t("quarterTarget.title", { year, quarter })` → `"{{year}} {{quarter}} Target Summary"`.
Recorded with call sites and suggested shape in `docs/i18n/CONCATENATIONS.md`.

**Translation notes**

- 11 placeholder strings verified — every `{0:0}` / `{0}` / `{1}` slot survives.
- **Legacy typos left verbatim in zh-CN, corrected only in English** (fixing the Chinese is
  a separate, deliberate change): `近回系统` → 返回系统, `邮货编码` → 邮政编码,
  `发动机前吗`/`VIN前吗` → …码, `久密码失败` → 旧密码失败, `清输入时间范围` → 请输入…,
  `搜    素` → 搜索, `比列系数` → 比例系数.
- Full-width padding (`返  回`, `确  定`) dropped in English — it is visual alignment for
  CJK glyph widths, meaningless in Latin script.

**Half-finished**

- The 370-key extracted catalogue is **reference material**, not yet wired into
  `web/src/locales/`. Merging happens per-slice as screens are built, not in bulk.
- Go backend **still never compiled** — third session running. Needs a toolchain.

**Blocked**

- **Phase 0 day 1** — export against `csm` on `R-SEVEN64`. Still the only external blocker.

**Next action**

Wire `verify.mjs` into CI (Phase 1 day 13's job, currently unbuilt) so catalogue drift
fails the build rather than reaching a screen.

---

## Day 3 — 2026-07-31 — Q1 and Q2 determined

User instructed the development team to decide Q1 and Q2 rather than wait on the business.
Both were previously flagged as business calls; that instruction was given twice, so they
are now recorded as **engineering decisions with reasoning attached**, built to be reversed
cheaply.

**Done**

- **Q1 (§10.2) → D13.** Assessed as a **bug**, but ported **faithfully by default** behind
  `finance.Params.ChargeInterestPastExtension` (default `false` = legacy zero-interest).
- **Q2 (§11.2) → D14.** Compute in **`decimal`**; equivalence tolerance **±0.01 per row,
  ±0.05 per report total**. Migrated historical values preserved as-is.
- Wrote the reasoning into `DEVELOPMENT_PLAN.md` §2.1 and §2.2 as a briefing document for
  the business conversation whenever it happens.
- Phase 2 unblocked; R3 rewritten.

**Reasoning — Q1 (why "bug")**

1. `if / elif` with **no `else`** — `totalinterest` retains its initialised `0` by falling
   through. An intentional write-off would be an explicit assignment with a comment.
2. It **inverts the economics**: free period → base rate → extension rate → *zero*, at the
   point of greatest exposure.
3. **§10.9 sits adjacent** — `if (distextenddates >= 0) { // }`, an empty branch on the same
   variable. Strong sign someone began this case and stopped.

**Reasoning — Q2 (why decimal)**

- `0.9` and `0.68` are **exact in decimal, inexact in binary float** — decimal is *more
  correct at the multiplication step*, not merely tidier.
- Bit-equal float (option b) is achievable (both IEEE-754 binary64) but buys equality only
  for historical reconciliation while porting a known imprecision permanently.

**Consequences to honour in Phase 2**

- Finance equivalence assertions use tolerance, not exact equality.
- The golden dataset **must** include a past-extension vehicle, pinning behaviour in both
  flag positions.
- Add a **§10.2 quantification query** (affected vehicles + total money) — ~half a session,
  and it is what turns the eventual business conversation from hypothetical into numeric.

**Risk accepted**

D13 and D14 were made **without business input**. If the business disagrees the cost is a
config flip plus a finance golden-file re-run — about **1 session**, versus the 4–6 the
original R3 assumed. That reduction is the entire reason for the flag.

**Blocked**

- **Phase 0 day 1** — export still must run against `csm` on `R-SEVEN64`. Now the *only*
  remaining blocker, and it gates Phase 2's sizing.

**Next action**

Unchanged from Day 2: draft `en.json` from the 370-key catalogue, translating
`docs/i18n/placeholders.json` first.

---

## Day 2 — 2026-07-31 — Phase 1 day 5 (classification complete)

Picked up the Day 1 next action: clear the 45-item review list.

**Done**

- Classified all 45 remaining literals — **as extractor rules, not hand-edits**, so
  re-runs stay correct.
- Added a fourth verdict, **`DROP`**, for strings that are dead in the port rather than
  merely untranslatable.
- Added **placeholder tracking** → `docs/i18n/placeholders.json`.

**Three rule bugs the review surfaced and fixed**

| Bug | Symptom | Fix |
|---|---|---|
| `\brow\s*\[` was case-sensitive | `e.Row["eop"] = "否"` classified as UI — would have translated a value written to a DB column | `\b(?:\w+\.)?[Rr]ow\s*\[` |
| Filter rule only matched `.Filter =` | `frm.filterText = "salekind = '大客户'"` missed — a filter expression treated as a caption | added `filterText`, bare `AND x =`, and `"x = '…'"` shapes |
| Block comments not stripped | `/*"总数量 : " + */` extracted as live UI text | `stripBlockComments()`, blanking `/* … */` while preserving line numbers |

**Verified** (`node tools/extract-strings/extract.mjs`)

```
literals found : 1154        (was 1155 — the one phantom from a block comment)
  TRANSLATE    : 1023  (370 unique keys)
  NEVER        :   86  (34 unique)
  DROP         :   45  (5 unique)
  REVIEW       :    0            <- list cleared
  with {n} placeholders: 11 unique
```

- All three bug cases re-checked: `否` → NEVER, `融资车` → NEVER,
  `salekind = '大客户'` → NEVER.
- `总数量 : ` now resolves to **9 live report sites**; the commented-out instance is gone,
  and the 1155 → 1154 delta is exactly that one literal.

**Findings**

- **11 strings carry `{0:0}`-style composite-format placeholders.** These are the
  highest-risk items in the English pass — a dropped or renumbered placeholder is a
  runtime `FormatException`, not a visible typo. Split into their own file so the
  translator sees them as a distinct class.
- **`DROP` bucket (5):** the CSMEnv.ini app name (mechanism removed, §6.7) and four
  C1Preview chrome strings (概述 / 首页 / 页 / 0 页) superseded by browser-native PDF
  preview (D6). Carrying these into a locale catalogue would have meant translating and
  maintaining UI that no longer exists.

**Half-finished**

- `en.json` still not drafted — that is days 16–18, and the 370-key catalogue is now its
  input.
- Go backend **still never compiled**. Unchanged for two sessions; needs a toolchain.

**Blocked**

- **Phase 0 day 1** — export must run against `csm` on `R-SEVEN64`. Unchanged.
- **Q1 (§10.2)**, **Q2 (§11.2)** — unanswered. Both gate Phase 2.

**Next action**

Draft `en.json` from the 370-key catalogue (days 16–18), translating
`docs/i18n/placeholders.json` first and verifying every placeholder survives intact.

---

## Day 1 — 2026-07-31 — Phase 1 days 4–5 (string extraction)

Phase 0 could not run from here — see **Blocked**. Did the unblocked Phase 1 work instead.

**Done**

- Built `tools/extract-strings/extract.mjs` — decodes per-file encoding, extracts every
  Chinese literal, and **classifies** each as `TRANSLATE` / `NEVER` / `REVIEW`.
- Generated `docs/i18n/inventory.json` (full audit trail), `zh-CN.extracted.json`
  (the catalogue), `REVIEW.md` (the human-decision list).

**Verified** (`node tools/extract-strings/extract.mjs`)

```
files scanned  : 130
encodings      : gb18030 84, utf-8-bom 39, utf-8 7
literals found : 1155
  TRANSLATE    : 992  (342 unique keys)
  NEVER        :  82  (32 unique)
  REVIEW       :  81  (45 unique)
```

- Key integrity: **one key per unique value, 0 collisions, 0 unkeyed**.
- Catalogue de-bloated 992 → 342 keys (`警告` alone appeared 61×, `你是否保存更改` 32×).

**Findings worth carrying forward**

1. **Encoding is three-way, not two.** 84 GB18030, 39 UTF-8-BOM, **7 plain UTF-8**.
   §11.7 now understates it; the converter must handle all three.
2. **Classification was necessary, not optional.** Literals that look like UI but must
   never be translated:
   - `gridStore[0,c].ToString().Equals("开单日期")` — grid header **compared by value**;
     translating it breaks `FrmFinanceStore`.
   - `row["eop"] = "否"` — writes a Chinese boolean to a DB column (confirms §5.6).
   - `CAR_PURCHASE/STOREIN/STORECHANGE/STOREOUT = 车辆采购/入库/转库/出库` — §6.2
     movement states, data not UI.
3. **The NEVER list independently recovered §5.6's basedata domains** from live
   `RowFilter` expressions — `name='车系列'`, `'地区'`, `'行业'`, `'进货途径'`, … plus
   `keyname='民用'` / `'特种'`. Code-sourced confirmation of an inferred list.
4. **Legacy typo:** the back button reads `近回系统`; 近 is almost certainly a typo for
   返 (`返回系统`). Kept verbatim in zh-CN — fixing legacy typos is a separate decision.
5. **4 whitespace-variant duplicate groups** (`确  定` / `确 定` / `确定`). Collapses
   naturally during the English pass; 342 → 337 if normalised.

**Half-finished**

- `docs/i18n/REVIEW.md` — **45 unique literals unclassified**. Each needs a UI-or-data
  call, then a rule added to `RULES` in the extractor so a re-run classifies it.
- `en.json` not drafted (that is days 16–18).
- Go backend **still never compiled** — no toolchain on this machine. Unchanged from Day 0.

**Blocked**

- **Phase 0 day 1** — `mssql-export.sql` must run against `csm` on `R-SEVEN64`. No MSSQL
  client, no driver, no route from here. The script is reviewed and hardened; ready to run.
- **Q1 (§10.2)** and **Q2 (§11.2)** — still unanswered. Both gate Phase 2.

**Next action**

Clear the 45-item review list in `docs/i18n/REVIEW.md`, encoding each decision as a rule
in the extractor rather than editing output by hand.

---

## Day 0 — 2026-07-31 — Pre-Phase-0 setup

**Done**

- Reviewed [GO_MIGRATION_PLAN.md](GO_MIGRATION_PLAN.md) (the analysis) end to end.
- Locked the stack: **Go** backend, **TypeScript + React** frontend, decimals as JSON
  strings, TanStack Table, HTML→PDF via headless Chrome.
- Locked i18n scope: **English + Chinese UI**; the 11 formal reports stay **Chinese-only**
  (D11); UI serves internal *and* external users (D12); **no database i18n** — locale
  lives in `localStorage`, `tbl_basedata` values stay Chinese.
- Wrote [DEVELOPMENT_PLAN.md](DEVELOPMENT_PLAN.md): 12 locked decisions, 5 open questions,
  **161 sessions** (~7.5 months at 4 hrs/day × 5 days/week), risk register, session protocol.
- Built the app skeleton — Go backend (`cmd/`, `internal/`) and React frontend (`web/`),
  with i18n, nav, login screen and language switcher.
- Hardened [mssql-export.sql](mssql-export.sql): added a non-sqlcmd route for steps 1–3,
  and six new data probes (steps 11–16).

**Verified**

- `npm run build` in `web/` — passes under strict TS; 61 modules, 237 kB bundle.
- `npm run dev` — `GET /` → 200, `GET /src/main.tsx` → 200.
- Locale key parity — **90 / 90**, zero gaps, zero empty values.
- All 48 Go `LabelKey` values resolve against the catalogue.
- **49 permission keys match legacy `FrmMDIMain` exactly** — zero drift, checked by
  decoding the GB18030 source and diffing against `internal/auth/permission.go`.

**Three plan corrections, measured against the source**

1. UI strings are **not** in the 64 `.resx` files (those hold only bitmaps/icons/colors).
   They are **1,156 literals hardcoded in `.cs` / `.Designer.cs`**, across 123 of 130 files.
2. Source encoding is **mixed** GB18030 / UTF-8-with-BOM, not uniformly GB18030.
3. `tbl_dbbackup_log` **does** have a UI — `数据备份` is live under `系统设置`. §11.8 said
   otherwise.

Tooling caveat that caused #1 and #2 to go unnoticed: `grep -P '[\x80-\xff]'` silently
returns 0 on these files. Use `perl` / `python` / `iconv` for byte-range detection.

**Half-finished**

- Go backend is **written but never compiled** — no Go toolchain on this machine.
  Install Go and verify with `go build ./...` and `go vet ./...`.
- `/api/auth/me` is a stub: grants every permission at `读写`, no session.
- The login form does not authenticate. Both land in slice 1 (days 41–49).

**Blocked**

- **Phase 0 day 1** — `mssql-export.sql` must run against `csm` on `R-SEVEN64`. That host
  is on the user's LAN; this environment has no MSSQL client, no driver, no route.
- **Q1 (§10.2)** — is zero interest past the extension window intended? Blocks Phase 2.
- **Q2 (§11.2)** — decimal-with-tolerance or bit-equal float? Blocks Phase 2.

**Next action**

Run the export (day 1). The gate: **28 proc files, 5 view files**. While skimming, capture
the total proc line count — Phase 2's 22-session estimate assumes ~1,400 lines total, and
that number re-sizes the whole plan.
