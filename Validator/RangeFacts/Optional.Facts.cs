global using Xunit;

namespace Validator.OptionalFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData("abc", 'a')]
        [InlineData("aabc", 'a')]

        public void TextStartsWithTheGivenCharacter_TrueCases(string text, char a)
        {
            var pattern = new Optionally(new Character(a));
            var match = pattern.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[1..], match.RemainingText());
        }

        [Theory]
        [InlineData("", 'a')]
        [InlineData(null, 'a')]
        [InlineData("bbc", 'a')]

        public void TextDoesNotStartWithTheGivenCharacter_TrueCases(string text, char a)
        {
            var pattern = new Optionally(new Character(a));
            var match = pattern.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text, match.RemainingText());
        }
    }
}
