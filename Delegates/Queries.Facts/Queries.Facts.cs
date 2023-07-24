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
            string word = "banana";
            var result = word.FindFirstUniqueChar();
            Assert.Equal('b', result);
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

        [Fact]
        public void Palindromes_GetValidResults()
        {
            string word = "aabaac";
            var result = word.Palindromes();
            Assert.True(result.Contains("aabaa"));
            Assert.True(result.Contains("aba"));
            Assert.True(result.Contains("aa"));
            Assert.True(result.Contains("a"));
            Assert.True(result.Contains("b"));
            Assert.Equal(10, result.Length);
        }

        [Fact]
        public void SubbarraySum_ReturnsValidSubsets()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5 };
            var result = numbers.SubarraySum(10);
            var list = new List<List<int>>()
            {
                new List<int> { 1 },
                new List<int> { 1, 2 },
                new List<int> { 1, 2, 3 },
                new List<int> { 1, 2, 3, 4 },
                new List<int> { 2 },
                new List<int> { 2, 3 },
                new List<int> { 2, 3, 4 },
                new List<int> { 3 },
                new List<int> { 3, 4 },
                new List<int> { 4 },
                new List<int> { 4, 5 },
                new List<int> { 5 }
            };
            Assert.Equal(list, result);
        }
    }
}