namespace BTreeFour
{
    public class BTreeFourthOrder<T> where T : IComparable<T>
    {
        public BTreeFourthOrder()
        {
            Root = new Node<T>();
        }

        public Node<T> Root { get; set; }

        public bool Search(T key) => Search(Root, key) != null;

        public void Insert(T key) => Insert(Root, key);

        public bool Remove(T key)
        {
            var node = Search(Root, key);
            if (node == null)
            {
                return false;
            }

            int index = Array.IndexOf(node.Keys, key);
            if (node.IsLeaf)
            {
                ManageLeafNode(node, key, index);
            }
            else
            {
                ManageInternalNode(node, index);
            }

            ReduceHeightIfNecesarry(node);
            return true;
        }

        private void ManageLeafNode(Node<T> node, T key, int index)
        {
            if (node.HasExtraKeys)
            {
                node.RemoveKey(key);
            }
            else if (!node.CanRotate(index))
            {
                node.MergeParentAndBrotherInItsPlace();
            }

        }

        private void ManageInternalNode(Node<T> node, int index)
        {
            if (node.ReplaceItWith_Inorder_PredecessorOrSuccessor(index))
            {
                return;
            }

            node.MergeChildWithNextOne(index);
            if (node.KeysCount == 0 && node != Root && !node.CanRotate(index))
            {
                node.MergeParentAndBrotherInItsPlace();
            }
        }

        private void ReduceHeightIfNecesarry(Node<T> node)
        {
            if (node == Root && Root.KeysCount == 0)
            {
                Root = node.Children[0];
                node = null;
            }
        }

        private Node<T> Search(Node<T> node, T key)
        {
            for (int index = 0; index <= node.KeysCount - 1; index++)
            {
                if (node.Keys[index].Equals(key))
                {
                    return node;
                }

                Node<T> throughChildren = SearchThroughChildren(node, key, index);
                if (throughChildren != null)
                {
                    return throughChildren;
                }
            }

            return null;
        }

        private Node<T> SearchThroughChildren(Node<T> node, T key, int i)
        {
            if (node.Keys[i].CompareTo(key) == 1 && !node.IsLeaf)
            {
                return Search(node.Children[i], key);
            }
            else if (node.Keys[i].CompareTo(key) == -1 && i == node.KeysCount - 1 && !node.IsLeaf)
            {
                return Search(node.Children[i + 1], key);
            }

            return null;
        }

        private void Insert(Node<T> node, T key)
        {
            if (node.IsLeaf)
            {
                AddKeyInNode(node, key);
                return;
            }

            InsertInChildren(node, key);
        }

        private void InsertInChildren(Node<T> node, T key)
        {
            for (int i = 0; i <= node.KeysCount - 1; i++)
            {
                if (key.CompareTo(node.Keys[i]) == 1 && i == node.KeysCount - 1)
                {
                    Insert(node.Children[i + 1], key);
                    return;
                }
                else if (key.CompareTo(node.Keys[i]) == -1)
                {
                    Insert(node.Children[i], key);
                    return;
                }
            }
        }

        private void AddKeyInNode(Node<T> node, T key)
        {
            if (node.KeysCount < 3)
            {
                node.AddKey(key);
            }
            else
            {
                SplitKeys(node, key);
            }
        }

        private void SplitKeys(Node<T> node, T key)
        {
            if (node.Parent == null)
            {
                node.Parent = new Node<T>();
                Root = node.Parent;
            }

            T[] temporary = SetUpTemporaryKeysArray(node, key, out T keyToGoUp);
            node.ClearKeys();
            node.AddKey(temporary[0]);
            var splited = SetUpRightChildAfterSplit(temporary);
            AddKeyInNode(node.Parent, keyToGoUp);
            ReestablishNodesConnections(node, splited, keyToGoUp);
        }

        private void ReestablishNodesConnections(Node<T> node, Node<T> splited, T keyToGoUp)
        {
            splited.Parent = Search(Root, keyToGoUp);
            while (!node.IsLeaf && node.CountChildren() > node.KeysCount + 1)
            {
                int childrenCount = node.CountChildren();
                node.Children[childrenCount - 1].Parent = splited;
                node.RemoveChild(childrenCount - 1);
            }
        }

        private Node<T> SetUpRightChildAfterSplit(T[] temporary)
        {
            Node<T> splited = new Node<T>();
            splited.AddKey(temporary[2]);
            splited.AddKey(temporary[3]);
            return splited;
        }

        private T[] SetUpTemporaryKeysArray(Node<T> node, T key, out T keyToGoUp)
        {
            T[] temporary = new T[4];
            node.Keys.CopyTo(temporary, 0);
            temporary[^1] = key;
            Array.Sort(temporary);
            keyToGoUp = temporary[temporary.Length / 2 - 1];
            return temporary;
        }
    }
}