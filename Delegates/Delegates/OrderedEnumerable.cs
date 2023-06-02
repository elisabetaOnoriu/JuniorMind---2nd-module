namespace Delegates
{
    internal class OrderedEnumerable<TElement, TKey> : OrderedEnum<TElement>
    {
        internal OrderedEnum<TElement> parent;
        internal Func<TElement, TKey> keySelector;
        internal IComparer<TKey> comparer;
        internal bool descending;

        internal OrderedEnumerable(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            parent = null;
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer != null ? comparer : Comparer<TKey>.Default;
            this.descending = descending;
        }

        internal override SourceSorter<TElement> GetSourceSorter(SourceSorter<TElement> next)
        {
            SourceSorter<TElement> sorter = new SourceSorter<TElement, TKey>(keySelector, comparer, descending, next);
            if (parent != null) sorter = parent.GetSourceSorter(sorter);
            return sorter;
        }
    }
}