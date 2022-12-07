global using Xunit;

namespace Validator.CharacterFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData('x', "xiaomi")]
        [InlineData('m', "mobile")]
        [InlineData('v', "valid")]
        public void FirstCharacterMatches_TrueCases(char letter, string text)
        {
            var x = new Character(letter);
            Assert.True(x.Match(text));
        }

        [Theory]
        [InlineData('x', "samsung")]
        [InlineData('1', "voila")]
        [InlineData(' ', "digital")]
        [InlineData('i', null)]
        [InlineData('e', "")]
        public void FirstCharacterMatches_FalseCases(char letter, string text)
        {
            var x = new Character(letter);
            Assert.False(x.Match(text));
        }
    }
}