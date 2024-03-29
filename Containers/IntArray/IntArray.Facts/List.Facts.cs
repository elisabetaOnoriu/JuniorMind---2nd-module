﻿namespace ListFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void AddMethod_ItInsertsAnItemAtTheEnd(object expectedElement, int index)
        {
            var list = new Collections.List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
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
        public void AddMethod_ThrowsExceptionIfListIsReadonly()
        {
            var list = new Collections.List<int>();
            var readOnlyList = list.ReadOnly();
            Assert.Throws<NotSupportedException>(() => readOnlyList.Add(1));
        }

        [Fact]
        public void CountMethod_ItReturnsTheLength()
        {
            var list = new Collections.List<object>() { true, "abc" };
            int expectedLength = 2;
            Assert.Equal(expectedLength, list.Count);
        }

        [Fact]
        public void Element_ItReturnsTheElementAtTheGivenPosition()
        {
            var list = new Collections.List<string>() { "xyz", "www"};
            string expectedElement = "www";
            Assert.Equal(expectedElement, list[1]);
        }

        [Fact]
        public void SetElement_ChangesValueAtGivenPositionWithNewGivenElement()
        {
            var list = new Collections.List<int>() { 1, 2, 3, 4 };
            list[3] = 22;
            Assert.Equal(22, list[3]);
        }

        [Fact]
        public void SetElement_ThrowsAnExceptionIfIndexIsOutsideTheBounds()
        {
            var list = new Collections.List<int>() { 1, 2, 3, 4 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list[4] = 22);
        }

        [Fact]
        public void ContainsMethod_VerifiesIfTheElementExistsIntheArray()
        {
            var list = new Collections.List<int>() { 1, 2, 3, 4};
            Assert.True(list.Contains(4));
        }

        [Fact]
        public void IndexOfMethod_ReturnsIndexOfGivenElement()
        {
            var list = new Collections.List<int>() { 1, 2, 3};
            Assert.Equal(1, list.IndexOf(2));
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        public void InsertMethod_AddsANewElementAtTheGivenPosition(int expectedElement, int index)
        {
            var list = new Collections.List<int>() { 1, 2, 4};
            list.Insert(2, 3);
            Assert.Equal(expectedElement, list[index]);
            Assert.Equal(4, list.Count);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(4)]
        public void InsertMethod_ThrowsExceptionIfIndexIsOutsideTheBounds(int index)
        {
            var list = new Collections.List<int>() { 1, 2, 4 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(index, 3));
        }

        [Fact]
        public void InsertMethod_ThrowsExceptionIfListIsReadonly()
        {
            var list = new Collections.List<int>() { 1, 2, 4 };
            var readOnlyList = list.ReadOnly();
            Assert.Throws<NotSupportedException>(() => readOnlyList.Insert(2, 3));
        }

        [Fact]
        public void ClearMethod_RemovesAllItemsFromArray()
        {
            var list = new Collections.List<int>() { 1, 2, 3};
            list.Clear();
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void ClearMethod_ThrowsExceptionIfListIsReadonly()
        {
            var list = new Collections.List<int>() { 1, 2, 3 };
            var readOnlyList = list.ReadOnly();
            Assert.Throws<NotSupportedException>(() => readOnlyList.Clear());
        }

        [Fact]
        public void RemoveMethod_RemovesFirstInstanceOfElement()
        {
            var list = new Collections.List<int>() { 1, 2, 4, 1, 4};
            list.Remove(1);
            Assert.Equal(2, list[0]);
            Assert.Equal(1, list[2]);
        }

        [Fact]
        public void RemoveAtMethod_RemovesElementAtTheGivenPosition()
        {
            var list = new Collections.List<int>() { 1, 2, 4, 3};
            list.RemoveAt(2);
            Assert.Equal(3, list[2]);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void RemoveAtMethod_ThrowsExceptionIfListIsReadonly()
        {
            var list = new Collections.List<int>() { 1, 2, 4, 3 };
            var readOnlyList = list.ReadOnly();
            Assert.Throws<NotSupportedException>(() => readOnlyList.RemoveAt(2));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(4)]
        public void RemoveAtMethod_ThrowsExceptionIfIndexIsOutsideTheBounds(int index)
        {
            var list = new Collections.List<int>() { 1, 2, 4, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(index));
        }

        [Fact]
        public void YieldKeywordWorksAsAnEnumerator()
        {
            var list = new Collections.List<string> { "abc", "def", "ghi" };
            var getEnum = list.GetEnumerator();
            getEnum.MoveNext();
            Assert.Equal("abc", getEnum.Current);
            getEnum.MoveNext();
            Assert.Equal("def", getEnum.Current);
            getEnum.MoveNext();
            Assert.Equal("ghi", getEnum.Current);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CopyTo_CopiesListToAnArrayStartingAtTheGivenPosition(int index)
        {
            var list = new List<int>() { 4, 5, 6, 7};
            int[] array = new int[5];
            array[0] = 3;
            list.CopyTo(array, 1);
            Assert.Equal(list[index], array[index + 1]);
        }

        [Fact]
        public void CopyTo_ThrowsExceptionIfArrayIsNull()
        {
            var list = new List<int>() { 4, 5, 6, 7 };
            int[] array = null;
            Assert.Throws<ArgumentNullException>(() => list.CopyTo(array, 0));
        }

        [Fact]
        public void CopyTo_ThrowsExceptionIfArraysLengthIsInsufficient()
        {
            var list = new List<int>() { 4, 5, 6, 7 };
            int[] array = new int[] { 1, 2, 3 };
            Assert.Throws<ArgumentException>(() => list.CopyTo(array, 2));
        }

        [Fact]
        public void CopyTo_ThrowsExceptionIfIndexIsOutsideTheBounds()
        {
            var list = new List<int>() { 4, 5, 6, 7 };
            int[] array = new int[] { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(array, -1));
        }
    }
}