using System.Collections;

namespace Collections
{
    public class List<T> : IEnumerable
    {
        T[] items;
        int count;

        public List()
        {
            Array.Resize(ref items, 4);
        }

        public virtual void Add(T element)
        {
            EnsureCapacity();
            items[count] = element;
            count++;
        }

        public int Count { get => count; }

        public virtual T this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }

        public int IndexOf(object element)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].Equals(element))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Contains(T element)
        {
            return this.IndexOf(element) > -1;
        }

        public virtual void Insert(int index, T element)
        {
            EnsureCapacity();
            ShiftToRight(index);
            items[index] = element;
            count++;
        }

        public void Clear()
        {
            Array.Resize(ref items, 0);
            count = 0;
        }

        public virtual void Remove(T element)
        {
            int elementIndex = IndexOf(element);
            if (elementIndex > -1)
            {
                RemoveAt(elementIndex);
            }
        }

        public virtual void RemoveAt(int index)
        {
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ListEnum<T> GetEnumerator()
        {
            return new ListEnum<T>(this);
        }

        public IEnumerable Items()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return items[i];
            }
        }
    }
}