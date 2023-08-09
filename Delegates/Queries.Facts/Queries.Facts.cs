global using Xunit;
using Queries;
namespace QueriesFacts
{
    public class UnitTest1
    {
        List<Feature> features = new()
        {
            new Feature(1),
            new Feature(2),
            new Feature(3),
            new Feature(4),
            new Feature(5),
        };

        ProductFeature[] products = new[]
        {
            new ProductFeature("cd", 1, 2, 3, 4, 5),
            new ProductFeature("dvd", 1),
            new ProductFeature("laptop", 1, 2),
            new ProductFeature("pc", 0),
        };

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
        public void StringToInt_ParsesProperly()
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
            string[] palindromes = new[]
            {
                "a",
                "a",
                "b",
                "a",
                "a",
                "c",
                "aa",
                "aa",
                "aba",
                "aabaa",

            };
            Assert.Equal(palindromes, result);
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

        [Fact]
        public void OperationCombinations_ReturnsValidCombinations()
        {
            var combinations = 3.OperationCombinations(0);
            List<List<int>> expected = new()
            {
                new List<int> { 1, 2, -3 },
                new List<int> { -1, -2, 3 }
            };
            Assert.Equal(expected, combinations);
        }

        [Fact]
        public void GetTriplets_ReturnsValidTriplets()
        {
            var triplets = new int[] { 1, 2, 3, 4, 5 }.GetTriplets();
            List<List<int>> expected = new()
            {
                new List<int> { 3, 4, 5 },
            };
            Assert.Equal(expected, triplets);
        }

        [Fact]
        public void AnyFeature_ReturnsValidProducts()
        {
            ProductFeature[] productsWithFeatures = new[]
            {
                new ProductFeature("cd", 1, 2, 3, 4, 5),
                new ProductFeature("dvd", 1),
                new ProductFeature("laptop", 1, 2)
            };

            var result = products.AnyFeature(features).ToArray();
            Assert.True(productsWithFeatures[0].Features.Count == result[0].Features.Count);
            Assert.True(productsWithFeatures[1].Features.Count == result[1].Features.Count);
            Assert.True(productsWithFeatures[2].Features.Count == result[2].Features.Count);
            Assert.True(productsWithFeatures.Length == result.Length);        
        }

        [Fact]
        public void AllFeatures_ReturnsValidProducts()
        {
            ProductFeature[] productsWithFeatures = new[]
            {
                new ProductFeature("cd", 1, 2, 3, 4, 5),
            };

            var result = products.AllFeatures(features).ToArray();
            Assert.True(productsWithFeatures[0].Features.Count == result[0].Features.Count);
            Assert.True(productsWithFeatures.Length == result.Length);
        }

        [Fact]
        public void NoneFeatures_ReturnsValidProducts()
        {
            ProductFeature[] productsWithFeatures = new[]
            {
                new ProductFeature("pc", 0)
            };

            var result = products.NoneFeatures(features).ToArray();
            Assert.True(productsWithFeatures[0].Features.Count == result[0].Features.Count);
            Assert.True(productsWithFeatures.Length == result.Length);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void MergeGroups_ReturnesMergedCollections(int index)
        {
            Product[] products1 = new[]
            {
                new Product("mango", 20),
                new Product("apple", 20),
                new Product("grapes", 20),
                new Product("strawberies", 20)
            };

            Product[] products2 = new[]
            {
                new Product("mango", 15),
                new Product("papaya", 20),
                new Product("grapes", 15),
                new Product("banana", 20)
            };

            Product[] result = new[]
            {
                new Product("mango", 35),
                new Product("apple", 20),
                new Product("grapes", 35),
                new Product("strawberies", 20),
                new Product("papaya", 20),
                new Product("banana", 20)
            };

            var merged = products1.MergeGroups(products2).ToArray();
            Assert.Equal(result.Length, merged.Length);
            Assert.Equal(result[index].Quantity, merged[index].Quantity);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void MaxTestResults(int index)
        {
            List<TestResults> testResults = new()
            {
                new TestResults(1, "fam100", 80),
                new TestResults(2, "fam101", 75),
                new TestResults(3, "fam100", 90),
                new TestResults(4, "fam110", 80),
                new TestResults(5, "fam111", 100),
                new TestResults(6, "fam111", 85)
            };

            List<TestResults> expected = new()
            {
                new TestResults(2, "fam101", 75),
                new TestResults(3, "fam100", 90),
                new TestResults(4, "fam110", 80),
                new TestResults(5, "fam111", 100),
            };

            var result = testResults.MaxTestResults().ToList();
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected[0].Id, result[0].Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        public static void TopWords_ReturnsTopMostUsedWords(int index)
        {
            string text = "Ana is busy trying to solve a bug. The bug is undetectable, Ana says. Ana solved the issue";
            List<(string, int)> expected = new()
            {
                ("ana", 3),
                ("is", 2),
                ("bug", 2),
                ("the", 2),
                ("busy", 1),
                ("trying", 1),
                ("to", 1),
                ("solve", 1),
                ("a", 1),               
                ("undetectable", 1),
                ("says", 1),
                ("solved", 1),
                ("issue", 1)
            };
            int[] topThree = new[] { 3, 2, 2 };
            var topWords = text.TopWords();
            Assert.Equal(expected[index], topWords.Item1.ToArray()[index]);
            Assert.Equal(topThree, topWords.Item2);
        }

        [Fact]
        public static void SudokuValidator_IsValid()
        {
            int[][] sudoku = new[]
            {
                new int[] { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
                new int[] { 6, 7, 2, 1, 9, 5, 3, 4, 8 },
                new int[] { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
                new int[] { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
                new int[] { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
                new int[] { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
                new int[] { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
                new int[] { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
                new int[] { 3, 4, 5, 2, 8, 6, 1, 7, 9 }
            };

            Assert.True(sudoku.SudokuValidator());
        }

        [Fact]
        public static void SudokuValidator_IsNotValid()
        {
            int[][] sudoku = new[]
            {
                new int[] { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
                new int[] { 2, 7, 2, 1, 9, 5, 3, 4, 8 },
                new int[] { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
                new int[] { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
                new int[] { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
                new int[] { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
                new int[] { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
                new int[] { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
                new int[] { 3, 4, 5, 2, 8, 6, 1, 7, 9 }
            };

            Assert.False(sudoku.SudokuValidator());
        }

        [Fact]
        public static void SudokuValidator_IsNotValid_Contains0()
        {
            int[][] sudoku = new[]
            {
                new int[] { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
                new int[] { 0, 7, 2, 1, 9, 5, 3, 4, 8 },
                new int[] { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
                new int[] { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
                new int[] { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
                new int[] { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
                new int[] { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
                new int[] { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
                new int[] { 3, 4, 5, 2, 8, 6, 1, 7, 9 }
            };

            Assert.False(sudoku.SudokuValidator());
        }

        [Fact]
        public static void EvaluatePostFixNotation()
        {
            string[] splittedExpression = new[] {"10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+"};
            Assert.Equal(22, splittedExpression.EvaluatePostfixExpression());
        }
    }
}