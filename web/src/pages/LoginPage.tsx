import { useState } from "react";
import { useTranslation } from "react-i18next";
import LanguageSwitcher from "../components/LanguageSwitcher";

/**
 * Login screen. Fields mirror FrmLogon: 编码 / 姓名 / 口令.
 *
 * Not wired to an endpoint yet -- authentication lands in slice 1, together
 * with the bcrypt-over-legacy-DES verification path (6.5). The form exists so
 * the i18n and layout work is done and reviewed before then.
 */
export default function LoginPage({ onSignIn }: { onSignIn: () => void }) {
  const { t } = useTranslation();
  const [code, setCode] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  return (
    <div className="login">
      <form
        className="login__card"
        onSubmit={(e) => {
          e.preventDefault();
          onSignIn();
        }}
      >
        <header className="login__header">
          <h1 className="login__title">{t("app.name")}</h1>
          <p className="login__tagline">{t("app.tagline")}</p>
        </header>

        <label className="field">
          <span className="field__label">{t("login.code")}</span>
          <input
            className="field__input"
            value={code}
            onChange={(e) => setCode(e.target.value)}
            autoComplete="username"
          />
        </label>

        <label className="field">
          <span className="field__label">{t("login.username")}</span>
          <input
            className="field__input"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </label>

        <label className="field">
          <span className="field__label">{t("login.password")}</span>
          <input
            className="field__input"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            autoComplete="current-password"
          />
        </label>

        <button className="btn btn--primary" type="submit">
          {t("login.submit")}
        </button>

        <footer className="login__footer">
          <LanguageSwitcher />
        </footer>
      </form>
    </div>
  );
}
