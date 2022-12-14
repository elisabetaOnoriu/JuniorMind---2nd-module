namespace Validator.ListFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData("1,2,3")]
        [InlineData("1,2,3,")]
        public void TextStartsWithTheGivenElementSeparatorText(string text)
        {
            var pattern = new List(new Range('1', '9'), new Character(','));
            var match = pattern.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[(text.IndexOf('3') + 1)..], match.RemainingText());
        }

        [Fact]
        public void TextStartsWithTheGivenElementSeparatorGroup_ContainsThemMultiplied_ConsumesWholeText()
        {
            string text = "1; 22  ;\n 333 \t; 22";
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), whitespace);
            var list = new List(digits, separator);
            var match = list.Match(text);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void TextStartsWithTheGivenElementSeparatorGroup_ContainsThemMultiplied()
        {
            string text = "1 \n;";
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), whitespace);
            var list = new List(digits, separator);
            var match = list.Match(text);
            Assert.True(match.Success());
            Assert.Equal(" \n;", match.RemainingText());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(",2bc")]

        public void TextDoesNotStartWithTheGivenCharacter_TrueCases(string text)
        {
            var pattern = new List(new Range('1', '9'), new Character(','));
            var match = pattern.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text, match.RemainingText());
        }
    }
}
