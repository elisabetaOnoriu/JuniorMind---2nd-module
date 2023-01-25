global using Xunit;
using Collections;

namespace SortedIntArrayFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(3, 1)]
        [InlineData(4, 2)]
        public void AddMethod_ItInsertsAnItemAtTheEnd_ItSortsItselfAfterImplementation(int expectedElement, int index)
        {
            var sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(1);
            sortedIntArray.Add(4);
            sortedIntArray.Add(3);
            Assert.Equal(expectedElement, sortedIntArray[index]);
            Assert.Equal(3, sortedIntArray.Count);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(4, 2)]
        [InlineData(5, 3)]
        [InlineData(5, 4)]
        public void AddMethod_ItInsertsAnItemAtTheEnd_ArraySizeIsDoubled_ItSortsItselfAfterImplementation(int expectedElement, int index)
        {
            var sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(1);
            sortedIntArray.Add(2);
            sortedIntArray.Add(5);
            sortedIntArray.Add(4);
            sortedIntArray.Add(5);
            Assert.Equal(expectedElement, sortedIntArray[index]);
            Assert.Equal(5, sortedIntArray.Count);
        }

        [Fact]
        public void SetElement_ChangesValueAtGivenPositionWithNewGivenElement_ItSortsItselfAfterImplementation()
        {
            var sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(1);
            sortedIntArray.Add(2);
            sortedIntArray.Add(3);
            sortedIntArray.Add(4);
            sortedIntArray[3] = 22;
            Assert.Equal(22, sortedIntArray[3]);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(4, 3)]
        public void InsertMethod_AddsANewElementAtTheGivenPosition_ItSortsItselfAfterImplementation(int expectedElement, int index)
        {
            var sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(1);
            sortedIntArray.Add(2);
            sortedIntArray.Add(4);
            sortedIntArray.Insert(0, 0);
            Assert.Equal(expectedElement, sortedIntArray[index]);
            Assert.Equal(4, sortedIntArray.Count);
        }

        [Fact]
        public void InsertMethod_ItIsNotWorkingIfPositionIsChangedAfterSorting()
        {
            var sortedIntArray = new SortedIntArray();
            sortedIntArray.Add(1);
            sortedIntArray.Add(2);
            sortedIntArray.Add(4);
            sortedIntArray.Insert(1, 5);
            Assert.Equal(4, sortedIntArray[2]);
            Assert.Equal(3, sortedIntArray.Count);
        }
    }
}