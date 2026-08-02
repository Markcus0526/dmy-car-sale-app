import { useCallback, useEffect, useMemo, useState } from "react";
import { useTranslation } from "react-i18next";
import type { ColumnDef } from "@tanstack/react-table";

import { ApiError, NetworkError } from "../api/client";
import { listStoreOut, type StoreOutRow } from "../api/stock";
import DataGrid from "../components/DataGrid";
import FilterBar from "../components/FilterBar";
import HistoryModal from "../components/HistoryModal";
import { formatDecimal, asDecimal } from "../types/decimal";
import type { Filter, FilterField } from "../types/filter";

/** 出库处理 — dispatched vehicles. Slice 7's list. */

const FILTER_FIELDS: FilterField[] = [
  { field: "vin", labelKey: "onroad.col.vin", kind: "text" },
  { field: "outbillno", labelKey: "storeout.outbillno", kind: "text" },
  { field: "customername", labelKey: "storeout.customer", kind: "text" },
  { field: "salecompany", labelKey: "storeout.company", kind: "text" },
  { field: "salekind", labelKey: "storeout.kind", kind: "text" },
  { field: "saleregion", labelKey: "storeout.region", kind: "text" },
  { field: "carno", labelKey: "storeout.carno", kind: "text" },
  { field: "handlername", labelKey: "storein.handler", kind: "text" },
  { field: "outdate", labelKey: "storeout.outdate", kind: "date" },
];

export default function StoreOutPage() {
  const { t, i18n } = useTranslation();

  const [rows, setRows] = useState<StoreOutRow[]>([]);
  const [loading, setLoading] = useState(true);
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [truncated, setTruncated] = useState(false);
  const [history, setHistory] = useState<StoreOutRow | null>(null);

  const search = useCallback(async (filter: Filter) => {
    setLoading(true);
    setErrorKey(null);
    try {
      const res = await listStoreOut(filter);
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

  const columns = useMemo<ColumnDef<StoreOutRow, unknown>[]>(
    () => [
      { accessorKey: "vin", header: () => t("onroad.col.vin") },
      { accessorKey: "outbillno", header: () => t("storeout.outbillno") },
      { accessorKey: "outdate", header: () => t("storeout.outdate") },
      { accessorKey: "customername", header: () => t("storeout.customer") },
      { accessorKey: "salecompany", header: () => t("storeout.company") },
      { accessorKey: "salekind", header: () => t("storeout.kind") },
      { accessorKey: "saleregion", header: () => t("storeout.region") },
      { accessorKey: "carno", header: () => t("storeout.carno") },
      {
        accessorKey: "outprice",
        header: () => t("storeout.outprice"),
        cell: (ctx) => {
          const v = ctx.getValue() as string | null;
          return v == null ? "" : formatDecimal(asDecimal(v), i18n.language);
        },
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
        <h1 className="page__title">{t("menu.movement.storeout")}</h1>
      </div>

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
