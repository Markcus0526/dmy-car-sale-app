/**
 * Mirrors `internal/query`. Kept deliberately narrow: the server validates
 * every field against a per-resource allowlist, so anything the client can
 * express is either accepted or rejected explicitly — never silently widened.
 */

export type FilterOp = "contains" | "eq" | "gte" | "lte";

export interface FilterCondition {
  field: string;
  op: FilterOp;
  value: string;
}

export interface Filter {
  conditions: FilterCondition[];
}

/** Matches query.MaxConditions. */
export const MAX_CONDITIONS = 8;

/**
 * A field the user may filter on.
 *
 * `field` is the API name, not a column name — the server maps it. That
 * indirection is what stops internal names such as `vw_storeout.Expr1` from
 * becoming part of the public surface.
 */
export interface FilterField {
  field: string;
  /** i18n key for the label, so the picker is translated like everything else. */
  labelKey: string;
  kind: "text" | "date" | "number";
}

/** Operators that make sense for a field kind, matching query.checkKind. */
export function opsFor(kind: FilterField["kind"]): FilterOp[] {
  return kind === "text" ? ["contains", "eq"] : ["gte", "lte", "eq"];
}

/** Drop blank conditions the way the legacy dialog ignored empty inputs. */
export function pruneFilter(f: Filter): Filter {
  return { conditions: f.conditions.filter((c) => c.value.trim() !== "") };
}
