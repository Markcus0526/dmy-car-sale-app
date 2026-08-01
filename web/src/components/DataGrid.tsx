import {
  flexRender,
  getCoreRowModel,
  getSortedRowModel,
  useReactTable,
  type ColumnDef,
  type SortingState,
} from "@tanstack/react-table";
import { useState } from "react";
import { useTranslation } from "react-i18next";

interface Props<T> {
  columns: ColumnDef<T, unknown>[];
  rows: T[];
  loading?: boolean;
  /** i18n key shown when there are no rows. */
  emptyKey?: string;
  onRowClick?: (row: T) => void;
}

/**
 * The one grid, replacing C1FlexGrid across every list screen (D5).
 *
 * Headless TanStack Table rather than a component library: the quarterly-target
 * screen (§6.3) needs an editable dual-block grid with computed subtotals, and
 * that is far easier to build on a headless table than to fight a packaged one
 * into. AG Grid's editable features are also paid, which would swap one
 * commercial dependency for another — part of what this migration escapes.
 *
 * Sorting is client-side for now. That is correct while a screen holds one
 * page of rows; when a list grows past that, sorting moves to the server
 * alongside pagination and this component takes a `manualSorting` flag.
 */
export default function DataGrid<T>({
  columns,
  rows,
  loading = false,
  emptyKey = "grid.empty",
  onRowClick,
}: Props<T>) {
  const { t } = useTranslation();
  const [sorting, setSorting] = useState<SortingState>([]);

  const table = useReactTable({
    data: rows,
    columns,
    state: { sorting },
    onSortingChange: setSorting,
    getCoreRowModel: getCoreRowModel(),
    getSortedRowModel: getSortedRowModel(),
  });

  return (
    <div className="grid">
      <div className="grid__scroll">
        <table className="grid__table">
          <thead>
            {table.getHeaderGroups().map((hg) => (
              <tr key={hg.id}>
                {hg.headers.map((header) => {
                  const sortable = header.column.getCanSort();
                  const dir = header.column.getIsSorted();
                  return (
                    <th
                      key={header.id}
                      className={sortable ? "grid__th grid__th--sortable" : "grid__th"}
                      onClick={sortable ? header.column.getToggleSortingHandler() : undefined}
                      // Announce sort state rather than relying on the arrow alone.
                      aria-sort={
                        dir === "asc" ? "ascending" : dir === "desc" ? "descending" : "none"
                      }
                    >
                      {flexRender(header.column.columnDef.header, header.getContext())}
                      {dir && <span className="grid__sort">{dir === "asc" ? "▲" : "▼"}</span>}
                    </th>
                  );
                })}
              </tr>
            ))}
          </thead>

          <tbody>
            {table.getRowModel().rows.map((row) => (
              <tr
                key={row.id}
                className={onRowClick ? "grid__tr grid__tr--clickable" : "grid__tr"}
                onClick={onRowClick ? () => onRowClick(row.original) : undefined}
              >
                {row.getVisibleCells().map((cell) => (
                  <td key={cell.id} className="grid__td">
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Overlay rather than replacing the table: swapping the whole grid out
          on every search makes the layout jump and loses scroll position. */}
      {loading && <div className="grid__overlay">{t("common.loading")}</div>}

      {!loading && rows.length === 0 && <div className="grid__empty">{t(emptyKey)}</div>}

      {!loading && rows.length > 0 && (
        <div className="grid__footer">{t("grid.rowCount", { count: rows.length })}</div>
      )}
    </div>
  );
}
