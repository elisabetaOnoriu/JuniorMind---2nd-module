namespace Delegates
{
    internal class MultiComparer<TSource, TKey> : IComparer<TSource>
    {
        internal IComparer<TKey> actual;
        internal IComparer<TSource> next;
        Func<TSource, TKey> keySelector;

        internal MultiComparer(IComparer<TKey> comparer, Func <TSource, TKey> keySelector)
        {
            actual = comparer;
            this.keySelector = keySelector;
            next = null;
        }

        public int Compare(TSource? x, TSource? y)
        {
            int compared = actual.Compare(keySelector(x), keySelector(y));
            if (compared != 0)
            {
                compared = next.Compare(x, y);
            }

            return compared;
        }
    }
}