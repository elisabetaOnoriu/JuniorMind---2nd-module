using System.Collections;
namespace Delegates
{
    internal class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>
    {
        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
        {
            var list = new List<TElement>(this);
            Delegates.InsertionSort(list, keySelector, comparer, descending);
            return (IOrderedEnumerable<TElement>)list;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            foreach (var item in this)
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
