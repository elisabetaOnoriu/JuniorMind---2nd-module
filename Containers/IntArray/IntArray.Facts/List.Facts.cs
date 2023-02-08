namespace ListFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void AddMethod_ItInsertsAnItemAtTheEnd(object expectedElement, int index)
        {
            var list = new Collections.List<int> { 1, 2, 3};
            Assert.Equal(expectedElement, list[index]);
            Assert.Equal(3, list.Count);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 4)]
        public void AddMethod_ItInsertsAnItemAtTheEnd_ArraySizeIsDoubled(object expectedElement, int index)
        {
            var list = new Collections.List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            Assert.Equal(expectedElement, list[index]);
            Assert.Equal(5, list.Count);
        }

        [Fact]
        public void CountMethod_ItReturnsTheLength()
        {
            var list = new Collections.List<object>();
            int expectedLength = 2;
            list.Add(true);
            list.Add("abc");
            Assert.Equal(expectedLength, list.Count);
        }

        [Fact]
        public void Element_ItReturnsTheElementAtTheGivenPosition()
        {
            var list = new Collections.List<string>();
            list.Add("xyz");
            list.Add("www");
            string expectedElement = "www";
            Assert.Equal(expectedElement, list[1]);
        }

        [Fact]
        public void SetElement_ChangesValueAtGivenPositionWithNewGivenElement()
        {
            var list = new Collections.List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list[3] = 22;
            Assert.Equal(22, list[3]);
        }

        [Fact]
        public void ContainsMethod_VerifiesIfTheElementExistsIntheArray()
        {
            var list = new Collections.List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            Assert.True(list.Contains(4));
        }

        [Fact]
        public void IndexOfMethod_ReturnsIndexOfGivenElement()
        {
            var list = new Collections.List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Assert.Equal(1, list.IndexOf(2));
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        public void InsertMethod_AddsANewElementAtTheGivenPosition(int expectedElement, int index)
        {
            var list = new Collections.List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(4);
            list.Insert(2, 3);
            Assert.Equal(expectedElement, list[index]);
            Assert.Equal(4, list.Count);
        }

        [Fact]
        public void ClearMethod_RemovesAllItemsFromArray()
        {
            var list = new Collections.List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Clear();
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void RemoveMethod_RemovesFirstInstanceOfElement()
        {
            var list = new Collections.List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(4);
            list.Add(1);
            list.Add(4);
            list.Remove(1);
            Assert.Equal(2, list[0]);
            Assert.Equal(1, list[2]);
        }

        [Fact]
        public void RemoveAtMethod_RemovesElementAtTheGivenPosition()
        {
            var list = new Collections.List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(4);
            list.Add(3);
            list.RemoveAt(2);
            Assert.Equal(3, list[2]);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void YieldKeywordWorksAsAnEnumerator()
        {
            var list = new Collections.List<object> { "abc", true, 3 };
            var toEnumerate = list.Items();
            var getEnum = toEnumerate.GetEnumerator();
            getEnum.MoveNext();
            Assert.Equal("abc", getEnum.Current);
            getEnum.MoveNext();
            Assert.Equal(true, getEnum.Current);
            getEnum.MoveNext();
            Assert.Equal(3, getEnum.Current);
        }
    }
}