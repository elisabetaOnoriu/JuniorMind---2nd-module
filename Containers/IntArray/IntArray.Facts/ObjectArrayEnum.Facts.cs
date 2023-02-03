using Collections;

namespace ListEnumFacts
{
    public class TestProgram
    {
        [Fact]
        public void UsingMoveNextAndCurrent_ItReturnsTheElementAtTheGivenPosition()
        {
            var objectArray = new Collections.List<object> { 1, 2, "abc" };
            var enumList = objectArray.GetEnumerator();
            enumList.MoveNext();
            Assert.Equal(1, enumList.Current);
        }

        [Fact]
        public void UsingReset_ItSetsThePositionToMinusOne()
        {
            var objectArray = new Collections.List<object> { 1, 2, "abc" };
            var enumList = objectArray.GetEnumerator();
            enumList.MoveNext();
            enumList.Reset();
            enumList.MoveNext();
            Assert.Equal(1, enumList.Current);
        }

        [Fact]
        public void CapacityIsEnsured()
        {
            var objectArray = new Collections.List<object> { 1, 2, "abc" };
            int count = 0;
            var enumList = objectArray.GetEnumerator();
            foreach (var obj in objectArray) { count++; }
            Assert.Equal(3, count);
        }
    }
}
