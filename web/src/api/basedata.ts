import { apiFetch } from "./client";

/**
 * Reference data — tbl_basedata, the key/value store behind every dropdown.
 *
 * `type` is a property of the DOMAIN, not of the row (answers Q4, read from
 * FrmBaseData.cs:93-101):
 *
 *   1 — value-only; `keyname` is hidden and stored as "".
 *   2 — key/value pair; `keyname` is shown, and import splits on the first ":".
 */
export const TYPE_VALUE_ONLY = 1;
export const TYPE_KEY_VALUE = 2;

export interface BaseDomain {
  /** The Chinese domain name as stored. Data and identifier at once. */
  name: string;
  type: number;
  /** Includes the blank placeholder row a domain carries when empty. */
  count: number;
}

export interface BaseValue {
  uid: number;
  name: string;
  type: number;
  keyname: string;
  value: string;
  rowVersion: number;
}

export interface DomainsResponse {
  domains: BaseDomain[];
  /** Domains whose rows disagree about `type` — a legacy data problem. */
  typeConflicts: string[];
}

/** Domain names travel in the path and are Chinese, so they must be encoded. */
const seg = (name: string) => encodeURIComponent(name);

export function fetchDomains(): Promise<DomainsResponse> {
  return apiFetch<DomainsResponse>("/api/basedata/domains");
}

export function fetchValues(name: string): Promise<{ values: BaseValue[] }> {
  return apiFetch<{ values: BaseValue[] }>(`/api/basedata/domains/${seg(name)}/values`);
}

export function createDomain(name: string, type: number): Promise<void> {
  return apiFetch<void>("/api/basedata/domains", {
    method: "POST",
    body: JSON.stringify({ name, type }),
  });
}

export function renameDomain(oldName: string, name: string): Promise<void> {
  return apiFetch<void>(`/api/basedata/domains/${seg(oldName)}`, {
    method: "PATCH",
    body: JSON.stringify({ name }),
  });
}

export function deleteDomain(name: string): Promise<void> {
  return apiFetch<void>(`/api/basedata/domains/${seg(name)}`, { method: "DELETE" });
}

export function addValue(name: string, keyname: string, value: string): Promise<{ uid: number }> {
  return apiFetch<{ uid: number }>(`/api/basedata/domains/${seg(name)}/values`, {
    method: "POST",
    body: JSON.stringify({ keyname, value }),
  });
}

export function updateValue(
  uid: number,
  keyname: string,
  value: string,
  rowVersion: number,
): Promise<void> {
  return apiFetch<void>(`/api/basedata/values/${uid}`, {
    method: "PATCH",
    body: JSON.stringify({ keyname, value, rowVersion }),
  });
}

export function deleteValue(uid: number): Promise<void> {
  return apiFetch<void>(`/api/basedata/values/${uid}`, { method: "DELETE" });
}

export function importValues(name: string, content: string): Promise<{ imported: number }> {
  return apiFetch<{ imported: number }>(`/api/basedata/domains/${seg(name)}/import`, {
    method: "POST",
    body: JSON.stringify({ content }),
  });
}

/**
 * Dropdown feed for other screens.
 *
 * Deliberately a different endpoint from fetchValues: it drops the blank
 * placeholder row, and it is gated on being signed in rather than on 基础信息.
 * Filling in a sale needs the 地区 list; it does not need the right to
 * administer reference data.
 */
export function lookup(name: string): Promise<{ values: BaseValue[] }> {
  return apiFetch<{ values: BaseValue[] }>(`/api/lookup/${seg(name)}`);
}
