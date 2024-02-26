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
        private bool ShouldSkipCell()
        {
            int firstNonHeaderColumnIndex = 1;
            int columnsNotToCount = 2;
            int columnsBefore = table.SelectedCol > firstNonHeaderColumnIndex ? table.SelectedCol - columnsNotToCount : 0;
            if (row == table.SelectedRow && column <= table.SelectedCol)
            {
                return false;
            }
            else if (row == table.SelectedRow
            && table[row, table.SelectedCol].Count > table.CellSize
            && table[row, table.SelectedCol].Count + columnsBefore * table.CellSize > (column - columnsNotToCount) * table.CellSize)
            {
                return table[row, table.SelectedCol].DisplayCell.Length > table.CellSize;
            }

            return false;
        }
    }
}
