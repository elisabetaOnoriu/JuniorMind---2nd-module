global using Xunit;

namespace Validator.RangeFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData('b', 'i', "bizar")]
        [InlineData('1', '5', "1marinar")]
        public void IsInRange_TrueCases(char start, char end, string text)
        {
            var result = new Range(start, end);
            var match = result.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[1..], match.RemainingText());
        }

        [Theory]
        [InlineData(' ', '/', "*code")]
        [InlineData('#', '?', ".code")]
        public void IsInRange_OddCharacters_TrueCases(char start, char end, string text)
        {
            var result = new Range(start, end);
            var match = result.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[1..], match.RemainingText());
        }

        [Theory]
        [InlineData('c', 'e', "whale")]
        [InlineData('a', 'z', "1234")]
        public void IsInRange_FalseCases(char start, char end, string text)
        {
            var result = new Range(start, end);
            var match = result.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }

        [Theory]
        [InlineData('#', '@', "~abc")]
        [InlineData('%', '%', "&abc")]
        public void IsInRange_OddCharacters_FalseCases(char start, char end, string text)
        {
            var result = new Range(start, end);
            var match = result.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }

        [Theory]
        [InlineData('a', 'z', null)]
        [InlineData('a', 'z', "")]
        public void IsInRange__IsNotNullOrEmpty_FalseCases(char start, char end, string text)
        {
            var result = new Range(start, end);
            var match = result.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }
    }
}