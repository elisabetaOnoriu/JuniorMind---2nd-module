﻿namespace BTreeFour
{
    internal class Node
    {
        Node[] children;
        Node parent;
        int[] keys;
        int keysCount;

        internal Node()
        {
            children = new Node[4];
            keys = new int[3];
        }

        internal int[] Keys { get => keys; set => keys = value; }

        internal int KeysCount { get => keysCount; set => KeysCount = value; }

        internal Node[] Children { get => children; set => children = value; }

        internal Node Parent { get => parent; set => parent = value; }

        internal void AddKey(int key)
        {
            keys[keysCount++] = key;
            InsertionSort();
        }

        internal void RemoveKey(int key)
        {
            for (int i = Array.IndexOf(Keys, key); i < KeysCount - 1; i++)
            {
                Keys[i] = Keys[i + 1];
            }

            Keys[KeysCount - 1] = 0;
            KeysCount--;
        }

        internal void ClearKeys()
        {
            for (int i = 0; i < KeysCount; i++)
            {
                keys[i] = 0;
            }

            keysCount = 0;
        }

        internal bool IsLeaf => CountChildren() == 0;

        internal bool HasMinimumChildren(Node node) =>  CountChildren() >= 2;

        private void InsertionSort()
        {
            for (int i = 1; i < KeysCount && KeysCount > 1; i++)
            {
                for (int j = i; j > 0 && keys[j - 1] > keys[j]; j--)
                {
                    (keys[j - 1], keys[j]) = (keys[j], keys[j - 1]);
                }
            }
        }

        private int CountChildren()
        {
            int counter = 0;
            foreach (var child in this.children)
            {
                counter = child == null ? counter : counter + 1;
            }

            return counter;
        }
    }
}