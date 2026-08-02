import { useState } from "react";
import { useTranslation } from "react-i18next";
import { NavLink, Route, Routes, Navigate } from "react-router-dom";

import type { MenuNode, MeResponse } from "../api/client";
import LanguageSwitcher from "./LanguageSwitcher";
import PlaceholderPage from "../pages/PlaceholderPage";
import OnRoadPage from "../pages/OnRoadPage";
import BaseDataPage from "../pages/BaseDataPage";
import JournalPage from "../pages/JournalPage";
import { PermissionProvider } from "../state/permissions";

/** Flatten the tree to the leaves that own a route. */
function leaves(nodes: MenuNode[]): MenuNode[] {
  return nodes.flatMap((n) =>
    n.children?.length ? leaves(n.children) : n.path ? [n] : [],
  );
}

function MenuSection({ node }: { node: MenuNode }) {
  const { t } = useTranslation();
  const [open, setOpen] = useState(true);

  if (!node.children?.length) {
    return node.path ? (
      <NavLink
        to={node.path}
        className={({ isActive }) =>
          `nav__link${isActive ? " nav__link--active" : ""}`
        }
      >
        {t(node.labelKey)}
      </NavLink>
    ) : null;
  }

  return (
    <div className="nav__section">
      <button
        className="nav__sectionToggle"
        onClick={() => setOpen((o) => !o)}
        aria-expanded={open}
      >
        <span className={`nav__chevron${open ? " nav__chevron--open" : ""}`}>
          ▸
        </span>
        {t(node.labelKey)}
      </button>
      {open && (
        <div className="nav__children">
          {node.children.map((child) => (
            <MenuSection key={child.id} node={child} />
          ))}
        </div>
      )}
    </div>
  );
}

interface AppShellProps {
  me: MeResponse;
  onLogout: () => void;
}

export default function AppShell({ me, onLogout }: AppShellProps) {
  const { t } = useTranslation();
  const routes = leaves(me.menu);
  const first = routes[0];

  return (
    <PermissionProvider permissions={me.permissions}>
    <div className="shell">
      <aside className="shell__sidebar">
        <div className="shell__brand">
          {/* Short name in the 264px sidebar; the full legal name is on login. */}
          <span className="shell__brandName" title={t("app.name")}>
            {t("app.shortName")}
          </span>
        </div>
        <nav className="nav">
          {me.menu.map((node) => (
            <MenuSection key={node.id} node={node} />
          ))}
        </nav>
      </aside>

      <div className="shell__main">
        <header className="shell__header">
          <div className="shell__user">
            <span className="shell__userLabel">{t("nav.signedInAs")}</span>
            <strong>{me.displayName}</strong>
          </div>
          <div className="shell__actions">
            <LanguageSwitcher />
            <button className="btn btn--subtle" onClick={onLogout}>
              {t("common.signOut")}
            </button>
          </div>
        </header>

        <main className="shell__content">
          <Routes>
            {routes.map((node) => (
              <Route
                key={node.id}
                path={node.path}
                element={
                  // Screens are wired in as their slice lands; the rest keep
                  // the placeholder so nav stays complete and honest.
                  node.id === "onroad" ? (
                    <OnRoadPage />
                  ) : node.id === "basedata" ? (
                    <BaseDataPage />
                  ) : node.id === "journal" ? (
                    <JournalPage />
                  ) : (
                    <PlaceholderPage
                      labelKey={node.labelKey}
                      permissionKey={node.permissionKey}
                    />
                  )
                }
              />
            ))}
            {first?.path && (
              <Route path="*" element={<Navigate to={first.path} replace />} />
            )}
          </Routes>
        </main>
      </div>
    </div>
    </PermissionProvider>
  );
}
