namespace LinkedList
{
    public class Node<T>
    {
        CircularDoublyLinkedList<T> list;
        Node<T> next;
        Node<T> previous;
        T data;

        public Node(T data)
        {
            this.data = data;
        }

        public Node<T> Next 
        { 
            get => next;
            set => next = value;
        }

        public T Data 
        { 
            get => data; 
            set => data = value;
        }

        public Node<T> Previous
        {
            get => previous;
            set => previous = value;
        }
    }
}
