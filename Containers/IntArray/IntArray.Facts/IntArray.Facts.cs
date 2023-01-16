using Collections;

namespace IntArrayFacts
{
    public class TestProgram
    {  
        [Fact]
        public void CountMethod_ItReturnsTheLength()
        {
            var array = new IntArray(new int[] { 1, 2, 3 });
            int expectedLength = 3;
            
            Assert.Equal(expectedLength, array.Count());
        }

        [Fact]
        public void ElementMethod_ItReturnsTheElementAtTheGivenPosition()
        {
            var array = new IntArray(new int[] { 1, 2, 3, 4, 5 });
            int expectedElement = 5;
            Assert.Equal(expectedElement, array.Element(4));
        }

        [Fact]
        public void AddMethod_ItInsertsAnItemAtTheEnd()
        {
            var array = new IntArray(new int[] { 1, 2, 3, 0 });
            array.Add(4);
            Assert.Equal(4, array.Element(3));
            Assert.Equal(4, array.Count());
        }

        [Fact]
        public void SetElementMethod_ChangesValueAtGivenPositionWithNewGivenElement()
        {
            var array = new IntArray(new int[] { 1, 2, 3, 4, 5 });
            array.SetElement(4, 22);
            Assert.Equal(22, array.Element(4));
        }

        [Fact]
        public void ContainsMethod_VerifiesIfTheElementExistsIntheArray()
        {
            var array = new IntArray(new int[] { 1, 2, 3, 4, 5 });
            Assert.True(array.Contains(3));
        }

        [Fact]
        public void IndexOfMethod_ReturnsIndexOfGivenElement()
        {
            var array = new IntArray(new int[] { 1, 2, 3, 4, 5 });
            int expectedIndex = 1;
            Assert.Equal(expectedIndex, array.IndexOf(2));
        }

        [Fact]
        public void InsertMethod_AddsANewElementAtTheGivenPosition()
        {
            var array = new IntArray(new int[] { 1, 2, 4 });
            array.Insert(2, 3);
            Assert.Equal(3, array.Element(2));
        }

        [Fact]
        public void ClearMethod_RemovesAllItemsFromArray()
        {
            var array = new IntArray(new int[] { 1, 2, 3 });
            array.Clear();
            Assert.Equal(0, array.Count());
        }

        [Fact]
        public void RemoveMethod_RemovesFirstInstanceOfElement()
        {
            var array = new IntArray(new int[] { 1, 2, 4, 1, 4 });
            array.Remove(1);
            Assert.Equal(2, array.Element(0));
            Assert.Equal(1, array.Element(2));
        }

        [Fact]
        public void RemoveAtMethod_RemovesElementAtTheGivenPosition()
        {
            var array = new IntArray(new int[] { 1, 2, 4, 3 });
            array.RemoveAt(2);
            Assert.Equal(3, array.Element(2));
            Assert.Equal(3, array.Count());
        }
    }
}