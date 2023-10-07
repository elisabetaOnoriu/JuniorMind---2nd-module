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
            Build();
        }

        public Cell this[int index, int secondIndex]
        {
            get => table[index, secondIndex];
            set => table[index, secondIndex] = value;
        }

        public int SelectedRow { get; set; } = 1;

        public int SelectedCol { get; set; } = 1;

        public bool IsEditing { get; set; }

        public int DefaultSize { get => defaultSize; set => defaultSize = value; }

        public int CellSize { get; set; } = 9;

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
            if (highlight && i == SelectedRow)
            {
                highlight = false;
            }

            return IsSelectedCell(i, j) || highlight;
        }
        public bool LayoutFits(int index)
        {
            if (index * CellSize + 4 < windowWidth)
            {
                return true;
            }

            return false;
        }

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
