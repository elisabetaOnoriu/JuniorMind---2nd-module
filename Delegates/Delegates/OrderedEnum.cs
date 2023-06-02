using System.Collections;
namespace Delegates
{
    internal abstract class OrderedEnum<TElement> : IOrderedEnumerable<TElement>
    {
        internal IEnumerable<TElement> source;

        public IEnumerator<TElement> GetEnumerator()
        {
            List<TElement> list = new(source);
            SourceSorter<TElement> sorter = GetSourceSorter(null);
            int[] map = sorter.Sort(list);
            for (int i = 0; i < list.Count; i++)
            {
                yield return list[map[i]];
            }
        }

        internal abstract SourceSorter<TElement> GetSourceSorter(SourceSorter<TElement> next);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IOrderedEnumerable<TElement> IOrderedEnumerable<TElement>.CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            OrderedEnumerable<TElement, TKey> result = new(source, keySelector, comparer, descending);
            result.parent = this;
            return result;
        }
    }
}
