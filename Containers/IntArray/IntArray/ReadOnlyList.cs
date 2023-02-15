namespace Collections
{
    public class ReadOnlyList<T> : List<T>
    {
        readonly List<T> list;

        public ReadOnlyList(List<T> list)
        : base()
        {
            this.list = list;
            this.count = list.Count;  
        }

        public override T this[int index] { get => list[index]; }

        public override bool IsReadOnly { get => true; }

        public override IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return list[i];
            }
        }
    }
}
