import { apiFetch } from "./client";

/**
 * Vehicle lifecycle (§6.2). The transitions are transactional and guarded
 * server-side; this module only carries the payloads.
 *
 * A wrong-state response is 409 with a `state` field code —
 * ALREADY_STORED_IN / NOT_STORED_IN / ALREADY_STORED_OUT — because the request
 * is well formed and permitted, the vehicle is simply not where the user
 * thinks it is. The answer is to reload, not to correct a field.
 */

/** actionkind values, exactly as stored. Data, never translated in storage. */
export const ACTION_PURCHASE = "车辆采购";
export const ACTION_STORE_IN = "车辆入库";
export const ACTION_TRANSFER = "车辆转库";
export const ACTION_STORE_OUT = "车辆出库";

export interface Change {
  uid: number;
  changeid: number;
  batchno: string;
  onroadid: number;
  storeplace: string;
  actionkind: string;
  actiondate: string;
  actionpay: string;
  settlementname: string;
  handlername: string;
  repairstate: string;
  reservestate: string;
  remark: string;
}

export function fetchHistory(onRoadID: number): Promise<{ changes: Change[] }> {
  return apiFetch<{ changes: Change[] }>(`/api/movement/history/${onRoadID}`);
}

export function newBatchNo(): Promise<{ batchno: string }> {
  return apiFetch<{ batchno: string }>("/api/movement/batchno");
}

export interface StoreInPayload {
  onroadid: number;
  batchno: string;
  storeplace: string;
  indate: string;
  inprice: string;
  passno: string;
  companyno: number;
  inpath: string;
  intype: string;
  incarpricekind: string;
  factoryoutdate: string;
  settlementname: string;
  handlername: string;
  remark: string;
}

export function storeIn(p: StoreInPayload): Promise<{ uid: number }> {
  return apiFetch<{ uid: number }>("/api/movement/storein", {
    method: "POST",
    body: JSON.stringify(p),
  });
}

export interface TransferPayload {
  onroadid: number;
  storeplace: string;
  actionpay: string;
  settlementname: string;
  handlername: string;
  repairstate: string;
  reservestate: string;
  remark: string;
}

export function transfer(p: TransferPayload): Promise<void> {
  return apiFetch<void>("/api/movement/transfer", {
    method: "POST",
    body: JSON.stringify(p),
  });
}
