package auth

import "testing"

// The legacy app granted write access only for 读写 and left a menu item
// disabled when the key was absent from Global.PERMISSION_LIST. Both are
// security-relevant, so both are pinned here rather than assumed.
func TestSetDenyByDefault(t *testing.T) {
	empty := Set{}

	if empty.CanWrite(PermStoreIn) {
		t.Error("CanWrite on an absent key must be false (deny-by-default)")
	}
	if empty.CanRead(PermStoreIn) {
		t.Error("CanRead on an absent key must be false (deny-by-default)")
	}
}

func TestSetLevels(t *testing.T) {
	cases := []struct {
		name              string
		level             Level
		wantRead, wantWri bool
	}{
		{"read-write", LevelReadWrite, true, true},
		{"read-only", LevelReadOnly, true, false},
		{"unavailable", LevelUnavailable, false, false},
		{"unknown value", Level("garbage"), false, false},
		{"empty value", Level(""), false, false},
	}

	for _, tc := range cases {
		t.Run(tc.name, func(t *testing.T) {
			s := Set{PermStoreIn: tc.level}
			if got := s.CanRead(PermStoreIn); got != tc.wantRead {
				t.Errorf("CanRead(%q) = %v, want %v", tc.level, got, tc.wantRead)
			}
			if got := s.CanWrite(PermStoreIn); got != tc.wantWri {
				t.Errorf("CanWrite(%q) = %v, want %v", tc.level, got, tc.wantWri)
			}
		})
	}
}

// A permission granted on one key must not leak to another. Guards against a
// future refactor that collapses the map into something coarser.
func TestSetIsPerKey(t *testing.T) {
	s := Set{PermStoreIn: LevelReadWrite}

	if !s.CanWrite(PermStoreIn) {
		t.Fatal("granted key should be writable")
	}
	if s.CanWrite(PermStoreOut) || s.CanRead(PermStoreOut) {
		t.Error("permission leaked to an unrelated key")
	}
}

// The constants are the exact strings in tbl_permission.fieldname. If someone
// "tidies" one into English, existing permission rows stop matching and users
// are silently denied every screen -- so pin a representative sample.
func TestPermissionKeysAreLegacyChineseLabels(t *testing.T) {
	want := map[string]string{
		"PermMovement": "车辆流转",
		"PermStoreIn":  "入库处理",
		"PermFinance":  "财务分析",
		"PermSettings": "系统设置",
	}
	got := map[string]string{
		"PermMovement": PermMovement,
		"PermStoreIn":  PermStoreIn,
		"PermFinance":  PermFinance,
		"PermSettings": PermSettings,
	}
	for name, w := range want {
		if got[name] != w {
			t.Errorf("%s = %q, want %q — permission keys are data, not display text", name, got[name], w)
		}
	}
}

func TestPermissionLevelsAreLegacyValues(t *testing.T) {
	if LevelReadWrite != "读写" || LevelReadOnly != "只读" || LevelUnavailable != "不可用" {
		t.Error("permission levels must stay the literal values stored in tbl_permission.permission")
	}
}
