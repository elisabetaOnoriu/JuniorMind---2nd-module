namespace Validator.Facts
{
    public class TestProgram
    {
        [Fact]
        public void JsonStringIsValid()
        {
            var jsonString = new String();
            var match = jsonString.Match(Quoted("abc"));
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Theory]
        [InlineData("abc\"")]
        [InlineData("\"abc")]
        public void JsonStringIsValid_IsDoubleQuoted_FalseCases(string text)
        {
            var jsonString = new String();
            var match = jsonString.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void JsonStringIsValid_IsNotNullOrEmpty_FalseCases(string text)
        {
            var jsonString = new String();
            var match = jsonString.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }

        [Fact]
        public void JsonStringIsValid_EmptyQuotes()
        {
            var jsonString = new String();
            var match = jsonString.Match(Quoted(""));
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void JsonStringIsValid_DoesNotContainControlCharacter()
        {
            var jsonString = new String();
            var match = jsonString.Match(Quoted("a\nb\rc"));
            Assert.False(match.Success());
            Assert.Equal(Quoted("a\nb\rc"), match.RemainingText());
        }

        [Theory]
        [InlineData(@"\""a\"" b")]
        [InlineData(@"a \/ b")]
        [InlineData(@"a \r b")]
        public void JsonStringIsValid_CanContainEscapedSequences_TrueCases(string text)
        {
            var jsonString = new String();
            var match = jsonString.Match(Quoted(text));
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void JsonStringIsValid_CanContainEscapedUnicodeCharacters_TrueCases()
        {
            var jsonString = new String();
            string text = @"a \u26Be b";
            var match = jsonString.Match(Quoted(text));
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void JsonStringIsValid_CanContainAnyMultipleEscapeSequences_TrueCases()
        {
            var jsonString = new String();
            string text = @"\\\u1212\n\t\r\\\b";
            var match = jsonString.Match(Quoted(text));
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void JsonStringIsValid_DoesNotContainUnrecognizedEscapeCharacters_FalseCases()
        {
            var jsonString = new String();
            string text = @"a\x";
            var match = jsonString.Match(Quoted(text));
            Assert.False(match.Success());
            Assert.Equal(Quoted(text), match.RemainingText());
        }

        [Theory]
        [InlineData(@"a\u12qw")]
        [InlineData(@"a\u123 5*")]
        [InlineData(@"a\u1234 a/u4567 i\u12io")]
        public void JsonStringIsValid_ContainsValidHexDigits_FalseCases(string text)
        {
            var jsonString = new String();
            var match = jsonString.Match(Quoted(text));
            Assert.False(match.Success());
            Assert.Equal(Quoted(text), match.RemainingText());
        }

        public static string Quoted(string text)
            => $"\"{text}\"";
    }
}
