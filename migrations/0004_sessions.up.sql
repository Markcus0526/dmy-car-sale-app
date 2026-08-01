-- Server-side sessions (D17).
--
-- Hand-written; describes the target schema, not the legacy one, so
-- tools/gen-migration/gen.mjs must not regenerate it.
--
-- WHY A TABLE AND NOT A JWT
--
-- This system has a permission-administration screen (权限设定). With a JWT,
-- revoking a user's access does not take effect until the token expires -- the
-- user keeps working with permissions an administrator has already removed.
-- That is the same failure plan 10.8 describes, moved from the client to the
-- token. Fixing permission enforcement and then making it un-revokable would
-- defeat the point.
--
-- The usual argument for JWT is horizontal scale. This is a few dozen users on
-- one server; there is no scale argument to answer.

CREATE TABLE `tbl_session` (
  `uid`         BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `userinfoid`  INT NOT NULL,

  -- SHA-256 of the token, never the token itself. A database leak should not
  -- hand over usable sessions -- the same reasoning that puts bcrypt on the
  -- password column. 64 hex chars.
  --
  -- SHA-256 rather than bcrypt: the token is 256 bits of CSPRNG output, so
  -- there is no low-entropy input to protect against brute force, and this is
  -- verified on every single request.
  `token_hash`  CHAR(64) NOT NULL,

  `created_at`  DATETIME NOT NULL,
  -- Idle timeout: bumped on use, so an active user is not logged out mid-task.
  `last_seen_at` DATETIME NOT NULL,
  -- Absolute timeout: a session dies at this point however active it has been,
  -- so a stolen cookie has a bounded life.
  `expires_at`  DATETIME NOT NULL,

  -- Recorded for the audit trail (plan 4) and to make "where was this session
  -- used from" answerable after an incident. IPv6-capable length.
  `ip`          VARCHAR(45) NULL,
  `user_agent`  VARCHAR(255) NULL,

  PRIMARY KEY (`uid`),

  -- Lookup path for every authenticated request: hash -> session.
  UNIQUE KEY `uq_tbl_session_token_hash` (`token_hash`),

  -- "Log out everywhere", and revoking sessions when an account is disabled.
  KEY `idx_tbl_session_userinfoid` (`userinfoid`),

  -- Expired-session sweep.
  KEY `idx_tbl_session_expires_at` (`expires_at`),

  CONSTRAINT `fk_tbl_session_userinfoid`
    FOREIGN KEY (`userinfoid`) REFERENCES `tbl_userinfo` (`uid`)
    ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ON DELETE CASCADE is deliberate and is the one place in this schema where a
-- cascade is correct: a deleted user must not leave live sessions behind.
-- Everywhere else, deletes should fail loudly rather than propagate.
--
-- No row_version: sessions are created and deleted, never concurrently edited,
-- so there is nothing for optimistic concurrency to protect.
