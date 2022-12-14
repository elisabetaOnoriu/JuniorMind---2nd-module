namespace Validator.OneOrMoreFacts
{
    public class TestProgram
    {
        [Fact]
        public void TextStartsWithTheGivenCharacter_ConsumesWholeText()
        {
            var text = "123";
            var pattern = new OneOrMore(new Range('1', '9'));
            var match = pattern.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[3..], match.RemainingText());
        }

        [Fact]
        public void TextStartsWithTheGivenCharacter_TrueCases()
        {
            var text = "1a";
            var pattern = new OneOrMore(new Range('1', '9'));
            var match = pattern.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[1..], match.RemainingText());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("bc")]

        public void TextDoesNotStartWithTheGivenCharacter_FalseCases(string text)
        {
            var pattern = new OneOrMore(new Range('1', '9'));
            var match = pattern.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }
    }
}
