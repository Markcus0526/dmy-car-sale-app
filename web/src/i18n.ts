import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import LanguageDetector from "i18next-browser-languagedetector";

import en from "./locales/en.json";
import zhCN from "./locales/zh-CN.json";

export const SUPPORTED_LOCALES = ["en", "zh-CN"] as const;
export type Locale = (typeof SUPPORTED_LOCALES)[number];

export const LOCALE_LABELS: Record<Locale, string> = {
  en: "English",
  "zh-CN": "中文",
};

// Keys are flat and contain literal dots ("menu.movement.onroad"), matching the
// LabelKey values the server emits. keySeparator/nsSeparator are therefore
// disabled -- with i18next's defaults a dotted key is treated as a path and the
// lookup silently falls through to the raw key.
void i18n
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    resources: {
      en: { translation: en },
      "zh-CN": { translation: zhCN },
    },
    fallbackLng: "en",
    supportedLngs: [...SUPPORTED_LOCALES],
    keySeparator: false,
    nsSeparator: false,
    interpolation: { escapeValue: false },
    detection: {
      // Locale preference is client-side only. The database stores no locale
      // (scope decision) -- so it does not roam between devices.
      order: ["localStorage", "navigator"],
      lookupLocalStorage: "carsaleman.locale",
      caches: ["localStorage"],
    },
  });

export default i18n;
