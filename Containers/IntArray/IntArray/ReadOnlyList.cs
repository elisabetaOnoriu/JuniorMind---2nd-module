using System.Collections;

namespace Collections
{
    public class ReadOnlyList<T> : IList<T>
    {
        readonly IList<T> list;

        public ReadOnlyList(IList<T> list)
        {
            this.list = list; 
        }

        public void Add(T item) => ThrowExceptionIfListIsReadonly();

        public int Count { get => list.Count; }

        public bool IsReadOnly { get => true; }

        public T this[int index]
        {
            get => list[index];
            set => ThrowExceptionIfListIsReadonly();
        }

        public int IndexOf(T item) => list.IndexOf(item);

        public bool Contains(T item) => list.Contains(item);

        public void Insert(int index, T item) => ThrowExceptionIfListIsReadonly();

        public void Clear() => ThrowExceptionIfListIsReadonly();

        public bool Remove(T item) 
        {
            ThrowExceptionIfListIsReadonly();
            return false;
        }

        public void RemoveAt(int index) => ThrowExceptionIfListIsReadonly();

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        private void ThrowExceptionIfListIsReadonly()
        {
            throw new NotSupportedException("Cannot modify the List because it is readonly");
        }
    }
}
