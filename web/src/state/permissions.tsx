import { createContext, useContext, type ReactNode } from "react";

/**
 * Permission levels, exactly as stored in tbl_permission.permission.
 *
 * These are Chinese because the DATA is Chinese. They are opaque authorization
 * values, not display text — never translate them, never render them, and
 * never key the i18n catalogue off them. The same rule governs the permission
 * keys themselves (see api/client.ts, MenuNode.permissionKey).
 */
export const LEVEL_WRITE = "读写";
export const LEVEL_READ = "只读";
export const LEVEL_NONE = "不可用";

const PermissionContext = createContext<Record<string, string>>({});

export function PermissionProvider({
  permissions,
  children,
}: {
  permissions: Record<string, string>;
  children: ReactNode;
}) {
  return (
    <PermissionContext.Provider value={permissions}>
      {children}
    </PermissionContext.Provider>
  );
}

/**
 * Whether the user may modify the given area.
 *
 * Deny by default: an unknown key means no write. This mirrors auth.Set on the
 * server, and it is the safe direction — a missing entry should cost someone a
 * greyed-out button, not grant them an edit.
 *
 * This gates the UI only. The real check is server-side on every route
 * (§10.8): the legacy app greyed out menu items and enforced nothing, which is
 * not a security boundary.
 */
export function useCanWrite(permissionKey: string): boolean {
  return useContext(PermissionContext)[permissionKey] === LEVEL_WRITE;
}
