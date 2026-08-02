import { useTranslation } from "react-i18next";

import Modal from "./Modal";

interface Props {
  titleKey: string;
  bodyKey: string;
  /** Interpolation values for bodyKey, e.g. the domain name and row count. */
  bodyValues?: Record<string, string | number>;
  confirmKey?: string;
  onConfirm: () => void;
  onCancel: () => void;
  busy?: boolean;
  /** Renders the confirm button as destructive. */
  danger?: boolean;
}

/**
 * Confirmation for irreversible actions.
 *
 * The legacy app used FrmMessage for this and asked the same flat "是否删除?"
 * regardless of scale. Here the body takes interpolation, so deleting a domain
 * can say how many rows go with it — the difference between removing one entry
 * and wiping a dropdown every screen reads from.
 */
export default function ConfirmDialog({
  titleKey,
  bodyKey,
  bodyValues,
  confirmKey = "common.confirm",
  onConfirm,
  onCancel,
  busy,
  danger,
}: Props) {
  const { t } = useTranslation();

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
          <button
            type="button"
            className={`btn ${danger ? "btn--danger" : "btn--primary"}`}
            onClick={onConfirm}
            disabled={busy}
          >
            {t(confirmKey)}
          </button>
        </>
      }
    >
      <p>{t(bodyKey, bodyValues)}</p>
    </Modal>
  );
}
