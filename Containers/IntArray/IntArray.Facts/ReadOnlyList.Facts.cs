namespace Collections.Facts
{
    public class ReadOnlyListFacts
    {
        [Fact]
        public void ListIsReadOnly_ShouldReturnTrue()
        {
            var list = new List<int>() { 4, 5, 6, 7 };
            var readOnlyList = list.ReadOnly();
            Assert.True(readOnlyList.IsReadOnly);
        }

        [Fact]
        public void CountIsPreserved_ShouldReturnTrue()
        {
            var list = new List<int>() { 4, 5, 6, 7 };
            var readOnlyList = list.ReadOnly();
            Assert.Equal(list.Count, readOnlyList.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ElementsArePreserved_ShouldReturnTrue(int index)
        {
            var list = new List<int>() { 4, 5, 6, 7 };
            var readOnlyList = list.ReadOnly();
            Assert.Equal(list[index], readOnlyList[index]);
        }


    }
}
