namespace Validator.RangeFacts
{
    public class TestProgram
    {
        [Fact]
        public void IsInRange()
        {
            string text = "hello";
            var range = new Range('b', 'i');
            Assert.True(range.Match(text));

            var otherRange = new Range('i', 'u');
            Assert.False(otherRange.Match(text));
        }
    }
}