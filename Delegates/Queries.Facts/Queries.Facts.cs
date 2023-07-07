global using Xunit;
using Queries;
namespace QueriesFacts
{
    public class UnitTest1
    {
        [Fact]
        public void CountVowelsAndConsonants_ReturnsCountOfBoth()
        {
            string word = "Agapornis";
            var result = word.CountVowelsAndConsonant();
            Assert.Equal((4, 5), result);
        }

        [Fact]
        public void FindFirstUniqueChar()
        {
            string word = "Agapornis";
            var result = word.FindFirstUniqueChar();
            Assert.Equal('A', result);
        }

        [Fact]
        public void StringToInt_ParsesProperly ()
        {
            string word = "12904";
            var result = word.StringToInt();
            Assert.Equal(12904, result);
        }

        [Fact]
        public void GetsRepeatedTheMost_ReturnsAChar()
        {
            string word = "Maria";
            var result = word.GetsRepeatedTheMost();
            Assert.Equal('a', result);
        }
    }
}