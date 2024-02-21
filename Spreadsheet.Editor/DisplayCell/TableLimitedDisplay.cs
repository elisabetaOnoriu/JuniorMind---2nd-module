using Table;
namespace DisplayCell
{
    internal class TableLimitedDisplay : IDisplayCell
    {
        Table.Table table;
        int row;
        int column;

        public TableLimitedDisplay(Table.Table table, int i, int j)
        {
            this.table = table;
            row = i;
            column = j;
        }

        public string DisplayContent()
        {
            int startIndex = table[row, column].VisibleContentStartIndex;
            return table[row, column].Content[startIndex..Math.Min(table.CellSizeFittingWidth(column) + startIndex, table[row, column].Size)];
        }
    }
}
