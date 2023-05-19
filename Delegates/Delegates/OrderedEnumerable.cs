using System.Collections;
namespace Delegates
{
    internal class OrderedEnumerable<TElement, TKey> : IOrderedEnumerable<TElement>
    {
        IEnumerable<TElement> innerSource;
        Func<TElement, TKey> previousKeySelector;
        IComparer<TKey> previousComparer;

        public OrderedEnumerable(IEnumerable<TElement> innerSource, Func<TElement, TKey> previousKeySelector,
        IComparer<TKey> previousComparer)
        {
            this.innerSource = innerSource;
            this.previousKeySelector = previousKeySelector;
            this.previousComparer = previousComparer;
        }
      
        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
        {
            var list = new List<TElement>(innerSource);
            MultiInsertionSort(list, keySelector, comparer, descending);
            return new OrderedEnumerable<TElement, TKey>(list, keySelector, comparer);
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

        internal void MultiInsertionSort<TKey>
         (List<TElement> list, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            for (int i = 1; i < list.Count; i++)
            {
                for (int j = i; descending ? j >= 0 : j > 0 &&
                     CustomMultiComparer(keySelector, list[j - 1], list[j], comparer) == (descending ? -1 : 1); j--)
                {
                    (list[j - 1], list[j]) = (list[j], list[j - 1]);
                }
            }
        }

        internal int CustomMultiComparer<TKey>(Func<TElement, TKey> keySelector, 
            TElement item1, TElement item2, IComparer<TKey> comparer)
        {
            int firstComparer = previousComparer.Compare(previousKeySelector(item1), previousKeySelector(item2));
            int actualComparer = comparer.Compare(keySelector(item1), keySelector(item2));
            if (firstComparer == -1)
            {
                return -1;
            }
            else if (firstComparer == 1)
            {
                return 1;
            }
            else if (actualComparer == -1)
            {
                return -1;
            }
            else if (actualComparer == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
