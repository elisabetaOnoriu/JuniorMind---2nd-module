using Table;
namespace DisplayCell
{
    public class TableLimitedDisplay : IDisplayCell
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
            string result = table[row, column].Content[startIndex..Math.Min(table.CellSizeFittingWidth(column) + startIndex,
                table[row, column].Size)];
            int numberOfCellsCovered = (int)Math.Ceiling((double)result.Length / table.CellSize);
            int startingPoint = 0;
            for (int k = row; k <= numberOfCellsCovered + row; k++)
            {
                table.ShownContent[row, k] = result.Substring(startingPoint, table.CellSize).PadRight(table.CellSize);
                startingPoint += table.CellSize;
            }

            return result;
        }
    }
}
