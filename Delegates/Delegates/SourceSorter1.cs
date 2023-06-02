namespace Delegates
{
    internal class SourceSorter<TElement, TKey> : SourceSorter<TElement>
    {
        internal Func<TElement, TKey> keySelector;
        internal IComparer<TKey> comparer;
        internal bool descending;
        internal SourceSorter<TElement> next;
        internal TKey[] keys;

        internal SourceSorter(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, SourceSorter<TElement> next)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.next = next;
        }

        internal override void GenerateKeys(List<TElement> elements)
        {
            keys = new TKey[elements.Count];
            for (int i = 0; i < elements.Count; i++)
            {
                keys[i] = keySelector(elements[i]);
            }

            if (next != null)
            {
                next.GenerateKeys(elements);
            }
        }

        internal override int CompareKeys(int index1, int index2)
        {
            int compared = comparer.Compare(keys[index1], keys[index2]);
            if (compared == 0)
            {
                if (next == null) return 0;
                return next.CompareKeys(index1, index2);
            }

            return descending ? -compared : compared;
        }
    }
}