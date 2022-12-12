global using Xunit;

namespace Validator.AnyFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData("eE", "e57")]
        [InlineData("eE", "Eva")]
        public void TextStartsWithAnyAcceptedItem_TrueCases(string accepted, string text)
        {
            var possibilities = new Any(accepted);
            var match = possibilities.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[1..], match.RemainingText());
        }

        [Fact]
        public void TextStartsWithAnyAcceptedItem_FalseCases()
        {
            var possibilities = new Any("Uu");
            var match = possibilities.Match("bbc");
            Assert.False(match.Success());
            Assert.Equal("bbc", match.RemainingText());
        }

        [Theory]
        [InlineData("eE", "")]
        [InlineData("eE", null)]
        public void TextStartsWithAnyAcceptedItem_FalseCases_NullOrEmptyCases(string accepted, string text)
        {
            var possibilities = new Any(accepted);
            var match = possibilities.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }
    }
}
