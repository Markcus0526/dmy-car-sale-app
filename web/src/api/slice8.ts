import { apiFetch } from "./client";
import type { Filter } from "../types/filter";

/**
 * 特种车统计表 and 赠送装修 (slice 8).
 *
 * What makes a vehicle "special" is `salekind = 大客户` (key-account sales),
 * NOT `carspeckind` as the screen name suggests — FrmSpecCar.cs:327. The
 * server sends the selector rather than this file hardcoding it, because it is
 * a stored data value and the Phase 0 dump may show the view already filters.
 */

export interface SpecCarRow {
  uid: number;
  onroadid: number;
  batchno: string;
  outbillno: string;
  salecompany: string;
  salekind: string;
  carspeckind: string;
  settlementname: string;
  handlername: string;
  outdate: string | null;
  customername: string;
  saleregion: string;
  carno: string;
  /** Decimal strings, or null for "not recorded" — never a number, never 0. */
  inprice: string | null;
  outprice: string;
  profitval: string | null;
  specprofitval: string | null;
  pricediff: string | null;
  isreport: string;
  remark: string;
  vin: string;
  engineno: string;
  cartype: string;
  carname: string;
  colorname: string;
}

export interface SpecCarListResponse {
  rows: SpecCarRow[];
  truncated: boolean;
  limit: number;
  /** The stored salekind value that selects special vehicles. */
  specialSaleKind: string;
}

export function listSpecCar(filter: Filter): Promise<SpecCarListResponse> {
  return apiFetch<SpecCarListResponse>("/api/speccar/list", {
    method: "POST",
    body: JSON.stringify({ filter }),
  });
}

// ---------------------------------------------------------------------------

export interface FitRow {
  uid: number;
  consumer: string;
  seller: string;
  fitdate: string | null;
  /** null means "no charge recorded" — distinct from a recorded zero. */
  fitprice: string | null;
  remark: string;
  rowVersion: number;
}

export function listFit(filter: Filter): Promise<{ rows: FitRow[]; truncated: boolean }> {
  return apiFetch<{ rows: FitRow[]; truncated: boolean }>("/api/fit/list", {
    method: "POST",
    body: JSON.stringify({ filter }),
  });
}

export interface FitPayload {
  consumer: string;
  seller: string;
  fitdate: string;
  fitprice: string;
  remark: string;
  rowVersion?: number;
}

export function createFit(p: FitPayload): Promise<{ uid: number }> {
  return apiFetch<{ uid: number }>("/api/fit", { method: "POST", body: JSON.stringify(p) });
}

export function updateFit(uid: number, p: FitPayload): Promise<void> {
  return apiFetch<void>(`/api/fit/${uid}`, { method: "PATCH", body: JSON.stringify(p) });
}

export function deleteFit(uid: number): Promise<void> {
  return apiFetch<void>(`/api/fit/${uid}`, { method: "DELETE" });
}
