using System.Collections;

namespace Collections
{
    public class List<T> : IList<T>
    {
        protected T[] items;
        int count;

        public List()
        {
            Array.Resize(ref items, 4);
        }

        public virtual void Add(T item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException("Cannot modify the List because it is readonly");
            }
            else
            {
                EnsureCapacity();
                items[count] = item;
                count++;
            }
        }

        public int Count { get => count; }

        public bool IsReadOnly
        {
            get { return false; }
        }

    public virtual T this[int index]
        {
            get => items[index];
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException("index");     
                }
                else
                {
                    items[index] = value; 
                }
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
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is outside the bounds");
            }
            else if (IsReadOnly)
            {
                throw new NotSupportedException("Cannot modify the List because it is readonly");    
            }       
            else 
            {
                EnsureCapacity();
                ShiftToRight(index);
                items[index] = item;
                count++;
            }
        }

        public void Clear()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException(); 
            }
            else
            {
                Array.Resize(ref items, 0);
                count = 0;
            }
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
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is outside of the bounds");
            } 
            else if (IsReadOnly)
            {
                throw new NotSupportedException();    
            }
            else
            {
                ShiftToLeft(index);
                items[^1] = default;
                count--;
            }      
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
            else if (arrayIndex < 0 || array.Length - arrayIndex < Count)
            {
                throw new ArgumentOutOfRangeException("array");
            }
            else if (array is int[,])
            {
                throw new ArgumentException("Array is multidimensional", nameof(array));
            }
            else
            {
                for (int i = arrayIndex; i < arrayIndex + Count; i++)
                {
                    array[arrayIndex++] = items[i];
                }
            }  
        }
    }
}