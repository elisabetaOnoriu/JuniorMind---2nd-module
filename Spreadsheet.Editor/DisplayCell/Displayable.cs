using Table;

namespace DisplayCell
{
    public class Displayable
    {
        private readonly Table.Table table;
        private readonly int row;
        private readonly int column;

        public Displayable(Table.Table table, int i, int j)
        {
            this.table = table;
            row = i;
            column = j;
        }

        public IDisplayCell Get()
        {
            if (CellsAreIntersecting(out int difference))
            {
                return new FragmentedDisplay(table, row, column, difference);
            }
            if (ShouldSkipCell())
            {
                return new NotShowableDisplay();
            }
            if (ContentShouldBeCellLimited())
            {
                return new CellLimitedDisplay(table, row, column);
            }

            return new TableLimitedDisplay(table, row, column);
        }

        private bool ContentShouldBeCellLimited()
        {
            int overlappedCellsAfterIt = (table[row, column].Count / table.CellSize) - (table[row, column].Count % table.CellSize > 0 ? 0 : 1);
            bool result = !table.IsHeader(row, column) && table[row, column + 1].Count > 0;

            for (int k = 1; k <= overlappedCellsAfterIt && !result; k++)
            {
                result = table[row, column + k].Count > 0;
            }

            return result && !table[row, column].IsEditing;
        }

        private bool ShouldSkipCell() => table.OverlappedCells.Contains((row, column));

        private bool CellsAreIntersecting(out int difference)
        {
            difference = 0;
            if (table.SelectedRow != row || table.SelectedCol >= column || !table.IsEditing || table[row, column].Count == 0)
            {
                return false;
            }

            int selectedColumnLengthShown = GetDisplayedContentLength(table.SelectedCol);
            int columnLengthShown = GetDisplayedContentLength(column);
            int indexOfIteratedCell = table.CellSize * (column - 1);

            difference = ((table.SelectedCol - 1) * table.CellSize + selectedColumnLengthShown) - indexOfIteratedCell;
            return table[row, table.SelectedCol].Count > table.CellSize && difference > 0 && difference < table[row, column].Size;
        }

        private int GetDisplayedContentLength(int col)
        {
            var columnContent = new TableLimitedDisplay(table, row, col);
            return Math.Min(columnContent.DisplayContent().Length, table[row, col].Count);
        }
    }
}
