﻿namespace LinkedList
{
    public class Node<T>
    {
        CircularDoublyLinkedList<T> list = null;
        Node<T> next;
        Node<T> previous;

        public Node(T data)
        {
            Data = data;
        }

        public CircularDoublyLinkedList<T> List { get => list; internal set => list = value; }

        public Node<T> Next { get => next; internal set => next = value;}

        public T Data { get; set; }

        public Node<T> Previous { get => previous; internal set => previous = value;}
    }   
}
