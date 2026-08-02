import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import { ApiError, NetworkError } from "../api/client";
import {
  TYPE_KEY_VALUE,
  TYPE_VALUE_ONLY,
  addValue,
  createDomain,
  deleteDomain,
  deleteValue,
  fetchDomains,
  fetchValues,
  importValues,
  renameDomain,
  updateValue,
  type BaseDomain,
  type BaseValue,
} from "../api/basedata";
import ConfirmDialog from "../components/ConfirmDialog";
import FormField from "../components/FormField";
import Modal from "../components/Modal";
import { useCanWrite } from "../state/permissions";

/**
 * Reference data (slice 2) — the 17 dropdown domains behind every other screen.
 *
 * Two panes, matching the legacy FrmBaseData: domains on the left, the selected
 * domain's values on the right.
 *
 * D18 — no database i18n. Values are displayed exactly as stored, so an
 * English-locale user sees English chrome around Chinese options. Deliberate:
 * it keeps tbl_basedata byte-identical to source, which the Phase 1 checksums
 * depend on.
 *
 * Q5, decided here: the 17 KNOWN domain names get a catalogue label
 * (`basedata.domain.<name>`), because they are chrome and the set is fixed.
 * Anything else — including domains an operator creates at runtime — falls
 * back to the stored Chinese. A missing label must never render as a raw key.
 */

const PERMISSION_KEY = "基础信息";

/**
 * Domain names that have a translated label. Keyed by the stored Chinese,
 * which is the identifier; the value is the i18n key.
 */
const DOMAIN_LABELS: Record<string, string> = {
  车系列: "basedata.domain.carSeries",
  车型大类: "basedata.domain.carCategory",
  进货途径: "basedata.domain.purchaseChannel",
  进车状态: "basedata.domain.inboundState",
  状态名称: "basedata.domain.stateName",
  库位: "basedata.domain.storageLocation",
  地区: "basedata.domain.region",
  行业: "basedata.domain.industry",
  销售方式: "basedata.domain.salesMethod",
  销售顾问: "basedata.domain.salesConsultant",
  经手人: "basedata.domain.handler",
  批复人: "basedata.domain.approver",
  结算方式: "basedata.domain.settlement",
  返利状态: "basedata.domain.rebateState",
  特种车类型: "basedata.domain.specialVehicleType",
  是否上报: "basedata.domain.isReported",
  是否开票: "basedata.domain.isInvoiced",
  是否付款: "basedata.domain.isPaid",
  是否提车: "basedata.domain.isCollected",
};

type Dialog =
  | { kind: "none" }
  | { kind: "addDomain" }
  | { kind: "renameDomain"; domain: BaseDomain }
  | { kind: "deleteDomain"; domain: BaseDomain }
  | { kind: "addValue" }
  | { kind: "editValue"; value: BaseValue }
  | { kind: "deleteValue"; value: BaseValue }
  | { kind: "import" };

function messageKeyFor(err: unknown): string {
  if (err instanceof ApiError || err instanceof NetworkError) return err.messageKey;
  return "error.UNKNOWN";
}

export default function BaseDataPage() {
  const { t } = useTranslation();
  const canWrite = useCanWrite(PERMISSION_KEY);

  const [domains, setDomains] = useState<BaseDomain[]>([]);
  const [conflicts, setConflicts] = useState<string[]>([]);
  const [selected, setSelected] = useState<string | null>(null);
  const [values, setValues] = useState<BaseValue[]>([]);
  const [loading, setLoading] = useState(true);
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [dialog, setDialog] = useState<Dialog>({ kind: "none" });

  /** Resolve a domain's display label, falling back to the stored name. */
  const labelFor = (name: string) => {
    const key = DOMAIN_LABELS[name];
    return key ? t(key) : name;
  };

  const loadDomains = useCallback(async (keepSelection?: string | null) => {
    setLoading(true);
    setErrorKey(null);
    try {
      const res = await fetchDomains();
      setDomains(res.domains);
      setConflicts(res.typeConflicts);
      setSelected((current) => {
        const wanted = keepSelection !== undefined ? keepSelection : current;
        // Keep the selection only if it still exists — a rename or delete can
        // remove it under us, and a stale selection shows an empty right pane
        // with no explanation.
        if (wanted && res.domains.some((d) => d.name === wanted)) return wanted;
        return res.domains[0]?.name ?? null;
      });
    } catch (err) {
      setErrorKey(messageKeyFor(err));
    } finally {
      setLoading(false);
    }
  }, []);

  const loadValues = useCallback(async (name: string | null) => {
    if (!name) {
      setValues([]);
      return;
    }
    try {
      setValues((await fetchValues(name)).values);
    } catch (err) {
      setErrorKey(messageKeyFor(err));
      setValues([]);
    }
  }, []);

  useEffect(() => {
    void loadDomains();
  }, [loadDomains]);

  useEffect(() => {
    void loadValues(selected);
  }, [selected, loadValues]);

  const current = domains.find((d) => d.name === selected) ?? null;
  const showKeyName = current?.type === TYPE_KEY_VALUE;

  async function run(action: () => Promise<void>, keepSelection?: string | null) {
    try {
      await action();
      setDialog({ kind: "none" });
      await loadDomains(keepSelection);
      // loadDomains may leave `selected` unchanged, in which case the effect
      // above will not re-fire — refresh the values explicitly.
      await loadValues(keepSelection !== undefined ? keepSelection : selected);
    } catch (err) {
      throw err instanceof Error ? err : new Error(String(err));
    }
  }

  return (
    <section className="page page--wide">
      <div className="page__header">
        <h1 className="page__title">{t("menu.settings.baseData")}</h1>
        {canWrite && (
          <button className="btn btn--primary" onClick={() => setDialog({ kind: "addDomain" })}>
            {t("basedata.addDomain")}
          </button>
        )}
      </div>

      {errorKey && (
        <div className="alert" role="alert">
          {t(errorKey)}
        </div>
      )}

      {conflicts.length > 0 && (
        // Surfaced, not normalised. The schema cannot express "type is constant
        // within a domain", so legacy data may violate it — and picking a
        // winner silently would destroy the evidence of a real data problem.
        <div className="notice" role="status">
          {t("basedata.typeConflict", { names: conflicts.join("、") })}
        </div>
      )}

      {loading ? (
        <p className="centered">{t("common.loading")}</p>
      ) : (
        <div className="split">
          <aside className="split__list">
            {domains.map((d) => (
              <button
                key={d.name}
                className={`split__item${d.name === selected ? " split__item--active" : ""}`}
                onClick={() => setSelected(d.name)}
              >
                <span className="split__itemName">{labelFor(d.name)}</span>
                <span className="split__itemCount">{d.count}</span>
              </button>
            ))}
            {domains.length === 0 && <p className="split__empty">{t("basedata.noDomains")}</p>}
          </aside>

          <div className="split__detail">
            {current ? (
              <>
                <div className="split__detailHeader">
                  <div>
                    <h2 className="split__detailTitle">{labelFor(current.name)}</h2>
                    {/* The stored name is the identifier. Shown alongside a
                        translated label so an operator can always see the value
                        that actually keys the data. */}
                    {DOMAIN_LABELS[current.name] && (
                      <span className="split__detailKey">{current.name}</span>
                    )}
                  </div>
                  {canWrite && (
                    <div className="split__detailActions">
                      <button className="btn btn--subtle" onClick={() => setDialog({ kind: "renameDomain", domain: current })}>
                        {t("basedata.rename")}
                      </button>
                      <button className="btn btn--subtle" onClick={() => setDialog({ kind: "import" })}>
                        {t("basedata.import")}
                      </button>
                      <button className="btn btn--subtle" onClick={() => setDialog({ kind: "addValue" })}>
                        {t("common.add")}
                      </button>
                      <button className="btn btn--danger btn--subtle" onClick={() => setDialog({ kind: "deleteDomain", domain: current })}>
                        {t("common.delete")}
                      </button>
                    </div>
                  )}
                </div>

                <table className="grid__table">
                  <thead>
                    <tr>
                      {showKeyName && <th>{t("basedata.col.keyname")}</th>}
                      <th>{t("basedata.col.value")}</th>
                      {canWrite && <th aria-label={t("basedata.col.actions")} />}
                    </tr>
                  </thead>
                  <tbody>
                    {values
                      // The blank placeholder row exists only so an empty
                      // domain can exist at all. Never offered for editing.
                      .filter((v) => v.value !== "" || v.keyname !== "")
                      .map((v) => (
                        <tr key={v.uid}>
                          {showKeyName && <td>{v.keyname}</td>}
                          <td>{v.value}</td>
                          {canWrite && (
                            <td className="grid__actions">
                              <button className="btn btn--link" onClick={() => setDialog({ kind: "editValue", value: v })}>
                                {t("common.edit")}
                              </button>
                              <button className="btn btn--link btn--danger" onClick={() => setDialog({ kind: "deleteValue", value: v })}>
                                {t("common.delete")}
                              </button>
                            </td>
                          )}
                        </tr>
                      ))}
                  </tbody>
                </table>
                {values.every((v) => v.value === "" && v.keyname === "") && (
                  <p className="split__empty">{t("basedata.noValues")}</p>
                )}
              </>
            ) : (
              <p className="split__empty">{t("basedata.noDomains")}</p>
            )}
          </div>
        </div>
      )}

      {dialog.kind === "addDomain" && (
        <DomainDialog
          titleKey="basedata.addDomain"
          onCancel={() => setDialog({ kind: "none" })}
          onSubmit={(name, type) => run(() => createDomain(name, type), name)}
          withType
        />
      )}

      {dialog.kind === "renameDomain" && (
        <DomainDialog
          titleKey="basedata.rename"
          initialName={dialog.domain.name}
          onCancel={() => setDialog({ kind: "none" })}
          onSubmit={(name) => run(() => renameDomain(dialog.domain.name, name), name)}
        />
      )}

      {dialog.kind === "deleteDomain" && (
        <ConfirmDialog
          titleKey="basedata.deleteDomain"
          bodyKey="basedata.deleteDomainBody"
          bodyValues={{ name: labelFor(dialog.domain.name), count: dialog.domain.count }}
          danger
          onCancel={() => setDialog({ kind: "none" })}
          onConfirm={() => void run(() => deleteDomain(dialog.domain.name), null)}
        />
      )}

      {dialog.kind === "addValue" && current && (
        <ValueDialog
          titleKey="basedata.addValue"
          showKeyName={showKeyName}
          onCancel={() => setDialog({ kind: "none" })}
          onSubmit={(keyname, value) =>
            run(() => addValue(current.name, keyname, value).then(() => undefined))
          }
        />
      )}

      {dialog.kind === "editValue" && (
        <ValueDialog
          titleKey="basedata.editValue"
          showKeyName={showKeyName}
          initial={dialog.value}
          onCancel={() => setDialog({ kind: "none" })}
          onSubmit={(keyname, value) =>
            run(() => updateValue(dialog.value.uid, keyname, value, dialog.value.rowVersion))
          }
        />
      )}

      {dialog.kind === "deleteValue" && (
        <ConfirmDialog
          titleKey="basedata.deleteValue"
          bodyKey="basedata.deleteValueBody"
          bodyValues={{ value: dialog.value.value }}
          danger
          onCancel={() => setDialog({ kind: "none" })}
          onConfirm={() => void run(() => deleteValue(dialog.value.uid))}
        />
      )}

      {dialog.kind === "import" && current && (
        <ImportDialog
          domain={current}
          onCancel={() => setDialog({ kind: "none" })}
          onSubmit={(content) => run(() => importValues(current.name, content).then(() => undefined))}
        />
      )}
    </section>
  );
}

// ---------------------------------------------------------------------------

function DomainDialog({
  titleKey,
  initialName = "",
  withType,
  onCancel,
  onSubmit,
}: {
  titleKey: string;
  initialName?: string;
  withType?: boolean;
  onCancel: () => void;
  onSubmit: (name: string, type: number) => Promise<void>;
}) {
  const { t } = useTranslation();
  const [name, setName] = useState(initialName);
  const [type, setType] = useState(TYPE_VALUE_ONLY);
  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({});
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    setBusy(true);
    setErrorKey(null);
    setFieldErrors({});
    try {
      await onSubmit(name, type);
    } catch (err) {
      if (err instanceof ApiError && (err.status === 422 || err.status === 409)) {
        // 409 carries {"name": "DUPLICATE"}, so the message lands on the input
        // rather than in a banner the user has to map back to a field.
        setFieldErrors(err.fields);
        if (Object.keys(err.fields).length === 0) setErrorKey(err.messageKey);
      } else {
        setErrorKey(messageKeyFor(err));
      }
      setBusy(false);
    }
  }

  return (
    <Modal
      titleKey={titleKey}
      onClose={onCancel}
      busy={busy}
      footer={
        <>
          <button type="button" className="btn" onClick={onCancel} disabled={busy}>
            {t("common.cancel")}
          </button>
          <button type="submit" form="domain-form" className="btn btn--primary" disabled={busy}>
            {busy ? t("common.saving") : t("common.save")}
          </button>
        </>
      }
    >
      <form id="domain-form" onSubmit={(e) => void submit(e)} noValidate>
        {errorKey && (
          <div className="alert" role="alert">
            {t(errorKey)}
          </div>
        )}
        <FormField
          name="name"
          labelKey="basedata.domainName"
          value={name}
          onChange={setName}
          errorCode={fieldErrors.name}
          required
        />
        {withType && (
          <fieldset className="field">
            <legend className="field__label">{t("basedata.domainType")}</legend>
            {/*
              Type is fixed at creation and never offered on rename: it governs
              the shape of every row underneath, and flipping it later would
              leave a domain half-keyed. The legacy app took the same position.
            */}
            <label className="field__radio">
              <input
                type="radio"
                checked={type === TYPE_VALUE_ONLY}
                onChange={() => setType(TYPE_VALUE_ONLY)}
              />
              {t("basedata.typeValueOnly")}
            </label>
            <label className="field__radio">
              <input
                type="radio"
                checked={type === TYPE_KEY_VALUE}
                onChange={() => setType(TYPE_KEY_VALUE)}
              />
              {t("basedata.typeKeyValue")}
            </label>
          </fieldset>
        )}
      </form>
    </Modal>
  );
}

function ValueDialog({
  titleKey,
  showKeyName,
  initial,
  onCancel,
  onSubmit,
}: {
  titleKey: string;
  showKeyName: boolean;
  initial?: BaseValue;
  onCancel: () => void;
  onSubmit: (keyname: string, value: string) => Promise<void>;
}) {
  const { t } = useTranslation();
  const [keyname, setKeyName] = useState(initial?.keyname ?? "");
  const [value, setValue] = useState(initial?.value ?? "");
  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({});
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    setBusy(true);
    setErrorKey(null);
    setFieldErrors({});
    try {
      await onSubmit(keyname, value);
    } catch (err) {
      if (err instanceof ApiError && err.status === 422) setFieldErrors(err.fields);
      else setErrorKey(messageKeyFor(err));
      setBusy(false);
    }
  }

  return (
    <Modal
      titleKey={titleKey}
      onClose={onCancel}
      busy={busy}
      footer={
        <>
          <button type="button" className="btn" onClick={onCancel} disabled={busy}>
            {t("common.cancel")}
          </button>
          <button type="submit" form="value-form" className="btn btn--primary" disabled={busy}>
            {busy ? t("common.saving") : t("common.save")}
          </button>
        </>
      }
    >
      <form id="value-form" onSubmit={(e) => void submit(e)} noValidate>
        {errorKey && (
          <div className="alert" role="alert">
            {t(errorKey)}
          </div>
        )}
        {showKeyName && (
          <FormField
            name="keyname"
            labelKey="basedata.col.keyname"
            value={keyname}
            onChange={setKeyName}
            errorCode={fieldErrors.keyname}
          />
        )}
        <FormField
          name="value"
          labelKey="basedata.col.value"
          value={value}
          onChange={setValue}
          errorCode={fieldErrors.value}
          required
        />
      </form>
    </Modal>
  );
}

function ImportDialog({
  domain,
  onCancel,
  onSubmit,
}: {
  domain: BaseDomain;
  onCancel: () => void;
  onSubmit: (content: string) => Promise<void>;
}) {
  const { t } = useTranslation();
  const [content, setContent] = useState("");
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  const lines = content.split(/\r?\n/).filter((l) => l.trim() !== "").length;

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    setBusy(true);
    setErrorKey(null);
    try {
      await onSubmit(content);
    } catch (err) {
      setErrorKey(messageKeyFor(err));
      setBusy(false);
    }
  }

  return (
    <Modal
      titleKey="basedata.import"
      onClose={onCancel}
      busy={busy}
      footer={
        <>
          <button type="button" className="btn" onClick={onCancel} disabled={busy}>
            {t("common.cancel")}
          </button>
          <button
            type="submit"
            form="import-form"
            className="btn btn--primary"
            disabled={busy || lines === 0}
          >
            {busy ? t("common.saving") : t("basedata.importCount", { count: lines })}
          </button>
        </>
      }
    >
      <form id="import-form" onSubmit={(e) => void submit(e)} noValidate>
        {errorKey && (
          <div className="alert" role="alert">
            {t(errorKey)}
          </div>
        )}
        {/*
          The format is the legacy one, reproduced rather than improved:
          operators have files in it already, and a stricter parser would
          reject work that used to load.
        */}
        <p className="field__label">
          {domain.type === TYPE_KEY_VALUE
            ? t("basedata.importHelpKeyValue")
            : t("basedata.importHelpValueOnly")}
        </p>
        <textarea
          className="field__input field__textarea"
          rows={12}
          value={content}
          onChange={(e) => setContent(e.target.value)}
          aria-label={t("basedata.import")}
        />
      </form>
    </Modal>
  );
}
