using Collections;

namespace IntArrayFacts
{
    public class TestProgram
    {  
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void AddMethod_ItInsertsAnItemAtTheEnd(int expectedElement, int index)
        {
            var intArray = new IntArray();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(3);
            Assert.Equal(expectedElement, intArray.numbers[index]);
            Assert.Equal(3, intArray.Count());
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 4)]
        public void AddMethod_ItInsertsAnItemAtTheEnd_ArraySizeIsDoubled(int expectedElement, int index)
        {
            var intArray = new IntArray();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(3);
            intArray.Add(4);
            intArray.Add(5);
            Assert.Equal(expectedElement, intArray.numbers[index]);
            Assert.Equal(5, intArray.Count());
            Assert.Equal(8, intArray.numbers.Length);
        }

        [Fact]
        public void CountMethod_ItReturnsTheLength()
        {
            var intArray = new IntArray();
            int expectedLength = 2;
            intArray.Add(1);
            intArray.Add(2);
            Assert.Equal(expectedLength, intArray.Count());
        }

        [Fact]
        public void ElementMethod_ItReturnsTheElementAtTheGivenPosition()
        {
            var intArray = new IntArray();
            intArray.Add(1);
            intArray.Add(2);
            int expectedElement = 2;
            Assert.Equal(expectedElement, intArray.Element(1));
        }

        [Fact]
        public void SetElementMethod_ChangesValueAtGivenPositionWithNewGivenElement()
        {
            var intArray = new IntArray();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(3);
            intArray.Add(4);
            intArray.SetElement(3, 22);
            Assert.Equal(22, intArray.Element(3));
        }

        [Fact]
        public void ContainsMethod_VerifiesIfTheElementExistsIntheArray()
        {
            var intArray = new IntArray();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(3);
            intArray.Add(4);
            Assert.True(intArray.Contains(3));
        }

        [Fact]
        public void IndexOfMethod_ReturnsIndexOfGivenElement()
        {
            var intArray = new IntArray();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(3);
            int expectedIndex = 1;
            Assert.Equal(expectedIndex, intArray.IndexOf(2));
        }

        [Fact]
        public void InsertMethod_AddsANewElementAtTheGivenPosition()
        {
            var intArray = new IntArray();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(4);
            intArray.Insert(2, 3);
            Assert.Equal(3, intArray.Element(2));
        }

        [Fact]
        public void ClearMethod_RemovesAllItemsFromArray()
        {
            var intArray = new IntArray();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(3);
            intArray.Clear();
            Assert.Equal(0, intArray.Count());
        }

        [Fact]
        public void RemoveMethod_RemovesFirstInstanceOfElement()
        {
            var intArray = new IntArray();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(4);
            intArray.Add(1);
            intArray.Add(4);
            intArray.Remove(1);
            Assert.Equal(2, intArray.Element(0));
            Assert.Equal(1, intArray.Element(2));
        }

        [Fact]
        public void RemoveAtMethod_RemovesElementAtTheGivenPosition()
        {
            var intArray = new IntArray();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(4);
            intArray.Add(3);
            intArray.RemoveAt(2);
            Assert.Equal(3, intArray.Element(2));
            Assert.Equal(3, intArray.Count());
        }
    }
}