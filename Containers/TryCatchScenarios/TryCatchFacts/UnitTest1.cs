namespace TryCatchFacts
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int[] array = new int[] { 1, 2, 3 };
            Assert.Throws<IndexOutOfRangeException>(() => array[3]);
        }
    }
}