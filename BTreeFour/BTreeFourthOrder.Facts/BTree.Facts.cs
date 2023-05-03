using BTreeFour;
namespace BTreeFourthOrderFacts
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(5)]
        [InlineData(3)]
        [InlineData(21)]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(22)]
        [InlineData(23)]
        [InlineData(24)]
        [InlineData(25)]
        [InlineData(26)]
        public void InsertingKeyInBTree(int key)
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
            Assert.True(bTree.Search(key));
        }

        [Fact]
        public void DeletingKeyInBTree_LeafNode()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(5);
            bTree.Insert(3);
            bTree.Insert(21);
            bTree.Insert(1);
            bTree.Insert(4);
            bTree.Insert(22);
            Assert.True(bTree.Remove(4));
        }
    }
}