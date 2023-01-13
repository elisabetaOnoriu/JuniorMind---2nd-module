namespace IntArray.Facts
{
    public class UnitTest1
    {
        [Fact(Skip = "Remove this Skip as you implement")]
        public void AddMethod_ItInsertsAnItemAtTheEnd()
        {
            int[] array = new int[] { 1, 2, 3 };
            int[] shouldResult = new int[] { 1, 2, 3, 4 };
            array.Add(4);
            Assert.Equal(shouldResult, array);
        }

        [Fact(Skip = "Remove this Skip as you implement")]
        public void CountMethod_ItReturnsTheLength()
        {
            int[] array = new int[] { 1, 2, 3 };
            int expectedLength = 3;
            
            Assert.Equal(expectedLength, array.Count());
        }

        [Fact(Skip = "Remove this Skip as you implement")]
        public void ElementMethod_ItReturnsTheElementAtTheGivenPosition()
        {
            int[] array = new int[] { 1, 2, 3, 4, 5 };
            int expectedElement = 5;
            Assert.Equal(expectedElement, array.Element(4));
        }

        [Fact(Skip = "Remove this Skip as you implement")]
        public void SetElementMethod_ChangesValueAtGivenPositionWithNewGivenElement()
        {
            int[] array = new int[] { 1, 2, 3, 4, 5 };
            int expectedElement = 22;
            array.SetElement(4, 22);
            Assert.Equal(expectedElement, array);
        }

        [Fact(Skip = "Remove this Skip as you implement")]
        public void ContainsMethod_ChangesValueAtGivenPositionWithNewGivenElement()
        {
            int[] array = new int[] { 1, 2, 3, 4, 5 };
            Assert.True(array.Contains(3));
        }

        [Fact(Skip = "Remove this Skip as you implement")]
        public void IndexOfMethod_ReturnsIndexOfGivenElement()
        {
            int[] array = new int[] { 1, 2, 3, 4, 5 };
            int expectedIndex = 1;
            Assert.Equal(expectedIndex, array.IndexOf(2));
        }

        [Fact(Skip = "Remove this Skip as you implement")]
        public void InsertMethod_AddsANewElementAtTheGivenPosition()
        {
            int[] array = new int[] { 1, 2, 4 };
            int[] shouldEqual = new int[] { 1, 2, 3, 4 };
            array.Insert(2, 3);
            Assert.Equal(shouldEqual, array);
        }

        [Fact(Skip = "Remove this Skip as you implement")]
        public void ClearMethod_RemovesAllItemsFromArray()
        {
            int[] array = new int[] { 1, 2, 3 };
            int[] shouldEqual = Array.Empty<int>();
            array.Clear();
            Assert.Equal(shouldEqual, array);
        }

        [Fact(Skip = "Remove this Skip as you implement")]
        public void RemoveMethod_AddsANewElementAtTheGivenPosition()
        {
            int[] array = new int[] { 1, 2, 4, 1 };
            int[] shouldEqual = new int[] { 2, 4, 1 };
            array.Remove(1);
            Assert.Equal(shouldEqual, array);
        }

        [Fact(Skip = "Remove this Skip as you implement")]
        public void RemoveAtMethod_AddsANewElementAtTheGivenPosition()
        {
            int[] array = new int[] { 1, 2, 4, 3 };
            int[] shouldEqual = new int[] { 1, 2, 3 };
            array.RemoveAt(2);
            Assert.Equal(shouldEqual, array);
        }
    }
}