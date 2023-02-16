namespace Collections
{
    public class ReadOnlyList<T> : List<T>
    {
        readonly List<T> list;

        public ReadOnlyList(List<T> list)
        : base()
        {
            this.list = list; 
        }

        public override int Count { get => list.Count; }

        public override bool IsReadOnly { get => true; }

        public override T this[int index] { get => list[index]; }

        public override int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (list[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return list[i];
            }
        }
    }
}
