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
            if (CellsAreIntersecting(out int difference))
            {
                return new FragmentedDisplay(table, row, column, difference);
            }
            else if (ShouldSkipCell())
            {
                return new NotShowableDisplay();
            }           
            else if (ContentShouldBeCellLimited())
            {
                return new CellLimitedDisplay(table, row, column);
            }            
            
            return new TableLimitedDisplay(table, row, column);
        }

        private bool ContentShouldBeCellLimited()
        {
            int overlappedCellsAfterIt = table[row, column].Count / table.CellSize - 1;
            int remainder = table[row, column].Count % table.CellSize;
            overlappedCellsAfterIt = remainder > 0 ? overlappedCellsAfterIt + 1 : overlappedCellsAfterIt;
            bool result = !table.IsHeader(row, column)
                && (table[row, column + 1].Count > 0); //|| !table[row, column].IsEditing);
            int k = 1;
            while (k <= overlappedCellsAfterIt && !result)
            {
                result = table[row, column + k].Count > 0;
                k++;
            }

            return result ? !table[row, column].IsEditing : result;
        }
        private bool ShouldSkipCell() => table.OverlappedCells.Contains((row, column));

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
