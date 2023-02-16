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
            Assert.Equal(list[index], list.ReadOnly()[index]);
        }

        [Fact]
        public void IndexOf_ReturnsTheRightIndex()
        {
            var list = new List<int>() { 4, 5, 6, 7 };
            Assert.Equal(0, list.ReadOnly().IndexOf(4));
        }

        [Fact]
        public void Contains_ReturnsTrue_IfReadOnlyListContainsTheGivenElement()
        {
            var list = new List<int>() { 4, 5, 6, 7 };
            Assert.True(list.ReadOnly().Contains(5));
        }

        [Fact]
        public void YieldKeywordWorksAsAnEnumerator()
        {
            var list = new Collections.List<string> { "abc", "def", "ghi" };
            var getEnum = list.ReadOnly().GetEnumerator();
            getEnum.MoveNext();
            Assert.Equal("abc", getEnum.Current);
            getEnum.MoveNext();
            Assert.Equal("def", getEnum.Current);
            getEnum.MoveNext();
            Assert.Equal("ghi", getEnum.Current);
        }


    }
}
