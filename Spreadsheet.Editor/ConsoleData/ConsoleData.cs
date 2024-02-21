namespace ConsoleDatas
{
    public class ConsoleData
    {
        public ConsoleData()
        {
        }

        public int Height { get; set; }

        public int Width { get; set; }

        public (int first, int second) CursorPosition { get; set; }

        public int CursorLeft { get; set; }

        public int CursorTop { get; set; }

        public void Update(int cursorLeft, int cursorTop)
        {
            CursorLeft = cursorLeft;
            CursorTop = cursorTop;
        }
    }
}