import { useCallback, useEffect, useMemo, useState } from "react";
import { useTranslation } from "react-i18next";
import type { ColumnDef } from "@tanstack/react-table";

import { ApiError, NetworkError } from "../api/client";
import { createFit, deleteFit, listFit, updateFit, type FitRow } from "../api/slice8";
import ConfirmDialog from "../components/ConfirmDialog";
import DataGrid from "../components/DataGrid";
import FilterBar from "../components/FilterBar";
import FormField from "../components/FormField";
import Modal from "../components/Modal";
import { formatDecimal, asDecimal } from "../types/decimal";
import { useCanWrite } from "../state/permissions";
import type { Filter, FilterField } from "../types/filter";

/**
 * 赠送装修 — complimentary fit-out (tbl_fit), slice 8.
 *
 * tbl_fit has no foreign key to a vehicle or a sale: a fit-out is recorded
 * against a customer NAME, not against a car. That is the legacy shape and it
 * is left alone — adding a link would be a schema change to a table the
 * reports read, and the correct relationship is not recoverable from the repo.
 */

const PERMISSION_KEY = "赠送装修";

const FILTER_FIELDS: FilterField[] = [
  { field: "consumer", labelKey: "fit.consumer", kind: "text" },
  { field: "seller", labelKey: "fit.seller", kind: "text" },
  { field: "remark", labelKey: "fit.remark", kind: "text" },
  { field: "fitdate", labelKey: "fit.date", kind: "date" },
];

function messageKeyFor(err: unknown): string {
  if (err instanceof ApiError || err instanceof NetworkError) return err.messageKey;
  return "error.UNKNOWN";
}

type Dialog = { kind: "none" } | { kind: "edit"; row: FitRow | null } | { kind: "delete"; row: FitRow };

export default function FitPage() {
  const { t, i18n } = useTranslation();
  const canWrite = useCanWrite(PERMISSION_KEY);

  const [rows, setRows] = useState<FitRow[]>([]);
  const [loading, setLoading] = useState(true);
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [truncated, setTruncated] = useState(false);
  const [lastFilter, setLastFilter] = useState<Filter>({ conditions: [] });
  const [dialog, setDialog] = useState<Dialog>({ kind: "none" });

  const search = useCallback(async (filter: Filter) => {
    setLoading(true);
    setErrorKey(null);
    setLastFilter(filter);
    try {
      const res = await listFit(filter);
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

  const columns = useMemo<ColumnDef<FitRow, unknown>[]>(
    () => [
      { accessorKey: "consumer", header: () => t("fit.consumer") },
      { accessorKey: "seller", header: () => t("fit.seller") },
      { accessorKey: "fitdate", header: () => t("fit.date") },
      {
        accessorKey: "fitprice",
        header: () => t("fit.price"),
        // null renders blank, not "0.00": "no charge recorded" and "free" are
        // different facts and only one of them belongs in a total.
        cell: (ctx) => {
          const v = ctx.getValue() as string | null;
          return v == null ? "" : formatDecimal(asDecimal(v), i18n.language);
        },
        meta: { numeric: true },
      },
      { accessorKey: "remark", header: () => t("fit.remark") },
      ...(canWrite
        ? [
            {
              id: "actions",
              header: () => "",
              cell: (ctx) => (
                <>
                  <button
                    className="btn btn--link"
                    onClick={(e) => {
                      e.stopPropagation();
                      setDialog({ kind: "edit", row: ctx.row.original });
                    }}
                  >
                    {t("common.edit")}
                  </button>
                  <button
                    className="btn btn--link btn--danger"
                    onClick={(e) => {
                      e.stopPropagation();
                      setDialog({ kind: "delete", row: ctx.row.original });
                    }}
                  >
                    {t("common.delete")}
                  </button>
                </>
              ),
            } as ColumnDef<FitRow, unknown>,
          ]
        : []),
    ],
    [t, i18n.language, canWrite],
  );

  return (
    <section className="page page--wide">
      <div className="page__header">
        <h1 className="page__title">{t("menu.movement.repair")}</h1>
        {canWrite && (
          <button className="btn btn--primary" onClick={() => setDialog({ kind: "edit", row: null })}>
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
      {truncated && <div className="notice">{t("grid.truncated")}</div>}

      <DataGrid columns={columns} rows={rows} loading={loading} emptyKey="grid.empty" />

      {dialog.kind === "edit" && (
        <FitDialog
          row={dialog.row}
          onCancel={() => setDialog({ kind: "none" })}
          onSaved={() => {
            setDialog({ kind: "none" });
            void search(lastFilter);
          }}
        />
      )}

      {dialog.kind === "delete" && (
        <ConfirmDialog
          titleKey="fit.delete"
          bodyKey="fit.deleteBody"
          bodyValues={{ consumer: dialog.row.consumer }}
          danger
          onCancel={() => setDialog({ kind: "none" })}
          onConfirm={() => {
            void deleteFit(dialog.row.uid)
              .then(() => {
                setDialog({ kind: "none" });
                return search(lastFilter);
              })
              .catch((err) => {
                setErrorKey(messageKeyFor(err));
                setDialog({ kind: "none" });
              });
          }}
        />
      )}
    </section>
  );
}

function FitDialog({
  row,
  onCancel,
  onSaved,
}: {
  row: FitRow | null;
  onCancel: () => void;
  onSaved: () => void;
}) {
  const { t } = useTranslation();
  const [consumer, setConsumer] = useState(row?.consumer ?? "");
  const [seller, setSeller] = useState(row?.seller ?? "");
  const [fitdate, setDate] = useState(row?.fitdate ?? "");
  const [fitprice, setPrice] = useState(row?.fitprice ?? "");
  const [remark, setRemark] = useState(row?.remark ?? "");
  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({});
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [conflict, setConflict] = useState(false);
  const [busy, setBusy] = useState(false);

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    setBusy(true);
    setErrorKey(null);
    setFieldErrors({});
    setConflict(false);
    try {
      const payload = { consumer, seller, fitdate, fitprice, remark };
      if (row) {
        await updateFit(row.uid, { ...payload, rowVersion: row.rowVersion });
      } else {
        await createFit(payload);
      }
      onSaved();
    } catch (err) {
      if (err instanceof ApiError && err.status === 422) setFieldErrors(err.fields);
      else if (err instanceof ApiError && err.status === 409) setConflict(true);
      else setErrorKey(messageKeyFor(err));
      setBusy(false);
    }
  }

  return (
    <Modal
      titleKey={row ? "fit.edit" : "fit.add"}
      onClose={onCancel}
      busy={busy}
      footer={
        <>
          <button type="button" className="btn" onClick={onCancel} disabled={busy}>
            {t("common.cancel")}
          </button>
          <button type="submit" form="fit-form" className="btn btn--primary" disabled={busy}>
            {busy ? t("common.saving") : t("common.save")}
          </button>
        </>
      }
    >
      <form id="fit-form" onSubmit={(e) => void submit(e)} noValidate>
        {errorKey && (
          <div className="alert" role="alert">
            {t(errorKey)}
          </div>
        )}
        {conflict && (
          <div className="alert alert--warn" role="alert">
            <p>{t("error.CONFLICT")}</p>
          </div>
        )}
        <div className="form__grid">
          <FormField
            name="consumer"
            labelKey="fit.consumer"
            value={consumer}
            onChange={setConsumer}
            errorCode={fieldErrors.consumer}
            required
          />
          <FormField
            name="seller"
            labelKey="fit.seller"
            value={seller}
            onChange={setSeller}
            errorCode={fieldErrors.seller}
          />
          <FormField
            name="fitdate"
            labelKey="fit.date"
            type="date"
            value={fitdate}
            onChange={setDate}
            errorCode={fieldErrors.fitdate}
          />
          {/* Blank stays blank rather than becoming 0 — the column is nullable
              and "no charge recorded" is not the same as "free". */}
          <FormField
            name="fitprice"
            labelKey="fit.price"
            value={fitprice}
            onChange={setPrice}
            errorCode={fieldErrors.fitprice}
            numeric
          />
          <FormField
            name="remark"
            labelKey="fit.remark"
            value={remark}
            onChange={setRemark}
            errorCode={fieldErrors.remark}
          />
        </div>
      </form>
    </Modal>
  );
}
