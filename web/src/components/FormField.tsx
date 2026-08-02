import { useTranslation } from "react-i18next";

interface Props {
  name: string;
  labelKey: string;
  value: string;
  onChange: (value: string) => void;
  /** Field-level code from the server, e.g. "REQUIRED". Resolved here. */
  errorCode?: string;
  type?: "text" | "date" | "number";
  required?: boolean;
  disabled?: boolean;
  /** Right-aligns and switches to a monospace face for decimal columns. */
  numeric?: boolean;
}

/**
 * One labelled input with its validation message.
 *
 * The server sends field CODES ("REQUIRED"), never prose, so the message is
 * resolved from `error.field.*` on this side. That is what lets the backend
 * stay locale-agnostic while the user still reads their own language.
 */
export default function FormField({
  name,
  labelKey,
  value,
  onChange,
  errorCode,
  type = "text",
  required,
  disabled,
  numeric,
}: Props) {
  const { t } = useTranslation();
  const errorId = `${name}-error`;

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
      <input
        id={name}
        name={name}
        className={`field__input${numeric ? " field__input--numeric" : ""}`}
        type={type}
        value={value}
        disabled={disabled}
        // The browser's own validation bubbles are unlocalised and inconsistent
        // across engines; `required` here would fight the server's codes for
        // ownership of the message. The server is the single source of truth.
        aria-required={required}
        aria-invalid={errorCode ? true : undefined}
        aria-describedby={errorCode ? errorId : undefined}
        onChange={(e) => onChange(e.target.value)}
      />
      {errorCode && (
        <p className="field__error" id={errorId}>
          {t(`error.field.${errorCode}`)}
        </p>
      )}
    </div>
  );
}
