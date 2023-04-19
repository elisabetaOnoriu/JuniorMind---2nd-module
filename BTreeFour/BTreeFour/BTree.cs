namespace BTreeFour
{
    public class BTreeFourthOrder
    {
        Node root;
   
        public BTreeFourthOrder()
        {
            root = new Node();
        }

        public bool Search(int key) => Search(root, key, out _) != null;

        public void Insert(int key) => Insert(root, key);
       
        public bool Remove(int key)
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
                ManageInternalNode(node, key); 
            }
            
            return true;
        }

        private void ManageLeafNode(Node node, int key, int index)
        {
            if (node.HasExtraKeys)
            {
                node.RemoveKey(key);
            }

            if (!CanRotate(node, index))
            {
                MergeNeighboursInItsPlace(node);
            }              
        }     
        
        private bool CanRotate(Node node, int index)
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

        private void LeftRotate(Node node, int index)
        {
            node.Keys[index] = node.Parent.Keys[node.IndexAsChild];
            node.Parent.Keys[node.IndexAsChild] = node.Siblings[node.IndexAsChild + 1].Keys[0];
            node.Siblings[node.IndexAsChild + 1].RemoveKey(node.Siblings[node.IndexAsChild + 1].Keys[0]);   
        }

        private void RightRotate(Node node, int index)
        {
            node.Keys[index] = node.Parent.Keys[node.IndexAsChild - 1];
            node.Parent.Keys[node.IndexAsChild - 1] = node.Siblings[node.IndexAsChild - 1].Keys[^1];
            node.Siblings[node.IndexAsChild - 1].RemoveKey(node.Siblings[node.IndexAsChild - 1].Keys[^1]);       
        }

        private void MergeNeighboursInItsPlace(Node node)
        {
            if (node.IndexAsChild != node.Siblings.Length - 1)
            {
                node.Siblings[node.IndexAsChild + 1].AddKey(node.Parent.Keys[node.IndexAsChild]);
                node.Parent.RemoveKey(node.IndexAsChild);
                node.Parent.RemoveChild(node.IndexAsChild);
            }
            else if (node.IndexAsChild != 0)
            {
                node.Siblings[node.IndexAsChild - 1].AddKey(node.Parent.Keys[node.IndexAsChild - 1]);
                node.Parent.RemoveKey(node.IndexAsChild - 1);
                node.Parent.RemoveChild(node.IndexAsChild);         
            }
            
            if (node.Parent != null && node.Parent.KeysCount == 0 && !CanRotate(node.Parent, 0))
            {
                MergeNeighboursInItsPlace(node.Parent);
            }
        }

        private void ManageInternalNode(Node node, int index)
        {
            if (node == root)
            {
                ReplaceWith_Inorder_PredecessorOrSuccessor(index);
            }
            else if (!ReplaceWithChildKey(node, index))
            {
                MergeChildren(node, index);
                if (node.KeysCount == 0)
                {
                    ManageInternalNode(node.Parent, index);
                }
            }
        }

        private void ReplaceWith_Inorder_PredecessorOrSuccessor(int index)
        {
            if (FindInOrderPredecessorAndRemoveIt(out int lastKey))
            {
                root.Keys[index] = lastKey;
            }
            else if (FindInOrderSuccessorAndRemoveIt(out int firstKey))
            {
                root.Keys[index] = firstKey;
            }
        }

        private bool FindInOrderPredecessorAndRemoveIt(out int key)
        {
            Node node = FindInOrderPredecessorOrSuccessor(true);
            key = node.Keys[^1];
            return DeleteKey(node, key);
        }

        private bool FindInOrderSuccessorAndRemoveIt(out int key)
        {
            Node node = FindInOrderPredecessorOrSuccessor(false);
            key = node.Keys[0];
            return DeleteKey(node, key);
        }

        private bool DeleteKey(Node node, int key)
        {
            if (node.HasExtraKeys)
            {
                node.RemoveKey(key);
                return true;
            }

            return false;
        }

        private Node FindInOrderPredecessorOrSuccessor(bool left)
        {
            for (Node current = root; !current.IsLeaf; current = left ? current.Children[^1] : current.Children[0])
            {
                if (current.IsLeaf)
                { 
                    return current;
                }
            }

            return root;
        }

        private bool ReplaceWithChildKey(Node node, int index)
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

        private void ChooseReplacer(Node node, int index, int keyIndex, int predecessorOrSuccessor)
        {
            int siblingKeyToGet = node.Siblings[index + predecessorOrSuccessor].Keys[keyIndex];
            node.Keys[index] = siblingKeyToGet;
            node.Siblings[index + predecessorOrSuccessor].RemoveKey(siblingKeyToGet);
        }

        private void MergeChildren(Node node, int index)
        {
            node.RemoveKey(node.Keys[index]);
            node.Children[index].AddKey(node.Children[index + 1].Keys[0]);
            node.RemoveChild(index + 1);
        }

        private Node Search(Node node, int key, out int index)
        {
            for (index = 0; index <= node.KeysCount - 1; index++)
            {
                if (node.Keys[index] == key)
                {
                    return node;
                }

                Node foundThroughChildren = LiesThroughChildren(node, key, index);
                if (foundThroughChildren != null)
                {
                    return foundThroughChildren;
                }
            }

            index = 0;
            return null;
        }

        private Node LiesThroughChildren(Node node, int key, int i)
        {
            if (node.Keys[i] > key && !node.IsLeaf)
            {
                return Search(node.Children[i], key, out _);
            }
            else if (node.Keys[i] < key && i == node.KeysCount - 1 && !node.IsLeaf)
            {
                return Search(node.Children[i + 1], key, out _);
            }

            return null;
        }

        private void Insert(Node node, int key)
        {
            if (node.IsLeaf)
            {
                AddKeyInNode(node, key);
                return;
            }
            
            InsertInChildren(node, key);
        }

        private void InsertInChildren(Node node, int key)
        {
            for (int i = 0; i <= node.KeysCount - 1; i++)
            {
                if (key > node.Keys[i] && i == node.KeysCount - 1)
                {
                    Insert(node.Children[i + 1], key);
                    return;
                }
                else if (key < node.Keys[i])
                {
                    Insert(node.Children[i], key);
                    return;
                }
            }                    
        }

        private void AddKeyInNode(Node node, int key)
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

        private void SplitKeys(Node node, int key)
        { 
            if (node.Parent == null)
            {
                node.Parent = new Node();
                root = node.Parent;
            }

            int[] temporary = SetUpTemporaryKeysArray(node, key, out int keyToGoUp);
            AddKeyInNode(node.Parent, keyToGoUp);    
            node.ClearKeys();
            node.AddKey(temporary[0]);
            var splited = SetUpRightChildAfterSplit(temporary);
            ReestablishNodesConnections(node, splited, keyToGoUp);
        }

        private void ReestablishNodesConnections(Node node, Node splited, int keyToGoUp)
        {
            int keyIndexInParent = Array.IndexOf(node.Parent.Keys, keyToGoUp);       
            node.Parent.Children[keyIndexInParent] = node;
            node.Parent.Children[keyIndexInParent + 1] = splited;
            splited.Parent = node.Parent;
        }

        private Node SetUpRightChildAfterSplit(int[] temporary)
        {
            Node splited = new Node();
            splited.AddKey(temporary[2]);
            splited.AddKey(temporary[3]);
            return splited;
        }

        private int[] SetUpTemporaryKeysArray(Node node, int key, out int keyToGoUp)
        {
            int[] temporary = new int[4];
            node.Keys.CopyTo(temporary, 0);
            temporary[^1] = key;
            Array.Sort(temporary);
            keyToGoUp = temporary[temporary.Length / 2 - 1];
            return temporary;
        }
    }
}