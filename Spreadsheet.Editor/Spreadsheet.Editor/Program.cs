namespace SpreadsheetConsole
{
    class Program
    {
        static Table.Table table = new();
        static ConsoleKeyInfo keyInfo;
        static int firstCellSize = 5;
        static int visibleColumns;
        static int cursorPosition;

        static void Main(string[] args)
        {
            NavigateThroughCells();
            Console.ResetColor();
        }

        static void GenerateTable()
        {
            for (int i = 0; i < table.DefaultSize; i++)
            {
                for (int j = 0; table.LayoutFits(j); j++)
                {
                    visibleColumns++;
                    if (ShouldSkipCell(i, j))
                    {
                        continue;
                    }

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
                        TurnEditingModeON(true);
                        RedrawTable();
                        MoveCursorToEndOfContent(table.SelectedRow, table.SelectedCol);
                        cursorPosition = Console.CursorLeft;
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
        }

        private static bool EditSelectedCell(int i, int j)
        {
            TurnEditingModeON(true);
            ErasePreviousExtraContent(i, j);
            MoveCursorToEndOfContent(i, j);
            do
            {
                WriteInCell(i, j);
            } while (keyInfo.Key != ConsoleKey.Enter);

            TurnEditingModeON(false);
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
            if (CellSizeFittingWidth(j) > 0)
            {
                Console.SetCursorPosition(j * table.CellSize - 4 + size, i);
            }
        }

        static void RedrawTable()
        {
            Console.SetCursorPosition(0, 0);
            GenerateTable();
        }

        static void AddCharToCell(int i, int j)
        {
            int cellPosition = CursorCellPosition(j);
            if (!ContentFitsWidth(i, j))
            {
                table[i, j].AddChar(keyInfo.KeyChar, cellPosition + table[i, j].VisibleContentStartIndex);
                if (cellPosition + table[i, j].VisibleContentStartIndex == table[i, j].Count - 1)
                {
                    table[i, j].VisibleContentStartIndex++;
                }

                Update(1, i);
                return;
            }

            table[i, j].AddChar(keyInfo.KeyChar, cellPosition);
            Update(1, i);
        }

        static void WriteInCell(int i, int j)
        {
            keyInfo = Console.ReadKey(true);
            int cursorLeftIndexExcluded = Console.CursorLeft - firstCellSize;
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
                        Console.SetCursorPosition(firstCellSize + (j - 1) * table.CellSize, i);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (!ContentFitsWidth(i, j) && table[i, j].Count - table[i, j].VisibleContentStartIndex > VisibleContent(i, j).Length)
                    {
                        table[i, j].VisibleContentStartIndex++;
                        RedrawTable();
                    }
                    else if (cellPositionCursor < VisibleContent(i, j).Length && Console.CursorLeft < Console.WindowWidth - 1)
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
            if (newPosition < Console.WindowWidth)
            {
                Console.SetCursorPosition(newPosition, row);
            }
            else
            {
                Console.SetCursorPosition(newPosition - moveCursor, row);
            }
        }

        static int CursorCellPosition(int j)
        {
            return Console.CursorLeft - firstCellSize - (j - 1) * table.CellSize;
        }

        static bool ContentFitsWidth(int i, int j)
        {
            return (j - 1) * table.CellSize + table[i, j].Count + 1 + firstCellSize < Console.WindowWidth;
        }

        static int CellSizeFittingWidth(int j) => Console.WindowWidth - firstCellSize - (j - 1) * table.CellSize;

        static string VisibleContent(int i, int j)
        {
            if (table.IsSelectedCell(i, j) && table.IsEditing == true && table[i, j].IsEditing)
            {
                int startIndex = table[i, j].VisibleContentStartIndex;
                return table[i, j].Content[startIndex..Math.Min(CellSizeFittingWidth(j) + startIndex, table[i, j].Count)];
            }
            else if (!table.IsHeader(i, j) && j < visibleColumns - 1 && table[i, j + 1].Count > 0)
            {
                return table[i, j].DefaultSizedContent();
            }
            else if (ContentFitsWidth(i, j))
            {
                return table[i, j].Content;
            }

            return table[i, j].Content[..table.CellSize];
        }

        static bool ShouldSkipCell(int i, int j)
        {
            int firstNonHeaderColumnIndex = 1;
            int columnsNotToCount = 2;
            int columnsBefore = table.SelectedCol > firstNonHeaderColumnIndex ? table.SelectedCol - columnsNotToCount : 0;
            if (i == table.SelectedRow && j <= table.SelectedCol)
            {
                return false;
            }
            if (i == table.SelectedRow
            && table[i, table.SelectedCol].Count > table.CellSize
            && table[i, table.SelectedCol].Count + columnsBefore * table.CellSize > (j - 1) * table.CellSize)
            {
                return table.IsEditing;
            }

            return false;
        }

        static void TurnEditingModeON(bool on)
        {
            table.IsEditing = on;
            table[table.SelectedRow, table.SelectedCol].IsEditing = on;
        }
    }
}