global using Xunit;

namespace Validator.ManyFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData("iiiijk", 'i')]
        [InlineData("ijk", 'i')]

        public void StringContainsThePatternAtLeastOnce_TrueCases(string text, char a)
        {
            var pattern = new Many(new Character(a));
            var match = pattern.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[text.IndexOf('j')..], match.RemainingText());
        }

        [Theory]
        [InlineData("", 'i')]
        [InlineData(null, 'i')]
        [InlineData("abc", 'd')]

        public void StringDoesNotContainThePattern_TrueCases(string text, char a)
        {
            var pattern = new Many(new Character(a));
            var match = pattern.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text, match.RemainingText());
        }
    }
}
