global using Xunit;

namespace Validator.CharacterFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData('x', "xiaomi")]
        [InlineData('v', "valid")]
        public void FirstCharacterMatches_TrueCases(char letter, string text)
        {
            var x = new Character(letter);
            var match = x.Match(text);
            Assert.IsType<SuccessMatch>(match);
            Assert.Equal(text[1..], match.RemainingText());
        }

        [Theory]
        [InlineData('x', "samsung")]
        [InlineData(' ', "digital")]
        public void FirstCharacterMatches_FalseCases(char letter, string text)
        {
            var x = new Character(letter);
            var match = x.Match(text);
            Assert.IsType<FailedMatch>(match);
            Assert.Equal(text, match.RemainingText());
        }

        [Theory]
        [InlineData('i', null)]
        [InlineData('e', "")]
        public void FirstCharacterMatches_IsNotNullOrEmpty_FalseCases(char letter, string text)
        {
            var x = new Character(letter);
            var match = x.Match(text);
            Assert.IsType<FailedMatch>(match);
            Assert.Equal(text, match.RemainingText());
        }
    }
}