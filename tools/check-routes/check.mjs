#!/usr/bin/env node
// Verify every screen AppShell wires actually exists in the server's menu tree.
//
// A typo in a node id does not fail the build, does not fail typecheck, and
// does not throw at runtime: the condition simply never matches and the route
// falls through to PlaceholderPage. The screen just looks unbuilt. This caught
// exactly that -- `quarterTarget` written for a node whose id is
// `quarter-target`.

import { readFileSync } from "node:fs";

const shell = readFileSync("web/src/components/AppShell.tsx", "utf8");
const menu = readFileSync("internal/http/menu.go", "utf8");

const wired = [...shell.matchAll(/node\.id === "([^"]+)"/g)].map((m) => m[1]);
const known = new Set([...menu.matchAll(/\{ID: "([^"]+)"/g)].map((m) => m[1]));

if (wired.length === 0 || known.size === 0) {
  // Either regex silently matching nothing would make this check vacuous --
  // it would pass forever while verifying nothing at all.
  console.error(
    `refusing to pass vacuously: matched ${wired.length} wired ids and ` +
      `${known.size} menu ids. One of the patterns has stopped matching.`,
  );
  process.exit(1);
}

const missing = wired.filter((id) => !known.has(id));
if (missing.length > 0) {
  console.error(
    `AppShell wires ${missing.length} screen id(s) absent from menu.go, so they ` +
      `silently render the placeholder:\n  ${missing.join("\n  ")}`,
  );
  process.exit(1);
}
console.log(`${wired.length} wired screens, all present in the menu tree.`);
