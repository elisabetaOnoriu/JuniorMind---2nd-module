namespace DisplayCell
{
    public class FragmentedDisplay : IDisplayCell
    {
        Table.Table table;
        int row;
        int column;
        int difference;

        public FragmentedDisplay(Table.Table table, int i, int j, int difference)
        {
            this.table = table;
            row = i;
            column = j;
            this.difference = difference;
            table[i, j].DisplayFragmentedDifference = difference;
        }
        public string DisplayContent()
        {
            return table[row, column].Content[(difference + table[row, column].VisibleContentStartIndex)..];
        }

    }
}
