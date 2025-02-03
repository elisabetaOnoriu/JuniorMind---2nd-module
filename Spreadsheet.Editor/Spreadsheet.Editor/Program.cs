using DisplayCell;
namespace SpreadsheetConsole
{
    public class Program
    {
        static Table.Table table = new();
        static ConsoleKeyInfo keyInfo;
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
                    VerifyOverlapping(i, j);
                }

                Console.Write('\n');
            }
        }

        static void NavigateThroughCells()
        {
            do
            {
                PrepareConsoleForNavigation();
                keyInfo = Console.ReadKey();
                HandleKeyPress();
            }
            while (keyInfo.Key != ConsoleKey.Escape);
            SetConsoleData();
        }

        static void PrepareConsoleForNavigation()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            RedrawTable();
        }

        static void HandleKeyPress()
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    MoveSelection(-1, 0);
                    break;
                case ConsoleKey.DownArrow:
                    MoveSelection(1, 0);
                    break;
                case ConsoleKey.LeftArrow:
                    MoveSelection(0, -1);
                    break;
                case ConsoleKey.RightArrow:
                    MoveSelection(0, 1);
                    break;
                case ConsoleKey.Enter:
                    EnterEditMode();
                    break;
            }
        }

        static void MoveSelection(int rowOffset, int colOffset)
        {
            table.SelectedRow = Math.Clamp(table.SelectedRow + rowOffset, 1, table.DefaultSize - 1);
            table.SelectedCol = Math.Clamp(table.SelectedCol + colOffset, 1, table.DefaultSize - 2);

            if (table.SelectedCol == table.GetVisibleColumns())
            {
                table.SelectedCol = 1;
                table.SelectedRow++;
            }

            if (table.SelectedCol == 1 && colOffset == -1 && table.SelectedRow > 1)
            {
                table.SelectedCol = table.GetVisibleColumns() -  1;
                table.SelectedRow--;
            }
        }

        static void EnterEditMode()
        {
            MoveCursorToEndOfContent(table.SelectedRow, table.SelectedCol);
            table.TurnEditingModeON(true);
            RedrawTable();
            MoveCursorToEndOfContent(table.SelectedRow, table.SelectedCol);
            cursorPosition = Console.CursorLeft;
            EditSelectedCell(table.SelectedRow, table.SelectedCol);
        }
        public static void SetBackgroundAndForegroundColor(int i, int j)
        {
            if (table.CellHasToBeHighlighted(i, j))
            {
                ResetCursorPosition(i, j);
                Highlight();
            }
            else
            {     
                UnHighlight();
            }
        }

        public static void Highlight()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        public static void UnHighlight()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ResetCursorPosition(int i, int j)
        {
            if (!table.IsHeader(i, j))
            {
                Console.SetCursorPosition((j * table.CellSize - 4), i);
            }

            SetConsoleData();
        }

        private static bool EditSelectedCell(int i, int j)
        {
            table.TurnEditingModeON(true);
            if (table.OverlappedCells.Contains((i, j)))
            {
                ErasePreviousExtraContent(i, j);
            }
            
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
            if (!table.IsHeader(i, j))//table[i, j].Count == 0)
            {
                ResetCursorPosition(i, j);
                Console.Write(new String(' ', table.CellSize));
                ResetCursorPosition(i, j);
            }
        }

        private static void MoveCursorToEndOfContent(int i, int j)
        {
            int size = table[i, j].Count < table.CellSize ? table[i, j].Count : VisibleContent(i, j).Length;           
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
            bool startIndexRaises = false;
            if (cellPosition + nextPosition > table.CellSizeFittingWidth(j))
            {
                table[i, j].VisibleContentStartIndex++;
                startIndexRaises = true;
            }

            table[i, j].AddChar(keyInfo.KeyChar, cellPosition + table[i, j].VisibleContentStartIndex);
            Update(startIndexRaises ? 0 : 1, i);
        }

        static void WriteInCell(int i, int j)
        {
            keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.Enter:
                    table.IsEditing = false;
                    break;
                case ConsoleKey.Backspace:
                    HandleBackspace(i, j);
                    break;
                case ConsoleKey.LeftArrow:
                    MoveCursorLeft(i, j);
                    break;
                case ConsoleKey.RightArrow:
                    MoveCursorRight(i, j);
                    break;
                default:
                    AddCharToCell(i, j);
                    break;
            }
        }

        static void HandleBackspace(int i, int j)
        {
            int cursorLeftIndexExcluded = Console.CursorLeft - table.FirstCellSize;
            int index = cursorLeftIndexExcluded - (j - 1) * table.CellSize + table[i, j].VisibleContentStartIndex;

            if (index > 0)
            {
                table[i, j].RemoveChar(index);
                Console.CursorLeft--;
                Console.Write(" ");

                if (table[i, j].VisibleContentStartIndex > 0 && index > table.CellSize)
                {
                    table[i, j].VisibleContentStartIndex--;
                    Console.CursorLeft++;
                }

                Update(-1, i);
            }
        }

        static void MoveCursorLeft(int i, int j)
        {
            int cellPositionCursor = CursorCellPosition(j);

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
        }

        static void MoveCursorRight(int i, int j)
        {
            int cellPositionCursor = CursorCellPosition(j);

            if (!table.ContentFitsWidth(i, j) && cellPositionCursor == table[i, j].DisplayCell.Length
                && cellPositionCursor + table[i, j].VisibleContentStartIndex < table[i, j].Count)
            {
                table[i, j].VisibleContentStartIndex++;
                RedrawTable();
                Console.SetCursorPosition(table.FirstCellSize + (j - 1) * table.CellSize + table[i, j].DisplayCell.Length, i);
            }
            else if (cellPositionCursor < table.CellSize)
            {
                Console.SetCursorPosition(Console.CursorLeft + 1, i);
                cursorPosition++;
            }
        }

        private static void VerifyOverlapping(int i, int j)
        {
            if (RemoveFromOverlappedList(i, j))
            {
                return;
            }

            int remainder = table[i, j].Count % table.CellSize;
            int numberOfCellsCoveredByText = table[i, j].Count / table.CellSize;
            numberOfCellsCoveredByText = remainder > 0 ? numberOfCellsCoveredByText + 1 : numberOfCellsCoveredByText;
            for (int k = 1; k < numberOfCellsCoveredByText && (table[i, j].IsEditing || (table[i, j + 1].Count == 0 && !table[i, j].IsEditing)); k++)
            {
                if (table[i, j].OverlappedCells == null)
                {
                    table[i, j].OverlappedCells = new();
                }
                if (!table[i, j].OverlappedCells.Contains(j + k))
                {
                    table[i, j].OverlappedCells.Add(j + k);
                    table.OverlappedCells.Add((i, j + k));
                }
            }
        }

        private static bool RemoveFromOverlappedList(int i, int j)
        {
            int writtenCell = LookForWrittenCell(i, j);
            if (!table[i, j].IsEditing && writtenCell > -1 || 
                (table[i, j].Count % table.CellSize == 0 && writtenCell > -1))
            {
                for (int k = writtenCell; k < table[i, j].OverlappedCells.Count; k++)
                {
                    ErasePreviousExtraContent(i, table[i, j].OverlappedCells[k]);   
                    table.OverlappedCells.Remove((i, table[i, j].OverlappedCells[k])); 
                    if (table[i, table[i, j].OverlappedCells[k]].DisplayFragmentedDifference > 0)
                    {
                        table[i, table[i, j].OverlappedCells[k]].DisplayFragmentedDifference = 0;
                    }
                }

                table[i, j].OverlappedCells = table[i, j].OverlappedCells.GetRange(0, writtenCell);           
                return true;
            }

            return false;
        }

        private static int LookForWrittenCell(int i, int j)
        {
            for (int k = 0; table[i, j].OverlappedCells != null && k < table[i, j].OverlappedCells.Count; k++)
            {
                if (table[i, table[i, j].OverlappedCells[k]].IsEditing ||
                    table[i, table[i, j].OverlappedCells[k]].Count > 0)
                {
                    return k;
                }
            }

            return -1;
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
            var toDisplay = displayable.Get();

            if (toDisplay is NotShowableDisplay)
            {
                MoveToNextVisibleCell(i, j);
            }
            else
            {
                ResetCursorPosition(i, j);
                Console.CursorLeft += table[i, j].DisplayFragmentedDifference;
            }

            table[i, j].DisplayCell = toDisplay.DisplayContent();
            return table[i, j].DisplayCell;
        }

        static void MoveToNextVisibleCell(int i, int j)
        {
            if (j == table.GetVisibleColumns() - 1)
                ResetCursorPosition(i + 1, j);
            else
                ResetCursorPosition(i, j + 1);
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