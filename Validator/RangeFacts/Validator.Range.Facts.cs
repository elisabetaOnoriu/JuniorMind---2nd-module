global using Xunit;

namespace Validator.RangeFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData('b', 'i', "bizar")]
        [InlineData('1', '5', "1marinar")]
        [InlineData(' ', '/', "*code")]
        public void IsInRange_TrueCases(char start, char end, string text)
        {
            var result = new Range(start, end);
            Assert.True(result.Match(text));
        }

        [Theory]
        [InlineData('c', 'e', "whale")]
        [InlineData('a', 'z', "1234")]
        [InlineData('#', '@', "~abc")]
        [InlineData('a', 'z', null)]
        [InlineData('a', 'z', "")]
        public void IsInRange_FalseCases(char start, char end, string text)
        {
            var result = new Range(start, end);
            Assert.False(result.Match(text));
        }
    }
}