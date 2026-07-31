-- =====================================================================
-- Phase 0: extract the ground truth from the live SQL Server "csm" database.
--
-- Run against the production (or a restored copy of the) csm database.
-- Save the output of EVERY step into the repo -- Phases 2 and 4 of the
-- migration are scoped entirely by what these queries return.
--
-- Read-only: nothing here modifies the database.
--
-- ---------------------------------------------------------------------
-- HOW TO RUN -- read this before you start.
--
-- STEPS 4-15 are tabular and dump cleanly with sqlcmd:
--
--   sqlcmd -S R-SEVEN64 -U sa -d csm -i docs\mssql-export.sql -y 0 -s "|" -W ^
--          -o docs\mssql-dump.txt
--
-- STEPS 1-3 ARE DIFFERENT. Do NOT rely on the sqlcmd output for them.
-- Procedure and view bodies are nvarchar(max) containing newlines; sqlcmd's
-- text mode column-aligns and wraps them, producing output that cannot be
-- reconstructed into runnable SQL. The 28 proc bodies are the single most
-- important artefact of this whole phase -- get them as real files.
--
-- Use ONE of these instead (both produce one clean .sql file per object):
--
--   (a) SSMS: right-click the csm database -> Tasks -> Generate Scripts...
--       -> select Stored Procedures + Views -> Advanced: "Script DROP and
--       CREATE" = CREATE, "Types of data to script" = Schema only
--       -> Save as: "Single file per object" -> docs/mssql/
--
--   (b) PowerShell on the server (no SSMS needed):
--
--       $ErrorActionPreference = 'Stop'
--       Add-Type -AssemblyName 'Microsoft.SqlServer.Smo'
--       $srv = New-Object Microsoft.SqlServer.Management.Smo.Server 'R-SEVEN64'
--       $srv.ConnectionContext.LoginSecure = $false
--       $srv.ConnectionContext.Login    = 'sa'
--       $srv.ConnectionContext.Password = Read-Host -AsSecureString |
--           ForEach-Object { [Runtime.InteropServices.Marshal]::PtrToStringAuto(
--               [Runtime.InteropServices.Marshal]::SecureStringToBSTR($_)) }
--       $db  = $srv.Databases['csm']
--       New-Item -ItemType Directory -Force docs\mssql\procs, docs\mssql\views | Out-Null
--       foreach ($p in $db.StoredProcedures | Where-Object { -not $_.IsSystemObject }) {
--           $p.Script() -join "`r`n" |
--               Set-Content "docs\mssql\procs\$($p.Name).sql" -Encoding UTF8
--       }
--       foreach ($v in $db.Views | Where-Object { -not $_.IsSystemObject }) {
--           $v.Script() -join "`r`n" |
--               Set-Content "docs\mssql\views\$($v.Name).sql" -Encoding UTF8
--       }
--       "procs: $((Get-ChildItem docs\mssql\procs).Count)  views: $((Get-ChildItem docs\mssql\views).Count)"
--
--       Expect: procs 28, views 5.
--
-- Commit BOTH the per-object .sql files and docs/mssql-dump.txt.
-- ---------------------------------------------------------------------
-- =====================================================================

USE csm;
GO

PRINT '################ STEP 1: stored procedure bodies (28 expected) ################';
GO
-- The critical artefact. Every report, statistic and chart calculation lives here.
SELECT
    o.name                AS proc_name,
    m.definition          AS proc_body
FROM sys.sql_modules m
JOIN sys.objects o ON o.object_id = m.object_id
WHERE o.type = 'P'
ORDER BY o.name;
GO

PRINT '################ STEP 2: view definitions (5 expected) ################';
GO
-- vw_onroad, vw_storein, vw_storeout, vw_speccar, vw_department
SELECT
    o.name                AS view_name,
    m.definition          AS view_body
FROM sys.sql_modules m
JOIN sys.objects o ON o.object_id = m.object_id
WHERE o.type = 'V'
ORDER BY o.name;
GO

PRINT '################ STEP 3: functions & triggers (expected: none) ################';
GO
-- If this returns rows, they are logic the repository never revealed and the
-- migration plan must account for them.
SELECT
    o.type_desc           AS object_kind,
    o.name                AS object_name,
    m.definition          AS body
FROM sys.sql_modules m
JOIN sys.objects o ON o.object_id = m.object_id
WHERE o.type IN ('FN','IF','TF','TR')
ORDER BY o.type_desc, o.name;
GO

PRINT '################ STEP 4: authoritative column definitions ################';
GO
-- Supersedes docs/mysql/schema.sql wherever the two disagree.
-- Pay attention to COLUMN_DEFAULT and the identity/computed flags -- CmsDB.xsd
-- cannot express any of them.
SELECT
    c.TABLE_NAME,
    c.ORDINAL_POSITION,
    c.COLUMN_NAME,
    c.DATA_TYPE,
    c.CHARACTER_MAXIMUM_LENGTH   AS max_len,
    c.NUMERIC_PRECISION          AS num_prec,
    c.NUMERIC_SCALE              AS num_scale,
    c.IS_NULLABLE,
    c.COLUMN_DEFAULT,
    c.COLLATION_NAME,
    COLUMNPROPERTY(OBJECT_ID(c.TABLE_SCHEMA + '.' + c.TABLE_NAME),
                   c.COLUMN_NAME, 'IsIdentity')  AS is_identity,
    COLUMNPROPERTY(OBJECT_ID(c.TABLE_SCHEMA + '.' + c.TABLE_NAME),
                   c.COLUMN_NAME, 'IsComputed')  AS is_computed
FROM INFORMATION_SCHEMA.COLUMNS c
JOIN INFORMATION_SCHEMA.TABLES t
  ON  t.TABLE_NAME   = c.TABLE_NAME
  AND t.TABLE_SCHEMA = c.TABLE_SCHEMA
WHERE t.TABLE_TYPE = 'BASE TABLE'
ORDER BY c.TABLE_NAME, c.ORDINAL_POSITION;
GO

PRINT '################ STEP 5: computed column expressions ################';
GO
SELECT
    OBJECT_NAME(cc.object_id)  AS table_name,
    cc.name                    AS column_name,
    cc.definition              AS expression,
    cc.is_persisted
FROM sys.computed_columns cc
ORDER BY table_name, column_name;
GO

PRINT '################ STEP 6: primary keys, unique constraints, indexes ################';
GO
-- CmsDB.xsd only knows the PKs. Every non-clustered index below is a query
-- the old system depended on for performance -- recreate the equivalents in
-- MySQL rather than rediscovering them under production load.
SELECT
    OBJECT_NAME(i.object_id)   AS table_name,
    i.name                     AS index_name,
    i.type_desc,
    i.is_primary_key,
    i.is_unique,
    STUFF((SELECT ', ' + col.name +
                  CASE WHEN ic2.is_descending_key = 1 THEN ' DESC' ELSE '' END
           FROM sys.index_columns ic2
           JOIN sys.columns col
             ON  col.object_id = ic2.object_id
             AND col.column_id = ic2.column_id
           WHERE ic2.object_id = i.object_id
             AND ic2.index_id  = i.index_id
             AND ic2.is_included_column = 0
           ORDER BY ic2.key_ordinal
           FOR XML PATH('')), 1, 2, '')  AS key_columns
FROM sys.indexes i
JOIN sys.objects o ON o.object_id = i.object_id
WHERE o.type = 'U' AND i.type > 0
ORDER BY table_name, i.is_primary_key DESC, i.name;
GO

PRINT '################ STEP 7: foreign keys ################';
GO
-- Expect ~4. Anything beyond that is an undocumented relationship.
SELECT
    fk.name                              AS fk_name,
    OBJECT_NAME(fk.parent_object_id)     AS child_table,
    pc.name                              AS child_column,
    OBJECT_NAME(fk.referenced_object_id) AS parent_table,
    rc.name                              AS parent_column,
    fk.delete_referential_action_desc    AS on_delete,
    fk.update_referential_action_desc    AS on_update
FROM sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
JOIN sys.columns pc
  ON  pc.object_id = fkc.parent_object_id
  AND pc.column_id = fkc.parent_column_id
JOIN sys.columns rc
  ON  rc.object_id = fkc.referenced_object_id
  AND rc.column_id = fkc.referenced_column_id
ORDER BY child_table, fk.name;
GO

PRINT '################ STEP 8: default & check constraints ################';
GO
SELECT
    OBJECT_NAME(parent_object_id) AS table_name,
    name                          AS constraint_name,
    'DEFAULT'                     AS kind,
    definition
FROM sys.default_constraints
UNION ALL
SELECT
    OBJECT_NAME(parent_object_id),
    name,
    'CHECK',
    definition
FROM sys.check_constraints
ORDER BY table_name, kind, constraint_name;
GO

PRINT '################ STEP 9: row counts (sizes the data migration) ################';
GO
SELECT
    OBJECT_NAME(p.object_id) AS table_name,
    SUM(p.rows)              AS row_count
FROM sys.partitions p
JOIN sys.objects o ON o.object_id = p.object_id
WHERE o.type = 'U' AND p.index_id IN (0, 1)
GROUP BY p.object_id
ORDER BY row_count DESC;
GO

PRINT '################ STEP 10: database collation ################';
GO
-- Determines whether MySQL utf8mb4_unicode_ci will reproduce today's Chinese
-- sort order. Expect something like Chinese_PRC_CI_AS -- see plan section 3.4.
SELECT
    DATABASEPROPERTYEX('csm', 'Collation') AS db_collation,
    @@VERSION                              AS server_version;
GO

-- =====================================================================
-- STEPS 11-16: DATA probes.
--
-- Steps 1-10 recover the schema. These recover the reference data that Phase 0
-- day 2 has to answer open questions with. Without them the day-2 exit
-- criteria cannot be met -- see DEVELOPMENT_PLAN.md section 2.
--
-- All are small result sets. None contains customer PII.
-- =====================================================================

PRINT '################ STEP 11: tbl_env -- finance parameters (8 keys expected) ################';
GO
-- Drives the whole interest engine (plan 5.5, 6.1). Two things to check:
--   * All 8 keys present? A missing key silently produced a zero rate.
--   * EXACT key casing. The legacy app WROTE lowercase ("extend10color") and
--     READ mixed-case ("extend10Color"), which only worked because
--     DataTable.Select is case-insensitive. Go map lookups are not (plan 10.3).
SELECT * FROM tbl_env ORDER BY 1;
GO

PRINT '################ STEP 12: tbl_basedata -- domain inventory (answers Q4, Q5) ################';
GO
-- Plan 5.6 lists 17 domains but CmsDB.xsd cannot say what `type` 1 vs 2 means.
-- This settles it, and tells us which domains are small enums (translatable)
-- vs which hold free text such as people's names (never translated).
SELECT
    type,
    name           AS domain_name,
    COUNT(*)       AS value_count,
    MIN(keyname)   AS sample_keyname,
    MAX(keyname)   AS sample_keyname_2
FROM tbl_basedata
GROUP BY type, name
ORDER BY type, name;
GO

PRINT '################ STEP 13: tbl_permission -- key and value inventory ################';
GO
-- Confirms the permission VALUES really are only 读写 / 只读 / 不可用 (plan 6.4).
SELECT permission AS permission_value, COUNT(*) AS row_count
FROM tbl_permission
GROUP BY permission
ORDER BY row_count DESC;
GO
-- Confirms the permission KEYS match the menu labels the Go const enum encodes.
-- Any fieldname here that is absent from internal/auth/permission.go is a
-- screen we have not accounted for.
SELECT DISTINCT fieldname AS permission_key
FROM tbl_permission
ORDER BY fieldname;
GO

PRINT '################ STEP 14: the 是否* boolean columns (plan 5.6) ################';
GO
-- These are booleans stored as Chinese strings in varchar(50). Confirm the
-- exact value set before the UI maps them to Yes/No -- if anything other than
-- 是/否 appears, the mapping needs more cases.
SELECT 'storeout.isbill'    AS col, isbill    AS value, COUNT(*) AS n FROM tbl_storeout GROUP BY isbill
UNION ALL
SELECT 'storeout.ispayment', ispayment, COUNT(*) FROM tbl_storeout GROUP BY ispayment
UNION ALL
SELECT 'storeout.issend',    issend,    COUNT(*) FROM tbl_storeout GROUP BY issend
UNION ALL
SELECT 'onroad.isreport',    isreport,  COUNT(*) FROM tbl_onroad   GROUP BY isreport
ORDER BY col, value;
GO

PRINT '################ STEP 15: liveness of tbl_stats / tbl_dbbackup_log (answers Q8) ################';
GO
-- Plan 11.8 flags both as unclear ownership. Note 数据备份 IS a live menu entry
-- under 系统设置, so tbl_dbbackup_log likely is in use -- confirm here.
SELECT 'tbl_stats'         AS table_name, COUNT(*) AS row_count FROM tbl_stats
UNION ALL
SELECT 'tbl_dbbackup_log', COUNT(*) FROM tbl_dbbackup_log
UNION ALL
SELECT 'tbl_fit',          COUNT(*) FROM tbl_fit;
GO

PRINT '################ STEP 16: locate the finance columns (sizes the 10.2 exposure) ################';
GO
-- Section 10.2 is the largest open financial question: vehicles past their
-- extension window currently report ZERO interest. Before asking the business
-- whether that is intended, we want to know how many vehicles and how much
-- money it affects.
--
-- That query cannot be written blind -- it needs the real column names. This
-- step finds them; write the quantification query on day 2 using the result.
SELECT
    c.TABLE_NAME,
    c.COLUMN_NAME,
    c.DATA_TYPE,
    c.NUMERIC_PRECISION,
    c.NUMERIC_SCALE
FROM INFORMATION_SCHEMA.COLUMNS c
WHERE c.COLUMN_NAME IN
      ('billdate','inprice','outprice','interestrate','totalinterest',
       'nointerestdate','noextenddate','noallmoneydate','profitval')
ORDER BY c.TABLE_NAME, c.COLUMN_NAME;
GO

-- =====================================================================
-- Sanity check after running:
--   STEP 1  -> 28 rows (procedures)   <-- hard gate
--   STEP 2  ->  5 rows (views)        <-- hard gate
--   STEP 11 ->  8 rows (finance keys)
--   STEP 13 ->  permission values are only 读写 / 只读 / 不可用
--
-- Fewer procs or views means they were dropped from this instance and another
-- source (a backup, or a different server) is needed. Do not start Phase 1
-- until steps 1 and 2 return the expected counts.
-- =====================================================================
