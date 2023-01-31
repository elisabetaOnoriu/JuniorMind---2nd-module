﻿using Collections;

namespace ObjectArrayEnumFacts
{
    public class TestProgram
    {
        [Fact]
        public void UsingMoveNextAndCurrent_ItReturnsTheElementAtTheGivenPosition()
        {
            var objectArray = new ObjectArray { 1, 2, "abc" };
            var enumObjectArray = objectArray.GetEnumerator();
            enumObjectArray.MoveNext();
            Assert.Equal(1, enumObjectArray.Current);
        }

        [Fact]
        public void UsingReset_ItSetsThePositionToMinusOne()
        {
            var objectArray = new ObjectArray { 1, 2, "abc" };
            var enumObjectArray = objectArray.GetEnumerator();
            enumObjectArray.MoveNext();
            enumObjectArray.Reset();
            enumObjectArray.MoveNext();
            Assert.Equal(1, enumObjectArray.Current);
        }
    }
}
