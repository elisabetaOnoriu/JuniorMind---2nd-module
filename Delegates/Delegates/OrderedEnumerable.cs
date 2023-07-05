using Delegates;
internal class OrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
{
    internal IEnumerable<TSource> source;
    internal CustomComparer<TSource> baseComparer;

    internal OrderedEnumerable(IEnumerable<TSource> source, IComparer<TSource> comparer)
    {
        this.source = source;
        this.baseComparer = (CustomComparer<TSource>?)(comparer ?? Comparer<TSource>.Default);
    }

    public IEnumerator<TSource> GetEnumerator()
    {
        List<TSource> result = new(source);
        QuickSort(result, 0, result.Count - 1);
        return result.GetEnumerator();
    }

    public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> keyComparer, bool descending)
    {
        var customComparer = new CustomComparer<TSource>((e, f) => 
        baseComparer.Compare(e, f) == 0 
            ? keyComparer.Compare(keySelector(e), keySelector(f))
            : baseComparer.Compare(e, f));
        return new OrderedEnumerable<TSource>(source, customComparer);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    private void QuickSort(List<TSource> list, int left, int right)
    {
        if (left >= right)
        {
            return;
        }

        SortEachHalf(list, left, right);
    }

    private void SortEachHalf(List<TSource> list, int left, int right)
    {
        int partitioned = Partition(list, left, right);
        QuickSort(list, left, partitioned - 1);
        QuickSort(list, partitioned + 1, right);
    }

    private void Swap(List<TSource> list, int i, int j) => (list[i], list[j]) = (list[j], list[i]);

    private int Partition(List<TSource> list, int left, int right)
    {
        int pivot = right;
        int i = left - 1;
        for (int j = left; j < right; j++)
        {
            if (baseComparer.Compare(list[j], list[pivot]) < 0)
            {
                i++;
                Swap(list, i, j);
            }
        }

        Swap(list, i + 1, right);
        return i + 1;
    }
}