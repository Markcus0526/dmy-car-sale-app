import { useTranslation } from "react-i18next";
import { LOCALE_LABELS, SUPPORTED_LOCALES, type Locale } from "../i18n";

export default function LanguageSwitcher() {
  const { i18n, t } = useTranslation();

  // i18n.language may be a region variant the app does not ship (e.g. "en-GB").
  const current: Locale = SUPPORTED_LOCALES.includes(i18n.language as Locale)
    ? (i18n.language as Locale)
    : i18n.language.startsWith("zh")
      ? "zh-CN"
      : "en";

  return (
    <label className="lang">
      <span className="lang__label">{t("common.language")}</span>
      <select
        className="lang__select"
        value={current}
        onChange={(e) => void i18n.changeLanguage(e.target.value)}
      >
        {SUPPORTED_LOCALES.map((loc) => (
          <option key={loc} value={loc}>
            {LOCALE_LABELS[loc]}
          </option>
        ))}
      </select>
    </label>
  );
}
