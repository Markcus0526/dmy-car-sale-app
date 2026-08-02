import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import { ApiError, NetworkError } from "../api/client";
import {
  fetchQuarter,
  saveQuarter,
  type QuarterBlock,
  type QuarterGrid,
  type QuarterRow,
} from "../api/quarterstats";
import { useCanWrite } from "../state/permissions";

/**
 * 销售季度任务统计 (§6.3) — the editable dual-block grid.
 *
 * This screen is why slice 3 chose a headless table (D5): computed subtotal
 * rows, per-cell editing and a percentage column are far easier to build over
 * a headless model than to fight a packaged grid into. It does not use
 * DataGrid, which is a read-only list component — this is a spreadsheet.
 *
 * Editing is local; nothing is sent until Save, which PUTs the whole quarter.
 * The legacy form saved on year/quarter switch, which §6.3 names as the place
 * it is most likely to lose edits — so switching quarter here warns first.
 */

const PERMISSION_KEY = "销售季度任务统计";

function messageKeyFor(err: unknown): string {
  if (err instanceof ApiError || err instanceof NetworkError) return err.messageKey;
  return "error.UNKNOWN";
}

export default function QuarterTargetPage() {
  const { t } = useTranslation();
  const canWrite = useCanWrite(PERMISSION_KEY);

  const thisYear = new Date().getFullYear();
  const [year, setYear] = useState(thisYear);
  const [quarter, setQuarter] = useState(Math.floor(new Date().getMonth() / 3) + 1);
  const [grid, setGrid] = useState<QuarterGrid | null>(null);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [dirty, setDirty] = useState(false);

  const load = useCallback(async (y: number, q: number) => {
    setLoading(true);
    setErrorKey(null);
    try {
      setGrid(await fetchQuarter(y, q));
      setDirty(false);
    } catch (err) {
      setErrorKey(messageKeyFor(err));
      setGrid(null);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void load(year, quarter);
  }, [load, year, quarter]);

  /**
   * Switching year or quarter discards unsaved edits, so ask first. This is
   * the exact transition §6.3 flags as the legacy form's weak point — there it
   * fired a background save that could silently fail.
   */
  function switchTo(next: () => void) {
    if (dirty && !window.confirm(t("quarter.discardConfirm"))) return;
    next();
  }

  function editCell(block: "general" | "special", index: number, field: keyof QuarterRow, raw: string) {
    setGrid((g) => {
      if (!g) return g;
      // Counts, so an empty box means 0 rather than NaN. Non-numeric input is
      // ignored outright instead of becoming NaN and poisoning every subtotal.
      const n = raw === "" ? 0 : Number(raw);
      if (!Number.isInteger(n) || n < 0) return g;

      const rows = g[block].rows.map((r, i) => (i === index ? { ...r, [field]: n } : r));
      return { ...g, [block]: { ...g[block], rows } };
    });
    setDirty(true);
  }

  async function save() {
    if (!grid) return;
    setSaving(true);
    setErrorKey(null);
    try {
      // The response carries the server-recomputed subtotals and percentages,
      // so the screen shows exactly what was stored.
      setGrid(await saveQuarter(year, quarter, grid.general.rows, grid.special.rows));
      setDirty(false);
    } catch (err) {
      setErrorKey(messageKeyFor(err));
    } finally {
      setSaving(false);
    }
  }

  const years = Array.from({ length: 11 }, (_, i) => thisYear - 5 + i);

  return (
    <section className="page page--wide">
      <div className="page__header">
        <h1 className="page__title">{t("menu.statistics.quarterTarget")}</h1>
        <div className="split__detailActions">
          <select
            className="field__input"
            value={year}
            onChange={(e) => switchTo(() => setYear(Number(e.target.value)))}
            aria-label={t("quarter.year")}
          >
            {years.map((y) => (
              <option key={y} value={y}>
                {y}
              </option>
            ))}
          </select>
          <select
            className="field__input"
            value={quarter}
            onChange={(e) => switchTo(() => setQuarter(Number(e.target.value)))}
            aria-label={t("quarter.quarter")}
          >
            {[1, 2, 3, 4].map((q) => (
              <option key={q} value={q}>
                {t("quarter.q", { n: q })}
              </option>
            ))}
          </select>
          {canWrite && (
            <button
              className="btn btn--primary"
              onClick={() => void save()}
              disabled={saving || loading || !dirty}
            >
              {saving ? t("common.saving") : t("common.save")}
            </button>
          )}
        </div>
      </div>

      {errorKey && (
        <div className="alert" role="alert">
          {t(errorKey)}
        </div>
      )}
      {dirty && <div className="notice">{t("quarter.unsaved")}</div>}

      {loading ? (
        <p className="centered">{t("common.loading")}</p>
      ) : !grid ? null : (
        <>
          <QuarterBlockTable
            titleKey="quarter.general"
            block={grid.general}
            quarter={quarter}
            editable={canWrite}
            onEdit={(i, f, v) => editCell("general", i, f, v)}
          />
          <QuarterBlockTable
            titleKey="quarter.special"
            block={grid.special}
            quarter={quarter}
            editable={canWrite}
            onEdit={(i, f, v) => editCell("special", i, f, v)}
          />
        </>
      )}
    </section>
  );
}

function QuarterBlockTable({
  titleKey,
  block,
  quarter,
  editable,
  onEdit,
}: {
  titleKey: string;
  block: QuarterBlock;
  quarter: number;
  editable: boolean;
  onEdit: (index: number, field: keyof QuarterRow, value: string) => void;
}) {
  const { t } = useTranslation();
  const months = [(quarter - 1) * 3 + 1, (quarter - 1) * 3 + 2, (quarter - 1) * 3 + 3];
  const editableFields: (keyof QuarterRow)[] = ["target", "month1", "month2", "month3", "remain"];

  if (block.rows.length === 0) {
    return (
      <>
        <h2 className="split__detailTitle">{t(titleKey)}</h2>
        <p className="split__empty">{t("quarter.noSeries")}</p>
      </>
    );
  }

  return (
    <>
      <h2 className="split__detailTitle">{t(titleKey)}</h2>
      <div className="grid__scroll">
        <table className="grid__table">
          <thead>
            <tr>
              <th>{t("onroad.col.carseries")}</th>
              <th>{t("quarter.target")}</th>
              {months.map((m) => (
                <th key={m}>{t("quarter.month", { n: m })}</th>
              ))}
              <th>{t("quarter.achieved")}</th>
              <th>{t("quarter.remain")}</th>
              <th>{t("quarter.percent")}</th>
            </tr>
          </thead>
          <tbody>
            {block.rows.map((r, i) => (
              <tr key={r.carseries}>
                <td>{r.carseries}</td>
                {editableFields.slice(0, 4).map((f) => (
                  <td key={f} className="grid__cell--numeric">
                    {editable ? (
                      <input
                        className="grid__input"
                        type="number"
                        min={0}
                        step={1}
                        value={r[f] as number}
                        aria-label={`${r.carseries} ${f}`}
                        onChange={(e) => onEdit(i, f, e.target.value)}
                      />
                    ) : (
                      (r[f] as number)
                    )}
                  </td>
                ))}
                {/* Computed server-side, never editable — the whole point of
                    the original's read-only subtotal and percent rows. */}
                <td className="grid__cell--numeric grid__cell--computed">{r.achieved}</td>
                <td className="grid__cell--numeric">
                  {editable ? (
                    <input
                      className="grid__input"
                      type="number"
                      min={0}
                      step={1}
                      value={r.remain}
                      aria-label={`${r.carseries} remain`}
                      onChange={(e) => onEdit(i, "remain", e.target.value)}
                    />
                  ) : (
                    r.remain
                  )}
                </td>
                <td className="grid__cell--numeric grid__cell--computed">{r.percent}</td>
              </tr>
            ))}
            <tr className="grid__row--subtotal">
              <th scope="row">{t("quarter.subtotal")}</th>
              <td className="grid__cell--numeric">{block.subtotal.target}</td>
              <td className="grid__cell--numeric">{block.subtotal.month1}</td>
              <td className="grid__cell--numeric">{block.subtotal.month2}</td>
              <td className="grid__cell--numeric">{block.subtotal.month3}</td>
              <td className="grid__cell--numeric">{block.subtotal.achieved}</td>
              <td className="grid__cell--numeric">{block.subtotal.remain}</td>
              <td className="grid__cell--numeric">{block.subtotal.percent}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </>
  );
}
