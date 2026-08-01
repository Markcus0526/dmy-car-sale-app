import { useState, type FormEvent } from "react";
import { useTranslation } from "react-i18next";

import { ApiError, NetworkError } from "../api/client";
import LanguageSwitcher from "../components/LanguageSwitcher";

interface Props {
  onLogin: (username: string, password: string) => Promise<void>;
}

/**
 * Login screen. Fields mirror FrmLogon: 编码 / 姓名 / 口令.
 *
 * The legacy form had a 编码 (code) field alongside the name. Login
 * authenticates on username + password only (FrmLogon.cs:71-76 looks the user
 * up by username), so the code is not sent — it is kept visible because users
 * recognise the form, but it does not participate in authentication.
 */
export default function LoginPage({ onLogin }: Props) {
  const { t } = useTranslation();

  const [code, setCode] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [submitting, setSubmitting] = useState(false);
  const [errorKey, setErrorKey] = useState<string | null>(null);
  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({});

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();
    if (submitting) return;

    setSubmitting(true);
    setErrorKey(null);
    setFieldErrors({});

    try {
      await onLogin(username, password);
      // On success this component unmounts; no state update needed.
    } catch (err) {
      if (err instanceof ApiError) {
        setErrorKey(err.messageKey);
        setFieldErrors(err.fields);
      } else if (err instanceof NetworkError) {
        setErrorKey(err.messageKey);
      } else {
        setErrorKey("error.UNKNOWN");
      }
      setSubmitting(false);
    }
  }

  const fieldError = (name: string) =>
    fieldErrors[name] ? t(`error.field.${fieldErrors[name]}`) : null;

  return (
    <div className="login">
      <form className="login__card" onSubmit={(e) => void handleSubmit(e)}>
        <header className="login__header">
          <h1 className="login__title">{t("app.name")}</h1>
          <p className="login__tagline">{t("app.tagline")}</p>
        </header>

        {errorKey && (
          // role="alert" so a screen reader announces the failure rather than
          // leaving the user wondering why nothing happened.
          <div className="alert" role="alert">
            {t(errorKey)}
          </div>
        )}

        <label className="field">
          <span className="field__label">{t("login.code")}</span>
          <input
            className="field__input"
            value={code}
            onChange={(e) => setCode(e.target.value)}
            autoComplete="off"
            disabled={submitting}
          />
        </label>

        <label className="field">
          <span className="field__label">{t("login.username")}</span>
          <input
            className={`field__input${fieldError("username") ? " field__input--invalid" : ""}`}
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            autoComplete="username"
            autoFocus
            disabled={submitting}
            aria-invalid={Boolean(fieldError("username"))}
          />
          {fieldError("username") && (
            <span className="field__error">{fieldError("username")}</span>
          )}
        </label>

        <label className="field">
          <span className="field__label">{t("login.password")}</span>
          <input
            className={`field__input${fieldError("password") ? " field__input--invalid" : ""}`}
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            autoComplete="current-password"
            disabled={submitting}
            aria-invalid={Boolean(fieldError("password"))}
          />
          {fieldError("password") && (
            <span className="field__error">{fieldError("password")}</span>
          )}
        </label>

        <button className="btn btn--primary" type="submit" disabled={submitting}>
          {submitting ? t("login.submitting") : t("login.submit")}
        </button>

        <footer className="login__footer">
          <LanguageSwitcher />
        </footer>
      </form>
    </div>
  );
}
