import { apiFetch } from "./client";

/**
 * 特殊日记 — user-authored notes (tbl_log).
 *
 * NOT an audit trail, despite the README's claim: tbl_log's only user in the
 * legacy codebase is FrmSpecJournal, an editable grid of date/title/body rows
 * that staff write by hand. Whether to add a real audit trail is Q10.
 */
export interface JournalEntry {
  uid: number;
  logdate: string;
  title: string;
  cont: string;
  rowVersion: number;
}

export interface JournalListResponse {
  entries: JournalEntry[];
  truncated: boolean;
  limit: number;
}

export function fetchJournal(): Promise<JournalListResponse> {
  return apiFetch<JournalListResponse>("/api/journal");
}

export function createJournal(logdate: string, title: string, cont: string): Promise<{ uid: number }> {
  return apiFetch<{ uid: number }>("/api/journal", {
    method: "POST",
    body: JSON.stringify({ logdate, title, cont }),
  });
}

export function updateJournal(
  uid: number,
  logdate: string,
  title: string,
  cont: string,
  rowVersion: number,
): Promise<void> {
  return apiFetch<void>(`/api/journal/${uid}`, {
    method: "PATCH",
    body: JSON.stringify({ logdate, title, cont, rowVersion }),
  });
}

export function deleteJournal(uid: number): Promise<void> {
  return apiFetch<void>(`/api/journal/${uid}`, { method: "DELETE" });
}
