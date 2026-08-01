-- Password migration support (plan 6.5).
--
-- Hand-written, not generated: this describes the target schema, not the
-- legacy one, so it must not be overwritten by tools/gen-migration/gen.mjs.
--
-- The legacy `password` column stays for now. During the migration window a
-- user has exactly one of the two populated:
--
--   password_bcrypt set  -> verify with bcrypt (the normal path)
--   password set only    -> verify with legacy DES, then write a bcrypt hash
--                           and NULL out `password` (upgrade on first login)
--
-- After one release, drop the legacy path in code, then drop the column in a
-- later migration and force-reset any account still lacking a hash.

-- 60 is exact, not generous: a bcrypt hash is always 60 characters
-- ($2a$ + cost + $ + 22-char salt + 31-char digest). A wider column would
-- invite storing something that is not a bcrypt hash.
ALTER TABLE `tbl_userinfo`
  ADD COLUMN `password_bcrypt` VARCHAR(60) NULL
  COMMENT 'bcrypt hash; NULL until the user has logged in once post-migration'
  AFTER `password`;

-- Login looks users up by username. The legacy app selected the entire table
-- and filtered client-side in the DataSet (plan 2.4), so no index ever
-- existed for it -- but the port issues a real WHERE and needs one.
--
-- Deliberately NOT UNIQUE: the legacy data has never been constrained, so
-- duplicates may exist. cmd/migrate-data reports them; promoting this to a
-- unique index is a follow-up once the data is known to be clean.
CREATE INDEX `idx_tbl_userinfo_username` ON `tbl_userinfo` (`username`);
