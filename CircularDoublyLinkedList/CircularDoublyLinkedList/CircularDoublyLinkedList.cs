using System.Collections;
namespace LinkedList
{
    public class CircularDoublyLinkedList<T> : ICollection<T>
    {
        int count;
        Node<T> first = null;
        Node<T> last = null;

        public int Count { get => count; }

        public Node<T> First { get => first; }

        public Node<T> Last { get => last; }

        public bool IsReadOnly { get => false; }

        public void Add(T item)
        {
            Node<T> newNode = new(item);
            if (first == null && last == null)
            {
                newNode.Next = newNode;
                newNode.Previous = newNode;
                first = newNode;
                last = newNode;
            }
            else
            {
                last.Next = newNode;
                newNode.Next = first;
                newNode.Previous = last;
                first.Previous = newNode;
                last = newNode;
            }

            count++;
        }

        public void Clear()
        {
            first = null;
            last = null;
            count = 0;
        }

        public bool Contains(T item)
        {
            var enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException(null, nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException($"{arrayIndex}");
            }

            var current = first;
            for (int i = arrayIndex; i < arrayIndex + Count; i++)
            {
                array[i] = current.Data;
                current = current.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> Current = first;
            for (int i = 0; i < Count; i++)
            {
                yield return Current.Data;
                Current = Current.Next;
            }
        }

        public bool Remove(T item)
        {
            var current = first;
            for (int i = 0; i < count; i++)
            {
                if (current.Data.Equals(item))
                {
                    if (current == first)
                    {
                        current.Next = first;
                    }

                    if (current == last)
                    {
                        current.Previous = last;
                    }
                    else
                    {
                        current.Previous.Next = current.Next;
                    }

                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
