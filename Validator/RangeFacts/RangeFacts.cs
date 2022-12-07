namespace RangeFacts
{
    public class TestProgram
    {
        [Fact]
        public void IsInRange()
        {
            string text = "hello";
            var range = new Validator.Range('b', 'i');
            Assert.True(range.Match(text));

            var otherRange = new Validator.Range('i', 'u');
            Assert.False(otherRange.Match(text));
        }
    }
}