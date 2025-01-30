using Table;

namespace DisplayCell
{
    public class TableLimitedDisplay : IDisplayCell
    {
        private readonly Table.Table table;
        private readonly int row;
        private readonly int column;

        public TableLimitedDisplay(Table.Table table, int i, int j)
        {
            this.table = table;
            row = i;
            column = j;
        }

        public string DisplayContent()
        {
            string result = GetVisibleContent();
            PopulateTableWithContent(result);
            return result;
        }

        private string GetVisibleContent()
        {
            int startIndex = table[row, column].VisibleContentStartIndex;
            return table[row, column].Content[startIndex..Math.Min(table.CellSizeFittingWidth(column) + startIndex, table[row, column].Size)];
        }

        private void PopulateTableWithContent(string result)
        {
            int numberOfCellsCovered = (int)Math.Ceiling((double)result.Length / table.CellSize);
            int startingPoint = 0;

            for (int k = row; k <= numberOfCellsCovered + row; k++)
            {
                string contentToShow = GetContentToShow(result, startingPoint);
                table.ShownContent[row, k] = FormatContent(contentToShow);

                if (!table[row, column].IsEditing)
                    table.ShownContent[row, k] = table.ShownContent[row, k].PadRight(numberOfCellsCovered * table.CellSize);

                if (contentToShow.Length <= table.CellSize)
                    break;

                startingPoint += table.CellSize;
            }
        }

        private string GetContentToShow(string result, int startingPoint)
        {
            return result.Substring(startingPoint, Math.Min(table.CellSize, result.Length - startingPoint));
        }

        private string FormatContent(string content)
        {
            return content.PadRight(table.CellSize);
        }
    }
}
