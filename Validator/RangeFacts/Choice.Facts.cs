global using Xunit;

namespace Validator.ChoiceFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData("abc", 'b', 'f', 'a')]
        [InlineData("3bc", '1', '5', 'a')]
        public void StringMatches_TrueCases(string text, char start, char end, char letter)
        {
            var result = new Choice(new Range(start, end), new Character(letter));
            Assert.True(result.Match(text));
        }

        [Theory]
        [InlineData("gbc", 'b', 'f', 'a')]
        [InlineData("ybc", '1', '5', 'a')]
        public void StringMatches_FalseCases(string text, char start, char end, char letter)
        {
            var result = new Choice(new Range(start, end), new Character(letter));
            Assert.False(result.Match(text));
        }

        [Theory]
        [InlineData("012")]
        [InlineData("88")]
        public void HexMatches_NumberCases_TrueCases(string text)
        {
            var digit = new Choice(new Character('0'), new Range('1', '9'));
            var hex = new Choice(digit,
                                 new Choice(
                                     new Range('a', 'f'), new Range('A', 'F')));
            Assert.True(hex.Match(text));
        }

        [Theory]
        [InlineData("A8")]
        [InlineData("F12")]
        public void HexMatches_UpperLetterCases_TrueCases(string text)
        {
            var digit = new Choice(new Character('0'), new Range('1', '9'));
            var hex = new Choice(digit,
                                 new Choice(
                                     new Range('a', 'f'), new Range('A', 'F')));
            Assert.True(hex.Match(text));
        }

        [Theory]
        [InlineData("b8")]
        [InlineData("d12")]
        public void HexMatches_LowerLetterCases_TrueCases(string text)
        {
            var digit = new Choice(new Character('0'), new Range('1', '9'));
            var hex = new Choice(digit,
                                 new Choice(
                                     new Range('a', 'f'), new Range('A', 'F')));
            Assert.True(hex.Match(text));
        }

        [Theory]
        [InlineData("P12")]
        [InlineData("n8")]
        public void HexMatches_FalseCases(string text)
        {
            var digit = new Choice(new Character('0'), new Range('1', '9'));
            var hex = new Choice(digit,
                                 new Choice(
                                     new Range('a', 'f'), new Range('A', 'F')));
            Assert.False(hex.Match(text));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void HexMatches_IsNotNullOrEmpty_FalseCases(string text)
        {
            var digit = new Choice(new Character('0'), new Range('1', '9'));
            var hex = new Choice(digit,
                                 new Choice(
                                     new Range('a', 'f'), new Range('A', 'F')));
            Assert.False(hex.Match(text));
        }
    }
}