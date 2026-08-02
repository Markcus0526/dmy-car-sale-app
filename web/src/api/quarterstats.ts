import { apiFetch } from "./client";

/**
 * 销售季度任务统计 (§6.3).
 *
 * `achieved` and `percent` are computed SERVER-side and echoed back on save.
 * That is deliberate: the percentage rounds half away from zero to match
 * .NET's "{0:0.00}" formatting, and JavaScript's toFixed rounds differently on
 * exactly the midpoint inputs a report comparison would flag. The client never
 * reimplements it.
 */
export interface QuarterRow {
  carseries: string;
  target: number;
  month1: number;
  month2: number;
  month3: number;
  remain: number;
  /** Grid row 7 in the original — always zero there. Carried, not invented. */
  extra: number;
  /** Computed: month1+month2+month3. */
  achieved: number;
  /** Computed, e.g. "60.00%". A string, never a number. */
  percent: string;
}

export interface QuarterBlock {
  rows: QuarterRow[];
  subtotal: QuarterRow;
}

export interface QuarterGrid {
  year: number;
  quarter: number;
  general: QuarterBlock;
  special: QuarterBlock;
}

export function fetchQuarter(year: number, quarter: number): Promise<QuarterGrid> {
  return apiFetch<QuarterGrid>(`/api/quarterstats?year=${year}&quarter=${quarter}`);
}

/**
 * Save a WHOLE quarter.
 *
 * PUT of the entire grid, per §6.3, precisely so partial-save races disappear.
 * The legacy form saved on year/quarter switch and is the place the plan
 * identifies as most likely to lose edits.
 */
export function saveQuarter(
  year: number,
  quarter: number,
  general: QuarterRow[],
  special: QuarterRow[],
): Promise<QuarterGrid> {
  return apiFetch<QuarterGrid>(`/api/quarterstats?year=${year}&quarter=${quarter}`, {
    method: "PUT",
    body: JSON.stringify({ general, special }),
  });
}
