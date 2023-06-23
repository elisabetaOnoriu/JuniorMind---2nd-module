namespace Delegates
{
    internal class ConnectedSelectorAndComparer<TSource, TKey> : IComparer<TSource>
    {
        readonly Func<TSource, TKey> projection;
        readonly IComparer<TKey> comparer;

        public ConnectedSelectorAndComparer(Func<TSource, TKey> projection, IComparer<TKey> comparer)
        {
            this.comparer = comparer ?? Comparer<TKey>.Default;
            this.projection = projection;
        }

        public int Compare(TSource x, TSource y) => comparer.Compare(projection(x), projection(y));
    }
}
