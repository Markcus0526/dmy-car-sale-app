import { useCallback, useEffect, useMemo, useState } from "react";
import { useTranslation } from "react-i18next";
import type { ColumnDef } from "@tanstack/react-table";

import { ApiError, NetworkError, listOnRoad, type OnRoadRow } from "../api/client";
import DataGrid from "../components/DataGrid";
import FilterBar from "../components/FilterBar";
import { formatDecimal, asDecimal } from "../types/decimal";
import type { Filter, FilterField } from "../types/filter";

/**
 * The first real list screen, and the template for slices 4–8.
 *
 * Field names are the API's, not column names — the server maps them through
 * an allowlist, so nothing here can name a database column.
 */
const FILTER_FIELDS: FilterField[] = [
  { field: "vin", labelKey: "onroad.col.vin", kind: "text" },
  { field: "engineno", labelKey: "onroad.col.engineno", kind: "text" },
  { field: "billno", labelKey: "onroad.col.billno", kind: "text" },
  { field: "carname", labelKey: "onroad.col.carname", kind: "text" },
  { field: "carseries", labelKey: "onroad.col.carseries", kind: "text" },
  { field: "colorname", labelKey: "onroad.col.colorname", kind: "text" },
  { field: "carstate", labelKey: "onroad.col.carstate", kind: "text" },
  { field: "billdate", labelKey: "onroad.col.billdate", kind: "date" },
];

export default function OnRoadPage() {
  const { t, i18n } = useTranslation();

  const [rows, setRows] = useState<OnRoadRow[]>([]);
  const [loading, setLoading] = useState(true);
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [truncated, setTruncated] = useState(false);

  const search = useCallback(async (filter: Filter) => {
    setLoading(true);
    setErrorKey(null);
    try {
      const res = await listOnRoad(filter);
      setRows(res.rows);
      setTruncated(res.truncated);
    } catch (err) {
      setErrorKey(
        err instanceof ApiError || err instanceof NetworkError
          ? err.messageKey
          : "error.UNKNOWN",
      );
      setRows([]);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void search({ conditions: [] });
  }, [search]);

  const columns = useMemo<ColumnDef<OnRoadRow, unknown>[]>(
    () => [
      { accessorKey: "vin", header: () => t("onroad.col.vin") },
      { accessorKey: "engineno", header: () => t("onroad.col.engineno") },
      { accessorKey: "carseries", header: () => t("onroad.col.carseries") },
      { accessorKey: "carname", header: () => t("onroad.col.carname") },
      { accessorKey: "colorname", header: () => t("onroad.col.colorname") },
      { accessorKey: "carstate", header: () => t("onroad.col.carstate") },
      { accessorKey: "billno", header: () => t("onroad.col.billno") },
      { accessorKey: "billdate", header: () => t("onroad.col.billdate") },
      {
        accessorKey: "inprice",
        header: () => t("onroad.col.inprice"),
        // Formatted for display only. The value stays a Decimal string
        // everywhere else — no arithmetic is ever done on it here (D3).
        cell: (ctx) => {
          const v = ctx.getValue() as string | null;
          return v == null ? "" : formatDecimal(asDecimal(v), i18n.language);
        },
        meta: { numeric: true },
      },
    ],
    [t, i18n.language],
  );

  return (
    <section className="page page--wide">
      <h1 className="page__title">{t("menu.movement.onroad")}</h1>

      <FilterBar fields={FILTER_FIELDS} onSearch={(f) => void search(f)} busy={loading} />

      {errorKey && (
        <div className="alert" role="alert">
          {t(errorKey)}
        </div>
      )}

      {truncated && (
        // Said out loud rather than hidden: a silently capped list reads as
        // "there are only this many", which is how wrong conclusions start.
        <div className="notice">{t("grid.truncated")}</div>
      )}

      <DataGrid columns={columns} rows={rows} loading={loading} emptyKey="grid.empty" />
    </section>
  );
}
