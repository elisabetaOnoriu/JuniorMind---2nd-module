namespace LinkedList
{
    public class Node<T>
    {
        Node<T> next;
        Node<T> previous;
        T data;

        public Node(T data)
        {
            this.data = data;
        }

        public CircularDoublyLinkedList<T> List { get; set; } = null;

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
