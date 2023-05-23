using System.Collections;
namespace Delegates
{
    internal class OrderedEnumerable<TElement, TKey> : IOrderedEnumerable<TElement>
    {
        IEnumerable<TElement> innerSource;
        Func<TElement, TKey> previousKeySelector;

        public OrderedEnumerable(IEnumerable<TElement> innerSource, Func<TElement, TKey> previousKeySelector,
        IComparer<TKey> previousComparer)
        {
            this.innerSource = innerSource;
            this.previousKeySelector = previousKeySelector;
            PreviousComparer = previousComparer;
        }

        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey1>(Func<TElement, TKey1> keySelector, IComparer<TKey1>? comparer, bool descending)
        {
            var list = new List<TElement>(innerSource);
            var newComparer = new MultiComparer<TKey, TKey1>(PreviousComparer, comparer);
            MultiInsertionSort(list, keySelector, newComparer, descending);
            PreviousComparer = (IComparer<TKey>)newComparer;
            return new OrderedEnumerable<TElement, TKey>(list, previousKeySelector, PreviousComparer);
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            foreach (var item in innerSource)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal IComparer<TKey> PreviousComparer { get; set; }

        internal void MultiInsertionSort<TKey1>(List<TElement> list, Func<TElement, TKey1> keySelector, MultiComparer<TKey, TKey1> comparer, bool descending)
        {
            for (int i = 1; i < list.Count; i++)
            {
                for (int j = i; descending ? j >= 0 : j > 0 &&
                     comparer.Compare(new Tuple<TKey, TKey1>(previousKeySelector(list[j-1]), keySelector(list[j - 1])), 
                     new Tuple<TKey, TKey1>(previousKeySelector(list[j]), keySelector(list[j]))) == (descending ? -1 : 1); j--)
                {
                    (list[j - 1], list[j]) = (list[j], list[j - 1]);
                }
            }
        }
    }
}
