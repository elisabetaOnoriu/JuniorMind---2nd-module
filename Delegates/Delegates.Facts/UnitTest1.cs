namespace Delegates.Facts
{
    public class UnitTest1
    {
        [Fact]
        public void OrderedEnumerable_ReturnsOrderedCollection()
        {
            var list = new List<int>() { 5, 2, 3, 6, 1 };
            Assert.Equal(new List<int>() { 1, 2, 3, 5, 6 }, Delegates.OrderBy(list, x => x, Comparer<int>.Default));
        }
    }
}