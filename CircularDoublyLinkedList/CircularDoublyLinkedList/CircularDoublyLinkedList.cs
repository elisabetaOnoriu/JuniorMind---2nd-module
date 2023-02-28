using System.Collections;
namespace LinkedList
{
    public class CircularDoublyLinkedList<T> : ICollection<T>
    {
        int count;
        Node<T> sentinel = new(default);

        public CircularDoublyLinkedList()
        {
            sentinel.Next = sentinel;
            sentinel.Previous = sentinel;
        }

        public int Count { get => count; }

        public Node<T> First { get => sentinel.Next; }

        public Node<T> Last { get => sentinel.Previous; }

        public bool IsReadOnly { get => false; }

        public void AddAfter(Node<T> node, Node<T> newNode)
        {
            if (node == Last)
            {
                Add(newNode);
            }

            node.Next.Previous = newNode;
            newNode.Next = node.Next;
            node.Next = newNode;
            newNode.Previous = node;
            count++;
        }

        public void AddAfter(Node<T> node, T item)
        { 
            if (node == Last)
            {
                Add(item);
            }

            Node<T> newNode = new(item);
            node.Next.Previous = newNode;
            newNode.Next = node.Next;
            node.Next = newNode;
            newNode.Previous = node;
            count++;
        }

        public void AddBefore(Node<T> node, Node<T> newNode)
        {
            if (node == First)
            {
                Add(node);
            }

            newNode.Previous = node.Previous;
            newNode.Next = node;
            node.Previous.Next = newNode;
            node.Previous = newNode;
            count++;
        }

        public void AddBefore(Node<T> node, T item)
        {
            if (node == First)
            {
                Add(node);
            }

            Node<T> newNode = new Node<T>(item);
            newNode.Previous = node.Previous;
            newNode.Next = node;
            node.Previous.Next = newNode;
            node.Previous = newNode;
            count++;
        }

        public void AddFirst(Node<T> newNode)
        {
            newNode.Next = sentinel.Next;
            sentinel.Next.Previous = newNode;
            newNode.Previous = sentinel;
            sentinel.Next = newNode;
            count++;
        }

        public void AddFirst(T item)
        {
            Node<T> newNode = new(item);
            newNode.Next = sentinel.Next;
            sentinel.Next.Previous = newNode;
            newNode.Previous = sentinel;
            sentinel.Next = newNode;
            count++;
        }
       public void Add(Node<T> newNode)
        {
            Last.Next = newNode;
            newNode.Next = sentinel;
            newNode.Previous = Last;
            sentinel.Previous = newNode;
            count++;
        }

        public void Add(T item)
        {
            Node<T> newNode = new(item);
            Last.Next = newNode;
            newNode.Next = sentinel;
            newNode.Previous = Last;
            sentinel.Previous = newNode;
            count++;
        }

        public void Clear()
        {
            sentinel.Next = sentinel;
            sentinel.Previous = sentinel;
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

            var current = First;
            for (int i = arrayIndex; i < arrayIndex + Count; i++)
            {
                array[i] = current.Data;
                current = current.Next;
            }
        }

        public Node<T> Find(T value)
        {
            var Current = First;
            while (Current != sentinel)
            {
                if (Current.Data.Equals(value))
                {
                    return Current;
                }

                Current = Current.Next;
            }

            return null;
        }

        public Node<T> FindLast(T value)
        {
            var Current = Last;
            while (Current != sentinel)
            {
                if (Current.Data.Equals(value))
                {
                    return Current;
                }

                Current = Current.Previous;
            }

            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> Current = First;
            for (int i = 0; i < Count; i++)
            {
                yield return Current.Data;
                Current = Current.Next;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Remove(Node<T> node)
        {
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            node = null;
        }

        public bool Remove(T item)
        {
            var current = First;
            for (int i = 0; i < count; i++)
            {
                if (current.Data.Equals(item))
                {
                    Remove(current);
                    return true;
                }

                current = current.Next;
            }

            return false;
        }
    }
}
