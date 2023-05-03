namespace BTreeFour
{
    public class BTreeFourthOrder<T> where T : IComparable<T>
    {
        Node<T> root;
   
        public BTreeFourthOrder()
        {
            root = new Node<T>();
        }

        public Node<T> Root { get => root; }

        public bool Search(T key) => Search(root, key) != null;

        public void Insert(T key) => Insert(root, key);
       
        public bool Remove(T key)
        {
            var node = Search(root, key);
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
            
            return true;
        }

        private void ManageLeafNode(Node<T> node, T key, int index)
        {
            if (node.HasExtraKeys)
            {
                node.RemoveKey(key);
            }

            if (!node.CanRotate(index))
            {
                node.MergeParentAndBrotherInItsPlace();
            }              
        }     

        private void ManageInternalNode(Node<T> node, int index)
        {
            if (node == root)
            {
                ReplaceItWith_Inorder_PredecessorOrSuccessor(index);
            }
            else if (!node.ReplaceWithPredecessorOrSuccessor(index))
            {
                node.MergeChildWithNextOne(index);
                if (node.KeysCount == 0)
                {
                    node.MergeParentAndBrotherInItsPlace();     
                }
            }
            
            if (node.Parent == root && node.Parent.KeysCount == 0)
            {
                node.Parent = null;
                root = node;
            }
        }

        private void ReplaceItWith_Inorder_PredecessorOrSuccessor(int index)
        {
            if (FindInOrderPredecessorAndRemoveIt(out T lastKey))
            {
                root.Keys[index] = lastKey;
            }
            else if (FindInOrderSuccessorAndRemoveIt(out T firstKey))
            {
                root.Keys[index] = firstKey;
            }
        }

        private bool FindInOrderPredecessorAndRemoveIt(out T key)
        {
            Node<T> node = FindInOrderPredecessorOrSuccessor(true);
            key = node.Keys[^1];
            return node.DeleteKey(key);
        }

        private bool FindInOrderSuccessorAndRemoveIt(out T key)
        {
            Node<T> node = FindInOrderPredecessorOrSuccessor(false);
            key = node.Keys[0];
            return node.DeleteKey(key);
        }

        private Node<T> FindInOrderPredecessorOrSuccessor(bool left)
        {
            for (Node<T> current = root; !current.IsLeaf; current = left ? current.Children[^1] : current.Children[0])
            {
                if (current.IsLeaf)
                { 
                    return current;
                }
            }

            return root;
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
                root = node.Parent;
            }

            T[] temporary = SetUpTemporaryKeysArray(node, key, out T keyToGoUp);          
            node.ClearKeys();
            node.AddKey(temporary[0]);
            var splited = SetUpRightChildAfterSplit(temporary);
            AddKeyInNode(node.Parent, keyToGoUp);
            ReestablishNodesConnections(node, splited);           
        }

        private void ReestablishNodesConnections(Node<T> node, Node<T> splited)
        {           
            if (node.Parent != null)
            {
                splited.Parent = node.Parent.Siblings == null ? node.Parent : node.Parent.Siblings[node.Parent.IndexAsChild - 1];
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