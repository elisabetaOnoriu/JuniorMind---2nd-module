namespace DisplayCell
{
    public class CellLimitedDisplay : IDisplayCell
    {
        Table.Table table;
        int row;
        int column;

        public CellLimitedDisplay(Table.Table table, int i, int j)
        {
            this.table = table;
            row = i;
            column = j;
        }

        public string DisplayContent()
        {
            return table[row, column].DefaultSizedContent();
        }
    }
}