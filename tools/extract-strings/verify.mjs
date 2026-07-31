#!/usr/bin/env node
/**
 * Verify a translated catalogue against its source catalogue.
 *
 * Run after any edit to a locale file, and in CI. Four checks, in order of how
 * badly they fail at runtime:
 *
 *   1. PLACEHOLDER INTEGRITY -- the only check that fails as a crash rather
 *      than a display bug. A dropped or renumbered {0:0} throws
 *      FormatException / breaks interpolation at the call site.
 *   2. Key parity          -- a missing key renders as the raw key string.
 *   3. Empty values        -- renders as blank UI.
 *   4. Untranslated values -- target value still identical to source.
 *
 * Usage: node tools/extract-strings/verify.mjs <source.json> <target.json>
 */

import fs from "node:fs";

const [sourcePath, targetPath] = process.argv.slice(2);
if (!sourcePath || !targetPath) {
  console.error("usage: verify.mjs <source.json> <target.json>");
  process.exit(2);
}

const source = JSON.parse(fs.readFileSync(sourcePath, "utf8"));
const target = JSON.parse(fs.readFileSync(targetPath, "utf8"));

/**
 * Keys that are fragments of a runtime-concatenated sentence.
 *
 * A separator fragment ("年 ") legitimately translates to whitespace in
 * English, so whitespace-only is allowed for these and flagged as a warning
 * instead. The list is derived from the extractor's inventory, not hand
 * maintained -- see docs/i18n/CONCATENATIONS.md.
 */
const inventoryPath = sourcePath.replace(/[^/]+$/, "inventory.json");
const fragmentKeys = new Set();
if (fs.existsSync(inventoryPath)) {
  for (const e of JSON.parse(fs.readFileSync(inventoryPath, "utf8")).entries) {
    if (e.concatenated && e.key) fragmentKeys.add(e.key);
  }
}

const PLACEHOLDER = /\{\d+(?:[,:][^}]*)?\}/g;

/** Placeholder *indices* — {0:0} and {0} are the same slot. */
const slots = (s) =>
  [...(s.match(PLACEHOLDER) ?? [])]
    .map((p) => p.match(/\d+/)[0])
    .sort()
    .join(",");

const problems = { placeholder: [], missing: [], extra: [], empty: [], untranslated: [] };
const warnings = { fragment: [] };

for (const [key, srcVal] of Object.entries(source)) {
  if (!(key in target)) {
    problems.missing.push(key);
    continue;
  }
  const tgtVal = target[key];

  if (typeof tgtVal !== "string" || tgtVal === "") {
    problems.empty.push(key);
    continue;
  }
  if (tgtVal.trim() === "") {
    // Whitespace-only is correct for a separator fragment, wrong otherwise.
    if (fragmentKeys.has(key)) warnings.fragment.push(key);
    else problems.empty.push(key);
    continue;
  }

  const a = slots(srcVal);
  const b = slots(tgtVal);
  if (a !== b) {
    problems.placeholder.push(
      `${key}\n      source {${a || "none"}}  "${srcVal}"\n      target {${b || "none"}}  "${tgtVal}"`,
    );
  }

  // Identical values are legitimate for symbols and codes ("VIN", "%", "EOP"),
  // so only flag values that still contain CJK.
  if (tgtVal === srcVal && /[㐀-鿿]/.test(tgtVal)) {
    problems.untranslated.push(`${key} = ${JSON.stringify(tgtVal)}`);
  }
}

for (const key of Object.keys(target)) {
  if (!(key in source)) problems.extra.push(key);
}

const label = {
  placeholder: "PLACEHOLDER MISMATCH (runtime crash)",
  missing: "missing keys",
  extra: "keys not in source",
  empty: "empty values",
  untranslated: "still in Chinese",
};

let failed = false;
console.log(`source: ${sourcePath}  (${Object.keys(source).length} keys)`);
console.log(`target: ${targetPath}  (${Object.keys(target).length} keys)\n`);

for (const [kind, list] of Object.entries(problems)) {
  const ok = list.length === 0;
  console.log(`${ok ? "PASS" : "FAIL"}  ${label[kind]}: ${list.length}`);
  if (!ok) {
    failed = true;
    for (const item of list.slice(0, 20)) console.log(`      ${item}`);
    if (list.length > 20) console.log(`      … and ${list.length - 20} more`);
  }
}

if (warnings.fragment.length) {
  console.log(
    `\nWARN  whitespace-only separator fragments: ${warnings.fragment.length}` +
      `\n      ${warnings.fragment.join(", ")}` +
      `\n      Allowed, but these must merge into one interpolated key at port` +
      `\n      time — see docs/i18n/CONCATENATIONS.md.`,
  );
}

process.exit(failed ? 1 : 0);
