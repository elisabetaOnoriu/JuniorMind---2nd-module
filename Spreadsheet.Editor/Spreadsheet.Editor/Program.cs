namespace SpreadsheetConsole
{
    class Program
    {
        static int selectedRow = 1;
        static int selectedCol = 1;
        static bool isEditing = false;
        const int defaultSize = 21;
        const int cellSize = 9;
        const string defaultCell = "         ";
        static string[,] table = new string[defaultSize, defaultSize];
        static ConsoleKeyInfo keyInfo;

        static void Main(string[] args)
        {
            BuildTable();
            NavigateThroughCells();
            Console.ResetColor();
        }

        static void BuildTable()
        {
            for (int i = 1; i < defaultSize; i++)
            {
                for (int j = 1; j < defaultSize; j++)
                {
                    table[i, j] = defaultCell;
                }

                SetHeaders(i);
            }

            table[0, 0] = "     ";
        }

        static void GenerateTable(string[,] table)
        {
            bool done = false;
            for (int i = 0; i < defaultSize &&!done; i++)
            {
                for (int j = 0; j < defaultSize && LayoutFits(j); j++)
                {
                    SetBackgroundAndForegroundColor(i, j);
                    Console.Write(table[i, j]); 
                    if (EditSelectedCell(i, j))
                    {
                        done = true;
                        break;
                    }                                                        
                }

                Console.Write('\n');
            }
        }

        private static bool LayoutFits(int index)
        {
            if (index * cellSize + 4 < Console.WindowWidth)
            {
                return true;
            }

            return false;
        }

        static void NavigateThroughCells()
        {
            do
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                RedrawTable();
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedRow > 1)
                            selectedRow--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedRow < defaultSize - 1)
                            selectedRow++;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (selectedCol > 1)
                            selectedCol--;
                        break;

                    case ConsoleKey.RightArrow:
                        if (selectedCol < defaultSize - 2)
                            selectedCol++;
                        break;

                    case ConsoleKey.Enter:
                        isEditing = true;
                        EditSelectedCell(selectedRow, selectedCol);
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.Escape);
        }

        private static void SetBackgroundAndForegroundColor(int i, int j)
        {
            if (CellHasToBeHighlighted(i, j))
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static bool CellHasToBeHighlighted(int i, int j)
        {
            bool highlight = IsHeader(i, j);
            if (highlight && i == selectedRow)
            {
                highlight = false;
            }

            return IsSelectedCell(i, j) || highlight;
        }

        private static bool IsSelectedCell(int i, int j) => i == selectedRow && j == selectedCol;

        private static bool IsHeader(int i, int j) => i == 0 || j == 0;

        static void SetHeaders(int i)
        {
            table[i, 0] = $"{i}".PadLeft(5);
            if (LayoutFits(i))
            {
                table[0, i] = $"    {(char)('A' + i - 1)}    ";
            }

            table[i, defaultSize - 1] = new string(' ', cellSize);
        }

        private static bool EditSelectedCell(int i, int j)
        {
            if (!(isEditing && IsSelectedCell(i, j)))
            {
               return false; 
            }
            
            int position = 0;
            Console.SetCursorPosition(j * cellSize - 4, i);
            do
            {              
                position = WriteInCell(i, j, position);
            } while (keyInfo.Key != ConsoleKey.Enter);
            
            return true;            
        }

        static void RedrawTable()
        {
            Console.Clear();
            GenerateTable(table);
        }

        static int AddCharToCell(int i, int j, int position)
        {
            try
            {
                table[i, j] = table[i,j][..position] + keyInfo.KeyChar + new string(' ', cellSize - position - 1);
                position++;
                isEditing = false;
                RedrawTable();
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Your content length reached the maximum cell size.");
            }

            return position;
        }

        static int WriteInCell(int i, int j, int position)
        {
            keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                isEditing = false;
            }
            else if (char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsWhiteSpace(keyInfo.KeyChar))
            {
                return AddCharToCell(i, j, position);                     
            }

            return position;
        }
    }    
}