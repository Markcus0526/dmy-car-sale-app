import { useEffect, useMemo, useRef, useState } from "react";
import { useTranslation } from "react-i18next";

import { fetchCarType, fetchCarTypes, type CarType } from "../api/cartype";

interface Props {
  value: number;
  onChange: (uid: number, carType: CarType | null) => void;
  errorCode?: string;
  disabled?: boolean;
}

/**
 * Type-to-filter picker for tbl_cartype, replacing the raw id box that slice 4
 * shipped as a stopgap.
 *
 * Not a plain <select>: a dealership carries hundreds of car types, and a
 * native select with hundreds of Chinese options is unusable. Not a packaged
 * combobox either — this needs no dependency, and the one behaviour that
 * matters is filtering across code AND name at once, since staff search by
 * whichever they remember.
 */
export default function CarTypePicker({ value, onChange, errorCode, disabled }: Props) {
  const { t } = useTranslation();
  const [types, setTypes] = useState<CarType[]>([]);
  const [truncated, setTruncated] = useState(false);
  const [query, setQuery] = useState("");
  const [open, setOpen] = useState(false);
  const [loadError, setLoadError] = useState(false);
  /**
   * The selected type, resolved separately when it is not in the list — a
   * soft-deleted type is gone from the picker but must still display.
   */
  const [resolved, setResolved] = useState<CarType | null>(null);
  const boxRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    let cancelled = false;
    fetchCarTypes()
      .then((res) => {
        if (cancelled) return;
        setTypes(res.types);
        setTruncated(res.truncated);
      })
      .catch(() => {
        if (!cancelled) setLoadError(true);
      });
    return () => {
      cancelled = true;
    };
  }, []);

  const selected = useMemo(
    () => types.find((c) => c.uid === value) ?? resolved,
    [types, value, resolved],
  );

  // Resolve a selection the list does not contain (discontinued type).
  useEffect(() => {
    if (!value || types.length === 0) return;
    if (types.some((c) => c.uid === value)) return;
    if (resolved?.uid === value) return;
    let cancelled = false;
    fetchCarType(value)
      .then((ct) => {
        if (!cancelled) setResolved(ct);
      })
      .catch(() => {
        /* a dangling cartypeid is possible in legacy data; leave it unresolved
           and show the raw id rather than blanking the field */
      });
    return () => {
      cancelled = true;
    };
  }, [value, types, resolved]);

  // Close when focus or a click leaves the control.
  useEffect(() => {
    if (!open) return;
    function onDocMouseDown(e: MouseEvent) {
      if (!boxRef.current?.contains(e.target as Node)) setOpen(false);
    }
    document.addEventListener("mousedown", onDocMouseDown);
    return () => document.removeEventListener("mousedown", onDocMouseDown);
  }, [open]);

  const filtered = useMemo(() => {
    const q = query.trim().toLowerCase();
    if (!q) return types.slice(0, 50);
    return types
      .filter(
        (c) =>
          c.carcode.toLowerCase().includes(q) ||
          c.carname.toLowerCase().includes(q) ||
          c.carseries.toLowerCase().includes(q),
      )
      .slice(0, 50);
  }, [types, query]);

  const label = selected
    ? `${selected.carcode} — ${selected.carname}`
    : value
      ? String(value) // dangling id: show it rather than pretending it is empty
      : "";

  return (
    <div className={`field${errorCode ? " field--invalid" : ""}`} ref={boxRef}>
      <label className="field__label" htmlFor="cartype-picker">
        {t("onroad.col.cartype")}
        <span className="field__required" aria-hidden="true">
          {" *"}
        </span>
      </label>

      <input
        id="cartype-picker"
        className="field__input"
        role="combobox"
        aria-expanded={open}
        aria-controls="cartype-listbox"
        aria-autocomplete="list"
        autoComplete="off"
        disabled={disabled}
        value={open ? query : label}
        placeholder={t("cartype.search")}
        onFocus={() => {
          setQuery("");
          setOpen(true);
        }}
        onChange={(e) => setQuery(e.target.value)}
        onKeyDown={(e) => {
          if (e.key === "Escape" && open) {
            e.stopPropagation(); // do not let Escape close the whole modal
            setOpen(false);
          }
        }}
      />

      {open && (
        <ul className="picker__list" id="cartype-listbox" role="listbox">
          {loadError && <li className="picker__empty">{t("error.NETWORK")}</li>}
          {!loadError && filtered.length === 0 && (
            <li className="picker__empty">{t("cartype.noMatches")}</li>
          )}
          {filtered.map((c) => (
            <li key={c.uid}>
              <button
                type="button"
                role="option"
                aria-selected={c.uid === value}
                className={`picker__option${c.uid === value ? " picker__option--active" : ""}`}
                // mouseDown, not click: the input's blur would close the list
                // before a click ever lands.
                onMouseDown={(e) => {
                  e.preventDefault();
                  onChange(c.uid, c);
                  setOpen(false);
                }}
              >
                <span className="picker__code">{c.carcode}</span>
                <span className="picker__name">{c.carname}</span>
                <span className="picker__series">{c.carseries}</span>
              </button>
            </li>
          ))}
          {truncated && <li className="picker__empty">{t("cartype.truncated")}</li>}
        </ul>
      )}

      {errorCode && <p className="field__error">{t(`error.field.${errorCode}`)}</p>}
    </div>
  );
}
