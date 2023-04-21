namespace BTreeFour
{
    internal class Node<T> where T : IComparable<T>
    {
        Node<T>[] children;
        Node<T> parent;
        T[] keys;
        int keysCount;

        internal Node()
        {
            children = new Node<T>[4];
            keys = new T[3];
        }

        internal T[] Keys { get => keys; set => keys = value; }

        internal int KeysCount { get => keysCount; set => KeysCount = value; }

        internal Node<T>[] Children { get => children; set => children = value; }

        internal Node<T> Parent { get => parent; set => parent = value; }

        internal Node<T>[] Siblings { get => this.Parent.Children; set => this.Parent.Children = value; }

        internal int IndexAsChild { get => Array.IndexOf(Siblings, this); }

        internal void AddKey(T key)
        {
            keys[keysCount++] = key;
            InsertionSort();
        }

        internal void RemoveKey(T key)
        {
            for (int i = Array.IndexOf(Keys, key); i < KeysCount - 1; i++)
            {
                Keys[i] = Keys[i + 1];
            }

            Keys[KeysCount - 1] = default;
            KeysCount--;
        }

        internal void ClearKeys()
        {
            for (int i = 0; i < KeysCount; i++)
            {
                keys[i] = default;
            }

            keysCount = 0;
        }

        internal bool IsLeaf => CountChildren() == 0;

        internal bool HasExtraKeys { get => KeysCount > 1; }

        private void InsertionSort()
        {
            for (int i = 1; i < KeysCount && KeysCount > 1; i++)
            {
                for (int j = i; j > 0 && keys[j - 1].CompareTo(keys[j]) == 1; j--)
                {
                    (keys[j - 1], keys[j]) = (keys[j], keys[j - 1]);
                }
            }
        }

        internal void RemoveChild(int index)
        {
            for (int i = index; i < CountChildren() - 1; i++)
            {
                children[i] = children[i + 1];
            }

            children[CountChildren() - 1] = null;
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