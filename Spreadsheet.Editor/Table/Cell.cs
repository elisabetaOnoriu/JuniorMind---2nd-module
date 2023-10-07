namespace Table
{
    public class Cell
    {
        public Cell(int size = 9)
        {
            Size = size;
            Content = new string(' ', Size);
        }

        public void AddChar(char toAdd, int index)
        {
            string half = Count > 0 ? Content[..(index)] : "";
            string otherHalf = Count > index ? Content[index..] : "";
            Content = (half + toAdd + otherHalf).PadRight(Size);
            Count++;
        }

        public void RemoveChar(int index)
        {
            Content = (Content[..index] + Content[(index + 1)..]).PadRight(Size);
            Count--;
        }

        public int Size { get; set; } = 9;

        public int Count { get; set; } = 0;

        public string Content { get; internal set; } = "";
}
}