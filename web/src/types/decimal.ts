/**
 * Money and quantity values cross the wire as JSON **strings**, never numbers.
 *
 * The source database carries 148 decimal columns of prices, profits and
 * interest. JavaScript numbers are float64; parsing them loses precision and
 * silently corrupts the profit and finance report totals -- which is the exact
 * failure GO_MIGRATION_PLAN.md 11.2 exists to prevent.
 *
 * `Decimal` is a branded string, so arithmetic on it is a compile error:
 *
 *   const total = row.inprice * row.qty;   // Type error. Good.
 *
 * When you genuinely must compute client-side (the quarterly-target grid in
 * 6.3 is the main case), bring in decimal.js and convert explicitly at the
 * boundary. Do not reach for Number().
 */
declare const decimalBrand: unique symbol;

export type Decimal = string & { readonly [decimalBrand]: true };

/** Wrap a server-supplied string as a Decimal. Does not validate. */
export function asDecimal(raw: string): Decimal {
  return raw as Decimal;
}

/**
 * Format a Decimal for display in the given locale.
 *
 * Formatting is presentation-only and never feeds back into a calculation, so
 * the float64 round-trip here is safe. Never use this to derive a value you
 * intend to store or send.
 */
export function formatDecimal(
  value: Decimal,
  locale: string,
  options?: Intl.NumberFormatOptions,
): string {
  const n = Number(value);
  if (!Number.isFinite(n)) return value;
  return new Intl.NumberFormat(locale, {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
    ...options,
  }).format(n);
}
