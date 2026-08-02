import { useState } from "react";
import { useTranslation } from "react-i18next";

import {
  MAX_CONDITIONS,
  opsFor,
  pruneFilter,
  type Filter,
  type FilterCondition,
  type FilterField,
  type FilterOp,
} from "../types/filter";

interface Props {
  fields: FilterField[];
  onSearch: (filter: Filter) => void;
  busy?: boolean;
}

/**
 * The replacement for FrmSearch (§2.6).
 *
 * The legacy dialog let the user pick up to three text columns and one date
 * column by clicking grid headers, then concatenated a filter string. This
 * does the same job with an explicit field picker, and emits a structured
 * filter the server validates against an allowlist.
 *
 * Written once and reused by every list screen — the legacy block was
 * copy-pasted eleven times.
 */
export default function FilterBar({ fields, onSearch, busy = false }: Props) {
  const { t } = useTranslation();

  const firstField = fields[0];
  const blank = (): FilterCondition => ({
    field: firstField?.field ?? "",
    op: firstField ? opsFor(firstField.kind)[0]! : "contains",
    value: "",
  });

  const [conditions, setConditions] = useState<FilterCondition[]>([blank()]);

  if (!firstField) return null;

  const kindOf = (field: string) =>
    fields.find((f) => f.field === field)?.kind ?? "text";

  function update(i: number, patch: Partial<FilterCondition>) {
    setConditions((prev) =>
      prev.map((c, idx) => {
        if (idx !== i) return c;
        const next = { ...c, ...patch };
        // Changing the field can invalidate the operator — a date column has
        // no `contains`. Snap to the first valid one rather than sending
        // something the server will reject.
        if (patch.field !== undefined) {
          const allowed = opsFor(kindOf(next.field));
          if (!allowed.includes(next.op)) next.op = allowed[0]!;
          next.value = "";
        }
        return next;
      }),
    );
  }

  function submit(e: React.FormEvent) {
    e.preventDefault();
    onSearch(pruneFilter({ conditions }));
  }

  function reset() {
    const cleared = [blank()];
    setConditions(cleared);
    onSearch({ conditions: [] });
  }

  return (
    <form className="filter" onSubmit={submit}>
      {conditions.map((c, i) => {
        const kind = kindOf(c.field);
        return (
          <div className="filter__row" key={i}>
            <select
              className="filter__field"
              value={c.field}
              onChange={(e) => update(i, { field: e.target.value })}
              disabled={busy}
              aria-label={t("filter.field")}
            >
              {fields.map((f) => (
                <option key={f.field} value={f.field}>
                  {t(f.labelKey)}
                </option>
              ))}
            </select>

            <select
              className="filter__op"
              value={c.op}
              onChange={(e) => update(i, { op: e.target.value as FilterOp })}
              disabled={busy}
              aria-label={t("filter.operator")}
            >
              {opsFor(kind).map((op) => (
                <option key={op} value={op}>
                  {t(`filter.op.${op}`)}
                </option>
              ))}
            </select>

            <input
              className="filter__value"
              // A native date input gives the browser's own picker and emits
              // yyyy-mm-dd, which is one of the layouts query.parseDate accepts.
              type={kind === "date" ? "date" : "text"}
              value={c.value}
              onChange={(e) => update(i, { value: e.target.value })}
              disabled={busy}
              aria-label={t("filter.value")}
            />

            {conditions.length > 1 && (
              <button
                type="button"
                className="btn btn--subtle filter__remove"
                onClick={() => setConditions((p) => p.filter((_, idx) => idx !== i))}
                disabled={busy}
                aria-label={t("filter.remove")}
              >
                ×
              </button>
            )}
          </div>
        );
      })}

      <div className="filter__actions">
        <button
          type="button"
          className="btn btn--subtle"
          onClick={() => setConditions((p) => [...p, blank()])}
          disabled={busy || conditions.length >= MAX_CONDITIONS}
        >
          {t("filter.addCondition")}
        </button>
        <span className="filter__spacer" />
        <button type="button" className="btn" onClick={reset} disabled={busy}>
          {t("common.reset")}
        </button>
        <button type="submit" className="btn btn--primary" disabled={busy}>
          {busy ? t("common.loading") : t("common.search")}
        </button>
      </div>

      {/* All conditions are ANDed, as the legacy dialog always did. Saying so
          avoids the user assuming OR and mis-reading an empty result. */}
      {conditions.length > 1 && <p className="filter__hint">{t("filter.allMustMatch")}</p>}
    </form>
  );
}
