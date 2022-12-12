global using Xunit;

namespace Validator.SequenceFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData("abcd", 'a', 'b')]
        [InlineData("1234", '1', '2')]
        public void SequenceIsValid_TrueCases(string text, char a, char b)
        {
            var sequence = new Sequence(new Character(a), new Character(b));
            var match = sequence.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[2..], match.RemainingText());
        }

        [Theory]
        [InlineData("abcd", 'a', 'b', 'c')]
        [InlineData("1234", '1', '2', '3')]
        public void SequenceIsValid_SequenceOfSequences_TrueCases(string text, char a, char b, char c)
        {
            var sequence = new Sequence(new Character(a), new Character(b));
            var sequenceOfSequences = new Sequence(sequence, new Character(c));
            var match = sequenceOfSequences.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[3..], match.RemainingText());
        }

        [Theory]
        [InlineData("u1234")]
        [InlineData("uB005 ab")]
        public void SequenceIsValid_HexSequenceOfSequences_TrueCases(string text)
        {
            var hex = new Choice(new Range('0', '9'),
                                 new Range('a', 'f'),
                                 new Range('A', 'F'));
            var hexSeq = new Sequence(new Character('u'),
                                      new Sequence(
                                          hex,
                                          hex,
                                          hex,
                                          hex));
            var match = hexSeq.Match(text);
            Assert.True(match.Success());
            Assert.Equal(text[5..], match.RemainingText());
        }

        [Theory]
        [InlineData("51234")]
        [InlineData("uBV05 ab")]
        [InlineData("uB10 ")]
        public void SequenceIsValid_HexSequenceOfSequencesFalseCases(string text)
        {
            var hex = new Choice(new Range('0', '9'),
                                 new Range('a', 'f'),
                                 new Range('A', 'F'));
            var hexSeq = new Sequence(new Character('u'),
                                      new Sequence(
                                          hex,
                                          hex,
                                          hex,
                                          hex));
            var match = hexSeq.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }

        [Theory]
        [InlineData("ab", 'a', 'b', 'c')]
        [InlineData("xyz", '1', '2', '6')]
        public void SequenceIsValid_FalseCases(string text, char a, char b, char c)
        {
            var sequence = new Sequence(new Character(a), new Character(b),
                                        new Character(c));
            var match = sequence.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }

        [Theory]
        [InlineData("", 'a', 'b', 'c')]
        [InlineData(null, '1', '2', '6')]
        public void SequenceIsValid_IsNotNullOrEmpty_FalseCases(string text, char a, char b, char c)
        {
            var sequence = new Sequence(new Character(a), new Character(b),
                                        new Character(c));
            var match = sequence.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }
    }
}
