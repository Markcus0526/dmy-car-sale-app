import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import { ApiError, NetworkError } from "../api/client";
import { fetchHistory, type Change } from "../api/movement";
import Modal from "./Modal";

/**
 * A vehicle's movement history — slice 10, what FrmActionHis actually shows.
 *
 * It reads tbl_storechange, not tbl_log: the plan called FrmActionHis an
 * audit-trail viewer over tbl_log until day 23, but that form renders a
 * List<StoreChange> its caller passes in.
 *
 * A timeline rather than the original's monospace text blob. The legacy form
 * concatenated labels and tab characters into a read-only textbox, which does
 * not wrap, does not select cleanly, and cannot be read by a screen reader.
 */
export default function HistoryModal({
  onRoadID,
  vin,
  onClose,
}: {
  onRoadID: number;
  vin: string;
  onClose: () => void;
}) {
  const { t } = useTranslation();
  const [changes, setChanges] = useState<Change[]>([]);
  const [loading, setLoading] = useState(true);
  const [errorKey, setErrorKey] = useState<string | null>(null);

  useEffect(() => {
    let cancelled = false;
    fetchHistory(onRoadID)
      .then((res) => {
        if (!cancelled) setChanges(res.changes);
      })
      .catch((err: unknown) => {
        if (cancelled) return;
        setErrorKey(
          err instanceof ApiError || err instanceof NetworkError
            ? err.messageKey
            : "error.UNKNOWN",
        );
      })
      .finally(() => {
        if (!cancelled) setLoading(false);
      });
    return () => {
      cancelled = true;
    };
  }, [onRoadID]);

  /**
   * actionkind is stored Chinese. Resolve a label from it, falling back to the
   * stored value — an unrecognised action must show as itself, never as a raw
   * i18n key.
   */
  const actionLabel = (kind: string) => {
    const key = `movement.action.${kind}`;
    const label = t(key);
    return label === key ? kind : label;
  };

  return (
    <Modal titleKey="movement.history" onClose={onClose}>
      <p className="history__subject">{t("movement.historyFor", { vin })}</p>

      {errorKey && (
        <div className="alert" role="alert">
          {t(errorKey)}
        </div>
      )}

      {loading ? (
        <p className="centered">{t("common.loading")}</p>
      ) : changes.length === 0 ? (
        <p className="split__empty">{t("movement.empty")}</p>
      ) : (
        <ol className="history">
          {changes.map((c) => (
            <li key={c.uid} className="history__item">
              <div className="history__head">
                <span className="history__action">{actionLabel(c.actionkind)}</span>
                <time className="history__date">{c.actiondate}</time>
                <span className="history__ref">#{c.changeid}</span>
              </div>
              <dl className="history__fields">
                <dt>{t("movement.col.place")}</dt>
                <dd>{c.storeplace}</dd>
                <dt>{t("movement.col.handler")}</dt>
                <dd>{c.handlername || "—"}</dd>
                <dt>{t("movement.col.settlement")}</dt>
                <dd>{c.settlementname || "—"}</dd>
                {c.actionpay !== "0.00" && c.actionpay !== "0" && (
                  <>
                    <dt>{t("movement.col.pay")}</dt>
                    <dd>{c.actionpay}</dd>
                  </>
                )}
              </dl>
              {c.remark && <p className="history__remark">{c.remark}</p>}
            </li>
          ))}
        </ol>
      )}
    </Modal>
  );
}
