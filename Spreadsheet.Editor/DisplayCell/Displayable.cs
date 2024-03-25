namespace DisplayCell
{
    public class Displayable
    {
        Table.Table table;
        int row;
        int column;

        public Displayable(Table.Table table, int i, int j)
        {
            this.table = table;
            row = i;
            column = j;
        }

        public IDisplayCell Get()
        {
            if (ShouldSkipCell())
            {
                return new NotShowableDisplay();
            }
            else if (ContentShouldBeCellLimited())
            {
                return new CellLimitedDisplay(table, row, column);
            }
            else if (CellsAreIntersecting(out int difference))
            {
                return new FragmentedDisplay(table, row, column, difference);
            }
            
            return new TableLimitedDisplay(table, row, column);
        }

        private bool ContentShouldBeCellLimited()
        {
            return !table.IsHeader(row, column)
                && !table[row, column].IsEditing
                && table[row, column + 1].Count > 0;
        }
        private bool ShouldSkipCell()
        {
            int columnsNotToCount = 2;
            int columnsBeforeSelectedOne = GetColumnsBefore(table.SelectedCol, columnsNotToCount);
            if (row == table.SelectedRow && column < table.SelectedCol)
            {
                return false;
            }
            else if (row == table.SelectedRow
            && table[row, table.SelectedCol].Count > table.CellSize
            && table[row, table.SelectedCol].Count + columnsBeforeSelectedOne * table.CellSize > (column - columnsNotToCount) * table.CellSize)
            {
                return table[row, column].Count == 0;
            }

            return false;
        }

        private int GetColumnsBefore(int column, int columnsNotToCount)
        {
            int firstNonHeaderColumnIndex = 1;
            return column > firstNonHeaderColumnIndex ? column - columnsNotToCount : 0;
        }
        private bool CellsAreIntersecting(out int differenceBetweenSelectedCellAndIteratedCell)
        {
            if (table.SelectedRow != row || table.SelectedCol >= column || !table.IsEditing || table[row, column].Count == 0)
            {
                differenceBetweenSelectedCellAndIteratedCell = 0;
                return false;
            }

            var selectedColumnContentShown = new TableLimitedDisplay(this.table, row, this.table.SelectedCol);
            int selectedColumnLengthShown = Math.Min(selectedColumnContentShown.DisplayContent().Length, table[row, table.SelectedCol].Count);
            var columnContentShown = new TableLimitedDisplay(this.table, row, column);
            int columnLengthShown = Math.Min(columnContentShown.DisplayContent().Length, table[row, column].Count);
            int indexOfIteratedCell = table.CellSize * (column - 1);
            differenceBetweenSelectedCellAndIteratedCell =
                (table.SelectedCol - 1) * table.CellSize + selectedColumnLengthShown - indexOfIteratedCell
                ;
            return table[row, table.SelectedCol].Count > this.table.CellSize && differenceBetweenSelectedCellAndIteratedCell > 0
                && differenceBetweenSelectedCellAndIteratedCell < table[row, column].Size;
        }
    }
}
