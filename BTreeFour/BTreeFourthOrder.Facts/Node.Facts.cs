global using Xunit;
using BTreeFour;

namespace NodeFacts
{
    public class TestProgram
    {
        [Fact]
        public void AddKey_SortingIsDoneAutomatically()
        {
            Node<int> node = new();
            node.AddKey(3);
            node.AddKey(2);
            node.AddKey(1);
            Assert.Equal(new int[] { 1, 2, 3 }, node.Keys);
        }

        [Fact]
        public void RemoveKey_ItemIsRemovedAnd_OrderIsPreserved()
        {
            Node<int> node = new();
            node.AddKey(3);
            node.AddKey(2);
            node.AddKey(1);
            node.RemoveKey(2);
            Assert.Equal(new int[] { 1, 3, 0 }, node.Keys);
        }

        [Fact]
        public void ClearKeys_EverythingIsRemoved()
        {
            Node<int> node = new();
            node.AddKey(3);
            node.AddKey(2);
            node.AddKey(1);
            node.ClearKeys();
            Assert.Equal(node.KeysCount, 0);
        }

        [Fact]
        public void DeleteKey_ReturnsFalseIfItDoesntHaveExtraKeys()
        {
            Node<int> node = new();
            node.AddKey(2);
            node.AddKey(1);
            Assert.True(node.DeleteKey(2));
            Assert.False(node.DeleteKey(1));
        }

        [Fact]
        public void RemoveChild_ChildrenAreRedistributed()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(5);
            bTree.Insert(3);
            bTree.Insert(21);
            bTree.Insert(1);
            bTree.Insert(4);
            bTree.Insert(22);
            Assert.Equal(bTree.Root.CountChildren(), 3);
            bTree.Root.RemoveChild(0);
            Assert.Equal(bTree.Root.CountChildren(), 2);
            Assert.NotEqual(bTree.Root.Children[0], null);
        }

        [Fact]
        public void MergeParentAndBrotherInItsPlace_LeftBrother()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(1);
            bTree.Insert(2);
            bTree.Insert(3);
            bTree.Insert(4);
            bTree.Insert(5);
            bTree.Insert(6);
            bTree.Remove(6);
            bTree.Remove(3);
            Assert.Equal(new int[] { 1, 2, 0 }, bTree.Root.Children[0].Keys);
        }

        [Fact]
        public void MergeParentAndBrotherInItsPlace_RightBrother()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(5);
            bTree.Insert(3);
            bTree.Insert(21);
            bTree.Insert(1);
            bTree.Insert(4);
            bTree.Insert(22);
            bTree.Insert(23);
            bTree.Insert(24);
            bTree.Insert(25);
            bTree.Insert(26);
            bTree.Remove(21);
            Assert.Equal(new int[] { 22, 23, 0 }, bTree.Root.Children[1].Children[0].Keys);
        }

        [Theory]
        [InlineData(new int[] {1, 3, 0}, 0, 0)]
        [InlineData(new int[] {21, 0, 0}, 0, 1)]
        public void MergeParentAndBrotherInItsPlace_RebalanceParent(int[] level, int index, int index2)
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(5);
            bTree.Insert(3);
            bTree.Insert(21);
            bTree.Insert(1);
            bTree.Insert(4);
            bTree.Insert(22);
            bTree.Insert(23);
            bTree.Insert(24);
            bTree.Insert(25);
            bTree.Insert(26);
            bTree.Remove(4);
            Assert.Equal(level, bTree.Root.Children[index].Children[index2].Keys);
        }

        [Fact]
        public void FindInOrderSuccessor_ForRoot()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(1);
            bTree.Insert(2);
            bTree.Insert(3);
            bTree.Insert(4);
            bTree.Remove(2);
            Assert.Equal(new int[] { 3, 0, 0 }, bTree.Root.Keys);
            Assert.Equal(new int[] { 1, 0, 0 }, bTree.Root.Children[0].Keys);
        }

        [Fact]
        public void FindInOrderPredecessor()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(5);
            bTree.Insert(3);
            bTree.Insert(6);
            bTree.Insert(7);
            bTree.Insert(4);
            bTree.Remove(5);
            Assert.Equal(new int[] { 4, 0, 0}, bTree.Root.Keys);
            Assert.Equal(new int[] { 3, 0, 0}, bTree.Root.Children[0].Keys);
        }

        [Fact]
        public void MergeChildWithNextOne()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(1);
            bTree.Insert(2);
            bTree.Insert(3);
            bTree.Insert(4);
            bTree.Insert(5);
            bTree.Insert(6);
            bTree.Remove(2);
            Assert.Equal(new int[] { 4, 0, 0 }, bTree.Root.Keys);
            Assert.Equal(new int[] { 1, 3, 0 }, bTree.Root.Children[0].Keys);
            Assert.Equal(new int[] { 5, 6, 0 }, bTree.Root.Children[1].Keys);
        }

        [Theory]
        [InlineData(new int[] { 1, 4, 0 }, 0, 0)]
        [InlineData(new int[] { 21, 0, 0 }, 0, 1)]
        public void MergeChildWithNextOne_HasNoMoreKeys_CanRotate(int[] level, int index, int index2)
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(5);
            bTree.Insert(3);
            bTree.Insert(21);
            bTree.Insert(1);
            bTree.Insert(4);
            bTree.Insert(22);
            bTree.Insert(23);
            bTree.Insert(24);
            bTree.Insert(25);
            bTree.Insert(26);
            bTree.Remove(3);
            Assert.Equal(level, bTree.Root.Children[index].Children[index2].Keys);
        }

        [Fact]
        public void MergeChildWithNextOne_HasNoMoreKeys_CannotRotate()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(5);
            bTree.Insert(3);
            bTree.Insert(21);
            bTree.Insert(1);
            bTree.Insert(4);
            bTree.Insert(22);
            bTree.Remove(3);
            Assert.Equal(new int[] {5, 0, 0}, bTree.Root.Keys);
            Assert.Equal(new int[] {1, 4, 0}, bTree.Root.Children[0].Keys);
        }

        [Fact]
        public void MergeChildWithNextOne_HeightShrinks()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(5);
            bTree.Insert(3);
            bTree.Insert(21);
            bTree.Insert(1);
            bTree.Remove(21);
            bTree.Remove(3);
            Assert.Equal(new int[] { 1, 5, 0 }, bTree.Root.Keys);
            Assert.Equal(0, bTree.Root.CountChildren());
        }

        [Theory]
        [InlineData(0, 21)]
        [InlineData(1, 23)]
        public void InsertChild_ItSortsItself(int index, int key)
        {
            Node<int> node = new();
            Node<int> childNode = new();
            childNode.AddKey(23);
            Node<int> childNode2 = new();
            childNode2.AddKey(21);
            node.InsertChild(childNode);
            node.InsertChild(childNode2);
            Assert.True(node.Children[index].Keys[0] == key);
        }
    }
}