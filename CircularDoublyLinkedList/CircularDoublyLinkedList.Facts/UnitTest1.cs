using LinkedList;
namespace CircularDoublyLinkedListFacts
{
    public class UnitTest1
    {   
        [Fact]
        public void LinkListIsDoubled_PointsPrevious()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            var previous = linkedList.Last.Previous;
            Assert.Equal(2, previous.Data);
        }

        [Fact]
        public void LinkedListIsCircular()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            Assert.Equal(1, linkedList.Last.Next.Data);
            Assert.Equal(3, linkedList.First.Previous.Data);
        }

        [Fact]
        public void AddANode_FirstNodeIsRecognisedAsFirst()
        {
            var linkedList = new CircularDoublyLinkedList<int>();
            linkedList.Add(1);
            linkedList.Add(2);
            Assert.Equal(1, linkedList.First.Data);
        }

        [Fact]
        public void AddANode_LastNodeIsRecognisedAsLast()
        {
            var linkedList = new CircularDoublyLinkedList<int>();
            linkedList.Add(1);
            linkedList.Add(2);
            Assert.Equal(2, linkedList.Last.Data);
        }

        [Fact]
        public void Clear_NodeIsCleared()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            linkedList.Clear();
            Assert.Equal(0, linkedList.Count);
            Assert.Equal(null, linkedList.First);
            Assert.Equal(null, linkedList.Last);
        }

        [Fact]
        public void Contains_ReturnsTrueIfSo()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            Assert.True(linkedList.Contains(3));
            Assert.False(linkedList.Contains(4));
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public void CopyTo_NodesDataAreCopied(int index, int item)
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            int[] array = new int[3];
            linkedList.CopyTo(array, 0);
            Assert.Equal(item, array[index]);
        }

        [Fact]
        public void CopyTo_ArrayIsNull_ExceptionIsThrown()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            int[] array = null;
            Assert.Throws<ArgumentNullException>(() => linkedList.CopyTo(array, 0));
        }

        [Fact]
        public void CopyTo_ArraysLengthIsInsufficient_ExceptionIsThrown()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            int[] array = new int[1];
            Assert.Throws<ArgumentException>(() => linkedList.CopyTo(array, 0));
        }

        [Fact]
        public void CopyTo_IndexIsOutsideTheBounds_ExceptionIsThrown()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            int[] array = new int[2];
            Assert.Throws<ArgumentOutOfRangeException>(() => linkedList.CopyTo(array, -1));
        }

        [Fact]
        public void GetEnumerator_ReturnsEachItem()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            var enumerator = linkedList.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(1, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(2, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(3, enumerator.Current);
        }

        [Fact]
        public void Remove_SpecifiedDatasNodeIsDeleted()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            linkedList.Remove(2);
            var enumerator = linkedList.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(1, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(3, enumerator.Current);
        }
    }
}