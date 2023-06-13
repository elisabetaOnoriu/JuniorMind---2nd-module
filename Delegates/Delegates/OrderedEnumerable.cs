using System.Collections;
namespace Delegates
{
    internal class OrderedEnumerable<TSource, TKey> : IOrderedEnumerable<TSource>
    {
        internal IEnumerable<TSource> source;
        internal Func<TSource, TKey> keySelector;
        internal IComparer<TSource> comparer;
        internal MultiComparer<TSource, TKey> multiComparer;
        internal bool descending;

        internal OrderedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Comparer<TSource> comparer, bool descending)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.multiComparer = new MultiComparer<TSource, TKey>(comparer, keySelector);
        }

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey1>(Func<TSource, TKey1> keySelector1, IComparer<TKey1>? comparer1, bool descending)
        {
            multiComparer.next = comparer1;
            return new OrderedEnumerable<TSource, TKey1>(source, keySelector1, comparer1, descending);
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            List<TSource> list = new(source);
            InsertionSort(list);
            for (int i = 0; i < list.Count; i++)
            {
                yield return list[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void InsertionSort(List<TSource> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                for (int j = i; descending ? j >= 0 : j > 0 && comparer.Compare(list[j - 1], list[j]) == (descending ? 1 : -1); j--)
                {
                    (list[j - 1], list[j]) = (list[j], list[j - 1]);
                }
            }
        }

    }
}