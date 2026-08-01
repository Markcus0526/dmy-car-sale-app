package auth

import (
	"bytes"
	"crypto/cipher"
	"crypto/des"
	"crypto/subtle"
	"encoding/base64"
	"errors"
	"fmt"
	"sync"

	"golang.org/x/crypto/bcrypt"
)

// Password handling, and the migration off the legacy scheme.
//
// WHAT THE LEGACY SYSTEM DOES (CommonMisc.cs, CsmEncrypt.DESEncode)
//
//	key    "123456ABCDEF"[:8] = "123456AB", UTF-8 bytes
//	iv     12 34 56 78 90 AB CD EF   (Global.DESKeys, hardcoded)
//	cipher DES-CBC, PKCS#7 padding    (DESCryptoServiceProvider defaults)
//	input  UTF-8 bytes of the password
//	output Base64
//
// Login compares CIPHERTEXT: FrmLogon.cs:71-76 encrypts what was typed and
// string-compares it against tbl_userinfo.password.
//
// This is reversible encryption, not hashing, and the IV is fixed -- so it is
// a deterministic, unsalted, invertible transform with a key that is published
// in the source. Anyone with the database has every plaintext password. Users
// reuse passwords, so the blast radius is not limited to this application.
//
// We reproduce it EXACTLY, and only to verify existing credentials once, so
// they can be re-hashed with bcrypt on first successful login (plan 6.5).
// Nothing new is ever written in this format.

// legacyDESKey is Global.STR_DES_KEY truncated to 8 bytes, exactly as
// DESEncode does with Substring(0, 8).
var legacyDESKey = []byte("123456AB")

// legacyDESIV is Global.DESKeys. DES block size is 8, so this doubles as the
// key size -- do not "simplify" one into the other; they are unrelated values
// that happen to share a length.
var legacyDESIV = []byte{0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF}

// ErrLegacyFormat means the stored value is not valid legacy ciphertext.
var ErrLegacyFormat = errors.New("auth: stored password is not valid legacy DES ciphertext")

// legacyDESEncode reproduces CsmEncrypt.DESEncode byte for byte.
//
// Deprecated by construction: used only to verify an existing credential
// during migration. Never call it to store a password.
func legacyDESEncode(plaintext string) (string, error) {
	block, err := des.NewCipher(legacyDESKey)
	if err != nil {
		return "", fmt.Errorf("auth: des cipher: %w", err)
	}

	padded := pkcs7Pad([]byte(plaintext), block.BlockSize())
	out := make([]byte, len(padded))
	cipher.NewCBCEncrypter(block, legacyDESIV).CryptBlocks(out, padded)

	return base64.StdEncoding.EncodeToString(out), nil
}

// legacyDESDecode reverses it. Present for the migration tooling and for
// tests; the running application has no reason to recover a plaintext
// password, and doing so would be a step backwards.
func legacyDESDecode(ciphertext string) (string, error) {
	raw, err := base64.StdEncoding.DecodeString(ciphertext)
	if err != nil {
		return "", ErrLegacyFormat
	}

	block, err := des.NewCipher(legacyDESKey)
	if err != nil {
		return "", fmt.Errorf("auth: des cipher: %w", err)
	}
	if len(raw) == 0 || len(raw)%block.BlockSize() != 0 {
		return "", ErrLegacyFormat
	}

	out := make([]byte, len(raw))
	cipher.NewCBCDecrypter(block, legacyDESIV).CryptBlocks(out, raw)

	unpadded, err := pkcs7Unpad(out, block.BlockSize())
	if err != nil {
		return "", err
	}
	return string(unpadded), nil
}

func pkcs7Pad(b []byte, blockSize int) []byte {
	n := blockSize - len(b)%blockSize
	return append(b, bytes.Repeat([]byte{byte(n)}, n)...)
}

func pkcs7Unpad(b []byte, blockSize int) ([]byte, error) {
	if len(b) == 0 || len(b)%blockSize != 0 {
		return nil, ErrLegacyFormat
	}
	n := int(b[len(b)-1])
	if n == 0 || n > blockSize || n > len(b) {
		return nil, ErrLegacyFormat
	}
	for _, c := range b[len(b)-n:] {
		if int(c) != n {
			return nil, ErrLegacyFormat
		}
	}
	return b[:len(b)-n], nil
}

// BcryptCost is deliberately above bcrypt.DefaultCost (10).
//
// Login is not a hot path here -- this is an internal system with a few dozen
// users -- so the extra ~4x work per attempt is invisible to a legitimate user
// and meaningful against offline cracking if the database leaks again.
const BcryptCost = 12

// hashCost is the cost actually used. Production always uses BcryptCost;
// export_test.go lowers it for the test binary only, because cost 12 under
// -race is ~14s per call and would put a minute of pure key stretching into
// every CI run. TestHashPasswordUsesProductionCost guards the real constant.
var hashCost = BcryptCost

// HashPassword produces the value stored in tbl_userinfo.password_bcrypt.
func HashPassword(plaintext string) (string, error) {
	h, err := bcrypt.GenerateFromPassword([]byte(plaintext), hashCost)
	if err != nil {
		return "", fmt.Errorf("auth: bcrypt: %w", err)
	}
	return string(h), nil
}

// Credential is what tbl_userinfo stores for one user during the migration
// window: exactly one of the two fields is expected to be populated.
type Credential struct {
	// Bcrypt is tbl_userinfo.password_bcrypt. Preferred when present.
	Bcrypt string
	// Legacy is tbl_userinfo.password -- DES ciphertext. Nulled out once the
	// user has logged in once and been upgraded.
	Legacy string
}

// Verify checks plaintext against a stored credential.
//
// needsUpgrade is true when the user authenticated via the legacy path and the
// caller should now write a bcrypt hash and clear the legacy column. Handling
// that in the caller keeps this function free of storage concerns and testable
// without a database.
//
// After one release, delete the legacy branch and force-reset any account that
// still has no bcrypt hash (plan 6.5 step 3).
func Verify(plaintext string, c Credential) (ok bool, needsUpgrade bool) {
	if c.Bcrypt != "" {
		err := bcrypt.CompareHashAndPassword([]byte(c.Bcrypt), []byte(plaintext))
		return err == nil, false
	}

	if c.Legacy == "" {
		// No credential at all. Still spend the time a bcrypt comparison would,
		// so a missing user is not distinguishable from a wrong password by
		// response latency.
		dummyCompare(plaintext)
		return false, false
	}

	candidate, err := legacyDESEncode(plaintext)
	if err != nil {
		return false, false
	}
	// Constant-time: the stored ciphertext is not a secret in any meaningful
	// sense here, but leaking a prefix-match length through timing is free to
	// avoid.
	if subtle.ConstantTimeCompare([]byte(candidate), []byte(c.Legacy)) != 1 {
		return false, false
	}
	return true, true
}

// dummyCompare burns roughly one bcrypt verification's worth of time, so a
// user that does not exist is not distinguishable from a wrong password by
// response latency.
//
// Computed lazily: at package scope it would cost a full key-stretch on every
// process start, including short-lived tooling that never authenticates.
var (
	dummyOnce sync.Once
	dummyHash []byte
)

func dummyCompare(plaintext string) {
	dummyOnce.Do(func() {
		dummyHash, _ = bcrypt.GenerateFromPassword([]byte("dummy"), hashCost)
	})
	_ = bcrypt.CompareHashAndPassword(dummyHash, []byte(plaintext))
}
