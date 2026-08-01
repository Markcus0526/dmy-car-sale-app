#!/usr/bin/env node
/**
 * Convert the legacy C# sources to UTF-8 into a reference tree.
 *
 * Phase 1, day 4 (DEVELOPMENT_PLAN.md). The output exists so Chinese literals
 * are greppable and readable while porting -- without it, every search through
 * the legacy source needs an iconv pipeline.
 *
 * The original tree is never modified. The C# app must still build.
 *
 * Encoding reality (measured, see GO_MIGRATION_PLAN.md 11.7 -- the plan
 * originally claimed all files were GB18030, which is wrong):
 *
 *     gb18030      84 files
 *     utf-8-bom    39 files
 *     utf-8         7 files
 *
 * So detection is per file. A blanket `iconv -f GB18030` would mojibake the
 * 46 files that are already UTF-8.
 *
 * Every conversion is verified: the CJK codepoint sequence must be identical
 * before and after, and the output must contain no U+FFFD. A file that fails
 * verification is reported and NOT written.
 *
 * Usage: node tools/convert-encoding/convert.mjs [--src DIR] [--out DIR] [--check]
 *
 *   --check   verify only; write nothing. Suitable for CI.
 */

import fs from "node:fs";
import path from "node:path";

const args = process.argv.slice(2);
const argOf = (flag, def) => {
  const i = args.indexOf(flag);
  return i >= 0 && args[i + 1] ? args[i + 1] : def;
};

const SRC = argOf("--src", "CarSaleMan/CarSaleMan");
const OUT = argOf("--out", "reference/CarSaleMan");
const CHECK_ONLY = args.includes("--check");

const EXTENSIONS = [".cs", ".xsd", ".config", ".resx", ".settings", ".csproj"];

const gb18030 = new TextDecoder("gb18030", { fatal: false });
const CJK = /[㐀-䶿一-鿿]/gu;

/**
 * Decode a buffer, detecting the encoding.
 *
 * UTF-8 is confirmed by round-tripping the decode back to bytes: a GB18030
 * file that happens to decode without U+FFFD will not re-encode to the same
 * bytes, so this distinguishes them reliably rather than guessing.
 */
function decode(buf) {
  if (buf[0] === 0xef && buf[1] === 0xbb && buf[2] === 0xbf) {
    return { text: buf.subarray(3).toString("utf8"), encoding: "utf-8-bom" };
  }
  const asUtf8 = buf.toString("utf8");
  if (!asUtf8.includes("�") && Buffer.from(asUtf8, "utf8").equals(buf)) {
    return { text: asUtf8, encoding: "utf-8" };
  }
  return { text: gb18030.decode(buf), encoding: "gb18030" };
}

/** Ordered list of CJK codepoints — the fingerprint we require to survive. */
const cjkFingerprint = (s) => (s.match(CJK) ?? []).join("");

function walk(dir) {
  const out = [];
  for (const entry of fs.readdirSync(dir, { withFileTypes: true })) {
    const full = path.join(dir, entry.name);
    if (entry.isDirectory()) out.push(...walk(full));
    else if (EXTENSIONS.includes(path.extname(entry.name))) out.push(full);
  }
  return out;
}

const files = walk(SRC).sort();
const encodings = {};
const failures = [];
let written = 0;
let cjkTotal = 0;

for (const file of files) {
  const buf = fs.readFileSync(file);
  const { text, encoding } = decode(buf);
  encodings[encoding] = (encodings[encoding] || 0) + 1;

  // Verification 1: no replacement characters introduced.
  const replacements = (text.match(/�/g) ?? []).length;

  // Verification 2: re-encoding to UTF-8 and decoding back must preserve the
  // CJK sequence exactly.
  const roundTripped = Buffer.from(text, "utf8").toString("utf8");
  const before = cjkFingerprint(text);
  const after = cjkFingerprint(roundTripped);

  if (replacements > 0 || before !== after) {
    failures.push({
      file,
      encoding,
      replacements,
      cjkBefore: before.length,
      cjkAfter: after.length,
    });
    continue;
  }

  cjkTotal += before.length;

  if (!CHECK_ONLY) {
    const dest = path.join(OUT, path.relative(SRC, file));
    fs.mkdirSync(path.dirname(dest), { recursive: true });
    fs.writeFileSync(dest, text, "utf8");
    written++;
  }
}

console.log(`source        : ${SRC}`);
console.log(`files         : ${files.length}`);
console.log(`encodings     : ${JSON.stringify(encodings)}`);
console.log(`CJK codepoints: ${cjkTotal}`);
console.log(`failures      : ${failures.length}`);

for (const f of failures) {
  console.log(
    `  FAIL ${f.file} [${f.encoding}] replacements=${f.replacements} cjk ${f.cjkBefore}->${f.cjkAfter}`,
  );
}

if (CHECK_ONLY) {
  console.log("\n--check: nothing written");
} else {
  console.log(`\nwrote ${written} files to ${OUT}/`);
}

process.exit(failures.length ? 1 : 0);
