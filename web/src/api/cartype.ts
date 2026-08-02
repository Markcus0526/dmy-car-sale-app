import { apiFetch } from "./client";

/**
 * Car types — the lookup half of 车型价格设置, enough to pick one.
 *
 * Pricing beyond `inprice` is deliberately absent from this endpoint: those
 * columns feed the finance engine, and a picker is not the place to expose
 * cost and margin to everyone who can open a vehicle form.
 */
export interface CarType {
  uid: number;
  carseries: string;
  carcode: string;
  carname: string;
  subsets: string;
  /** Default cost price. A decimal string — never a number. */
  inprice: string;
  insidesetcode: string;
  insidesetname: string;
  vinprefix: string;
  enginenoprefix: string;
}

export interface CarTypeListResponse {
  types: CarType[];
  truncated: boolean;
  limit: number;
}

export function fetchCarTypes(): Promise<CarTypeListResponse> {
  return apiFetch<CarTypeListResponse>("/api/cartypes");
}

/**
 * Resolve one car type, including soft-deleted ones.
 *
 * Used when opening an existing vehicle whose type has since been
 * discontinued: it is gone from the picker but must still render, or the field
 * blanks out on exactly the historic records people look up most.
 */
export function fetchCarType(uid: number): Promise<CarType> {
  return apiFetch<CarType>(`/api/cartypes/${uid}`);
}
