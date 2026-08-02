import { useCallback, useEffect, useMemo, useState } from "react";
import { useTranslation } from "react-i18next";
import type { ColumnDef } from "@tanstack/react-table";

import { ApiError, NetworkError } from "../api/client";
import { listSpecCar, type SpecCarRow } from "../api/slice8";
import DataGrid from "../components/DataGrid";
import FilterBar from "../components/FilterBar";
import HistoryModal from "../components/HistoryModal";
import { formatDecimal, asDecimal } from "../types/decimal";
import type { Filter, FilterField } from "../types/filter";

/**
 * 特种车统计表 — key-account vehicle sales (slice 8).
 *
 * "Special" here means `salekind = 大客户`, not `carspeckind` (FrmSpecCar.cs:327).
 * The view itself is unfiltered (migration 0007) because it is not known
 * whether the original filtered; the predicate is applied here as a visible,
 * removable filter condition rather than hidden in SQL.
 */

const FILTER_FIELDS: FilterField[] = [
  { field: "vin", labelKey: "onroad.col.vin", kind: "text" },
  { field: "outbillno", labelKey: "storeout.outbillno", kind: "text" },
  { field: "salekind", labelKey: "storeout.kind", kind: "text" },
  { field: "carspeckind", labelKey: "speccar.kind", kind: "text" },
  { field: "customername", labelKey: "storeout.customer", kind: "text" },
  { field: "salecompany", labelKey: "storeout.company", kind: "text" },
  { field: "saleregion", labelKey: "storeout.region", kind: "text" },
  { field: "outdate", labelKey: "storeout.outdate", kind: "date" },
];

export default function SpecCarPage() {
  const { t, i18n } = useTranslation();

  const [rows, setRows] = useState<SpecCarRow[]>([]);
  const [loading, setLoading] = useState(true);
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [truncated, setTruncated] = useState(false);
  const [history, setHistory] = useState<SpecCarRow | null>(null);
  /** The stored salekind that selects special vehicles; supplied by the server. */
  const [selector, setSelector] = useState<string | null>(null);

  const search = useCallback(async (filter: Filter) => {
    setLoading(true);
    setErrorKey(null);
    try {
      const res = await listSpecCar(filter);
      setRows(res.rows);
      setTruncated(res.truncated);
      setSelector(res.specialSaleKind);
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
    // The default filter IS the definition of this screen. Sent as an ordinary
    // condition so it shows in the filter bar and the user can widen it —
    // if the Phase 0 dump shows the view already filters, this becomes
    // redundant rather than wrong.
    void search({ conditions: [{ field: "salekind", op: "eq", value: "大客户" }] });
  }, [search]);

  const money = (v: string | null) =>
    v == null ? "" : formatDecimal(asDecimal(v), i18n.language);

  const columns = useMemo<ColumnDef<SpecCarRow, unknown>[]>(
    () => [
      { accessorKey: "vin", header: () => t("onroad.col.vin") },
      { accessorKey: "outbillno", header: () => t("storeout.outbillno") },
      { accessorKey: "outdate", header: () => t("storeout.outdate") },
      { accessorKey: "customername", header: () => t("storeout.customer") },
      { accessorKey: "salekind", header: () => t("storeout.kind") },
      { accessorKey: "carspeckind", header: () => t("speccar.kind") },
      {
        accessorKey: "outprice",
        header: () => t("storeout.outprice"),
        cell: (ctx) => money(ctx.getValue() as string | null),
        meta: { numeric: true },
      },
      {
        accessorKey: "profitval",
        header: () => t("speccar.profit"),
        cell: (ctx) => money(ctx.getValue() as string | null),
        meta: { numeric: true },
      },
      {
        accessorKey: "specprofitval",
        header: () => t("speccar.specProfit"),
        cell: (ctx) => money(ctx.getValue() as string | null),
        meta: { numeric: true },
      },
      {
        id: "actions",
        header: () => "",
        cell: (ctx) => (
          <button
            className="btn btn--link"
            onClick={(e) => {
              e.stopPropagation();
              setHistory(ctx.row.original);
            }}
          >
            {t("movement.history")}
          </button>
        ),
      },
    ],
    [t, i18n.language],
  );

  return (
    <section className="page page--wide">
      <div className="page__header">
        <h1 className="page__title">{t("menu.movement.speccar")}</h1>
      </div>

      {selector && <p className="history__subject">{t("speccar.selector", { kind: selector })}</p>}

      <FilterBar fields={FILTER_FIELDS} onSearch={(f) => void search(f)} busy={loading} />

      {errorKey && (
        <div className="alert" role="alert">
          {t(errorKey)}
        </div>
      )}
      {truncated && <div className="notice">{t("grid.truncated")}</div>}

      <DataGrid columns={columns} rows={rows} loading={loading} emptyKey="grid.empty" />

      {history && (
        <HistoryModal
          onRoadID={history.onroadid}
          vin={history.vin}
          onClose={() => setHistory(null)}
        />
      )}
    </section>
  );
}
