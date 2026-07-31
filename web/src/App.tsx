import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import { fetchMe, ApiError, NetworkError, type MeResponse } from "./api/client";
import AppShell from "./components/AppShell";
import LoginPage from "./pages/LoginPage";

type State =
  | { status: "signedOut" }
  | { status: "loading" }
  | { status: "ready"; me: MeResponse }
  | { status: "error"; messageKey: string };

export default function App() {
  const { t, i18n } = useTranslation();
  const [state, setState] = useState<State>({ status: "signedOut" });

  // Keep <html lang> in step so the browser picks correct fonts and hyphenation.
  useEffect(() => {
    document.documentElement.lang = i18n.language;
  }, [i18n.language]);

  const load = useCallback(async () => {
    setState({ status: "loading" });
    try {
      setState({ status: "ready", me: await fetchMe() });
    } catch (err) {
      const messageKey =
        err instanceof ApiError || err instanceof NetworkError
          ? err.messageKey
          : "error.UNKNOWN";
      setState({ status: "error", messageKey });
    }
  }, []);

  switch (state.status) {
    case "signedOut":
      return <LoginPage onSignIn={() => void load()} />;

    case "loading":
      return <div className="centered">{t("common.loading")}</div>;

    case "error":
      return (
        <div className="centered">
          <p>{t(state.messageKey)}</p>
          <button className="btn" onClick={() => void load()}>
            {t("common.retry")}
          </button>
        </div>
      );

    case "ready":
      return <AppShell me={state.me} />;
  }
}
