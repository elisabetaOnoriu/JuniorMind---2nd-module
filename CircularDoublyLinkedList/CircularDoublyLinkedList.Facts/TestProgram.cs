using LinkedList;
namespace CircularDoublyLinkedListFacts
{
    public class TestProgram
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
            Assert.Equal(1, linkedList.Last.Next.Next.Data);
            Assert.Equal(3, linkedList.First.Previous.Previous.Data);
        }

        [Fact]
        public void AddAfter_AddsNode()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 4};
            Node<int> toAdd = new Node<int>(3);
            linkedList.AddAfter(linkedList.First.Next, toAdd);
            Assert.Equal(linkedList.First.Next, toAdd.Previous);
            Assert.Equal(linkedList.Last, toAdd.Next);
        }

        [Fact]
        public void AddAfter_AddsByValue()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 4 };
            linkedList.AddAfter(linkedList.First.Next, 3);
            Assert.Equal(3, linkedList.First.Next.Next.Data);
            Assert.Equal(3, linkedList.Last.Previous.Data);
        }

        [Fact]
        public void AddBefore_AddsNode()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 4 };
            Node<int> toAdd = new Node<int>(3);
            linkedList.AddBefore(linkedList.Last, toAdd);
            Assert.Equal(linkedList.First.Next, toAdd.Previous);
            Assert.Equal(linkedList.Last, toAdd.Next);
        }

        [Fact]
        public void AddBefore_AddsByValue()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 4 };
            linkedList.AddBefore(linkedList.Last, 3);
            Assert.Equal(3, linkedList.First.Next.Next.Data);
            Assert.Equal(3, linkedList.Last.Previous.Data);
        }

        [Fact]
        public void AddFirst_AddsNode()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 2, 3, 4 };
            Node<int> toAdd = new Node<int>(1);
            linkedList.AddFirst(toAdd);
            Assert.Equal(linkedList.First, toAdd);
            Assert.Equal(linkedList.Last.Next.Next, toAdd);
        }


        [Fact]
        public void AddFirst_AddsByValue()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 4 };
            linkedList.AddFirst(3);
            Assert.Equal(3, linkedList.First.Data);
            Assert.Equal(3, linkedList.Last.Next.Next.Data);
        }

        [Fact]
        public void Add_Last_AddsNode()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1 };
            linkedList.Add(2);
            Assert.Equal(2, linkedList.Last.Data);
        }

        [Fact]
        public void Add_Last_AddsByValue()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1 };
            Node<int> toAdd = new(2);
            linkedList.Add(toAdd);
            Assert.Equal(2, linkedList.Last.Data);
        }

        [Fact]
        public void Clear_NodeIsCleared()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            linkedList.Clear();
            Assert.Equal(0, linkedList.Count);
            Assert.Equal(linkedList.Last, linkedList.First);
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
        public void Find_ReturnsFirstNodeThatHoldsThatData()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3, 1 };
            Assert.Equal(linkedList.First, linkedList.Find(1));
        }

        [Fact]
        public void FindLast_ReturnsLastNodeThatHoldsThatData()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3, 1 };
            Assert.Equal(linkedList.Last, linkedList.FindLast(1));
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
        public void Remove_RemovesByNode()
        {
            var linkedList = new CircularDoublyLinkedList<int>() { 1, 2, 3 };
            linkedList.Remove(linkedList.First.Next);
            var enumerator = linkedList.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(1, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(3, enumerator.Current);
        }

        [Fact]
        public void Remove_RemovesByValue()
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