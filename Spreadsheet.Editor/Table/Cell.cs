namespace Table
{
    public class Cell
    {
        int defaultLength;
        public Cell(int length = 9)
        {
            defaultLength = length;
            Content = new string(' ', length);
        }
      
        public int VisibleContentStartIndex { get; set; } = 0;
        
        public int Size { get => Content.Length; }

        public int Count { get; set; } = 0;

        public bool IsEditing { get; set; } = false;

        public string Content { get; internal set; } = "";

        public string DefaultSizedContent() => Content[..defaultLength];

        public void AddChar(char toAdd, int index)
        {
            string half = Count > 0 ? Content[..index] : "";
            string otherHalf = Count > index ? Content[index..] : "";
            Content = (half + toAdd + otherHalf).PadRight(Size);
            Count++;
        }

        public void RemoveChar(int index)
        {
            string otherHalf = index == this.Count ?  "" : Content[(index + 1)..];
            {
                Content = (Content[..index] + otherHalf).PadRight(Size);
            }
            Count--;
        }       
    }
}