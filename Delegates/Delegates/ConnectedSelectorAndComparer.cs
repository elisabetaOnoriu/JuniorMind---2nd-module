namespace Delegates
{
    internal class ConnectedSelectorAndComparer<TSource, TKey> : IComparer<TSource>
    {
        readonly Func<TSource, TKey> keySelector;
        readonly IComparer<TKey> comparer;

        public ConnectedSelectorAndComparer(Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            this.comparer = comparer ?? Comparer<TKey>.Default;
            this.keySelector = keySelector;
        }

        public int Compare(TSource x, TSource y) => comparer.Compare(keySelector(x), keySelector(y));
    }
}
