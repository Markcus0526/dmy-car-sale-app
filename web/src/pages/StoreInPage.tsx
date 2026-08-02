import { useCallback, useEffect, useMemo, useState } from "react";
import { useTranslation } from "react-i18next";
import type { ColumnDef } from "@tanstack/react-table";

import { ApiError, NetworkError } from "../api/client";
import { listStoreIn, type StoreInRow } from "../api/stock";
import { transfer } from "../api/movement";
import DataGrid from "../components/DataGrid";
import FilterBar from "../components/FilterBar";
import HistoryModal from "../components/HistoryModal";
import Modal from "../components/Modal";
import FormField from "../components/FormField";
import LookupSelect from "../components/LookupSelect";
import { formatDecimal, asDecimal } from "../types/decimal";
import { useCanWrite } from "../state/permissions";
import type { Filter, FilterField } from "../types/filter";

/** 入库处理 — vehicles in stock. Slice 5's list, with the slice 6 transfer. */

const PERMISSION_STOREIN = "入库处理";
const PERMISSION_STORECHANGE = "库位变化";

const FILTER_FIELDS: FilterField[] = [
  { field: "vin", labelKey: "onroad.col.vin", kind: "text" },
  { field: "engineno", labelKey: "onroad.col.engineno", kind: "text" },
  { field: "batchno", labelKey: "storein.batchno", kind: "text" },
  { field: "storeplace", labelKey: "storein.storeplace", kind: "text" },
  { field: "carname", labelKey: "onroad.col.carname", kind: "text" },
  { field: "carseries", labelKey: "onroad.col.carseries", kind: "text" },
  { field: "colorname", labelKey: "onroad.col.colorname", kind: "text" },
  { field: "handlername", labelKey: "storein.handler", kind: "text" },
  { field: "indate", labelKey: "storein.indate", kind: "date" },
];

function messageKeyFor(err: unknown): string {
  if (err instanceof ApiError || err instanceof NetworkError) return err.messageKey;
  return "error.UNKNOWN";
}

export default function StoreInPage() {
  const { t, i18n } = useTranslation();
  const canTransfer = useCanWrite(PERMISSION_STORECHANGE);
  useCanWrite(PERMISSION_STOREIN); // presence documents the screen's own gate

  const [rows, setRows] = useState<StoreInRow[]>([]);
  const [loading, setLoading] = useState(true);
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [truncated, setTruncated] = useState(false);
  const [lastFilter, setLastFilter] = useState<Filter>({ conditions: [] });
  const [history, setHistory] = useState<StoreInRow | null>(null);
  const [moving, setMoving] = useState<StoreInRow | null>(null);

  const search = useCallback(async (filter: Filter) => {
    setLoading(true);
    setErrorKey(null);
    setLastFilter(filter);
    try {
      const res = await listStoreIn(filter);
      setRows(res.rows);
      setTruncated(res.truncated);
    } catch (err) {
      setErrorKey(messageKeyFor(err));
      setRows([]);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void search({ conditions: [] });
  }, [search]);

  const columns = useMemo<ColumnDef<StoreInRow, unknown>[]>(
    () => [
      { accessorKey: "vin", header: () => t("onroad.col.vin") },
      { accessorKey: "batchno", header: () => t("storein.batchno") },
      { accessorKey: "storeplace", header: () => t("storein.storeplace") },
      { accessorKey: "carseries", header: () => t("onroad.col.carseries") },
      { accessorKey: "carname", header: () => t("onroad.col.carname") },
      { accessorKey: "colorname", header: () => t("onroad.col.colorname") },
      { accessorKey: "indate", header: () => t("storein.indate") },
      {
        accessorKey: "inprice",
        header: () => t("storein.inprice"),
        cell: (ctx) => {
          const v = ctx.getValue() as string | null;
          return v == null ? "" : formatDecimal(asDecimal(v), i18n.language);
        },
        meta: { numeric: true },
      },
      {
        id: "state",
        header: () => t("storein.state"),
        // outflag is the one piece of lifecycle state worth showing here:
        // whether the car is still on the lot.
        cell: (ctx) =>
          ctx.row.original.outflag === 1 ? t("storein.dispatched") : t("storein.inStock"),
      },
      {
        id: "actions",
        header: () => "",
        cell: (ctx) => (
          <>
            {canTransfer && ctx.row.original.outflag === 0 && (
              <button
                className="btn btn--link"
                onClick={(e) => {
                  e.stopPropagation();
                  setMoving(ctx.row.original);
                }}
              >
                {t("storechange.title")}
              </button>
            )}
            <button
              className="btn btn--link"
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
    [t, i18n.language, canTransfer],
  );

  return (
    <section className="page page--wide">
      <div className="page__header">
        <h1 className="page__title">{t("menu.movement.storein")}</h1>
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

      {moving && (
        <TransferForm
          row={moving}
          onClose={() => setMoving(null)}
          onDone={() => {
            setMoving(null);
            void search(lastFilter);
          }}
        />
      )}
    </section>
  );
}

/** 车辆转库 — slice 6. One transaction: move the stock row, record the movement. */
function TransferForm({
  row,
  onClose,
  onDone,
}: {
  row: StoreInRow;
  onClose: () => void;
  onDone: () => void;
}) {
  const { t } = useTranslation();
  const [storeplace, setStorePlace] = useState("");
  const [actionpay, setActionPay] = useState("");
  const [settlementname, setSettlement] = useState("");
  const [handlername, setHandler] = useState("");
  const [remark, setRemark] = useState("");
  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({});
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    setBusy(true);
    setErrorKey(null);
    setFieldErrors({});
    try {
      await transfer({
        onroadid: row.onroadid,
        storeplace,
        actionpay,
        settlementname,
        handlername,
        repairstate: "",
        reservestate: "",
        remark,
      });
      onDone();
    } catch (err) {
      if (err instanceof ApiError && (err.status === 422 || err.status === 409)) {
        setFieldErrors(err.fields);
        if (err.fields.state) setErrorKey(`error.field.${err.fields.state}`);
      } else {
        setErrorKey(messageKeyFor(err));
      }
      setBusy(false);
    }
  }

  return (
    <Modal
      titleKey="storechange.title"
      onClose={onClose}
      busy={busy}
      footer={
        <>
          <button type="button" className="btn" onClick={onClose} disabled={busy}>
            {t("common.cancel")}
          </button>
          <button type="submit" form="transfer-form" className="btn btn--primary" disabled={busy}>
            {busy ? t("common.saving") : t("storechange.submit")}
          </button>
        </>
      }
    >
      <p className="history__subject">
        {row.vin} · {t("storechange.from", { place: row.storeplace })}
      </p>
      <form id="transfer-form" onSubmit={(e) => void submit(e)} noValidate>
        {errorKey && (
          <div className="alert" role="alert">
            {t(errorKey)}
          </div>
        )}
        <div className="form__grid">
          <LookupSelect
            name="storeplace"
            labelKey="storechange.to"
            domain="库位"
            value={storeplace}
            onChange={setStorePlace}
            errorCode={fieldErrors.storeplace}
            required
          />
          <FormField
            name="actionpay"
            labelKey="storechange.pay"
            value={actionpay}
            onChange={setActionPay}
            errorCode={fieldErrors.actionpay}
            numeric
          />
          <LookupSelect
            name="settlementname"
            labelKey="storein.settlement"
            domain="批复人"
            value={settlementname}
            onChange={setSettlement}
            errorCode={fieldErrors.settlementname}
            required
          />
          <LookupSelect
            name="handlername"
            labelKey="storein.handler"
            domain="经手人"
            value={handlername}
            onChange={setHandler}
            errorCode={fieldErrors.handlername}
            required
          />
          <FormField
            name="remark"
            labelKey="storein.remark"
            value={remark}
            onChange={setRemark}
            errorCode={fieldErrors.remark}
          />
        </div>
      </form>
    </Modal>
  );
}
