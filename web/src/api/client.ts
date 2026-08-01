/**
 * Thin fetch wrapper.
 *
 * The server returns machine-readable error CODES, never prose, so every
 * failure is localised on this side via the `error.*` catalogue. That is what
 * keeps user-visible language out of Go source.
 */

export interface ApiErrorBody {
  code: string;
  fields?: Record<string, string>;
  requestId?: string;
}

export class ApiError extends Error {
  readonly code: string;
  readonly fields: Record<string, string>;
  readonly requestId?: string;
  readonly status: number;

  constructor(status: number, body: ApiErrorBody) {
    super(body.code);
    this.name = "ApiError";
    this.status = status;
    this.code = body.code;
    this.fields = body.fields ?? {};
    this.requestId = body.requestId;
  }

  /** i18n key for the human-readable message. */
  get messageKey(): string {
    return `error.${this.code}`;
  }
}

/** Raised when the request never reached the server. */
export class NetworkError extends Error {
  readonly code = "NETWORK";
  get messageKey(): string {
    return "error.NETWORK";
  }
}

export async function apiFetch<T>(
  path: string,
  init?: RequestInit,
): Promise<T> {
  let res: Response;
  try {
    res = await fetch(path, {
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
        ...(init?.headers ?? {}),
      },
      ...init,
    });
  } catch {
    throw new NetworkError();
  }

  if (!res.ok) {
    let body: ApiErrorBody = { code: "UNKNOWN" };
    try {
      body = (await res.json()) as ApiErrorBody;
    } catch {
      /* non-JSON error body; keep UNKNOWN */
    }
    throw new ApiError(res.status, body);
  }

  if (res.status === 204) return undefined as T;
  return (await res.json()) as T;
}

// ---------------------------------------------------------------------------

export interface MenuNode {
  id: string;
  /**
   * The Chinese label stored in tbl_permission.fieldname. An opaque
   * authorization key -- never render it, never translate it.
   */
  permissionKey: string;
  /** i18n key for the display label. */
  labelKey: string;
  path?: string;
  children?: MenuNode[];
}

export interface MeResponse {
  username: string;
  displayName: string;
  permissions: Record<string, string>;
  menu: MenuNode[];
}

export function fetchMe(): Promise<MeResponse> {
  return apiFetch<MeResponse>("/api/auth/me");
}

/**
 * Log in. The session token comes back as an HttpOnly cookie, which this code
 * deliberately cannot read (D17) — the browser attaches it to later requests
 * because every call sets `credentials: "include"`.
 */
export function login(username: string, password: string): Promise<MeResponse> {
  return apiFetch<MeResponse>("/api/auth/login", {
    method: "POST",
    body: JSON.stringify({ username, password }),
  });
}

/**
 * Log out.
 *
 * Never rejects: a user who clicked "sign out" must end up signed out in the
 * UI even if the request failed. The server-side revoke is what actually ends
 * the session, and it is retried implicitly by the session expiring.
 */
export async function logout(): Promise<void> {
  try {
    await apiFetch<void>("/api/auth/logout", { method: "POST" });
  } catch {
    /* deliberately ignored — see above */
  }
}
