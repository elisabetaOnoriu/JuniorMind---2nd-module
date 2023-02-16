using System.Collections;

namespace Collections
{
    public class ReadOnlyList<T> : IList<T>
    {
        readonly List<T> list;

        public ReadOnlyList(List<T> list)
        {
            this.list = list; 
        }

        public void Add(T item)
        {
            ThrowExceptionIfListIsReadonly();
        }

        public int Count { get => list.Count; }

        public bool IsReadOnly { get => true; }

        public T this[int index]
        {
            get
            {
                ThrowExceptionIfArgumentIsOutOfRange(index, Count);
                return list[index];
            }
            set
            {
                ThrowExceptionIfListIsReadonly();
            }
        }


        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (list[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) > -1;
        }

        public void Insert(int index, T item)
        {
            ThrowExceptionIfListIsReadonly();
        }

        public void Clear()
        {
            ThrowExceptionIfListIsReadonly();
        }
        public bool Remove(T item)
        {
            ThrowExceptionIfListIsReadonly();
            return false;
        }

        public void RemoveAt(int index)
        {
            ThrowExceptionIfListIsReadonly();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return list[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException(null, nameof(array));
            }

            ThrowExceptionIfArgumentIsOutOfRange(arrayIndex, Count);
            for (int i = arrayIndex; i < arrayIndex + Count; i++)
            {
                array[arrayIndex] = list[i];
            }
        }

        private void ThrowExceptionIfListIsReadonly()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException("Cannot modify the List because it is readonly");
            }
        }

        private void ThrowExceptionIfArgumentIsOutOfRange(int index, int length)
        {
            if (index < 0 || index >= length)
            {
                throw new ArgumentOutOfRangeException($"{index}");
            }
        }
    }
}
