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

        public string DisplayCell { get; set; } = "";

        public void AddChar(char toAdd, int index)
        {
            if (index >= Count - 1 && Count >= Size)
            {
                Content += toAdd;
            }
            else
            {
                string half = Count > 0 ? Content[..index] : "";
                string otherHalf = Count > index ? Content[index..] : "";
                Content = (half + toAdd + otherHalf).PadRight(Size);
            }
            
            Count++;
        }

        public void RemoveChar(int index)
        {
            if (index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            string otherHalf = index == this.Count ? "" : Content[index..];
            {
                Content = Content[..(index - 1)] + otherHalf;
            }

            Count--;
            Content = Content.PadRight(this.defaultLength);
        }
    }
}