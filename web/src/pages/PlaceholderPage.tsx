import { useTranslation } from "react-i18next";

interface Props {
  labelKey: string;
  permissionKey: string;
}

/**
 * Stands in for a screen that arrives in a later slice.
 *
 * It deliberately renders the permission key alongside the translated title,
 * which makes the central i18n invariant visible while developing: the title
 * changes with the language, the permission key never does.
 */
export default function PlaceholderPage({ labelKey, permissionKey }: Props) {
  const { t } = useTranslation();

  return (
    <section className="page">
      <h1 className="page__title">{t(labelKey)}</h1>
      <div className="page__badge">{t("nav.notImplemented")}</div>
      <p className="page__body">{t("nav.notImplementedBody")}</p>
      <dl className="page__meta">
        <dt>{t("nav.permissionKey")}</dt>
        <dd>
          <code>{permissionKey}</code>
        </dd>
      </dl>
    </section>
  );
}
