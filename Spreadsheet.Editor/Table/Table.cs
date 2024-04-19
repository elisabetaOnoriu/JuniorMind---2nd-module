using ConsoleDatas;
using System.Reflection;
namespace Table
{
    public class Table
    {
        int defaultSize = 21;
        int windowWidth;
        Cell[,] table;

        public Table(int defaultSize = 21, int windowWidth = 120)
        {
            SelectedRow = 1;
            SelectedCol = 1;
            this.windowWidth = windowWidth;
            table = new Cell[defaultSize, defaultSize];
            ShownContent = new string[defaultSize, defaultSize];
            Build();
        }

        public Cell this[int index, int secondIndex]
        {
            get => table[index, secondIndex];
            set => table[index, secondIndex] = value;
        }

        public int FirstCellSize { get; set; } = 5; //cellsize/2.MathCeiling

        public List<(int, int)> OverlappedCells { get; set; } = new List<(int, int)> ();

        public string[,] ShownContent { get; set; }

        public int SelectedRow { get; set; } = 1;

        public int SelectedCol { get; set; } = 1;

        public bool IsEditing { get; set; } = false;

        public int DefaultSize { get => defaultSize; set => defaultSize = value; }

        public int CellSize { get; set; } = 9;

        public ConsoleData ConsoleData { get; set; } = new ConsoleData();

        public bool IsSelectedCell(int i, int j) => i == SelectedRow && j == SelectedCol;

        public bool IsHeader(int i, int j) => i == 0 || j == 0;

        public void Build()
        {
            for (int i = 0; i < defaultSize; i++)
            {                
                for (int j = 0; j < defaultSize; j++)
                {
                    table[i, j] = new Cell();
                }
                
                SetHeaders(i);
            }

            table[0, 0].Content = "     ";
        }
        public bool CellHasToBeHighlighted(int i, int j)
        {
            bool highlight = IsHeader(i, j);
            if (highlight && (i == SelectedRow || j == SelectedCol))
            {
                highlight = false;
            }

            return IsSelectedCell(i, j) || highlight;
        }

        public bool LayoutFits(int index)
        {
            if (index * CellSize + 5 < windowWidth)
            {
                return true;
            }

            return false;
        }

        public void TurnEditingModeON(bool on)
        {
            IsEditing = on;
            table[SelectedRow, SelectedCol].IsEditing = on;
        }

        public bool ContentFitsWidth(int i, int j)
        {
            return (j - 1) * CellSize + table[i, j].Count + 1 + FirstCellSize <= ConsoleData.Width - GetWidthOutsideOfTable();
        }

        public int GetWidthOutsideOfTable() => ConsoleData.Width - (GetVisibleColumns() - 1) * CellSize - FirstCellSize;

        public int GetVisibleColumns() => (ConsoleData.Width - FirstCellSize) / CellSize + 1;

        public int CellSizeFittingWidth(int j) => ConsoleData.Width - FirstCellSize - (j - 1) * CellSize - GetWidthOutsideOfTable();

        private void SetHeaders(int i)
        {
            table[i,0].Content = $"{i}".PadLeft(5);
            if (LayoutFits(i))
            {
                table[0, i].Content = $"    {(char)('A' + i - 1)}    ";
            }
        }
    }
}
