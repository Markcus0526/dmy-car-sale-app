# Session Log

Working history and end-of-session state. **Read the most recent entry before starting
work; append a new one before finishing.** See [DEVELOPMENT_PLAN.md](DEVELOPMENT_PLAN.md)
§11 — this is the 3:50–4:00 slot of every session.

Newest entry first.

---

## Where things stand

*Updated end of Day 12. Read this first; the entries below are the detail.*

| | |
|---|---|
| **Phase** | Phase 1 (foundation) in progress. **Phase 0 not yet run** |
| **Sessions logged** | 13 (Day 0–12) |
| **Plan** | [DEVELOPMENT_PLAN.md](DEVELOPMENT_PLAN.md) — 161 sessions, 17 locked decisions |
| **Next action** | Slice 1 is functionally complete. Next is slice 2 (reference data) or slice 3 (shared filter+grid, which gates 4–8). **Phase 1 proper is blocked on Phase 0** |

**Green — verified and repeatable**

- **Go backend compiles, vets and tests clean under `-race`.** Go 1.26.5 installed to the
  scratchpad (no sudo needed): `export PATH="$SP/go/bin:$PATH"`.
- `web/` builds under strict TS; dev server serves; app catalogue 90/90.
- String extraction: 1,447 literals → 1,316 TRANSLATE (**472 keys**) / 86 NEVER / 45 DROP /
  **0 REVIEW**. Covers `.cs` literals *and* `.resx` grid captions.
  Re-runnable: `node tools/extract-strings/extract.mjs`.
- English catalogue: **472/472** translated, `verify.mjs` exit=0 on both catalogues.
- Encoding conversion lossless: 201 files, 5,435 CJK codepoints, 0 failures.
  `node tools/convert-encoding/convert.mjs` (add `--check` to verify only).
- **Migrations apply to real MySQL 8.4** — 15 tables, 206 columns, 5 FKs, down leaves 0,
  re-apply works. `row_version` blocks stale writes; utf8mb4 stores 4-byte chars intact.
- **Legacy DES password verification matches two independent implementations**
  (python-cryptography, openssl legacy provider) byte-for-byte.
- **`auth.Service.Login`** — 22 tests: bcrypt path, legacy path + upgrade write, upgrade
  failure does not fail login, unknown user indistinguishable, repo errors propagate.
- **Migration 0003** (`password_bcrypt` + username index) verified on MySQL 8.4, down included.
- **Migration 0004** (`tbl_session`) verified: unique token hash, FK guard, ON DELETE CASCADE.
- **`auth.SessionService`** — idle + absolute timeouts, throttled touch, idempotent revoke,
  immediate `RevokeAllForUser`.
- **`store/mysql`** — 12 integration tests against real MySQL: Chinese round-trip, NULL
  handling, atomic upgrade advancing `row_version`, unknown permission levels denying,
  Shanghai wall-clock `DATETIME` round-trip.
- **Working end-to-end login** — `POST /api/auth/login` → HttpOnly cookie → `GET /api/auth/me`
  → menu filtered by real permissions. `POST /api/auth/logout` revokes server-side.
- 49 permission keys match legacy `FrmMDIMain` exactly.
- **`./scripts/check.sh`** — every check in one command; `--db` adds the MySQL migration and
  integration checks. **No CI**: GitHub Actions was removed by request, so nothing runs
  automatically. Run this before committing.

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

## Day 12 — 2026-08-01 — Login UI, and CI replaced by a local script

**Done**

- React login wired to the real API: `POST /api/auth/login`, session cookie, `GET /api/auth/me`,
  sign-out.
- **GitHub Actions removed by request.** Replaced with `./scripts/check.sh`.

**The script immediately caught a bug the workflow never could**

`0001_init.down.sql` left one table behind. `tbl_session` belongs to **0004**, and each
down file undoes only its own migration — so rolling back requires reverse order
(0004 → 0003 → 0002 → 0001), which is what `golang-migrate` does.

Both my check *and* the deleted workflow made the same wrong assumption: that 0001's down
undoes everything. **The workflow would have failed on its first real run** — it never ran,
because nothing was ever pushed. Removing CI in favour of a script I actually execute
surfaced this within minutes.

**And a second one underneath it**

`0002_foreign_keys.down.sql` was a `TODO` that did nothing. A rollback would have reported
success while leaving every foreign key in place. It is now generated from 0002's own
`ALTER TABLE` statements, reversed, so the pair cannot drift.

A down migration that silently no-ops is worse than an absent one: absence is visible,
a no-op looks like success.

**Verified**

```
./scripts/check.sh --db
  Go        gofmt build vet test-race        PASS
  Web       typecheck build                  PASS
  i18n      encoding, both catalogues, reproducible   PASS
  Migrations 16 tables, row_version 15, 6 FKs, bcrypt(60),
             non-unique username idx, full rollback 0, re-apply 16,
             4 behavioural assertions        PASS
  Store     integration vs MySQL 8.4         PASS
```

**Frontend design points**

- `logout()` never rejects: a user who clicked sign out must end up signed out in the UI
  even if the request failed. The server-side revoke is what actually ends the session.
- On load the app resolves an existing cookie; **401 is the normal "not signed in" answer**,
  not an error screen. Anything else gets a retry.
- Login errors stay inside the form rather than replacing the screen — a wrong password
  should not discard what was typed.
- The 编码 (code) field is kept because users recognise the form, but it is not sent:
  `FrmLogon.cs:71-76` authenticates on username + password only.

**Consequence of removing CI — worth being clear about**

Nothing gates a push now. The checks are all still there and all still pass, but they only
run when someone runs them. `./scripts/check.sh` before committing is now a discipline
rather than a guarantee, in the same way the session log is.

**Blocked**

- **Phase 0 day 1** — export against `csm` on `R-SEVEN64`.

**Next action**

Slice 1 is functionally complete. Either slice 2 (reference data) or slice 3 (shared query
filter + grid) — slice 3 gates slices 4–8, so it is the higher-leverage choice.

---

## Day 11 — 2026-08-01 — store/mysql + working login (slice 1)

First genuinely working authentication: login issues a session, `/me` resolves it, logout
kills it, and the menu is filtered by real permissions from `tbl_permission`.

**Done**

- `internal/store/mysql` — `Open` (charset-verified), `UserRepo`, `SessionRepo`.
- `internal/http` — `POST /api/auth/login`, `POST /api/auth/logout`, `GET /api/auth/me`
  behind `requireAuth`, plus `requirePermission` for future routes.
- `cmd/carsaleman` wires it all when a DSN is present.
- CI runs the store integration tests against the MySQL service container.

**Verified**

```
go test -race ./...                     all packages pass
store integration (real MySQL 8.4)      12/12
http                                    all pass
gofmt / vet / web build / encoding / catalogues   PASS
```

**Three bugs found, one of them real**

1. **`defer s.clearSessionCookie(w)` in logout never cleared the cookie.** The deferred
   call runs *after* `WriteHeader` has flushed the headers, and `http.SetCookie` is
   silently a no-op at that point — no error, no warning. Logout appeared to work while
   leaving the cookie in place. Now: revoke, set the cookie, *then* write the status.

   Worth noting the server-side revoke was always correct, so this was a tidiness bug
   rather than a security hole — but only because revocation does the real work. On a JWT
   design the same mistake would have been a genuine failure to log out, which is a small
   argument in favour of D17 that I had not anticipated.

2. **`requireAuth` panicked on a nil service.** With no DSN configured, every authenticated
   route dereferenced nil and the recover middleware turned it into a generic 500. My own
   comment claimed it would "fail loudly" — an unhandled panic is not a good loud failure.
   Now returns `503 SERVICE_UNAVAILABLE`, which tells an operator it is a missing DSN
   rather than a bug.

3. Two `/me` tests were asserting 401 against a server with no services wired, which is
   really the 503 case. Split into a proper unconfigured-server test plus auth tests on a
   fully wired server.

**Decisions inside the implementation**

- **`FindByUsername` uses `ORDER BY uid LIMIT 1`.** The username index is deliberately
  non-unique (0003) because the legacy data has never been constrained. Duplicates must
  resolve deterministically rather than failing someone's login.
- **`UpgradePassword` omits the `row_version` guard.** It is an idempotent credential
  upgrade triggered by a successful login, not a user edit — two concurrent logins both
  write a valid hash for the same password, and returning 409 to one would be worse than
  last-write-wins.
- **`LoadPermissions` returns an empty set, never nil.** A nil map invites being read as
  "unrestricted".
- **`Open` verifies the connection charset** rather than trusting the DSN. Getting this
  wrong is silent: 4-byte characters are mangled with no error.
- **`loc=Asia/Shanghai`, not UTC.** I had written UTC first. Every legacy `DATETIME` was
  written by a Windows client in Chinese local time, so reading them as UTC would shift
  every migrated date by 8 hours and break report equivalence (§9) — the acceptance
  criterion for the whole port. Pinned by a round-trip test.
- **`SameSite=Lax`, not Strict.** Strict drops the cookie when a user arrives via an
  external link, logging them out for no gain here; Lax still blocks the cross-site POST
  CSRF depends on.

**Test-time note**

The HTTP package built a bcrypt hash per test at production cost (12), which took 107s.
Now uses a precomputed cost-4 hash: 31s. `auth.TestHashPasswordUsesProductionCost` still
guards the real constant. Cross-package cost control is awkward because `hashCost` is
unexported — acceptable now, worth revisiting if more packages need it.

**Half-finished**

- The React app still calls `/api/auth/me` directly on load and has no login submission —
  the UI has not caught up with the API.
- `requirePermission` exists but no route uses it yet; the first will be a real CRUD slice.

**Blocked**

- **Phase 0 day 1** — export against `csm` on `R-SEVEN64`.

**Next action**

Wire the React login form to `POST /api/auth/login`, handle 401 by showing the form, and
drive nav from the real `/api/auth/me`.

---

## Day 10 — 2026-08-01 — D17 sessions (slice 1)

Asked to decide the session strategy myself, as with Q1/Q2.

**D17 — opaque server-side sessions, not JWT**

The deciding factor is specific to this system: there is a permission-administration screen
(`权限设定`). With a JWT, revoking a user's access does not take effect until the token
expires — the user keeps working with permissions an administrator has already removed.
**That is precisely the §10.8 failure this port exists to fix, relocated from the client to
the token.** Fixing permission enforcement and then making it un-revokable defeats itself.

The usual argument for JWT is horizontal scale. A few dozen users on one server has no
scale argument to answer.

Supporting choices:

- **HttpOnly cookie, not an `Authorization` header.** An SPA holding a token in
  `localStorage` is the classic XSS → account-takeover path; that weighs more now external
  users are in scope (D12).
- **The token is stored hashed** (SHA-256). A database leak should not yield usable
  sessions — the same reasoning that puts bcrypt on the password column. SHA-256 rather
  than bcrypt because the input is 256 bits of CSPRNG output: there is no low-entropy
  secret to slow an attacker over, and this runs on *every* request.
- **Permissions load per request**, never baked into the session, so a change takes effect
  on the next call.
- **Two clocks**: idle (8h, refreshed on use, so nobody is logged out mid-task) and
  absolute (24h, un-extendable, bounding a stolen cookie).

**Done**

- `migrations/0004_sessions.{up,down}.sql`
- `internal/auth/session.go` — issue, validate, revoke, revoke-all, sweep.
- 11 session tests (33 in the package).
- CI covers 0004 and asserts the session invariants.

**Verified on MySQL 8.4**

```
tbl_session          8 columns, char(64) hash, varchar(45) ip (IPv6 fits)
uq_token_hash        duplicate rejected
fk_userinfoid        orphan session rejected
ON DELETE CASCADE    deleting the user removed the session
0004 down            table gone
```

`ON DELETE CASCADE` is deliberate and is the **only** cascade in this schema: a deleted user
must not leave live sessions. Everywhere else a delete should fail loudly rather than
propagate.

**Two mistakes I made and caught**

1. **A CI ordering bug.** The new sessions step inserts a user and deletes it; the following
   `row_version` step then did `WHERE uid=1`. AUTO_INCREMENT had already moved past 1, so
   that update would match nothing and the assertion would fail *for the wrong reason* —
   a red build blamed on optimistic concurrency when the real cause is test coupling. Both
   steps now use `LAST_INSERT_ID()`. Confirmed by replaying the two steps in order.
2. **D17 was inserted above D16** in the decisions table. Reordered.

**Also**

MySQL's Docker entrypoint runs a *temporary* server during init, then stops it and starts
the real one. A single successful `SELECT 1` can land on the temporary server and be
followed by a restart — which is what caused an `ERROR 2002 socket` on the first attempt.
Local checks now require the connection to hold for five consecutive seconds. CI is
unaffected (the service container has its own health gate), but any local script needs this.

**Half-finished**

- `UserRepository` and `SessionRepository` still have no implementation. `store/mysql` is
  **not** blocked by Phase 0 — the MySQL schema exists and is verified; only
  `cmd/migrate-data` needs the live MSSQL. That is the next step.
- No HTTP endpoint yet.

**Blocked**

- **Phase 0 day 1** — export against `csm` on `R-SEVEN64`.

**Next action**

Implement `store/mysql` for `UserRepository` and `SessionRepository`, tested against the
Docker MySQL, then wire `POST /api/auth/login`.

---

## Day 9 — 2026-08-01 — Login service + password_bcrypt migration (slice 1)

**Done**

- `migrations/0003_password_bcrypt.{up,down}.sql` — the column plus a username index.
- `internal/auth/service.go` — `Login` over a `UserRepository` interface, so the whole flow
  is testable without a database.
- 7 new service tests (22 in the package).
- CI migrations job extended to cover 0003 and its independent rollback.

**Verified against MySQL 8.4**

```
apply 0001 + 0002 + 0003   OK
password_bcrypt            varchar(60) NULL, after `password`
idx_tbl_userinfo_username  non_unique = 1
60-char bcrypt hash        stored intact
0003 down                  column 0, index 0
0003 re-apply              OK
```

**Design decisions**

- **`VARCHAR(60)` exactly.** A bcrypt hash is always 60 chars; a wider column invites
  storing something that is not one. CI asserts the width.
- **The username index is deliberately NOT unique.** The legacy data has never been
  constrained, so duplicates may exist. `cmd/migrate-data` reports them; promoting to
  unique is a follow-up once the data is known clean. CI asserts `non_unique = 1` so a
  well-meaning "tidy-up" cannot quietly break the import.
- The index exists at all because the legacy app selected the whole table and filtered
  client-side (§2.4) — so no index was ever needed. The port issues a real `WHERE`.
- **A failed upgrade write does not fail the login.** The user proved they know the
  password; turning a storage blip into a lockout would be worse than retrying next login.
- **Unknown user and wrong password are indistinguishable**, including in latency — a
  missing user still burns one bcrypt comparison. Otherwise login is a username oracle.
- **Repository errors are not authentication errors.** A dropped connection must surface as
  a 500, never as "bad password".
- `0003` is hand-written and must not be regenerated by `gen.mjs` — it describes the target
  schema, not the legacy one.

**Two harness bugs I hit, both worth remembering**

1. **MySQL's init phase answers `mysqladmin ping` before the root password exists.** My
   readiness loop exited early and every statement failed with `Access denied`. Wait on an
   *authenticated query*, not on ping.
2. **My check reported `OK` for failed commands** — `mysql … | grep -v Warning` returns
   *grep's* status, not mysql's. Every migration "passed" while erroring. Fixed by
   capturing mysql's exit code before filtering, and confirmed with a negative control
   (a query against a non-existent table now fails correctly).

The second is the more dangerous pattern: a green check that cannot go red. It is the same
class of mistake as the CI step on Day 7 that printed `FAIL` without failing.

**Half-finished**

- No HTTP endpoint yet. `POST /api/auth/login` needs a **session strategy decision** first
  (§3 lists "session/JWT"); making that call mid-session would be scope creep.
- `UserRepository` has no implementation — `store/mysql` arrives with Phase 1 day 8–11,
  which is blocked.

**Blocked**

- **Phase 0 day 1** — export against `csm` on `R-SEVEN64`.

**Next action**

Decide the session strategy (signed cookie vs JWT), then wire `POST /api/auth/login` and
`GET /api/auth/me` to the real service.

---

## Day 8 — 2026-08-01 — Password migration (§6.5, slice 1 groundwork)

Phase 1 is blocked on Phase 0, so took the highest-risk unblocked item instead. Get this
wrong and **nobody can log in on cutover day**, and the failure presents as "bad password"
rather than as a bug.

**Done**

- `internal/auth/password.go` — exact reproduction of the legacy DES scheme, bcrypt hashing,
  and the dual-path `Verify` from §6.5.
- 12 tests, including cross-implementation vectors.

**Legacy scheme, recovered from the now-readable reference tree**

```
key    "123456ABCDEF"[:8] = "123456AB"   (UTF-8 bytes)
iv     12 34 56 78 90 AB CD EF            (Global.DESKeys, hardcoded)
cipher DES-CBC + PKCS#7                   (DESCryptoServiceProvider defaults)
output Base64
```

The plan's §6.5 said the key was `"123456AB"`; the constant is actually `"123456ABCDEF"`
and `DESEncode` truncates with `Substring(0, 8)`. Same effective key, but the plan would
have misled anyone implementing from it directly.

`FrmLogon.cs:71-76` encrypts what was typed and **string-compares the ciphertext**. With a
fixed IV that makes it a deterministic, unsalted, *invertible* transform whose key is
published in the source. Anyone holding the database holds every plaintext password — and
since people reuse passwords, the blast radius is not limited to this application.

**Verification — the part that matters**

A round-trip test would pass even with the wrong key, IV or padding, because it would be
wrong consistently. So the vectors come from **two independent implementations**, neither
of which is the code under test:

- `python-cryptography` (3DES with K1=K2=K3, which is mathematically single DES — its
  public API no longer exposes plain DES)
- `openssl enc -des-cbc -provider legacy`

Both agree byte-for-byte with each other, and Go matches both. .NET's
`DESCryptoServiceProvider` defaults to CBC + PKCS#7, so agreement with two
standards-conformant implementations is agreement with the legacy app.

Node was tried first and could not do it: OpenSSL 3 moved DES to the legacy provider, so
`crypto.createCipheriv("des-cbc", …)` fails with `ERR_OSSL_EVP_UNSUPPORTED`.

Vectors cover empty input, an exact block multiple (full extra padding block), UTF-8
multibyte (`张三`), and mixed CJK+ASCII (`口令123`).

**Design points**

- `Verify` returns `(ok, needsUpgrade)`. Storage concerns stay in the caller, so the whole
  thing is testable without a database.
- **bcrypt takes precedence over legacy even when both are populated** — otherwise clearing
  the legacy column becomes load-bearing for security rather than merely tidy.
- A missing credential still burns one bcrypt comparison, so a non-existent user is not
  distinguishable from a wrong password by response latency.
- Constant-time compare on the legacy path.
- `BcryptCost = 12`, above the default 10.

**Test time: 52s → 1.3s**

Cost 12 under `-race` is ~14s per call; four such tests put a minute of pure key stretching
into every CI run. `export_test.go` lowers the cost for the test binary only, and
`TestHashPasswordUsesProductionCost` asserts the production constant so the override cannot
quietly become the real setting.

**Note**

`go get` bumped the language version 1.22 → 1.25 as a side effect of adding
`golang.org/x/crypto`. CI reads `go-version-file: go.mod`, so this is consistent — but it
is a real change and was not asked for.

**Blocked**

- **Phase 0 day 1** — export against `csm` on `R-SEVEN64`.

**Next action**

Slice 1 continues: `tbl_userinfo.password_bcrypt` column (a new migration), then the login
handler wiring `Verify` to storage and performing the upgrade-on-success write.

---

## Day 7 — 2026-08-01 — Initial migrations (plan days 6–7)

**Done**

- `tools/gen-migration/gen.mjs` — generates the migrations from
  `docs/mysql/schema.sql`. Generated rather than hand-copied: 15 tables of DDL
  transcribed by hand reliably produces silent column-type errors, and a wrong
  `DECIMAL` scale is not something a later test would catch.
- Four files: `0001_init.{up,down}.sql`, `0002_foreign_keys.{up,down}.sql`.
- **Applied and exercised against real MySQL 8.4** in Docker.
- CI gains a `migrations` job with a MySQL service container (plan day 13's remaining item).

**Two decisions locked — both change every table**

**D15 — optimistic concurrency: `row_version INT UNSIGNED NOT NULL DEFAULT 1`**

Legacy compared every original column plus `@IsNull_*` flags on each UPDATE/DELETE (§2.5);
a naive `WHERE uid = ?` would turn today's concurrency violations into silent overwrites
(§11.4).

- *Rejected `updated_at`*: two updates inside one clock tick are indistinguishable, and
  clock skew across app instances makes it worse. A counter has neither problem.
- *Rejected whole-row comparison*: fragile around NULL handling and DECIMAL equality, and
  produces enormous WHERE clauses for no gain over a counter.
- *Deliberately did NOT add `created_at`/`updated_at`*: there is no source data for them,
  so every migrated row would claim to have been created at migration time. Fabricated
  timestamps in a financial system are worse than absent ones.

**D16 — collation: storage stays `utf8mb4_unicode_ci`; display order in Go via
`x/text/collate`**

Legacy sorts under `Chinese_PRC_CI_AS` (pinyin). MySQL's closest analogue,
`utf8mb4_zh_0900_as_cs`, is accent- **and case-sensitive** — which would silently change
equality semantics for permission values (`读写`) and the `是否*` columns the app compares
by value. **Changing sort order is a display bug; changing equality is a correctness bug.**
So: keep case-insensitive equality, sort for display in the application.
`ORDER BY carseries` in SQL is *not* authoritative for display order.

**Foreign keys deliberately split into 0002**

§5.3 requires validating for orphans *before* adding constraints, and that cannot happen
until `cmd/migrate-data` has run. A single migration that fails halfway through adding
constraints leaves a half-constrained schema. Load → verify → constrain.

**Verified against MySQL 8.4.11 (not just "it parses")**

```
0001 up        15 tables, 206 columns (191 source + 15 row_version)
0002 up        5 FKs, including the implied tbl_storechange.onroadid
collation      utf8mb4_unicode_ci on every table
0001 down      0 tables left
re-apply       clean
generator      191/191 source columns preserved, exactly 1 row_version per table
```

Behavioural, not structural:

- fresh update with the correct `row_version` → 1 row affected
- **stale update with a held version → 0 rows** — the lost write is prevented, and the
  handler turns that into `409 Conflict`
- FK rejects an orphan: `ERROR 1452 … fk_tbl_permission_userinfoid`
- utf8mb4: `测试𠮷` → `chars=3 bytes=10 hex=E6B58BE8AF95F0A0AEB7`

**A near-miss worth recording**

The first utf8mb4 check reported `CHAR_LENGTH = 10` instead of 3, which reads exactly like
broken 4-byte storage. It was not: nested shell quoting had lost the client charset. Re-run
via stdin with `--default-character-set=utf8mb4`, it was correct.

The lesson is not about the test. **`cmd/migrate-data` must set the connection charset
explicitly** — a client that negotiates a narrower charset mangles 4-byte characters on the
way in, silently, with no error. That is a data-corruption bug that would surface months
later in a report. Written into the CI job as a comment so it is not re-learned.

**CI note**

The behavioural step originally printed `FAIL` without failing the build — a test that
cannot fail. Rewritten to grep the output and exit non-zero, and validated with a negative
control (an injected `FAIL` line is caught).

**Blocked — and Phase 1 has now run out of unblocked work**

- **Phase 0 day 1** — export against `csm` on `R-SEVEN64`.

Everything remaining in Phase 1 depends on it: day 8–11 (`cmd/migrate-data`) needs both the
reconciled schema *and* a live MSSQL to read from; the `TODO(phase0)` markers in
`0001_init.up.sql` (defaults, indexes, checks, collation) cannot be resolved without the
dump. Non-PK indexes are the most consequential of those — each one is a query the old
system relied on, and rediscovering them under production load is the expensive route.

**Next action**

Run the export. Gate: **28 proc files, 5 view files**. Then re-run
`node tools/gen-migration/gen.mjs` against the reconciled `schema.sql`.

---

## Day 6 — 2026-08-01 — UTF-8 reference tree (plan day 4)

**Done**

- `tools/convert-encoding/convert.mjs` — per-file encoding detection, lossless conversion
  to `reference/` (gitignored; regenerates in under a second). Original tree untouched.
- Extractor extended to scan `.resx` grid captions.
- Catalogue **370 → 472 keys**, all translated.
- `GO_MIGRATION_PLAN.md` §11.7 rewritten — for the second time, see below.

**Verified**

```
conversion   201 files   5,435 CJK codepoints   0 failures
             decoded original === converted file, exactly
             original tree: 0 modified files
catalogue    472/472      verify exit=0
CI (local)   Go PASS   Web PASS   i18n PASS (incl. new --check step)
```

**Correction — and this one was mine to begin with**

Day 1 recorded that `.resx` files contain zero Chinese. That was measured with the
byte-range grep I later proved unreliable, and it was wrong. Being able to plain-grep the
UTF-8 tree exposed it within a minute:

> **27 of 63 `.resx` files embed a serialized C1FlexGrid `ColumnInfo` blob containing 144
> Chinese column captions — 116 of which appear nowhere in the `.cs` sources.**

These are the column headers on **every list screen**. Without them each grid would have
rendered Chinese headers inside an English UI — a whole category of missing translation,
invisible until someone opened a grid.

The `Name` field inside the blob gives a far better key stem than anything derived from
caption text: `common.col_inprice` rather than a slug.

Worth drawing the lesson out: the reference tree paid for itself immediately, and not for
the reason it was scheduled. It was planned as a *readability* aid for porting; it earned
its keep as an *auditing* aid, by making a bad measurement obvious.

**Key stability — a real weakness, caught by the verifier**

Adding a new source shifted value frequencies, so 18 values were promoted to `common.col_*`
keys and their previous keys were orphaned. `verify.mjs` reported `missing: 0, extra: 18` —
no translation lost, 18 stale keys pruned.

Keys are **not stable** across extractor changes. That is acceptable while the catalogue is
reference material, but once keys are referenced from `web/src/locales/` a rename becomes a
breaking change. The verifier is what makes this safe; do not weaken it.

**Also noted**

- The finance grid columns confirm §6.1's computed fields by name — `距离展期天数`,
  `距离免息天数`, `累计利息`, `货款金额`, `货款余额`, `全额还款到期日期`. Independent
  corroboration of the interest engine's outputs, from the UI side.
- More legacy typos, and the `.resx` sometimes has it *right* where the `.cs` has it wrong:
  `.resx` says `发动机前码` (correct 码) while the `.cs` label says `发动机前吗`.
  Also new: `转库日其` (should be 期).

**Blocked**

- **Phase 0 day 1** — export against `csm` on `R-SEVEN64`. Still the only external blocker.

**Next action**

Plan days 6–7: draft `migrations/0001_init.up.sql` from `docs/mysql/schema.sql`, explicitly
marking every field Phase 0 must confirm (defaults, indexes, checks, collation) so the gaps
are visible rather than silently guessed.

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
