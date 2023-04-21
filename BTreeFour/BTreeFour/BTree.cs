namespace BTreeFour
{
    public class BTreeFourthOrder<T> where T : IComparable<T>
    {
        Node<T> root;
   
        public BTreeFourthOrder()
        {
            root = new Node<T>();
        }

        public bool Search(T key) => Search(root, key, out _) != null;

        public void Insert(T key) => Insert(root, key);
       
        public bool Remove(T key)
        {
            var node = Search(root, key, out int index);
            if (node == null)
            {
                return false;
            }
  
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

            if (!CanRotate(node, index))
            {
                MergeParentAndBrotherInItsPlace(node);
            }              
        }     
        
        private bool CanRotate(Node<T> node, int index)
        {
            if (node.IndexAsChild != 0 && node.Siblings[node.IndexAsChild - 1].HasExtraKeys)
            {
                RightRotate(node, index);
                return true;
            }
            else if (node.IndexAsChild != node.Siblings.Length - 1 && node.Siblings[node.IndexAsChild + 1].HasExtraKeys)
            {
                LeftRotate(node, index);
                return true;
            }

            return false;
        }

        private void LeftRotate(Node<T> node, int index)
        {
            node.Keys[index] = node.Parent.Keys[node.IndexAsChild];
            node.Parent.Keys[node.IndexAsChild] = node.Siblings[node.IndexAsChild + 1].Keys[0];
            node.Siblings[node.IndexAsChild + 1].RemoveKey(node.Siblings[node.IndexAsChild + 1].Keys[0]);   
        }

        private void RightRotate(Node<T> node, int index)
        {
            node.Keys[index] = node.Parent.Keys[node.IndexAsChild - 1];
            node.Parent.Keys[node.IndexAsChild - 1] = node.Siblings[node.IndexAsChild - 1].Keys[^1];
            node.Siblings[node.IndexAsChild - 1].RemoveKey(node.Siblings[node.IndexAsChild - 1].Keys[^1]);       
        }

        private void MergeParentAndBrotherInItsPlace(Node<T> node)
        {
            if (node.IndexAsChild != node.Siblings.Length - 1)
            {
                node.Siblings[node.IndexAsChild + 1].AddKey(node.Parent.Keys[node.IndexAsChild]);
                node.Parent.RemoveKey(node.Keys[node.IndexAsChild]);
                node.Parent.RemoveChild(node.IndexAsChild);
            }
            else if (node.IndexAsChild != 0)
            {
                node.Siblings[node.IndexAsChild - 1].AddKey(node.Parent.Keys[node.IndexAsChild - 1]);
                node.Parent.RemoveKey(node.Keys[node.IndexAsChild - 1]);
                node.Parent.RemoveChild(node.IndexAsChild);         
            }
            
            if (node.Parent != null && node.Parent.KeysCount == 0 && !CanRotate(node.Parent, 0))
            {
                MergeParentAndBrotherInItsPlace(node.Parent);
            }
        }

        private void ManageInternalNode(Node<T> node, int index)
        {
            if (node == root)
            {
                ReplaceItWith_Inorder_PredecessorOrSuccessor(index);
            }
            else if (!ReplaceWithChildKey(node, index))
            {
                MergeChildren(node, index);
                if (node.KeysCount == 0)
                {
                    MergeParentAndBrotherInItsPlace(node);     
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
            return DeleteKey(node, key);
        }

        private bool FindInOrderSuccessorAndRemoveIt(out T key)
        {
            Node<T> node = FindInOrderPredecessorOrSuccessor(false);
            key = node.Keys[0];
            return DeleteKey(node, key);
        }

        private bool DeleteKey(Node<T> node, T key)
        {
            if (node.HasExtraKeys)
            {
                node.RemoveKey(key);
                return true;
            }

            return false;
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

        private bool ReplaceWithChildKey(Node<T> node, int index)
        {
            if (node.IndexAsChild != 0 && node.Siblings[node.IndexAsChild - 1].HasExtraKeys)
            {
                ChooseReplacer(node, index, node.Siblings[index - 1].KeysCount - 1, -1);
                return true;
            }
            else if (node.IndexAsChild != node.Children.Length && node.Siblings[node.IndexAsChild + 1].HasExtraKeys)
            {
                ChooseReplacer(node, index, 0, 1);
                return true;
            }

            return false;
        }

        private void ChooseReplacer(Node<T> node, int index, int keyIndex, int predecessorOrSuccessor)
        {
            T siblingKeyToGet = node.Siblings[index + predecessorOrSuccessor].Keys[keyIndex];
            node.Keys[index] = siblingKeyToGet;
            node.Siblings[index + predecessorOrSuccessor].RemoveKey(siblingKeyToGet);
        }

        private void MergeChildren(Node<T> node, int index)
        {
            node.RemoveKey(node.Keys[index]);
            node.Children[index].AddKey(node.Children[index + 1].Keys[0]);
            node.RemoveChild(index + 1);
        }

        private Node<T> Search(Node<T> node, T key, out int index)
        {
            for (index = 0; index <= node.KeysCount - 1; index++)
            {
                if (node.Keys[index].Equals(key))
                {
                    return node;
                }

                LiesThroughChildren(node, key, index);    
            }

            return null;
        }

        private void LiesThroughChildren(Node<T> node, T key, int i)
        {
            if (node.Keys[i].CompareTo(key) == 1 && !node.IsLeaf)
            {
                Search(node.Children[i], key, out _);
            }
            else if (node.Keys[i].CompareTo(key) == -1 && i == node.KeysCount - 1 && !node.IsLeaf)
            {
                Search(node.Children[i + 1], key, out _);
            }
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
            AddKeyInNode(node.Parent, keyToGoUp);    
            node.ClearKeys();
            node.AddKey(temporary[0]);
            var splited = SetUpRightChildAfterSplit(temporary);
            ReestablishNodesConnections(node, splited, keyToGoUp);
        }

        private void ReestablishNodesConnections(Node<T> node, Node<T> splited, T keyToGoUp)
        {
            int keyIndexInParent = Array.IndexOf(node.Parent.Keys, keyToGoUp);       
            node.Parent.Children[keyIndexInParent] = node;
            node.Parent.Children[keyIndexInParent + 1] = splited;
            splited.Parent = node.Parent;
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