import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import {
  ApiError,
  NetworkError,
  createOnRoad,
  getOnRoad,
  updateOnRoad,
  type OnRoadForm as FormValues,
} from "../api/client";
import FormField from "../components/FormField";
import Modal from "../components/Modal";

/**
 * Create/edit for on-road vehicles — and the template for the ~25 `*Add` /
 * `*Edit` screens the legacy app built one at a time.
 *
 * Three things this establishes for every screen that follows:
 *
 *   1. Field errors come back from the server as CODES and are resolved here,
 *      so validation messages are localised without the backend knowing a
 *      locale exists.
 *   2. Editing re-reads the row on open and carries its rowVersion back on
 *      save, so a concurrent edit is a visible 409 rather than a silent
 *      overwrite (§11.4).
 *   3. Money is typed and submitted as a string, never a number.
 */

const EMPTY: FormValues = {
  billno: "",
  billdate: "",
  vin: "",
  engineno: "",
  cartypeid: 0,
  cartype: "",
  carname: "",
  colorcode: "",
  colorname: "",
  subsets: "",
  insidesetcode: "",
  insidesetname: "",
  carstate: "",
  property: "",
  inprice: "",
  rowVersion: 0,
};

interface Props {
  /** undefined creates; a uid edits. */
  uid?: number;
  onClose: () => void;
  /** Called after a successful save so the list can refresh. */
  onSaved: () => void;
}

export default function OnRoadFormModal({ uid, onClose, onSaved }: Props) {
  const { t } = useTranslation();
  const editing = uid !== undefined;

  const [values, setValues] = useState<FormValues>(EMPTY);
  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({});
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [loading, setLoading] = useState(editing);
  const [saving, setSaving] = useState(false);
  /** Set on 409. The form stays open with the user's edits intact. */
  const [conflict, setConflict] = useState(false);

  const load = useCallback(async () => {
    if (uid === undefined) return;
    setLoading(true);
    setErrorKey(null);
    try {
      const detail = await getOnRoad(uid);
      const r = detail.row;
      setValues({
        billno: r.billno,
        // <input type="date"> requires exactly yyyy-mm-dd. The API sends that
        // shape already; anything else would silently render as blank.
        billdate: r.billdate ?? "",
        vin: r.vin,
        engineno: r.engineno,
        cartypeid: r.cartypeid,
        cartype: r.cartype,
        carname: r.carname,
        colorcode: r.colorcode,
        colorname: r.colorname,
        subsets: r.subsets,
        insidesetcode: r.insidesetcode,
        insidesetname: r.insidesetname,
        carstate: r.carstate,
        property: r.property,
        inprice: r.inprice ?? "",
        rowVersion: detail.rowVersion,
      });
      setConflict(false);
    } catch (err) {
      setErrorKey(messageKeyFor(err));
    } finally {
      setLoading(false);
    }
  }, [uid]);

  useEffect(() => {
    void load();
  }, [load]);

  const set = <K extends keyof FormValues>(key: K) =>
    (raw: string) => {
      setValues((v) => ({
        ...v,
        [key]: key === "cartypeid" ? Number(raw) || 0 : raw,
      }));
      // Clear this field's error as soon as the user touches it: leaving a
      // stale red message under a field being corrected is just noise.
      setFieldErrors((e) => {
        if (!(key in e)) return e;
        const { [key]: _removed, ...rest } = e;
        return rest;
      });
    };

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setSaving(true);
    setErrorKey(null);
    setFieldErrors({});
    setConflict(false);
    try {
      if (editing) {
        await updateOnRoad(uid, values);
      } else {
        await createOnRoad(values);
      }
      onSaved();
      onClose();
    } catch (err) {
      if (err instanceof ApiError && err.status === 422) {
        setFieldErrors(err.fields);
      } else if (err instanceof ApiError && err.status === 409) {
        // Someone else saved first. The form stays open holding the user's
        // work — discarding it here would be the second data loss in a row.
        setConflict(true);
      } else {
        setErrorKey(messageKeyFor(err));
      }
    } finally {
      setSaving(false);
    }
  }

  const footer = (
    <>
      <button type="button" className="btn" onClick={onClose} disabled={saving}>
        {t("common.cancel")}
      </button>
      <button
        type="submit"
        form="onroad-form"
        className="btn btn--primary"
        disabled={saving || loading}
      >
        {saving ? t("common.saving") : t("common.save")}
      </button>
    </>
  );

  return (
    <Modal
      titleKey={editing ? "onroad.form.editTitle" : "onroad.form.addTitle"}
      onClose={onClose}
      busy={saving}
      footer={footer}
    >
      {loading ? (
        <p className="centered">{t("common.loading")}</p>
      ) : (
        <form id="onroad-form" onSubmit={(e) => void handleSubmit(e)} noValidate>
          {errorKey && (
            <div className="alert" role="alert">
              {t(errorKey)}
            </div>
          )}

          {conflict && (
            <div className="alert alert--warn" role="alert">
              <p>{t("error.CONFLICT")}</p>
              <button type="button" className="btn" onClick={() => void load()}>
                {t("onroad.form.reload")}
              </button>
            </div>
          )}

          <div className="form__grid">
            <FormField
              name="billno"
              labelKey="onroad.col.billno"
              value={values.billno}
              onChange={set("billno")}
              errorCode={fieldErrors.billno}
              required
            />
            <FormField
              name="billdate"
              labelKey="onroad.col.billdate"
              type="date"
              value={values.billdate}
              onChange={set("billdate")}
              errorCode={fieldErrors.billdate}
              required
            />
            <FormField
              name="vin"
              labelKey="onroad.col.vin"
              value={values.vin}
              onChange={set("vin")}
              errorCode={fieldErrors.vin}
              required
            />
            <FormField
              name="engineno"
              labelKey="onroad.col.engineno"
              value={values.engineno}
              onChange={set("engineno")}
              errorCode={fieldErrors.engineno}
              required
            />
            {/*
              A raw cartypeid box is a stopgap. This becomes a picker once
              slice 2 lands tbl_cartype, which is also what supplies carseries
              and the default cost price. Flagged rather than hidden: typing a
              foreign key by hand is not a shippable interaction.
            */}
            <FormField
              name="cartypeid"
              labelKey="onroad.col.cartypeid"
              type="number"
              value={values.cartypeid ? String(values.cartypeid) : ""}
              onChange={set("cartypeid")}
              errorCode={fieldErrors.cartypeid}
              required
            />
            <FormField
              name="cartype"
              labelKey="onroad.col.cartype"
              value={values.cartype}
              onChange={set("cartype")}
              errorCode={fieldErrors.cartype}
              required
            />
            <FormField
              name="carname"
              labelKey="onroad.col.carname"
              value={values.carname}
              onChange={set("carname")}
              errorCode={fieldErrors.carname}
            />
            <FormField
              name="colorcode"
              labelKey="onroad.col.colorcode"
              value={values.colorcode}
              onChange={set("colorcode")}
              errorCode={fieldErrors.colorcode}
            />
            <FormField
              name="colorname"
              labelKey="onroad.col.colorname"
              value={values.colorname}
              onChange={set("colorname")}
              errorCode={fieldErrors.colorname}
            />
            <FormField
              name="subsets"
              labelKey="onroad.col.subsets"
              value={values.subsets}
              onChange={set("subsets")}
              errorCode={fieldErrors.subsets}
            />
            <FormField
              name="insidesetcode"
              labelKey="onroad.col.insidesetcode"
              value={values.insidesetcode}
              onChange={set("insidesetcode")}
              errorCode={fieldErrors.insidesetcode}
            />
            <FormField
              name="insidesetname"
              labelKey="onroad.col.insidesetname"
              value={values.insidesetname}
              onChange={set("insidesetname")}
              errorCode={fieldErrors.insidesetname}
            />
            <FormField
              name="carstate"
              labelKey="onroad.col.carstate"
              value={values.carstate}
              onChange={set("carstate")}
              errorCode={fieldErrors.carstate}
            />
            <FormField
              name="property"
              labelKey="onroad.col.property"
              value={values.property}
              onChange={set("property")}
              errorCode={fieldErrors.property}
            />
            {/*
              type="text", not type="number". A number input hands back a
              JS number, and 148 decimal columns in this system cannot survive
              float64 (§11.2). It stays a string from keystroke to column.
            */}
            <FormField
              name="inprice"
              labelKey="onroad.col.inprice"
              value={values.inprice}
              onChange={set("inprice")}
              errorCode={fieldErrors.inprice}
              numeric
            />
          </div>
        </form>
      )}
    </Modal>
  );
}

function messageKeyFor(err: unknown): string {
  if (err instanceof ApiError || err instanceof NetworkError) return err.messageKey;
  return "error.UNKNOWN";
}
