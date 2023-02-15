namespace Collections
{
    public class ReadOnlyList<T> : List<T>
    {
        public ReadOnlyList(List<T> list)
        : base()
        {
            this.count = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                this[i] = list[i];
            }
        }

        public override bool IsReadOnly { get => true; }
    }
}
