import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import { ApiError, NetworkError, type OnRoadRow } from "../api/client";
import { newBatchNo, storeIn } from "../api/movement";
import FormField from "../components/FormField";
import LookupSelect from "../components/LookupSelect";
import Modal from "../components/Modal";

/**
 * Store-in — the 车辆入库 transition (§6.2).
 *
 * Not a CRUD form. It submits a transition that the server performs in one
 * transaction: flag the vehicle, write the stock row, record the movement. A
 * 409 means someone stored the same car first, and the state code says which
 * way the vehicle actually went.
 */
export default function StoreInForm({
  vehicle,
  onClose,
  onDone,
}: {
  vehicle: OnRoadRow;
  onClose: () => void;
  onDone: () => void;
}) {
  const { t } = useTranslation();

  const [batchno, setBatchNo] = useState("");
  const [storeplace, setStorePlace] = useState("");
  const [indate, setInDate] = useState(todayLocal());
  // Prefilled from the vehicle's cost price, which is the number staff expect
  // to see. Editable, because a delivery can land at a different price.
  const [inprice, setInPrice] = useState(vehicle.inprice ?? "");
  const [passno, setPassNo] = useState("");
  const [inpath, setInPath] = useState("");
  const [intype, setInType] = useState("");
  const [factoryoutdate, setFactoryOut] = useState("");
  const [settlementname, setSettlement] = useState("");
  const [handlername, setHandler] = useState("");
  const [remark, setRemark] = useState("");

  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({});
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  // Minted server-side, per §6.6: the legacy client used a 12-hour clock, so
  // 09:00 and 21:00 produced the same number.
  useEffect(() => {
    let cancelled = false;
    newBatchNo()
      .then((res) => {
        if (!cancelled) setBatchNo(res.batchno);
      })
      .catch(() => {
        /* leave blank; the server generates one if it is empty */
      });
    return () => {
      cancelled = true;
    };
  }, []);

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    setBusy(true);
    setErrorKey(null);
    setFieldErrors({});
    try {
      await storeIn({
        onroadid: vehicle.uid,
        batchno,
        storeplace,
        indate,
        inprice,
        passno,
        companyno: 0,
        inpath,
        intype,
        incarpricekind: "",
        factoryoutdate,
        settlementname,
        handlername,
        remark,
      });
      onDone();
    } catch (err) {
      if (err instanceof ApiError && (err.status === 422 || err.status === 409)) {
        setFieldErrors(err.fields);
        // A 409 carries {"state": "..."}, which belongs to no input — show it
        // as a banner rather than attaching it to an arbitrary field.
        if (err.fields.state) setErrorKey(`error.field.${err.fields.state}`);
      } else {
        setErrorKey(
          err instanceof ApiError || err instanceof NetworkError
            ? err.messageKey
            : "error.UNKNOWN",
        );
      }
      setBusy(false);
    }
  }

  return (
    <Modal
      titleKey="storein.title"
      onClose={onClose}
      busy={busy}
      footer={
        <>
          <button type="button" className="btn" onClick={onClose} disabled={busy}>
            {t("common.cancel")}
          </button>
          <button type="submit" form="storein-form" className="btn btn--primary" disabled={busy}>
            {busy ? t("common.saving") : t("storein.submit")}
          </button>
        </>
      }
    >
      <p className="history__subject">
        {vehicle.vin} · {vehicle.carname || vehicle.cartype}
      </p>

      <form id="storein-form" onSubmit={(e) => void submit(e)} noValidate>
        {errorKey && (
          <div className="alert" role="alert">
            {t(errorKey)}
          </div>
        )}

        <div className="form__grid">
          <FormField
            name="batchno"
            labelKey="storein.batchno"
            value={batchno}
            onChange={setBatchNo}
            errorCode={fieldErrors.batchno}
          />
          {/* 库位, 进货途径 and 进车状态 come from tbl_basedata — this is what
              slice 2 was for. They were free-text boxes where every operator
              invented their own spelling. */}
          <LookupSelect
            name="storeplace"
            labelKey="storein.storeplace"
            domain="库位"
            value={storeplace}
            onChange={setStorePlace}
            errorCode={fieldErrors.storeplace}
            required
          />
          <FormField
            name="indate"
            labelKey="storein.indate"
            type="date"
            value={indate}
            onChange={setInDate}
            errorCode={fieldErrors.indate}
            required
          />
          <FormField
            name="factoryoutdate"
            labelKey="storein.factoryoutdate"
            type="date"
            value={factoryoutdate}
            onChange={setFactoryOut}
            errorCode={fieldErrors.factoryoutdate}
            required
          />
          <LookupSelect
            name="inpath"
            labelKey="storein.inpath"
            domain="进货途径"
            value={inpath}
            onChange={setInPath}
            errorCode={fieldErrors.inpath}
            required
          />
          <LookupSelect
            name="intype"
            labelKey="storein.intype"
            domain="进车状态"
            value={intype}
            onChange={setInType}
            errorCode={fieldErrors.intype}
            required
          />
          <FormField
            name="passno"
            labelKey="storein.passno"
            value={passno}
            onChange={setPassNo}
            errorCode={fieldErrors.passno}
          />
          {/* text, not number: 148 decimal columns cannot survive float64. */}
          <FormField
            name="inprice"
            labelKey="storein.inprice"
            value={inprice}
            onChange={setInPrice}
            errorCode={fieldErrors.inprice}
            numeric
            required
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

/** Today from local parts — toISOString() would shift the date in Shanghai. */
function todayLocal(): string {
  const d = new Date();
  const pad = (n: number) => String(n).padStart(2, "0");
  return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}`;
}
