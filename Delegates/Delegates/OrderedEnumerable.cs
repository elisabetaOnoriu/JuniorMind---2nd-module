using Delegates;
internal class OrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
{
    internal IEnumerable<TSource> source;
    internal IComparer<TSource> baseComparer;

    internal OrderedEnumerable(IEnumerable<TSource> source, IComparer<TSource> comparer)
    {
        this.source = source;
        this.baseComparer = comparer ?? Comparer<TSource>.Default;
    }

    public IEnumerator<TSource> GetEnumerator()
    {
        List<TSource> result = new(source);
        InsertionSort(result);
        return result.GetEnumerator();
    }

    public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> keyComparer, bool descending)
    {
        IComparer<TSource> connectedComparer = new ConnectedSelectorAndComparer<TSource, TKey>(keySelector, keyComparer);
        return new OrderedEnumerable<TSource>(source, new MultiComparer<TSource>(baseComparer, connectedComparer));
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    void InsertionSort(List<TSource> list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            for (int j = i; j > 0 && baseComparer.Compare(list[j - 1], list[j]) == 1; j--)
            {
                (list[j - 1], list[j]) = (list[j], list[j - 1]);
            }
        }
    }

}