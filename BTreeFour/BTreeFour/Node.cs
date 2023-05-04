namespace BTreeFour
{
    public class Node<T> where T : IComparable<T>
    {
        Node<T>[] children;
        Node<T> parent;
        T[] keys;
        int keysCount;

        public Node()
        {
            children = new Node<T>[4];
            keys = new T[3];
        }

        public T[] Keys { get => keys; set => keys = value; }

        public int KeysCount { get => keysCount; set => keysCount = value; }

        public Node<T>[] Children 
        {
            get => children;
            set
            {
                for (int i = 0; i < CountChildren(); i++)
                {
                    children[i].Parent = this;
                }

                children = value;
            }
        }

        public Node<T> Parent 
        { 
            get => parent; 
            set
            {
                parent = value;
                parent.InsertChild(this);              
            }
        }

        public void InsertChild(Node<T> node)
        {
            Children[CountChildren()] = node;
            for (int i = 0; i < CountChildren() - 1; i++)
            {
                if (Children[i].Keys[Children[i].KeysCount - 1].CompareTo(Children[i + 1].Keys[0]) == 1)
                {
                    (Children[i], Children[i + 1]) = (Children[i + 1], Children[i]);
                }
            }
        }

        public Node<T>[] Siblings 
        {
            get
            {
                if (parent == null)
                {
                    return null;
                }

                return this.Parent.Children;
            }

            set => Parent.Children = value;
        }

        public int IndexAsChild { get => Array.IndexOf(Siblings, this); }

        public void AddKey(T key)
        {
            keys[keysCount++] = key;
            InsertionSort();
        }

        public void RemoveKey(T key)
        {
            for (int i = Array.IndexOf(Keys, key); i < KeysCount - 1; i++)
            {
                Keys[i] = Keys[i + 1];
            }

            Keys[KeysCount - 1] = default;
            KeysCount--;
        }

        public void ClearKeys()
        {
            for (int i = 0; i < KeysCount; i++)
            {
                keys[i] = default;
            }

            keysCount = 0;
        }

        public bool IsLeaf => CountChildren() == 0;

        public bool HasExtraKeys { get => KeysCount > 1; }

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
        public bool DeleteKey(T key)
        {
            if (HasExtraKeys)
            {
                RemoveKey(key);
                return true;
            }

            return false;
        }

        public void RemoveChild(int index)
        {
            for (int i = index; i < CountChildren() - 1; i++)
            {
                children[i] = children[i + 1];
            }

            children[CountChildren() - 1] = null;
        }

        public void MergeParentAndBrotherInItsPlace()
        {
            if (IndexAsChild != 0)
            {
                Siblings[IndexAsChild - 1].AddKey(Parent.Keys[IndexAsChild - 1]);
                Parent.RemoveKey(Parent.Keys[IndexAsChild - 1]);
                Parent.RemoveChild(IndexAsChild);
            }
            else if (IndexAsChild != Siblings.Length - 1)
            {
                Siblings[IndexAsChild + 1].AddKey(Parent.Keys[IndexAsChild]);
                Parent.RemoveKey(Parent.Keys[IndexAsChild]);
                Parent.RemoveChild(IndexAsChild);
            }
            
            if (Parent != null && Parent.KeysCount == 0 && !Parent.CanRotate(0))
            {
                Parent.MergeParentAndBrotherInItsPlace();
            }
        }

        public void MergeChildWithNextOne(int index)
        {
            RemoveKey(Keys[index]);
            Children[index].AddKey(Children[index + 1].Keys[0]);
            RemoveChild(index + 1);
        }

        public bool CanRotate(int index)
        {
            if (IndexAsChild != 0 && Siblings[IndexAsChild - 1].HasExtraKeys)
            {
                RightRotate(index);
                return true;
            }
            else if (IndexAsChild != Siblings.Length - 1 && Siblings[IndexAsChild + 1].HasExtraKeys)
            {
                LeftRotate(index);
                return true;
            }

            return false;
        }

        private void LeftRotate(int index)
        {
            Keys[index] = Parent.Keys[IndexAsChild];
            Parent.Keys[IndexAsChild] = Siblings[IndexAsChild + 1].Keys[0];
            Siblings[IndexAsChild + 1].RemoveKey(Siblings[IndexAsChild + 1].Keys[0]);
        }

        private void RightRotate(int index)
        {
            Keys[index] = Parent.Keys[IndexAsChild - 1];
            Parent.Keys[IndexAsChild - 1] = Siblings[IndexAsChild - 1].Keys[^1];
            Siblings[IndexAsChild - 1].RemoveKey(Siblings[IndexAsChild - 1].Keys[^1]);
        }

        public bool ReplaceWithPredecessorOrSuccessor(int index)
        {
            if (IndexAsChild != 0 && Siblings[IndexAsChild - 1].HasExtraKeys)
            {
                ChooseReplacer(index, Siblings[index - 1].KeysCount - 1, -1);
                return true;
            }
            else if (IndexAsChild != Children.Length && Siblings[IndexAsChild + 1].HasExtraKeys)
            {
                ChooseReplacer(index, 0, 1);
                return true;
            }

            return false;
        }

        public void ChooseReplacer(int index, int keyIndex, int predecessorOrSuccessor)
        {
            T siblingKeyToGet = Siblings[index + predecessorOrSuccessor].Keys[keyIndex];
            Keys[index] = siblingKeyToGet;
            Siblings[index + predecessorOrSuccessor].RemoveKey(siblingKeyToGet);
        }

        public int CountChildren()
        {
            int counter = 0;
            foreach (var child in Children)
            {
                counter = child == null ? counter : counter + 1;
            }

            return counter;
        }
    }
}