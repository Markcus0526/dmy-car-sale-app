import { useEffect, useRef, type ReactNode } from "react";
import { useTranslation } from "react-i18next";

interface Props {
  titleKey: string;
  onClose: () => void;
  children: ReactNode;
  footer?: ReactNode;
  /** Blocks Escape and the backdrop while a save is in flight. */
  busy?: boolean;
}

/**
 * The one modal, replacing the ~25 hand-built WinForms dialogs.
 *
 * Focus is moved in on open and restored on close, and Escape closes. The
 * legacy forms got this from the OS for free; on the web it has to be written,
 * and leaving it out makes every edit screen unusable by keyboard.
 */
export default function Modal({ titleKey, onClose, children, footer, busy }: Props) {
  const { t } = useTranslation();
  const dialogRef = useRef<HTMLDivElement>(null);
  const restoreFocusTo = useRef<Element | null>(null);

  useEffect(() => {
    restoreFocusTo.current = document.activeElement;
    // First field, not the dialog itself: the user came here to type.
    const first = dialogRef.current?.querySelector<HTMLElement>(
      "input:not([disabled]), select:not([disabled]), textarea:not([disabled])",
    );
    (first ?? dialogRef.current)?.focus();

    return () => {
      (restoreFocusTo.current as HTMLElement | null)?.focus?.();
    };
  }, []);

  useEffect(() => {
    function onKeyDown(e: KeyboardEvent) {
      if (e.key === "Escape" && !busy) {
        onClose();
        return;
      }
      if (e.key !== "Tab") return;

      // Keep Tab inside the dialog. Without this, tabbing walks out into the
      // page behind it, where clicking things is exactly what must not happen.
      const focusable = dialogRef.current?.querySelectorAll<HTMLElement>(
        'a[href], button:not([disabled]), input:not([disabled]), select:not([disabled]), textarea:not([disabled]), [tabindex]:not([tabindex="-1"])',
      );
      if (!focusable?.length) return;
      const first = focusable[0];
      const last = focusable[focusable.length - 1];
      if (!first || !last) return;
      if (e.shiftKey && document.activeElement === first) {
        e.preventDefault();
        last.focus();
      } else if (!e.shiftKey && document.activeElement === last) {
        e.preventDefault();
        first.focus();
      }
    }
    document.addEventListener("keydown", onKeyDown);
    return () => document.removeEventListener("keydown", onKeyDown);
  }, [onClose, busy]);

  return (
    <div
      className="modal__backdrop"
      onMouseDown={(e) => {
        // mouseDown, not click: a click that STARTED inside the dialog and
        // ended on the backdrop (a sloppy drag while selecting text) would
        // otherwise discard the form.
        if (e.target === e.currentTarget && !busy) onClose();
      }}
    >
      <div
        className="modal"
        role="dialog"
        aria-modal="true"
        aria-labelledby="modal-title"
        ref={dialogRef}
        tabIndex={-1}
      >
        <header className="modal__header">
          <h2 className="modal__title" id="modal-title">
            {t(titleKey)}
          </h2>
          <button
            type="button"
            className="modal__close"
            onClick={onClose}
            disabled={busy}
            aria-label={t("common.close")}
          >
            ×
          </button>
        </header>
        <div className="modal__body">{children}</div>
        {footer && <footer className="modal__footer">{footer}</footer>}
      </div>
    </div>
  );
}
