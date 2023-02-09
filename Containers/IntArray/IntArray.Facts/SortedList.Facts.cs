using Collections;

namespace SortedListFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        public void AddMethod_ItSortsItself(object expectedElement, int index)
        {
            var sortedList = new SortedList<int> { 1, 2, 4 };
            sortedList.Add(3);
            Assert.Equal(expectedElement, sortedList[index]);
            Assert.Equal(4, sortedList.Count);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 4)]
        public void AddMethod_ItInsertsAnItemAtTheEnd_ArraySizeIsDoubled(object expectedElement, int index)
        {
            var sortedList = new SortedList<int>();
            sortedList.Add(1);
            sortedList.Add(2);
            sortedList.Add(5);
            sortedList.Add(4);
            sortedList.Add(3);
            Assert.Equal(expectedElement, sortedList[index]);
            Assert.Equal(5, sortedList.Count);
        }

        [Fact]
        public void CountMethod_ItReturnsTheLength()
        {
            var sortedList = new SortedList<double>();
            int expectedLength = 2;
            sortedList.Add(3);
            sortedList.Add(4.2);
            Assert.Equal(expectedLength, sortedList.Count);
        }

        [Fact]
        public void Element_ItReturnsTheElementAtTheGivenPosition()
        {
            var sortedList = new SortedList<string>();
            sortedList.Add("xyz");
            sortedList.Add("www");
            string expectedElement = "www";
            Assert.Equal(expectedElement, sortedList[0]);
        }

        [Fact]
        public void SetElement_ChangesValueAtGivenPositionWithNewGivenElement()
        {
            var sortedList = new SortedList<int>();
            sortedList.Add(1);
            sortedList.Add(2);
            sortedList.Add(3);
            sortedList.Add(4);
            sortedList[3] = 22;
            Assert.Equal(22, sortedList[3]);
        }

        [Fact]
        public void ContainsMethod_VerifiesIfTheElementExistsIntheArray()
        {
            var sortedList = new Collections.SortedList<int>();
            sortedList.Add(1);
            sortedList.Add(2);
            sortedList.Add(3);
            sortedList.Add(4);
            Assert.True(sortedList.Contains(4));
        }

        [Fact]
        public void IndexOfMethod_ReturnsIndexOfGivenElement()
        {
            var sortedList = new SortedList<int>();
            sortedList.Add(1);
            sortedList.Add(2);
            sortedList.Add(3);
            Assert.Equal(1, sortedList.IndexOf(2));
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        public void InsertMethod_AddsANewElementAtTheGivenPosition(int expectedElement, int index)
        {
            var sortedList = new SortedList<int>();
            sortedList.Add(1);
            sortedList.Add(2);
            sortedList.Add(4);
            sortedList.Insert(2, 3);
            Assert.Equal(expectedElement, sortedList[index]);
            Assert.Equal(4, sortedList.Count);
        }

        [Fact]
        public void ClearMethod_RemovesAllItemsFromArray()
        {
            var sortedList = new SortedList<int>();
            sortedList.Add(1);
            sortedList.Add(2);
            sortedList.Add(3);
            sortedList.Clear();
            Assert.Equal(0, sortedList.Count);
        }

        [Fact]
        public void RemoveMethod_RemovesFirstInstanceOfElement()
        {
            var sortedList = new SortedList<int>();
            sortedList.Add(1);
            sortedList.Add(2);
            sortedList.Add(4);
            sortedList.Add(1);
            sortedList.Add(4);
            sortedList.Remove(1);
            Assert.Equal(2, sortedList[1]);
            Assert.Equal(4, sortedList[3]);
        }

        [Fact]
        public void RemoveAtMethod_RemovesElementAtTheGivenPosition()
        {
            var sortedList = new SortedList<int> { 1, 2, 4, 3};
            sortedList.RemoveAt(2);
            Assert.Equal(4, sortedList[2]);
            Assert.Equal(3, sortedList.Count);
        }

        [Fact]
        public void YieldKeywordWorksAsAnEnumerator()
        {
            var sortedList = new SortedList<string> { "abc", "bce", "tuv" };
            var getEnum = sortedList.GetEnumerator();
            getEnum.MoveNext();
            Assert.Equal("abc", getEnum.Current);
            getEnum.MoveNext();
            Assert.Equal("bce", getEnum.Current);
            getEnum.MoveNext();
            Assert.Equal("tuv", getEnum.Current);
        }
    }
}