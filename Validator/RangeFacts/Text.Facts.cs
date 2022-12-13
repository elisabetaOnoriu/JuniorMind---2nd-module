global using Xunit;

namespace Validator.TextFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData("valid", "validate")]
        [InlineData("match", "match")]
        public void TextHasThatPrefix_TrueCases(string prefix, string text)
        {
            var start = new Text(prefix);
            var match = start.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[5..], match.RemainingText());
        }

        [Fact]
        public void TextHasThatPrefix_FalseCases()
        {
            var possibilities = new Any("Uu");
            var match = possibilities.Match("bbc");
            Assert.False(match.Success());
            Assert.Equal("bbc", match.RemainingText());
        }

        [Theory]
        [InlineData("begin", "end")]
        [InlineData("cloud", "clou")]
        public void TextHasThatPrefix_FalseCases_NullOrEmptyCases(string prefix, string text)
        {
            var start = new Text(prefix);
            var match = start.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }
    }
}
