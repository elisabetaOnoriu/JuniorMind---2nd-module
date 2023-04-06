namespace BTreeFour
{
    public class BTreeFourthOrder
    {
        Node root;
   
        public BTreeFourthOrder()
        {
            root = new Node();
        }

        public bool Search(int key) => Search(root, key);

        internal bool Search(Node node, int key)
        {
            for (int i = 0; i <= node.KeysCount - 1; i++)
            {
                if (node.Keys[i] == key || LiesThroughChildren(node, key, i))
                {
                    return true;
                }
            }

            return false;
        }

        public void Insert(int key) => Insert(root, key);
        
        public void Remove(int key)
        {

        }

        private bool LiesThroughChildren(Node node, int key, int i)
        {
            if (node.Keys[i] > key && !node.IsLeaf)
            {
                return Search(node.Children[i], key);
            }

            if (node.Keys[i] < key && i == node.KeysCount - 1 && !node.IsLeaf)
            {
                return Search(node.Children[i + 1], key);
            }

            return false;
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

                if (key < node.Keys[i])
                {
                    Insert(node.Children[i], key);
                }
            }
                       
        }

        private void AddKeyInNode(Node node, int key)
        {     
            if (node.KeysCount == 3)
            {
                SplitKeys(node, key);
                return;
            }
            
            if (node.KeysCount < 3)
            {
                node.AddKey(key);
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