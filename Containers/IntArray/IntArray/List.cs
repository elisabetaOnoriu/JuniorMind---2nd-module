using System.Collections;
namespace Collections
{
    public class List<T> : IList<T>
    {
        protected T[] items;
        protected int count;

        public List()
        {
            Array.Resize(ref items, 4);
        }

        public virtual void Add(T item)
        {
            ThrowExceptionIfListIsReadonly();
            EnsureCapacity();
            items[count] = item;
            count++;
        }

        public int Count { get => count; }

        public virtual bool IsReadOnly { get; }

        public ReadOnlyList<T> ReadOnly()
        {  
            return new(this);
        }

        public virtual T this[int index]
        {
            get
            {
                ThrowExceptionIfArgumentIsOutOfRange(index, Count);
                return items[index];
            }
            set
            {
                ThrowExceptionIfArgumentIsOutOfRange(index, Count);
                items[index] = value;
            }
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].Equals(item))
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

        public virtual void Insert(int index, T item)
        {
            ThrowExceptionIfArgumentIsOutOfRange(index, Count);
            ThrowExceptionIfListIsReadonly();
            EnsureCapacity();
            ShiftToRight(index);
            items[index] = item;
            count++;
        }

        public void Clear()
        {
            ThrowExceptionIfListIsReadonly();
            Array.Resize(ref items, 0);
            count = 0;
        }

        public bool Remove(T item)
        {
            int elementIndex = IndexOf(item);
            if (elementIndex > -1)
            {
                RemoveAt(elementIndex);
                return true;
            }

            return false;
        }

        public virtual void RemoveAt(int index)
        {
            ThrowExceptionIfArgumentIsOutOfRange(index, Count - 1);
            ThrowExceptionIfListIsReadonly();
            ShiftToLeft(index);
            items[^1] = default;
            count--;     
        }

        private void ShiftToLeft(int index)
        {
            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }
        }

        private void ShiftToRight(int index)
        {
            for (int i = count; i > index; i--)
            {
                items[i] = items[i - 1];
            }
        }

        private void EnsureCapacity()
        {
            if (items.Length == count)
            {
                Array.Resize(ref items, count * 2);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return items[i];
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
                array[arrayIndex] = items[i];
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
                throw new ArgumentOutOfRangeException($"{ index }");
            }
        }
    }
}