using Collections;

namespace ObjectArrayFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData("abc", 0)]
        [InlineData(true, 1)]
        [InlineData(3, 2)]
        public void AddMethod_ItInsertsAnItemAtTheEnd(object expectedElement, int index)
        {
            var objectArray = new ObjectArray();
            objectArray.Add("abc");
            objectArray.Add(true);
            objectArray.Add(3);
            Assert.Equal(expectedElement, objectArray[index]);
            Assert.Equal(3, objectArray.Count);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 4)]
        public void AddMethod_ItInsertsAnItemAtTheEnd_ArraySizeIsDoubled(object expectedElement, int index)
        {
            var objectArray = new ObjectArray();
            objectArray.Add(1);
            objectArray.Add(2);
            objectArray.Add(3);
            objectArray.Add(4);
            objectArray.Add(5);
            Assert.Equal(expectedElement, objectArray[index]);
            Assert.Equal(5, objectArray.Count);
        }

        [Fact]
        public void CountMethod_ItReturnsTheLength()
        {
            var objectArray = new ObjectArray();
            int expectedLength = 2;
            objectArray.Add(1);
            objectArray.Add(2);
            Assert.Equal(expectedLength, objectArray.Count);
        }

        [Fact]
        public void Element_ItReturnsTheElementAtTheGivenPosition()
        {
            var objectArray = new ObjectArray();
            objectArray.Add(1);
            objectArray.Add(2);
            int expectedElement = 2;
            Assert.Equal(expectedElement, objectArray[1]);
        }

        [Fact]
        public void SetElement_ChangesValueAtGivenPositionWithNewGivenElement()
        {
            var objectArray = new ObjectArray();
            objectArray.Add(1);
            objectArray.Add(2);
            objectArray.Add(3);
            objectArray.Add(4);
            objectArray[3] = 22;
            Assert.Equal(22, objectArray[3]);
        }

        [Fact]
        public void ContainsMethod_VerifiesIfTheElementExistsIntheArray()
        {
            var objectArray = new ObjectArray();
            object first = 1, second = 2, third = 3, fourth = 4;
            objectArray.Add(first);
            objectArray.Add(second);
            objectArray.Add(third);
            objectArray.Add(fourth);
            Assert.True(objectArray.Contains(third));
        }

        [Fact]
        public void IndexOfMethod_ReturnsIndexOfGivenElement()
        {
            var objectArray = new ObjectArray();
            objectArray.Add(1);
            objectArray.Add(2);
            objectArray.Add(3);
            var searchedObject = objectArray[2];
            Assert.Equal(2, objectArray.IndexOf(searchedObject));
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        public void InsertMethod_AddsANewElementAtTheGivenPosition(int expectedElement, int index)
        {
            var objectArray = new ObjectArray();
            objectArray.Add(1);
            objectArray.Add(2);
            objectArray.Add(4);
            objectArray.Insert(2, 3);
            Assert.Equal(expectedElement, objectArray[index]);
            Assert.Equal(4, objectArray.Count);
        }

        [Fact]
        public void ClearMethod_RemovesAllItemsFromArray()
        {
            var objectArray = new ObjectArray();
            objectArray.Add(1);
            objectArray.Add(2);
            objectArray.Add(3);
            objectArray.Clear();
            Assert.Equal(0, objectArray.Count);
        }

        [Fact]
        public void RemoveMethod_RemovesFirstInstanceOfElement()
        {
            var objectArray = new ObjectArray();
            object first = 1;
            objectArray.Add(first);
            objectArray.Add(2);
            objectArray.Add(4);
            objectArray.Add(1);
            objectArray.Add(4);
            objectArray.Remove(first);
            Assert.Equal(2, objectArray[0]);
            Assert.Equal(1, objectArray[2]);
        }

        [Fact]
        public void RemoveAtMethod_RemovesElementAtTheGivenPosition()
        {
            var objectArray = new ObjectArray();
            objectArray.Add(1);
            objectArray.Add(2);
            objectArray.Add(4);
            objectArray.Add(3);
            objectArray.RemoveAt(2);
            Assert.Equal(3, objectArray[2]);
            Assert.Equal(3, objectArray.Count);
        }
    }
}