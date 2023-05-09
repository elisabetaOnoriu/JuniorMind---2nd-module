namespace BTreeFour
{
    public class Node<T> where T : IComparable<T>
    {
        Node<T> parent;
        int keysCount;
        Node<T>[] children;

        public Node()
        {
            children = new Node<T>[4];
            Keys = new T[3];
        }

        public T[] Keys { get; set; }

        public int KeysCount { get => keysCount; set => keysCount = value; }

        public Node<T>[] Children { get => children; set =>children = value; }

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

        public Node<T>[] Siblings {get => Parent.Children; set => Parent.Children = value; }

        public int IndexAsChild { get => Array.IndexOf(Siblings, this); }

        public void AddKey(T key)
        {
            Keys[keysCount++] = key;
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
                Keys[i] = default;
            }

            keysCount = 0;
        }

        public bool IsLeaf => CountChildren() == 0;

        public bool HasExtraKeys { get => KeysCount > 1; }

        private void InsertionSort()
        {
            for (int i = 1; i < KeysCount && KeysCount > 1; i++)
            {
                for (int j = i; j > 0 && Keys[j - 1].CompareTo(Keys[j]) == 1; j--)
                {
                    (Keys[j - 1], Keys[j]) = (Keys[j], Keys[j - 1]);
                }
            }
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
                Children[i] = Children[i + 1];
            }

            Children[CountChildren() - 1] = null;
        }

        public void MergeParentAndBrotherInItsPlace()
        {
            if (IndexAsChild != 0)
            {
                Siblings[IndexAsChild - 1].AddKey(Parent.Keys[IndexAsChild - 1]);
                Parent.RemoveKey(Parent.Keys[IndexAsChild - 1]);
                Parent.RemoveChild(IndexAsChild);
            }
            else
            {
                Siblings[IndexAsChild + 1].AddKey(Parent.Keys[IndexAsChild]);
                Parent.RemoveKey(Parent.Keys[IndexAsChild]);
                Parent.RemoveChild(IndexAsChild);
            }
            
            if (Parent.KeysCount == 0 && Parent.Parent != null)
            {
                RebalanceParent();
            }
        }

        private void RebalanceParent()
        {          
            if (Parent.CanRotate(0))
            {
                Parent.KeysCount++;
                return;
            }

            Parent.MergeParentAndBrotherInItsPlace();
            TransferSiblingsChildren(IndexAsChild != 0 ? IndexAsChild - 1 : IndexAsChild);           
            Parent.KeysCount++;
        }

        private void TransferSiblingsChildren(int childIndex)
        {
            InsertChild(Siblings[childIndex].Children[0]);
            InsertChild(Siblings[childIndex].Children[1]);
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

        internal bool ReplaceItWith_Inorder_PredecessorOrSuccessor(int index)
        {
            if (FindInOrderPredecessorAndRemoveIt(out T key) || FindInOrderSuccessorAndRemoveIt(out key))
            {
                Keys[index] = key;
                return true;
            }

            return false;
        }

        private bool FindInOrderPredecessorAndRemoveIt(out T key)
        {
            Node<T> predecessor = FindInOrderPredecessorOrSuccessor(true);
            key = predecessor.Keys[KeysCount - 1];
            return predecessor.DeleteKey(key);
        }

        private bool FindInOrderSuccessorAndRemoveIt(out T key)
        {
            Node<T> successor = FindInOrderPredecessorOrSuccessor(false);
            key = successor.Keys[0];
            return successor.DeleteKey(key);
        }

        private Node<T> FindInOrderPredecessorOrSuccessor(bool left)
        {
            var firstChildToFind = left ? Children[0] : Children[CountChildren() - 1];
            for (var current = firstChildToFind; current != null; current = left ? current.Children[current.CountChildren() - 1] : current.Children[0])
            {
                if (current.IsLeaf)
                {
                    return current;
                }
            }

            return null;
        }

        private void LeftRotate(int index)
        {
            Keys[index] = Parent.Keys[IndexAsChild];
            Parent.Keys[IndexAsChild] = Siblings[IndexAsChild + 1].Keys[0];
            Siblings[IndexAsChild + 1].RemoveKey(Siblings[IndexAsChild + 1].Keys[0]);
            if (!IsLeaf)
            {
                InsertChild(Siblings[IndexAsChild + 1].Children[0]);
            }
        }

        private void RightRotate(int index)
        {
            var sibling = Siblings[IndexAsChild - 1];
            Keys[index] = Parent.Keys[IndexAsChild - 1];
            Parent.Keys[IndexAsChild - 1] = sibling.Keys[sibling.KeysCount - 1];
            Siblings[IndexAsChild - 1].RemoveKey(sibling.Keys[sibling.KeysCount - 1]);
            if (!IsLeaf)
            {
                InsertChild(sibling.Children[sibling.CountChildren() - 1]);
            }
        }
    }
}