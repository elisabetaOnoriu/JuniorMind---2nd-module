using System.Collections;
namespace Delegates
{
    internal class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>
    {
        IEnumerable<TElement> innerSource;

        public OrderedEnumerable(IEnumerable<TElement> innerSource)
        {
            this.innerSource = innerSource;
        }

        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
        {
            return Delegates.OrderBy(innerSource, keySelector, comparer);
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
    }
}
