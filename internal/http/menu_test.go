package http

import (
	"strings"
	"testing"

	"github.com/Markcus0526/carsaleman/internal/auth"
)

func TestFilterMenuDenyByDefault(t *testing.T) {
	if got := filterMenu(menuTree, auth.Set{}); len(got) != 0 {
		t.Errorf("empty permission set yielded %d nodes, want 0", len(got))
	}
}

// A parent whose children are all denied must disappear too -- otherwise the
// nav shows an expandable section that opens onto nothing.
func TestFilterMenuDropsChildlessParents(t *testing.T) {
	perms := auth.Set{auth.PermMovement: auth.LevelReadWrite} // parent only

	got := filterMenu(menuTree, perms)
	for _, n := range got {
		if n.ID == "movement" {
			t.Fatal("parent with no permitted children should be dropped")
		}
	}
}

func TestFilterMenuKeepsPermittedBranch(t *testing.T) {
	perms := auth.Set{
		auth.PermMovement: auth.LevelReadOnly,
		auth.PermStoreIn:  auth.LevelReadOnly,
	}

	got := filterMenu(menuTree, perms)
	if len(got) != 1 || got[0].ID != "movement" {
		t.Fatalf("want only the movement section, got %+v", got)
	}
	if len(got[0].Children) != 1 || got[0].Children[0].ID != "storein" {
		t.Errorf("want only the permitted child, got %+v", got[0].Children)
	}
}

// filterMenu returns nodes by value; a bug that mutated the shared tree would
// leak one user's filtered children into the next request.
func TestFilterMenuDoesNotMutateSharedTree(t *testing.T) {
	before := len(menuTree[0].Children)

	filterMenu(menuTree, auth.Set{
		auth.PermMovement: auth.LevelReadWrite,
		auth.PermStoreIn:  auth.LevelReadWrite,
	})

	if after := len(menuTree[0].Children); after != before {
		t.Errorf("menuTree mutated: %d children before, %d after", before, after)
	}
}

// The two string fields must stay distinct in kind: PermissionKey is the
// Chinese data key, LabelKey is an ASCII i18n key. Swapping them is the
// failure mode the whole design exists to prevent.
func TestMenuKeysAreDistinctInKind(t *testing.T) {
	var walk func([]MenuNode)
	walk = func(nodes []MenuNode) {
		for _, n := range nodes {
			if n.PermissionKey == "" {
				t.Errorf("%s: empty PermissionKey", n.ID)
			}
			if !strings.HasPrefix(n.LabelKey, "menu.") {
				t.Errorf("%s: LabelKey %q should be an i18n key under menu.", n.ID, n.LabelKey)
			}
			if n.LabelKey == n.PermissionKey {
				t.Errorf("%s: LabelKey and PermissionKey must not be the same string", n.ID)
			}
			for _, r := range n.LabelKey {
				if r > 127 {
					t.Errorf("%s: LabelKey %q contains non-ASCII — it is an i18n key, not display text", n.ID, n.LabelKey)
					break
				}
			}
			walk(n.Children)
		}
	}
	walk(menuTree)
}

func TestAllPermissionKeysCoversTree(t *testing.T) {
	keys := allPermissionKeys()

	seen := make(map[string]bool, len(keys))
	for _, k := range keys {
		if seen[k] {
			t.Errorf("duplicate permission key %q", k)
		}
		seen[k] = true
	}

	var count func([]MenuNode) int
	count = func(nodes []MenuNode) int {
		n := len(nodes)
		for _, node := range nodes {
			n += count(node.Children)
		}
		return n
	}
	if want := count(menuTree); len(keys) != want {
		t.Errorf("allPermissionKeys returned %d, tree has %d nodes", len(keys), want)
	}
}
