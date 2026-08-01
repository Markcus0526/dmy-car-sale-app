-- Reverses 0003.
--
-- Note what this loses: every bcrypt hash written since 0003 was applied.
-- Users upgraded after that point have had their legacy `password` NULLed,
-- so rolling back leaves them with no credential at all and they must be
-- reset. That is inherent to the upgrade-on-login design, not an oversight --
-- roll back only before real logins have occurred.

DROP INDEX `idx_tbl_userinfo_username` ON `tbl_userinfo`;

ALTER TABLE `tbl_userinfo` DROP COLUMN `password_bcrypt`;
