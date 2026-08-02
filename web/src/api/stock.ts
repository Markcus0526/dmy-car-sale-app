import { apiFetch } from "./client";
import type { Filter } from "../types/filter";

/**
 * Stock list screens — 入库处理 over vw_storein, 出库处理 over vw_storeout.
 *
 * Both views are provisional until the Phase 0 dump (migration 0006): the
 * column contracts are exact, only the JOIN semantics are unconfirmed.
 *
 * The finance columns are deliberately absent from these types. They belong to
 * the finance engine and would have to be unpicked from here later.
 */

export interface StoreInRow {
  uid: number;
  batchno: string;
  storeplace: string;
  onroadid: number;
  indate: string | null;
  /** Decimal string — never a number. */
  inprice: string;
  passno: string;
  inpath: string;
  intype: string;
  settlementname: string;
  handlername: string;
  remark: string;
  /** 1 once dispatched. The most useful filter on this screen. */
  outflag: number;
  vin: string;
  engineno: string;
  cartype: string;
  carname: string;
  colorname: string;
  carseries: string;
}

export interface StoreOutRow {
  uid: number;
  batchno: string;
  onroadid: number;
  outbillno: string;
  salecompany: string;
  salekind: string;
  settlementname: string;
  handlername: string;
  saleplace: string;
  outdate: string | null;
  customername: string;
  customerjobkind: string;
  saleregion: string;
  carno: string;
  outprice: string;
  remark: string;
  vin: string;
  engineno: string;
  cartype: string;
  carname: string;
  colorname: string;
  carseries: string;
}

interface ListResponse<T> {
  rows: T[];
  truncated: boolean;
  limit: number;
}

export function listStoreIn(filter: Filter): Promise<ListResponse<StoreInRow>> {
  return apiFetch<ListResponse<StoreInRow>>("/api/storein/list", {
    method: "POST",
    body: JSON.stringify({ filter }),
  });
}

export function listStoreOut(filter: Filter): Promise<ListResponse<StoreOutRow>> {
  return apiFetch<ListResponse<StoreOutRow>>("/api/storeout/list", {
    method: "POST",
    body: JSON.stringify({ filter }),
  });
}
