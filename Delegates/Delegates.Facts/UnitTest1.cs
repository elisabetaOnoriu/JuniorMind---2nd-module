namespace Delegates.Facts
{
    public class UnitTest1
    {
        [Fact]
        public void OrderedEnumerable_ReturnsOrderedCollection_ints()
        {
            var list = new List<int>() { 5, 2, 3, 6, 1 };
            Assert.Equal(new List<int>() { 1, 2, 3, 5, 6 }, list.MyOrderBy(x => x, Comparer<int>.Default));
        }

        [Fact]
        public void OrderedEnumerable_ReturnsOrderedCollection_chars()
        {
            var list = new List<int>() { 'a', 'g', 'e', 't', 'b' };
            Assert.Equal(new List<int>() { 'a', 'b', 'e', 'g', 't' }, list.MyOrderBy(x => x, Comparer<int>.Default));
        }

        [Fact]
        public void OrderedEnumerable_ReturnsOrderedCollection_strings()
        {

            string[] fruits = new string[] { "grape", "passionfruit", "banana", "mango",
                      "orange", "raspberry", "apple", "blueberry" };
            Assert.Equal(new string[] { "apple", "grape", "mango", "banana", "orange", "blueberry",
                "raspberry", "passionfruit" }, 
            fruits.MyOrderBy(y => y.Length, Comparer<int>.Default).MyThenBy(x => x, Comparer<string>.Default));
        }
    }
}