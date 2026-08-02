import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import { lookup, type BaseValue } from "../api/basedata";

interface Props {
  name: string;
  labelKey: string;
  /** The tbl_basedata domain to read, e.g. 库位. Stored Chinese — an identifier. */
  domain: string;
  value: string;
  onChange: (value: string) => void;
  errorCode?: string;
  required?: boolean;
  disabled?: boolean;
}

/**
 * A dropdown backed by a tbl_basedata domain.
 *
 * This is what slice 2 was for: 库位, 进货途径, 销售方式 and the rest stop being
 * free-text boxes where every operator invents their own spelling.
 *
 * D18 — values render exactly as stored, so an English-locale user sees English
 * chrome around Chinese options. Deliberate, and the reason there is no
 * translation lookup here.
 *
 * A native <select>, unlike CarTypePicker: these domains hold a handful of
 * entries each, where a filter box would be friction rather than help.
 */
export default function LookupSelect({
  name,
  labelKey,
  domain,
  value,
  onChange,
  errorCode,
  required,
  disabled,
}: Props) {
  const { t } = useTranslation();
  const [options, setOptions] = useState<BaseValue[]>([]);
  const [failed, setFailed] = useState(false);

  useEffect(() => {
    let cancelled = false;
    lookup(domain)
      .then((res) => {
        if (!cancelled) setOptions(res.values);
      })
      .catch(() => {
        if (!cancelled) setFailed(true);
      });
    return () => {
      cancelled = true;
    };
  }, [domain]);

  /**
   * A value already on the record that is no longer in the domain — someone
   * deleted the entry after the record was written. It is still shown, because
   * silently dropping it would blank a field the user never touched.
   */
  const orphaned = value !== "" && !options.some((o) => o.value === value);

  return (
    <div className={`field${errorCode ? " field--invalid" : ""}`}>
      <label className="field__label" htmlFor={name}>
        {t(labelKey)}
        {required && (
          <span className="field__required" aria-hidden="true">
            {" *"}
          </span>
        )}
      </label>
      <select
        id={name}
        name={name}
        className="field__input"
        value={value}
        disabled={disabled}
        aria-required={required}
        aria-invalid={errorCode ? true : undefined}
        onChange={(e) => onChange(e.target.value)}
      >
        <option value="">{failed ? t("error.NETWORK") : "—"}</option>
        {orphaned && <option value={value}>{value}</option>}
        {options.map((o) => (
          <option key={o.uid} value={o.value}>
            {o.keyname ? `${o.keyname} — ${o.value}` : o.value}
          </option>
        ))}
      </select>
      {errorCode && <p className="field__error">{t(`error.field.${errorCode}`)}</p>}
    </div>
  );
}
