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
            sentinel.List = this;
        }

        public int Count { get => count; }

        public Node<T> First => count == 0 ? null : sentinel.Next;

        public Node<T> Last => count == 0 ? null : sentinel.Previous;

        public bool IsReadOnly { get => false; }

        public void AddAfter(Node<T> node, Node<T> newNode)
        {
            if (node == null || newNode == null)
            {
                throw new ArgumentNullException();
            }

            if (node.List != this || newNode.List != null)
            {
                throw new InvalidOperationException();
            }

            newNode.List = this;
            node.Next.Previous = newNode;   
            newNode.Next = node.Next;    
            node.Next = newNode;           
            newNode.Previous = node;
            count++;
        }

        public void AddAfter(Node<T> node, T item) => AddAfter(node, new Node<T>(item));

        public void AddBefore(Node<T> node, Node<T> newNode) => AddAfter(node.Previous, newNode);

        public void AddBefore(Node<T> node, T item) => AddBefore(node, new Node<T>(item));

        public void AddFirst(Node<T> newNode) => AddAfter(sentinel, newNode);

        public void AddFirst(T item) => AddFirst(new Node<T>(item));

        public void AddLast(Node<T> newNode) => AddAfter(sentinel.Previous, newNode);

        public void AddLast(T item) => AddLast(new Node<T>(item));

        public void Add(Node<T> newNode) => AddLast(newNode);

        public void Add(T item) => AddLast(item);

        public void Clear()
        {
            sentinel.Next = sentinel;
            sentinel.Previous = sentinel;
            count = 0;
        }

        public bool Contains(T item)
        {
            for (var current = sentinel.Next; current != sentinel; current = current.Next)
            {
                if (current.Data.Equals(item))
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

            var current = sentinel.Next;
            for (int i = arrayIndex; i < arrayIndex + Count; i++, current = current.Next)
            {
                array[i] = current.Data;
            }
        }

        public Node<T> Find(T value)
        {
            for (var current = sentinel.Next; current != sentinel; current = current.Next)
            {
                if (current.Data.Equals(value))
                {
                    return current;
                }
            }

            return null;
        }

        public Node<T> FindLast(T value)
        {
            for (var current = sentinel.Previous; current != sentinel; current = current.Previous)
            {
                if (current.Data.Equals(value))
                {
                    return current;
                }
            }

            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var current = sentinel.Next; current != sentinel; current = current.Next)
            {
                yield return current.Data;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Remove(Node<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.List != this || count == 0)
            {
                throw new InvalidOperationException();
            }

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
        }

        public bool Remove(T item)
        {
            var searchedNode = Find(item);
            if (searchedNode != null)
            {
                Remove(searchedNode);
                return true;
            }
            
            return false;
        }

        public void RemoveFirst() => Remove(sentinel.Next);

        public void RemoveLast() => Remove(sentinel.Previous);
    }
}
