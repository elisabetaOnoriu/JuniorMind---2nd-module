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
        [InlineData("012", 'a', 'f', '0', '1', '9', 'A', 'F')]
        [InlineData("A8", 'a', 'f', '0', '1', '9', 'A', 'F')]
        [InlineData("F12", 'a', 'f', '0', '1', '9', 'A', 'F')]
        public void HexMatches_TrueCases(string text, char LetterStart, char letterEnd,
                                         char digitBase, char digitStart, char digitEnd,
                                         char upperLetterStart, char upperLetterEnd)
        {
            var digit = new Choice(new Character(digitBase), new Range(digitStart, digitEnd));
            var hex = new Choice(digit,
                                 new Choice(
                                     new Range(LetterStart, letterEnd), new Range(upperLetterStart, upperLetterEnd)));
            Assert.True(hex.Match(text));
        }

        [Theory]
        [InlineData("P12", 'a', 'f', '0', '1', '9', 'A', 'F')]
        [InlineData("n8", 'a', 'f', '0', '1', '9', 'A', 'F')]
        [InlineData("", 'a', 'f', '0', '1', '9', 'A', 'F')]
        [InlineData(null, 'a', 'f', '0', '1', '9', 'A', 'F')]
        public void HexMatches_FalseCases(string text, char LetterStart, char letterEnd,
                                 char digitBase, char digitStart, char digitEnd,
                                 char upperLetterStart, char upperLetterEnd)
        {
            var digit = new Choice(new Character(digitBase), new Range(digitStart, digitEnd));
            var hex = new Choice(digit,
                                 new Choice(
                                     new Range(LetterStart, letterEnd), new Range(upperLetterStart, upperLetterEnd)));
            Assert.False(hex.Match(text));
        }
    }
}