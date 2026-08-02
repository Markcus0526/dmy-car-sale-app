#!/usr/bin/env bash
#
# Run every project check locally.
#
# This replaces the GitHub Actions workflow, removed by request. The checks
# themselves are unchanged -- they encode findings that were expensive to
# discover and are noted inline below. Nothing runs automatically now, so run
# this before committing.
#
#   ./scripts/check.sh          # everything except the database checks
#   ./scripts/check.sh --db     # also start MySQL in Docker and verify migrations
#
# Requires: go, node. --db additionally requires docker.

set -uo pipefail
cd "$(dirname "$0")/.."

WITH_DB=0
[[ "${1:-}" == "--db" ]] && WITH_DB=1

FAILED=0
pass() { printf '  \033[32mPASS\033[0m  %s\n' "$1"; }
fail() { printf '  \033[31mFAIL\033[0m  %s\n' "$1"; FAILED=1; }
step() {
  local name="$1"; shift
  if out=$("$@" 2>&1); then pass "$name"; else fail "$name"; echo "$out" | tail -15 | sed 's/^/        /'; fi
}
section() { printf '\n\033[1m%s\033[0m\n' "$1"; }

# ---------------------------------------------------------------------------
section "Go"

if ! command -v go >/dev/null; then
  fail "go is not installed"
else
  if [[ -z "$(gofmt -l . 2>/dev/null)" ]]; then pass "gofmt"; else fail "gofmt"; gofmt -l . | sed 's/^/        /'; fi
  step "go build"      go build ./...
  step "go vet"        go vet ./...
  step "go test -race" go test -race ./...
fi

# ---------------------------------------------------------------------------
section "Web"

if ! command -v npm >/dev/null; then
  fail "npm is not installed"
else
  [[ -d web/node_modules ]] || (cd web && npm ci >/dev/null 2>&1)
  step "typecheck" bash -c 'cd web && npm run typecheck'
  step "build"     bash -c 'cd web && npm run build'
fi

# ---------------------------------------------------------------------------
section "i18n and encoding"

# Every legacy source must decode with no U+FFFD and an unchanged CJK
# codepoint sequence. Encoding is mixed (gb18030 / utf-8-bom / utf-8), so a
# blanket iconv would corrupt the files that are already UTF-8.
step "encoding lossless"   node tools/convert-encoding/convert.mjs --check

# Placeholder integrity matters most: a dropped {0:0} is a runtime
# FormatException, not a visible typo.
step "app catalogue"       node tools/extract-strings/verify.mjs web/src/locales/zh-CN.json web/src/locales/en.json
step "extracted catalogue" node tools/extract-strings/verify.mjs docs/i18n/zh-CN.extracted.json docs/i18n/en.extracted.json

# A wired screen id that does not exist in menu.go fails silently: the
# condition never matches and the route renders PlaceholderPage, so the screen
# just looks unbuilt. Nothing else catches it.
step "screen routes wired"  node tools/check-routes/check.mjs

# A diff means either the legacy source changed or someone hand-edited
# generated output. Both need a human.
node tools/extract-strings/extract.mjs >/dev/null 2>&1
if git diff --quiet -- docs/i18n/zh-CN.extracted.json; then
  pass "extraction reproducible"
else
  fail "extraction reproducible — re-run tools/extract-strings/extract.mjs and commit"
fi

# ---------------------------------------------------------------------------
if (( WITH_DB )); then
section "Migrations (Docker MySQL)"

CONTAINER=csm-check
docker rm -f "$CONTAINER" >/dev/null 2>&1
docker run -d --name "$CONTAINER" -p 13306:3306 \
  -e MYSQL_ROOT_PASSWORD=test -e MYSQL_DATABASE=csm mysql:8 >/dev/null 2>&1

# The entrypoint runs a TEMPORARY server during init, then stops it and starts
# the real one. A single successful query can land on the temporary server and
# be followed by a restart, which surfaces as a confusing "ERROR 2002 socket".
# Require the connection to hold steady.
ready=0
for i in $(seq 1 150); do
  if docker exec "$CONTAINER" mysql -uroot -ptest -N -B -e "SELECT 1" >/dev/null 2>&1; then
    ready=$((ready+1)); (( ready >= 5 )) && break
  else ready=0; fi
  sleep 1
done
(( ready >= 5 )) && pass "mysql ready" || { fail "mysql never became ready"; docker rm -f "$CONTAINER" >/dev/null 2>&1; exit 1; }

# --default-character-set=utf8mb4 is NOT optional: without it the client can
# negotiate a narrower charset and 4-byte characters are silently mangled on
# the way in, with no error. That is the failure cmd/migrate-data must never
# hit.
m() { docker exec -i "$CONTAINER" mysql -uroot -ptest csm --default-character-set=utf8mb4 2>/dev/null; }
q() { docker exec -i "$CONTAINER" mysql -uroot -ptest csm -N -B --default-character-set=utf8mb4 -e "$1" 2>/dev/null; }

for f in 0001_init.up 0002_foreign_keys.up 0003_password_bcrypt.up 0004_sessions.up 0005_vw_onroad.up 0006_vw_storein_storeout.up; do
  if m < "migrations/$f.sql"; then pass "apply $f"; else fail "apply $f"; fi
done

check() { # name expected actual
  [[ "$3" == "$2" ]] && pass "$1 ($3)" || fail "$1: got $3, want $2"
}
check "16 base tables"        16 "$(q "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema='csm' AND table_type='BASE TABLE';")"
check "3 views"                3 "$(q "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema='csm' AND table_type='VIEW';")"
check "row_version on 15"     15 "$(q "SELECT COUNT(*) FROM information_schema.columns WHERE table_schema='csm' AND column_name='row_version';")"
check "6 foreign keys"         6 "$(q "SELECT COUNT(*) FROM information_schema.table_constraints WHERE table_schema='csm' AND constraint_type='FOREIGN KEY';")"
# Exactly 60: a bcrypt hash always is, and a wider column would accept
# something that is not one.
check "password_bcrypt(60)"   60 "$(q "SELECT character_maximum_length FROM information_schema.columns WHERE table_schema='csm' AND table_name='tbl_userinfo' AND column_name='password_bcrypt';")"
# Deliberately non-unique until the legacy data is known clean.
check "username idx non-uniq"  1 "$(q "SELECT non_unique FROM information_schema.statistics WHERE table_schema='csm' AND table_name='tbl_userinfo' AND index_name='idx_tbl_userinfo_username';")"

# A migration you cannot roll back is not a migration.
#
# Rolled back in REVERSE order, which is what golang-migrate does and what the
# down files assume: each undoes only its own migration. Running 0001's down
# alone leaves tbl_session behind, because that table belongs to 0004.
for f in 0006_vw_storein_storeout.down 0005_vw_onroad.down 0004_sessions.down 0003_password_bcrypt.down 0002_foreign_keys.down 0001_init.down; do
  m < "migrations/$f.sql"
done
check "full rollback"          0 "$(q "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema='csm';")"  # tables AND views

for f in 0001_init.up 0002_foreign_keys.up 0003_password_bcrypt.up 0004_sessions.up 0005_vw_onroad.up 0006_vw_storein_storeout.up; do
  m < "migrations/$f.sql"
done
check "re-apply"              19 "$(q "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema='csm';")"  # 16 tables + 3 views

# The view column contracts are load-bearing: docs/mysql/schema.sql §3 records
# the exact list each view must expose, name for name, because the report code
# and the Go repositories select against them. A dropped or renamed column in a
# provisional definition would surface as a report that silently loses a field.
check "vw_onroad 19 cols"     19 "$(q "SELECT COUNT(*) FROM information_schema.columns WHERE table_schema='csm' AND table_name='vw_onroad';")"
check "vw_storein 42 cols"    42 "$(q "SELECT COUNT(*) FROM information_schema.columns WHERE table_schema='csm' AND table_name='vw_storein';")"
check "vw_storeout 55 cols"   55 "$(q "SELECT COUNT(*) FROM information_schema.columns WHERE table_schema='csm' AND table_name='vw_storeout';")"

# Guards the decisions baked into the schema. LAST_INSERT_ID(), not a
# hardcoded uid: AUTO_INCREMENT moves as earlier checks insert and delete.
out=$(docker exec -i "$CONTAINER" mysql -uroot -ptest csm -N -B --default-character-set=utf8mb4 2>/dev/null <<'SQL'
INSERT INTO tbl_userinfo (departmentcode, departmentname, username) VALUES ('D1','销售部','张三');
SET @uid = LAST_INSERT_ID();
UPDATE tbl_userinfo SET username='李四', row_version=row_version+1 WHERE uid=@uid AND row_version=1;
SELECT IF(ROW_COUNT()=1,'ok fresh-update','FAIL fresh update rejected');
UPDATE tbl_userinfo SET username='王五', row_version=row_version+1 WHERE uid=@uid AND row_version=1;
SELECT IF(ROW_COUNT()=0,'ok stale-blocked','FAIL stale update overwrote a newer row');
INSERT INTO tbl_session (userinfoid,token_hash,created_at,last_seen_at,expires_at)
  VALUES (@uid,REPEAT('a',64),NOW(),NOW(),NOW()+INTERVAL 24 HOUR);
DELETE FROM tbl_userinfo WHERE uid=@uid;
SELECT IF((SELECT COUNT(*) FROM tbl_session)=0,'ok session-cascade','FAIL user delete left sessions');
INSERT INTO tbl_basedata (type,name,keyname,value) VALUES (1,'车系列','民用','测试𠮷');
SELECT IF(CHAR_LENGTH(value)=3 AND LENGTH(value)=10,'ok utf8mb4-4byte','FAIL 4-byte character mangled') FROM tbl_basedata;
SET FOREIGN_KEY_CHECKS = 0;
INSERT INTO tbl_onroad (billno,billdate,vin,engineno,cartypeid,cartype,inflag,inkind)
  VALUES ('B1',NOW(),'ORPHAN','E1',999999,'C9',0,0);
SET FOREIGN_KEY_CHECKS = 1;
-- LEFT JOIN, not INNER: an orphaned cartypeid must not make the vehicle vanish
-- from every screen and report (see migrations/0005).
SELECT IF((SELECT COUNT(*) FROM vw_onroad WHERE vin='ORPHAN')=1,'ok orphan-visible','FAIL orphan vanished from vw_onroad');
SQL
)
if echo "$out" | grep -q FAIL; then
  fail "schema behaviour"; echo "$out" | sed 's/^/        /'
elif [[ "$(echo "$out" | grep -c '^ok ')" == 5 ]]; then
  pass "schema behaviour (5 assertions)"
else
  fail "schema behaviour: expected 5 assertions, got $(echo "$out" | grep -c '^ok ')"
fi

section "Database integration"
# Both packages skip themselves without a DSN, so they must be named here --
# the plain `go test ./...` above runs them as no-ops. internal/domain/movement
# in particular is entirely about transactions and row guards; skipping it
# silently would leave the lifecycle unverified while the suite still says PASS.
export CARSALEMAN_TEST_DSN='root:test@tcp(127.0.0.1:13306)/csm'
step "go test ./internal/store/mysql"     go test -race ./internal/store/mysql/
step "go test ./internal/domain/movement" go test -race ./internal/domain/movement/

# Guard against exactly that: assert the movement tests actually RAN.
#
# Two traps, both of which made this report a false failure:
#   -count=1  defeats the test cache. Without it `go test` prints "(cached)"
#             and no "--- PASS" line at all.
#   no pipe   `set -o pipefail` is on, and `grep -q` exits at the first match,
#             closing the pipe. `go test` then dies of SIGPIPE and pipefail
#             propagates THAT as the pipeline's status -- so the command passes
#             standalone and fails inside this script.
mv_out=$(go test -count=1 -v -run TestStoreInTwiceIsRejected ./internal/domain/movement/ 2>&1)
if grep -q -- "--- PASS" <<<"$mv_out"; then
  pass "movement tests really executed (not skipped)"
else
  fail "movement tests were skipped or failed — the DSN is not reaching them"
fi
unset CARSALEMAN_TEST_DSN

docker rm -f "$CONTAINER" >/dev/null 2>&1
fi

# ---------------------------------------------------------------------------
printf '\n'
if (( FAILED )); then
  printf '\033[31mFAILURES PRESENT\033[0m\n'; exit 1
fi
printf '\033[32mALL CHECKS PASS\033[0m'
(( WITH_DB )) || printf '  (database checks skipped; run with --db)'
printf '\n'
