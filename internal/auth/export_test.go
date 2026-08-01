package auth

import "golang.org/x/crypto/bcrypt"

// Production hashes at BcryptCost (12). Under -race that is ~14s per call, so
// the suite would spend a minute doing nothing but key stretching. Cost does
// not affect correctness of any behaviour under test, so tests run at MinCost.
//
// TestHashPasswordUsesProductionCost still asserts the production constant.
func init() { hashCost = bcrypt.MinCost }
