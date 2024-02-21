using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayCell
{
    internal class FragmentedDisplay : IDisplayCell
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
        }
        public string DisplayContent()
        {
            return table[row, column].Content[(table[row, column].Count - difference)..];
        }
    }
}
