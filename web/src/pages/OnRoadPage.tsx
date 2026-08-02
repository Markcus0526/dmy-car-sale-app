import { useCallback, useEffect, useMemo, useState } from "react";
import { useTranslation } from "react-i18next";
import type { ColumnDef } from "@tanstack/react-table";

import { ApiError, NetworkError, listOnRoad, type OnRoadRow } from "../api/client";
import DataGrid from "../components/DataGrid";
import FilterBar from "../components/FilterBar";
import OnRoadFormModal from "./OnRoadForm";
import HistoryModal from "../components/HistoryModal";
import StoreInForm from "./StoreInForm";
import { useCanWrite } from "../state/permissions";
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

/**
 * The permission key for this screen, exactly as stored in
 * tbl_permission.fieldname. Chinese because the DATA is Chinese — an opaque
 * authorization key, never rendered and never translated.
 */
const PERMISSION_KEY = "在途/未提车辆管理";

/** undefined = closed; null = creating; a number = editing that uid. */
type Editing = undefined | null | number;

export default function OnRoadPage() {
  const { t, i18n } = useTranslation();
  const canWrite = useCanWrite(PERMISSION_KEY);
  const [editing, setEditing] = useState<Editing>(undefined);
  /** The filter the grid currently shows, so a save can re-run it. */
  const [lastFilter, setLastFilter] = useState<Filter>({ conditions: [] });
  /** The vehicle whose movement history is open, if any. */
  const [history, setHistory] = useState<OnRoadRow | null>(null);
  /** The vehicle being stored in, if any. */
  const [storingIn, setStoringIn] = useState<OnRoadRow | null>(null);
  const canStoreIn = useCanWrite("入库处理");

  const [rows, setRows] = useState<OnRoadRow[]>([]);
  const [loading, setLoading] = useState(true);
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [truncated, setTruncated] = useState(false);

  const search = useCallback(async (filter: Filter) => {
    setLoading(true);
    setErrorKey(null);
    setLastFilter(filter);
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
      {
        id: "actions",
        header: () => "",
        cell: (ctx) => (
          <>
            {/* Offered only while the vehicle is still on the road. inflag 1
                means it is already in stock, and the server would refuse. */}
            {canStoreIn && ctx.row.original.inflag === 0 && (
              <button
                className="btn btn--link"
                onClick={(e) => {
                  e.stopPropagation();
                  setStoringIn(ctx.row.original);
                }}
              >
                {t("storein.submit")}
              </button>
            )}
            <button
              className="btn btn--link"
              // The row click opens the editor; stop this one reaching it.
              onClick={(e) => {
                e.stopPropagation();
                setHistory(ctx.row.original);
              }}
            >
              {t("movement.history")}
            </button>
          </>
        ),
      },
    ],
    [t, i18n.language, canStoreIn],
  );

  return (
    <section className="page page--wide">
      <div className="page__header">
        <h1 className="page__title">{t("menu.movement.onroad")}</h1>
        {/*
          Hidden for read-only users as a courtesy, not as a control. The
          server rejects the POST regardless — §10.8 records that the legacy
          app greyed out menu items and enforced nothing behind them.
        */}
        {canWrite && (
          <button className="btn btn--primary" onClick={() => setEditing(null)}>
            {t("common.add")}
          </button>
        )}
      </div>

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

      <DataGrid
        columns={columns}
        rows={rows}
        loading={loading}
        emptyKey="grid.empty"
        // Read-only users get no row affordance at all, rather than a click
        // that opens a form they cannot save.
        onRowClick={canWrite ? (row) => setEditing(row.uid) : undefined}
      />

      {history && (
        <HistoryModal
          onRoadID={history.uid}
          vin={history.vin}
          onClose={() => setHistory(null)}
        />
      )}

      {storingIn && (
        <StoreInForm
          vehicle={storingIn}
          onClose={() => setStoringIn(null)}
          onDone={() => {
            setStoringIn(null);
            void search(lastFilter);
          }}
        />
      )}

      {editing !== undefined && (
        <OnRoadFormModal
          uid={editing ?? undefined}
          onClose={() => setEditing(undefined)}
          // Re-run the same filter rather than resetting it: a user who
          // searched, edited, and lost their search would have to redo it.
          onSaved={() => void search(lastFilter)}
        />
      )}
    </section>
  );
}
