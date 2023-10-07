namespace SpreadsheetConsole
{
    class Program
    {
        static Table.Table table = new();
        static ConsoleKeyInfo keyInfo;

        static void Main(string[] args)
        {
            NavigateThroughCells();
            Console.ResetColor();
        }

        static void GenerateTable()
        {
            bool done = false;
            for (int i = 0; i < table.DefaultSize &&!done; i++)
            {
                for (int j = 0; j < table.DefaultSize && table.LayoutFits(j); j++)
                {
                    SetBackgroundAndForegroundColor(i, j);
                    Console.Write(table[i, j].Content); 
                    if (EditSelectedCell(i, j))
                    {
                        done = true;
                        break;
                    }                                                        
                }

                Console.Write('\n');
            }
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
                        if (table.SelectedRow > 1)
                            table.SelectedRow--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (table.SelectedRow < table.DefaultSize - 1)
                            table.SelectedRow++;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (table.SelectedCol > 1)
                            table.SelectedCol--;
                        break;

                    case ConsoleKey.RightArrow:
                        if (table.SelectedCol < table.DefaultSize - 2)
                            table.SelectedCol++;
                        break;

                    case ConsoleKey.Enter:
                        table.IsEditing = true;
                        EditSelectedCell(table.SelectedRow, table.SelectedCol);
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.Escape);
        }

        private static void SetBackgroundAndForegroundColor(int i, int j)
        {
            if (table.CellHasToBeHighlighted(i, j))
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


        private static bool EditSelectedCell(int i, int j)
        {
            if (!(table.IsEditing && table.IsSelectedCell(i, j)))
            {
               return false; 
            }

            Console.SetCursorPosition(j * table[i,j].Size - 4 + table[i, j].Count, i);
            do
            {              
                WriteInCell(i, j);
            } while (keyInfo.Key != ConsoleKey.Enter);
            
            return true;            
        }

        static void RedrawTable()
        {
            Console.Clear();
            GenerateTable();
        }

        static void AddCharToCell(int i, int j)
        {
            if (table[i, j].Count == table[i, j].Size)
            {
                Console.SetCursorPosition(0, table.DefaultSize);
                Console.WriteLine("Your content length reached the maximum cell size.");
                return;
            }
            
            table[i, j].AddChar(keyInfo.KeyChar, (Console.CursorLeft - 5) % table[i, j].Size);
            Update(1, i);
        }

        static void WriteInCell(int i, int j)
        {
            keyInfo = Console.ReadKey(true);
            int cursorLeftIndexExcluded = Console.CursorLeft - 5;
            switch (keyInfo.Key)
            {
                case ConsoleKey.Enter:
                    table.IsEditing = false;
                    break;
                case ConsoleKey.Backspace:
                    if (cursorLeftIndexExcluded % table.CellSize > 0)
                    {
                        table[i, j].RemoveChar(cursorLeftIndexExcluded % table[i, j].Size - 1);
                        Update(-1, i);
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (cursorLeftIndexExcluded % j > 0)
                        Console.SetCursorPosition(Console.CursorLeft - 1, i);
                    break;
                case ConsoleKey.RightArrow:
                    if (cursorLeftIndexExcluded < cursorLeftIndexExcluded % table.SelectedRow)
                        Console.SetCursorPosition(Console.CursorLeft + 1, i);
                    break;
                default:
                    AddCharToCell(i, j);
                    break;
            }
        }

        static void Update(int moveCursor, int column)
        {
            int cursorLeft = Console.CursorLeft;
            table.IsEditing = false;
            RedrawTable();
            Console.SetCursorPosition(cursorLeft + moveCursor, column);
        }
    }    
}