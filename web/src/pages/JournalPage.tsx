import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import { ApiError, NetworkError } from "../api/client";
import {
  createJournal,
  deleteJournal,
  fetchJournal,
  updateJournal,
  type JournalEntry,
} from "../api/journal";
import ConfirmDialog from "../components/ConfirmDialog";
import FormField from "../components/FormField";
import Modal from "../components/Modal";
import { useCanWrite } from "../state/permissions";

/**
 * 特殊日记 — the notes screen behind FrmSpecJournal.
 *
 * Rendered as a list of cards rather than a grid: the body is free text up to
 * 200 characters, and a grid cell truncates exactly the part someone opened
 * the screen to read.
 */

const PERMISSION_KEY = "特殊日记";

type Dialog =
  | { kind: "none" }
  | { kind: "edit"; entry: JournalEntry | null }
  | { kind: "delete"; entry: JournalEntry };

function messageKeyFor(err: unknown): string {
  if (err instanceof ApiError || err instanceof NetworkError) return err.messageKey;
  return "error.UNKNOWN";
}

/** Today in the user's timezone, as yyyy-mm-dd for <input type="date">. */
function today(): string {
  const d = new Date();
  const pad = (n: number) => String(n).padStart(2, "0");
  // Built from local parts, not toISOString(): that converts to UTC first, so
  // anyone east of Greenwich gets tomorrow's date for most of their evening.
  return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}`;
}

export default function JournalPage() {
  const { t, i18n } = useTranslation();
  const canWrite = useCanWrite(PERMISSION_KEY);

  const [entries, setEntries] = useState<JournalEntry[]>([]);
  const [truncated, setTruncated] = useState(false);
  const [loading, setLoading] = useState(true);
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [dialog, setDialog] = useState<Dialog>({ kind: "none" });

  const load = useCallback(async () => {
    setLoading(true);
    setErrorKey(null);
    try {
      const res = await fetchJournal();
      setEntries(res.entries);
      setTruncated(res.truncated);
    } catch (err) {
      setErrorKey(messageKeyFor(err));
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void load();
  }, [load]);

  const dateFormat = new Intl.DateTimeFormat(i18n.language, { dateStyle: "medium" });

  return (
    <section className="page">
      <div className="page__header">
        <h1 className="page__title">{t("menu.movement.journal")}</h1>
        {canWrite && (
          <button
            className="btn btn--primary"
            onClick={() => setDialog({ kind: "edit", entry: null })}
          >
            {t("common.add")}
          </button>
        )}
      </div>

      {errorKey && (
        <div className="alert" role="alert">
          {t(errorKey)}
        </div>
      )}
      {truncated && <div className="notice">{t("grid.truncated")}</div>}

      {loading ? (
        <p className="centered">{t("common.loading")}</p>
      ) : entries.length === 0 ? (
        <p className="split__empty">{t("journal.empty")}</p>
      ) : (
        <ul className="journal">
          {entries.map((e) => (
            <li key={e.uid} className="journal__entry">
              <div className="journal__head">
                <time className="journal__date" dateTime={e.logdate}>
                  {/* Parsed as a local date, not via Date(string): a bare
                      yyyy-mm-dd is treated as UTC midnight, which renders as
                      the previous day for anyone west of Greenwich. */}
                  {dateFormat.format(
                    new Date(
                      Number(e.logdate.slice(0, 4)),
                      Number(e.logdate.slice(5, 7)) - 1,
                      Number(e.logdate.slice(8, 10)),
                    ),
                  )}
                </time>
                {e.title && <h2 className="journal__title">{e.title}</h2>}
                {canWrite && (
                  <div className="journal__actions">
                    <button
                      className="btn btn--link"
                      onClick={() => setDialog({ kind: "edit", entry: e })}
                    >
                      {t("common.edit")}
                    </button>
                    <button
                      className="btn btn--link btn--danger"
                      onClick={() => setDialog({ kind: "delete", entry: e })}
                    >
                      {t("common.delete")}
                    </button>
                  </div>
                )}
              </div>
              {e.cont && <p className="journal__body">{e.cont}</p>}
            </li>
          ))}
        </ul>
      )}

      {dialog.kind === "edit" && (
        <JournalDialog
          entry={dialog.entry}
          onCancel={() => setDialog({ kind: "none" })}
          onSaved={() => {
            setDialog({ kind: "none" });
            void load();
          }}
        />
      )}

      {dialog.kind === "delete" && (
        <ConfirmDialog
          titleKey="journal.delete"
          bodyKey="journal.deleteBody"
          bodyValues={{ title: dialog.entry.title || dialog.entry.logdate }}
          danger
          onCancel={() => setDialog({ kind: "none" })}
          onConfirm={() => {
            void deleteJournal(dialog.entry.uid)
              .then(() => {
                setDialog({ kind: "none" });
                return load();
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

function JournalDialog({
  entry,
  onCancel,
  onSaved,
}: {
  entry: JournalEntry | null;
  onCancel: () => void;
  onSaved: () => void;
}) {
  const { t } = useTranslation();
  const [logdate, setDate] = useState(entry?.logdate ?? today());
  const [title, setTitle] = useState(entry?.title ?? "");
  const [cont, setCont] = useState(entry?.cont ?? "");
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
      if (entry) {
        await updateJournal(entry.uid, logdate, title, cont, entry.rowVersion);
      } else {
        await createJournal(logdate, title, cont);
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
      titleKey={entry ? "journal.edit" : "journal.add"}
      onClose={onCancel}
      busy={busy}
      footer={
        <>
          <button type="button" className="btn" onClick={onCancel} disabled={busy}>
            {t("common.cancel")}
          </button>
          <button type="submit" form="journal-form" className="btn btn--primary" disabled={busy}>
            {busy ? t("common.saving") : t("common.save")}
          </button>
        </>
      }
    >
      <form id="journal-form" onSubmit={(e) => void submit(e)} noValidate>
        {errorKey && (
          <div className="alert" role="alert">
            {t(errorKey)}
          </div>
        )}
        {conflict && (
          // The form stays open holding the user's text. Discarding it to show
          // a conflict message would be a second data loss on top of the first.
          <div className="alert alert--warn" role="alert">
            <p>{t("error.CONFLICT")}</p>
          </div>
        )}
        <FormField
          name="logdate"
          labelKey="journal.date"
          type="date"
          value={logdate}
          onChange={setDate}
          errorCode={fieldErrors.logdate}
          required
        />
        <FormField
          name="title"
          labelKey="journal.title"
          value={title}
          onChange={setTitle}
          errorCode={fieldErrors.title}
        />
        <div className={`field${fieldErrors.cont ? " field--invalid" : ""}`}>
          <label className="field__label" htmlFor="cont">
            {t("journal.content")}
          </label>
          <textarea
            id="cont"
            className="field__input"
            rows={6}
            maxLength={200}
            value={cont}
            onChange={(e) => setCont(e.target.value)}
          />
          {fieldErrors.cont && (
            <p className="field__error">{t(`error.field.${fieldErrors.cont}`)}</p>
          )}
        </div>
      </form>
    </Modal>
  );
}
