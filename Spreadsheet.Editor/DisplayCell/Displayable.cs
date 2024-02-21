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
            if (ContentShouldBeCellLimited())
            {
                return new CellLimitedDisplay(table, row, column);
            }
            else if (CellsAreIntersecting(out int difference))
            {
                return new FragmentedDisplay(table, row, column, difference);
            }           
            else if (ShouldSkipCell())
            {
                return new NotShowableDisplay();
            }
                        
            return new TableLimitedDisplay(table, row, column);
        }

        private bool ContentShouldBeCellLimited()
        {
            return !table.IsHeader(row, column) 
                && !table[row, column].IsEditing 
                && table[row, column + 1].Count > 0;
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
            int indexOfIteratedCell = table.CellSize * column + columnLengthShown;
            differenceBetweenSelectedCellAndIteratedCell = 
                indexOfIteratedCell - 
                this.table.SelectedCol * table.CellSize + selectedColumnLengthShown;
            return table[row, table.SelectedCol].Count > this.table.CellSize && differenceBetweenSelectedCellAndIteratedCell > 0;
        }

        private bool ShouldSkipCell()
        {
            int firstNonHeaderColumnIndex = 1;
            int columnsNotToCount = 2;
            int columnsBefore = table.SelectedCol > firstNonHeaderColumnIndex ? table.SelectedCol - columnsNotToCount : 0;
            if (row == table.SelectedRow && column <= table.SelectedCol
                || column == table.SelectedCol + 1)
            {
                return false;
            }
            else if (row == table.SelectedRow
            && table[row, table.SelectedCol].Count > table.CellSize
            && table[row, table.SelectedCol].Count + columnsBefore * table.CellSize > (column - 1) * table.CellSize)
            {
                return table.IsEditing;
            }

            return false;
        }
    }
}
