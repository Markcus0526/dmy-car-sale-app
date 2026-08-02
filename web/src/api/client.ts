import type { Filter } from "../types/filter";

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

// ---------------------------------------------------------------------------
// On-road vehicles (slice 4)

export interface OnRoadRow {
  uid: number;
  billno: string;
  billdate: string | null;
  vin: string;
  engineno: string;
  cartypeid: number;
  cartype: string;
  carname: string;
  colorcode: string;
  colorname: string;
  subsets: string;
  insidesetcode: string;
  insidesetname: string;
  carstate: string;
  property: string;
  /** Decimal string — never a number. See types/decimal.ts. */
  inprice: string | null;
  inflag: number;
  inkind: number;
  carseries: string;
}

export interface OnRoadListResponse {
  rows: OnRoadRow[];
  truncated: boolean;
  limit: number;
}

/**
 * POST, not GET: the filter is a structured object, and encoding it into a
 * query string would mean inventing a serialisation and parsing it back --
 * the string-munging this whole design replaces. It is still a read.
 */
export function listOnRoad(filter: Filter): Promise<OnRoadListResponse> {
  return apiFetch<OnRoadListResponse>("/api/onroad/list", {
    method: "POST",
    body: JSON.stringify({ filter }),
  });
}

/** The writable subset of a vehicle. Narrower than OnRoadRow on purpose. */
export interface OnRoadForm {
  billno: string;
  billdate: string;
  vin: string;
  engineno: string;
  cartypeid: number;
  cartype: string;
  carname: string;
  colorcode: string;
  colorname: string;
  subsets: string;
  insidesetcode: string;
  insidesetname: string;
  carstate: string;
  property: string;
  /** Decimal string, or "" for NULL. Never a number — see types/decimal.ts. */
  inprice: string;
  /**
   * The version last read. Required on update; the server rejects a missing
   * or zero value rather than writing anyway, so the concurrency check cannot
   * be skipped by omitting it.
   */
  rowVersion: number;
}

export interface OnRoadDetail {
  row: OnRoadRow;
  /** Concurrency token. Kept beside the row, not inside it — never rendered. */
  rowVersion: number;
}

/**
 * Read one vehicle for the edit modal.
 *
 * The modal always re-reads rather than editing the row cached in the grid: a
 * grid row can be minutes old, and opening against it means the user's first
 * save conflicts on a change they never saw.
 */
export function getOnRoad(uid: number): Promise<OnRoadDetail> {
  return apiFetch<OnRoadDetail>(`/api/onroad/${uid}`);
}

export function createOnRoad(form: OnRoadForm): Promise<OnRoadDetail> {
  return apiFetch<OnRoadDetail>("/api/onroad", {
    method: "POST",
    body: JSON.stringify(form),
  });
}

export function updateOnRoad(uid: number, form: OnRoadForm): Promise<OnRoadDetail> {
  return apiFetch<OnRoadDetail>(`/api/onroad/${uid}`, {
    method: "PATCH",
    body: JSON.stringify(form),
  });
}
