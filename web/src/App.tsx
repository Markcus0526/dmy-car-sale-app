import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import {
  fetchMe,
  login as apiLogin,
  logout as apiLogout,
  ApiError,
  NetworkError,
  type MeResponse,
} from "./api/client";
import AppShell from "./components/AppShell";
import LoginPage from "./pages/LoginPage";

type State =
  /** Resolving whether an existing session cookie is still valid. */
  | { status: "resolving" }
  | { status: "signedOut" }
  | { status: "ready"; me: MeResponse }
  /** A failure that is not "you are logged out" — server down, network gone. */
  | { status: "error"; messageKey: string };

/** Map any thrown value to an i18n key. */
function messageKeyFor(err: unknown): string {
  if (err instanceof ApiError || err instanceof NetworkError) return err.messageKey;
  return "error.UNKNOWN";
}

export default function App() {
  const { t, i18n } = useTranslation();
  const [state, setState] = useState<State>({ status: "resolving" });

  // Keep <html lang> in step so the browser picks correct fonts and hyphenation.
  useEffect(() => {
    document.documentElement.lang = i18n.language;
  }, [i18n.language]);

  // On load, try the existing session cookie. A 401 is the normal
  // "not signed in" answer, not an error worth showing — anything else is.
  const resolveSession = useCallback(async () => {
    setState({ status: "resolving" });
    try {
      setState({ status: "ready", me: await fetchMe() });
    } catch (err) {
      if (err instanceof ApiError && err.status === 401) {
        setState({ status: "signedOut" });
        return;
      }
      setState({ status: "error", messageKey: messageKeyFor(err) });
    }
  }, []);

  useEffect(() => {
    void resolveSession();
  }, [resolveSession]);

  const handleLogin = useCallback(async (username: string, password: string) => {
    // Errors propagate to LoginPage, which owns the form's error state —
    // replacing the whole screen for a wrong password would lose what the
    // user typed.
    const me = await apiLogin(username, password);
    setState({ status: "ready", me });
  }, []);

  const handleLogout = useCallback(async () => {
    await apiLogout();
    setState({ status: "signedOut" });
  }, []);

  switch (state.status) {
    case "resolving":
      return <div className="centered">{t("common.loading")}</div>;

    case "signedOut":
      return <LoginPage onLogin={handleLogin} />;

    case "error":
      return (
        <div className="centered">
          <p>{t(state.messageKey)}</p>
          <button className="btn" onClick={() => void resolveSession()}>
            {t("common.retry")}
          </button>
        </div>
      );

    case "ready":
      return <AppShell me={state.me} onLogout={() => void handleLogout()} />;
  }
}
