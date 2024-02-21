using ConsoleDatas;
using DisplayCell;
namespace SpreadsheetConsole
{
    public class Program
    {
        static Table.Table table = new();
        static ConsoleKeyInfo keyInfo;
        static int visibleColumns = table.GetVisibleColumns();
        static int cursorPosition;
        static int widthOutsideOfTable = table.GetWidthOutsideOfTable();

        static void Main(string[] args)
        {
            SetConsoleData();
            NavigateThroughCells();
            Console.ResetColor();
        }

        static void GenerateTable()
        {
            for (int i = 0; i < table.DefaultSize; i++)
            {
                for (int j = 0; table.LayoutFits(j); j++)
                {
                    
                    SetBackgroundAndForegroundColor(i, j);
                    Console.Write(VisibleContent(i, j));
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
                        table.TurnEditingModeON(true);
                        RedrawTable();
                        MoveCursorToEndOfContent(table.SelectedRow, table.SelectedCol);
                        cursorPosition = Console.CursorLeft;
                        EditSelectedCell(table.SelectedRow, table.SelectedCol);
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.Escape);
            SetConsoleData();
        }

        private static void SetBackgroundAndForegroundColor(int i, int j)
        {
            if (table.CellHasToBeHighlighted(i, j))
            {
                ResetCursorPosition(i, j);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void ResetCursorPosition(int i, int j)
        {
            if (!table.IsHeader(i, j))
            {
                Console.SetCursorPosition(j * table.CellSize - 4, i);
            }

            SetConsoleData();
        }

        private static bool EditSelectedCell(int i, int j)
        {
            table.TurnEditingModeON(true);
            ErasePreviousExtraContent(i, j);
            MoveCursorToEndOfContent(i, j);
            do
            {
                WriteInCell(i, j);
            } while (keyInfo.Key != ConsoleKey.Enter);

            table.TurnEditingModeON(false);
            return true;
        }

        private static void ErasePreviousExtraContent(int i, int j)
        {
            if (table[i, j].Count == 0)
            {
                Console.Write(new String(' ', table.CellSize));
                MoveCursorToEndOfContent(i, j);
            }
        }

        private static void MoveCursorToEndOfContent(int i, int j)
        {
            int size = table[i, j].Count < table.CellSize ? table[i, j].Count : VisibleContent(i, j).Length - 1;           
            if (table.CellSizeFittingWidth(j) > 0)
            {
                Console.SetCursorPosition(j * table.CellSize - 4 + size, i);
            }

            SetConsoleData();
        }

        static void RedrawTable()
        {
            Console.SetCursorPosition(0, 0);
            SetConsoleData();
            GenerateTable();
        }

        static void AddCharToCell(int i, int j)
        {
            int cellPosition = CursorCellPosition(j);
            int nextPosition = 1;
            if (cellPosition + nextPosition > table.CellSizeFittingWidth(j))
            {
                table[i, j].VisibleContentStartIndex++;
            }

            table[i, j].AddChar(keyInfo.KeyChar, cellPosition + table[i, j].VisibleContentStartIndex);
            Update(1, i);
        }

        static void WriteInCell(int i, int j)
        {
            keyInfo = Console.ReadKey(true);
            int cursorLeftIndexExcluded = Console.CursorLeft - table.FirstCellSize;
            int cellPositionCursor = CursorCellPosition(j);
            switch (keyInfo.Key)
            {
                case ConsoleKey.Enter:
                    table.IsEditing = false;
                    break;
                case ConsoleKey.Backspace:
                    int cellsBefore = j > 1 ? j - 1 : 0;
                    int index = cursorLeftIndexExcluded - cellsBefore * table.CellSize;
                    if (index + 1 > 0)
                    {
                        table[i, j].RemoveChar(index);
                        Update(-1, i);
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (cellPositionCursor > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, i);
                        cursorPosition--;
                    }
                    else if (table[i, j].VisibleContentStartIndex > 0)
                    {
                        table[i, j].VisibleContentStartIndex--;
                        RedrawTable();
                        Console.SetCursorPosition(table.FirstCellSize + (j - 1) * table.CellSize, i);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (!table.ContentFitsWidth(i, j) && table[i, j].Count - table[i, j].VisibleContentStartIndex > VisibleContent(i, j).Length)
                    {
                        table[i, j].VisibleContentStartIndex++;
                        RedrawTable();
                    }
                    else if (cellPositionCursor == VisibleContent(i, j).Length - 1 && Console.CursorLeft < Console.WindowWidth - 1)
                    {
                        Console.SetCursorPosition(Console.CursorLeft + 1, i);
                        cursorPosition++;
                    }

                    break;
                default:
                    AddCharToCell(i, j);
                    break;
            }
        }

        static void Update(int moveCursor, int row)
        {
            int newPosition = Console.CursorLeft + moveCursor;
            RedrawTable();
            if (newPosition < Console.WindowWidth - widthOutsideOfTable)
            {
                Console.SetCursorPosition(newPosition, row);
            }
            else
            {
                Console.SetCursorPosition(newPosition - moveCursor, row);
            }

            SetConsoleData();
        }

        static int CursorCellPosition(int j)
        {
            return Console.CursorLeft - table.FirstCellSize - (j - 1) * table.CellSize;
        }

        static string VisibleContent(int i, int j)
        {
            Displayable displayable = new(table, i, j);
            return displayable.Get().DisplayContent();
        }

        static void SetConsoleData()
        {
            table.ConsoleData.CursorTop = Console.CursorTop;
            table.ConsoleData.CursorLeft = Console.CursorLeft;
            table.ConsoleData.Height = Console.WindowHeight;    
            table.ConsoleData.Width = Console.WindowWidth;    
        }
    }
}