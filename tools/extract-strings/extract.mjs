#!/usr/bin/env node
/**
 * Extract Chinese string literals from the legacy C# source and classify them.
 *
 * Phase 1, day 5 (DEVELOPMENT_PLAN.md). Produces the zh-CN catalogue that every
 * future screen keys against, plus a review list of the literals a human must
 * rule on.
 *
 * Two things this tool exists to get right:
 *
 *  1. ENCODING. The sources are mixed GB18030 and UTF-8-with-BOM (plan 11.7).
 *     Detect per file. Note `grep -P '[\x80-\xff]'` silently returns 0 on these
 *     files -- that is how the original "all GB18030" claim went unnoticed.
 *
 *  2. CLASSIFICATION. Not every Chinese literal is UI text. Some are compared
 *     by value or written to the database, and translating them breaks the
 *     program. Real examples from this codebase:
 *
 *        gridStore[0, c].ToString().Equals("开单日期")   grid header compared by value
 *        row["eop"] = "否";                              writes to a DB column (plan 5.6)
 *        public const String STR_CARSERIES = "车型大类"; tbl_basedata filter key
 *        public const String CAR_STOREIN = "车辆入库";   movement state (plan 6.2)
 *
 *     Every literal lands in exactly one of: TRANSLATE / NEVER / REVIEW.
 *
 * Usage:  node tools/extract-strings/extract.mjs [--src DIR] [--out DIR]
 */

import fs from "node:fs";
import path from "node:path";

const args = process.argv.slice(2);
const argOf = (flag, def) => {
  const i = args.indexOf(flag);
  return i >= 0 && args[i + 1] ? args[i + 1] : def;
};

const SRC = argOf("--src", "CarSaleMan/CarSaleMan");
const OUT = argOf("--out", "docs/i18n");

// CJK Unified Ideographs + Extension A + fullwidth punctuation.
const CJK = /[\u3400-\u4dbf\u4e00-\u9fff\uff01-\uff60\u3000-\u303f]/;

// ---------------------------------------------------------------------------
// Encoding
// ---------------------------------------------------------------------------

const gb18030 = new TextDecoder("gb18030");

/** Decode a source file, detecting UTF-8 BOM vs GB18030. */
function decode(buf) {
  if (buf[0] === 0xef && buf[1] === 0xbb && buf[2] === 0xbf) {
    return { text: buf.subarray(3).toString("utf8"), encoding: "utf-8-bom" };
  }
  // A valid UTF-8 decode that round-trips byte-for-byte means it really is
  // UTF-8; otherwise treat it as GB18030.
  const asUtf8 = buf.toString("utf8");
  if (!asUtf8.includes("\uFFFD") && Buffer.from(asUtf8, "utf8").equals(buf)) {
    return { text: asUtf8, encoding: "utf-8" };
  }
  return { text: gb18030.decode(buf), encoding: "gb18030" };
}

/**
 * Blank out /* ... *\/ block comments, preserving line and column positions so
 * reported line numbers stay accurate.
 *
 * Without this, commented-out code is extracted as live UI text -- e.g.
 * FrmReportWholeSaleTotal.cs had `/*"总数量 : " + *\/` yield a phantom string.
 */
function stripBlockComments(text) {
  return text.replace(/\/\*[\s\S]*?\*\//g, (m) => m.replace(/[^\n]/g, " "));
}

/** .NET composite-format placeholders: {0}, {0:0}, {1,-10:N2} */
const PLACEHOLDER = /\{\d+(?:[,:][^}]*)?\}/g;

// ---------------------------------------------------------------------------
// Classification
// ---------------------------------------------------------------------------

/**
 * Rules are ordered: the first match wins, so NEVER rules are listed before
 * TRANSLATE rules. When in doubt a literal falls through to REVIEW rather than
 * being silently translated -- a wrong NEVER costs a review, a wrong TRANSLATE
 * costs a production bug.
 */
const RULES = [
  // ---- DROP: dead in the port, do not carry forward -------------------------
  {
    verdict: "DROP",
    reason: "CSMEnv.ini config value — the whole mechanism is removed (plan 6.7)",
    test: (l) => /InnerText\s*=|CSMEnv|WriteEnvironment|ReadEnvironment/.test(l),
  },
  {
    verdict: "DROP",
    reason: "C1Preview chrome — replaced by browser-native PDF preview (D6)",
    test: (l) => /OutlineViewCaption|ThumbnailViewCaption|prevPanel\w*\./.test(l),
  },

  // ---- NEVER TRANSLATE -----------------------------------------------------
  {
    verdict: "NEVER",
    reason: "permission value compared by value (plan 6.4)",
    test: (l) => /读写|只读|不可用/.test(l),
  },
  {
    verdict: "NEVER",
    // `filterText` matters: FrmSearch passes filter expressions between forms
    // through it, so the Chinese inside is a data value, not a caption.
    reason: "DataTable filter / RowFilter expression — queries data, not UI",
    test: (l) =>
      /\.Filter\s*=|RowFilter|filterText\s*=|\.Select\s*\(\s*"|\bAND\b\s+\w+\s*=|^\s*"\s*\w+\s*=\s*'/.test(l),
  },
  {
    verdict: "NEVER",
    // Case-insensitive and `.Row[` aware: `e.Row["eop"] = "否"` is a DataRow
    // write, and an earlier case-sensitive `row[` pattern missed it.
    reason: "written to a DataRow / DB column (plan 5.6 是否* strings)",
    test: (l) => /\b(?:\w+\.)?[Rr]ow\s*\[[^\]]+\]\s*=|\bdr\s*\[[^\]]+\]\s*=/.test(l),
  },
  {
    verdict: "NEVER",
    reason: "compared by value — translating breaks the comparison",
    test: (l) => /\.Equals\s*\(\s*"|==\s*"|!=\s*"|^\s*case\s+"/.test(l),
  },
  {
    verdict: "NEVER",
    reason: "const declaration used as a data key or state name",
    test: (l) => /\b(const|readonly)\s+[Ss]tring\b/.test(l),
  },

  // ---- TRANSLATE -----------------------------------------------------------
  {
    verdict: "TRANSLATE",
    reason: "control caption",
    test: (l) => /\.Text\s*=\s*@?"/.test(l),
  },
  {
    verdict: "TRANSLATE",
    reason: "toolbar button tooltip",
    test: (l) => /\.ToolTipText\s*=/.test(l),
  },
  {
    verdict: "TRANSLATE",
    reason: "grid or column header",
    test: (l) => /\.HeaderText|\.Caption\s*=|ColumnHeader/.test(l),
  },
  {
    verdict: "TRANSLATE",
    reason: "message box text",
    test: (l) => /MessageBox|ShowYesNoMsg|ShowCloseMsg|ShowMessage|FrmMessage/.test(l),
  },
  {
    verdict: "TRANSLATE",
    reason: "validation or warning message built with String.Format",
    test: (l) => /String\.Format\s*\(|errText\s*=|warningText\s*=/.test(l),
  },
  {
    verdict: "TRANSLATE",
    reason: "grid label cell (subtotal / total / row heading)",
    test: (l) => /\bgrid\w*\s*\[[^\]]+\]\s*=\s*@?"|\.Rows\s*\[[^\]]+\]\s*\[[^\]]+\]\s*=/.test(l),
  },
  {
    verdict: "TRANSLATE",
    reason: "audit-trail label built by concatenation (FrmActionHis)",
    test: (l) => /logtext\s*\+?=|\+\s*"[^"]*[：:]\s*"/.test(l),
  },
  {
    verdict: "TRANSLATE",
    reason: "combo box / list item declared in the designer",
    test: (l) => /^\s*@?"[^"]*",?\s*\}?\)?;?\s*$/.test(l),
  },
  {
    verdict: "TRANSLATE",
    reason: "report or chart title",
    test: (l) => /RenderText|RenderTable|\.Title\s*=|string\s+text\s*=/.test(l),
  },
  {
    verdict: "TRANSLATE",
    reason: "export filename shown to the user",
    test: (l) => /FileName\s*=\s*@?"/.test(l),
  },
];

function classify(line) {
  for (const r of RULES) {
    if (r.test(line)) return { verdict: r.verdict, reason: r.reason };
  }
  return { verdict: "REVIEW", reason: "unmatched context — needs a human call" };
}

// ---------------------------------------------------------------------------
// Key generation
// ---------------------------------------------------------------------------

/** FrmStoreInAddMan.Designer.cs -> storeInAddMan */
function namespaceOf(file) {
  const base = path.basename(file).replace(/\.(Designer|designer)?\.?cs$/, "");
  const stripped = base.replace(/^Frm/, "");
  return stripped.charAt(0).toLowerCase() + stripped.slice(1);
}

/** this.labelCode.Text = "编　码"  ->  labelCode */
function controlOf(line) {
  const m =
    line.match(/(?:this\.)?([A-Za-z_]\w*)\.(?:Text|HeaderText|Caption)\s*=/) ??
    line.match(/(?:this\.)?([A-Za-z_]\w*)\s*\[/);
  return m ? m[1] : null;
}

/**
 * Readable names for the shared UI vocabulary.
 *
 * Auto-derived stems are taken from the control name, which for grid cell
 * assignments is meaningless ("common.Cells_13" for 小计). These are the
 * high-frequency strings, so a small hand-map buys most of the readability.
 * Values not listed fall back to the derived stem.
 */
const COMMON_NAMES = new Map([
  ["警告", "warning"],
  ["通知", "notice"],
  ["提示", "hint"],
  ["你是否保存更改", "confirmSaveChanges"],
  ["小计", "subtotal"],
  ["小计 : ", "subtotalLabel"],
  ["总计", "total"],
  ["总计 ：", "totalLabel"],
  ["数量", "quantity"],
  ["数量 : ", "quantityLabel"],
  ["总数量 : ", "totalQuantityLabel"],
  ["浏览全部", "browseAll"],
  ["条件查询", "conditionalSearch"],
  ["导出Excel", "exportExcel"],
  ["车辆类别", "vehicleCategory"],
  ["车辆代码", "vehicleCode"],
  ["确定", "ok"],
  ["取消", "cancel"],
  ["保存", "save"],
  ["删除", "delete"],
  ["修改", "edit"],
  ["新增", "add"],
  ["查询", "search"],
  ["打印", "print"],
  // NOTE: the legacy source spells this 近回系统; 近 is almost certainly a typo
  // for 返 ("return"). Kept verbatim as the zh-CN value -- fixing legacy typos
  // is a separate, deliberate change, not something to smuggle into extraction.
  ["近回系统", "backToSystem"],
  ["返  回", "back"],
]);

function slugify(text) {
  return (
    text
      .replace(/[\s\u3000]+/g, "")
      .replace(/[^\p{L}\p{N}]/gu, "")
      .slice(0, 12) || "text"
  );
}

// ---------------------------------------------------------------------------
// Extraction
// ---------------------------------------------------------------------------

const literalRe = /"((?:[^"\\]|\\.)*)"/g;

function extract() {
  const files = fs
    .readdirSync(SRC)
    .filter((f) => f.endsWith(".cs"))
    .sort();

  const entries = [];
  const encodings = {};
  const seenKeys = new Map();

  for (const file of files) {
    const buf = fs.readFileSync(path.join(SRC, file));
    const { text: raw, encoding } = decode(buf);
    const text = stripBlockComments(raw);
    encodings[encoding] = (encodings[encoding] || 0) + 1;

    const ns = namespaceOf(file);

    text.split(/\r?\n/).forEach((line, idx) => {
      const trimmed = line.trim();
      if (trimmed.startsWith("//") || trimmed.startsWith("*")) return;

      literalRe.lastIndex = 0;
      let m;
      while ((m = literalRe.exec(line)) !== null) {
        const value = m[1];
        if (!CJK.test(value)) continue;

        const { verdict, reason } = classify(line);

        const placeholders = value.match(PLACEHOLDER) ?? [];

        // Is this literal a fragment of a runtime-concatenated sentence?
        //
        //   tsbTitleText.Text = "     " + year + "年 " + quarter + "任务汇总";
        //
        // Fragments cannot be translated independently -- word order differs
        // between languages, so the parts must merge into ONE interpolated key
        // at port time. Detected by a `+` adjoining the literal with a
        // non-literal operand on the other side.
        const before = line.slice(0, m.index);
        const after = line.slice(m.index + m[0].length);
        const concatenated =
          /\+\s*$/.test(before) || /^\s*\+/.test(after) ? true : undefined;

        entries.push({
          key: null, // assigned in a second pass, once repeats are known
          value,
          verdict,
          reason,
          ns,
          control: controlOf(line),
          // Translators must reproduce these exactly; a dropped {0:0} throws
          // FormatException at runtime, not a display glitch.
          placeholders: placeholders.length ? placeholders : undefined,
          concatenated,
          file,
          line: idx + 1,
          context: trimmed.slice(0, 160),
          encoding,
        });
      }
    });
  }

  assignKeys(entries, seenKeys);
  return { entries, encodings, fileCount: files.length };
}

/**
 * Assign catalogue keys to TRANSLATE entries, one key per unique VALUE.
 *
 * Without this the catalogue carries a separate key per call site: "警告"
 * appeared 61 times and "你是否保存更改" 32 times, which would mean translating
 * and maintaining the same string dozens of times over.
 *
 * A value used in 3+ distinct files is shared vocabulary and gets a
 * `common.*` key; anything narrower stays namespaced to its screen.
 */
function assignKeys(entries, seenKeys) {
  const translate = entries.filter((e) => e.verdict === "TRANSLATE");

  const byValue = new Map();
  for (const e of translate) {
    if (!byValue.has(e.value)) byValue.set(e.value, []);
    byValue.get(e.value).push(e);
  }

  const keyFor = new Map();
  for (const [value, sites] of byValue) {
    const files = new Set(sites.map((s) => s.file));

    // Prefer the most common control name as the readable key stem.
    const freq = new Map();
    for (const s of sites) {
      if (s.control) freq.set(s.control, (freq.get(s.control) ?? 0) + 1);
    }
    const derived =
      [...freq.entries()].sort((a, b) => b[1] - a[1])[0]?.[0] ?? slugify(value);

    // A curated name wins over a derived one; grid-cell stems in particular
    // ("Cells") carry no meaning.
    const curated = COMMON_NAMES.get(value);
    const stem = curated ?? derived;

    const shared = curated !== undefined || files.size >= 3;
    const base = shared ? `common.${stem}` : `${sites[0].ns}.${stem}`;

    const n = seenKeys.get(base) ?? 0;
    seenKeys.set(base, n + 1);
    keyFor.set(value, n === 0 ? base : `${base}_${n + 1}`);
  }

  for (const e of translate) e.key = keyFor.get(e.value);
}

// ---------------------------------------------------------------------------
// Output
// ---------------------------------------------------------------------------

const { entries, encodings, fileCount } = extract();

fs.mkdirSync(OUT, { recursive: true });

// 1. Full inventory -- the audit trail.
fs.writeFileSync(
  path.join(OUT, "inventory.json"),
  JSON.stringify({ generatedFrom: SRC, fileCount, encodings, entries }, null, 2) + "\n",
);

// 2. The zh-CN catalogue: TRANSLATE entries only, deduplicated by key.
const catalogue = {};
for (const e of entries) if (e.key) catalogue[e.key] = e.value;
fs.writeFileSync(
  path.join(OUT, "zh-CN.extracted.json"),
  JSON.stringify(Object.fromEntries(Object.entries(catalogue).sort()), null, 2) + "\n",
);

// 3. The review list -- everything a human must rule on.
const review = entries.filter((e) => e.verdict === "REVIEW");
const never = entries.filter((e) => e.verdict === "NEVER");
const dropped = entries.filter((e) => e.verdict === "DROP");
const withPlaceholders = entries.filter((e) => e.verdict === "TRANSLATE" && e.placeholders);
const fragments = entries.filter((e) => e.verdict === "TRANSLATE" && e.concatenated);

// Concatenation fragments, grouped by the call site that assembles them, so
// the port merges each group into a single interpolated key.
const bySite = new Map();
for (const e of fragments) {
  const site = `${e.file}:${e.line}`;
  if (!bySite.has(site)) bySite.set(site, { context: e.context, parts: [] });
  bySite.get(site).parts.push(e);
}

fs.writeFileSync(
  path.join(OUT, "CONCATENATIONS.md"),
  [
    "# Runtime-concatenated strings — merge these at port time",
    "",
    "Generated by `tools/extract-strings/extract.mjs`. Do not edit by hand.",
    "",
    "Each group below is **one sentence assembled at runtime from fragments**.",
    "Translating the fragments independently cannot work in general: word order",
    "differs between languages, so a layout that reads correctly in Chinese",
    "reorders wrongly in English.",
    "",
    "The port must replace each group with a **single interpolated key**, e.g.",
    "",
    "```",
    '"     " + year + "年 " + quarter + "任务汇总"',
    "    ->  t(\"quarterTarget.title\", { year, quarter })",
    '    en: "{{year}} {{quarter}} Target Summary"',
    '    zh: "{{year}}年 {{quarter}}任务汇总"',
    "```",
    "",
    `${bySite.size} call site(s), ${dedupe(fragments).length} unique fragment(s).`,
    "",
    ...[...bySite.entries()].flatMap(([site, g]) => [
      `## ${site}`,
      "",
      "```csharp",
      g.context,
      "```",
      "",
      "Fragments: " + g.parts.map((p) => `\`${p.key}\` = ${JSON.stringify(p.value)}`).join(", "),
      "",
    ]),
  ].join("\n"),
);

// Strings carrying composite-format placeholders get their own file: they are
// the highest-risk items in the English pass, because a dropped or renumbered
// {0:0} is a runtime FormatException rather than a visible typo.
fs.writeFileSync(
  path.join(OUT, "placeholders.json"),
  JSON.stringify(
    dedupe(withPlaceholders).map((e) => ({
      key: e.key,
      value: e.value,
      placeholders: e.placeholders,
      source: `${e.file}:${e.line}`,
    })),
    null,
    2,
  ) + "\n",
);

const md = [
  "# Extracted strings — review list",
  "",
  "Generated by `tools/extract-strings/extract.mjs`. Do not edit by hand; re-run instead.",
  "",
  "## NEVER TRANSLATE — do not add these to a locale catalogue",
  "",
  "Each is compared by value, used as a data key, or written to the database.",
  "Translating one does not produce a display bug — it produces a broken",
  "comparison or corrupted data.",
  "",
  "| Value | Why | Source |",
  "|---|---|---|",
  ...dedupe(never).map(
    (e) => `| \`${e.value}\` | ${e.reason} | ${e.file}:${e.line} |`,
  ),
  "",
  `## REVIEW — ${review.length} literals need a human call`,
  "",
  "Unmatched by any rule. For each: is it UI chrome (translate) or data",
  "(leave Chinese)? Move the decision into `RULES` in the extractor so a re-run",
  "classifies it automatically.",
  "",
  "| Value | Source | Context |",
  "|---|---|---|",
  ...dedupe(review).map(
    (e) =>
      `| \`${e.value}\` | ${e.file}:${e.line} | \`${e.context.replace(/\|/g, "\\|")}\` |`,
  ),
  "",
].join("\n");

fs.writeFileSync(path.join(OUT, "REVIEW.md"), md);

function dedupe(list) {
  const seen = new Set();
  return list.filter((e) => {
    if (seen.has(e.value)) return false;
    seen.add(e.value);
    return true;
  });
}

// ---------------------------------------------------------------------------

const counts = entries.reduce((a, e) => ((a[e.verdict] = (a[e.verdict] || 0) + 1), a), {});
console.log(`files scanned      : ${fileCount}`);
console.log(`encodings          : ${JSON.stringify(encodings)}`);
console.log(`literals found     : ${entries.length}`);
console.log(`  TRANSLATE        : ${counts.TRANSLATE ?? 0}  (${Object.keys(catalogue).length} unique keys)`);
console.log(`  NEVER            : ${counts.NEVER ?? 0}  (${dedupe(never).length} unique)`);
console.log(`  DROP             : ${counts.DROP ?? 0}  (${dedupe(dropped).length} unique)`);
console.log(`  REVIEW           : ${counts.REVIEW ?? 0}  (${dedupe(review).length} unique)`);
console.log(`  with {n} placeholders: ${dedupe(withPlaceholders).length} unique`);
console.log(`  concatenation fragments: ${dedupe(fragments).length} unique, ${bySite.size} call sites`);
console.log(
  `\nwrote ${OUT}/inventory.json, ${OUT}/zh-CN.extracted.json, ${OUT}/placeholders.json,` +
    `\n      ${OUT}/CONCATENATIONS.md, ${OUT}/REVIEW.md`,
);
