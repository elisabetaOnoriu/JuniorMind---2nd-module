using System.Linq;
using static System.Collections.Generic.CollectionExtensions;
namespace Queries
{
    public static class Queries
    {
        public static (int, int) CountVowelsAndConsonant(this string input)
        {
            return input.Where(c => char.IsLetter(c)).Aggregate((vowels: 0, consonants: 0), (counts, c) =>
            {
                return IsVowel(c)
                    ? (counts.vowels + 1, counts.consonants)
                    : (counts.vowels, counts.consonants + 1);
            });
        }

        public static char FindFirstUniqueChar(this string input) => input.GroupBy(c => c)
                                                                          .First(c => c.Count() == 1).Key;

        public static int StringToInt(this string input) => input.Aggregate(0, (number, next) => char.IsDigit(next) ? 
                                                            number * 10 + (next - '0') : throw new FormatException());

        public static char GetsRepeatedTheMost(this string input) => input.GroupBy(c => c).MaxBy(g => g.Count()).Key;

        public static IEnumerable<string> Palindromes(this string input)
        {
            return Enumerable
                .Range(1, input.Length)
                .SelectMany(length => Enumerable.Range(0, input.Length - length + 1)
                                                .Select(a => input.Substring(a, length)))
                                                .Where(b => b.SequenceEqual(b.Reverse()));
        }

        public static IEnumerable<IEnumerable<int>> SubarraySum(this int[] array, int k)
        {
            return Enumerable
               .Range(0, array.Length)
               .SelectMany(i => Enumerable.Range(i, array.Length - i),
               (i, j) => array[i..(j + 1)])
               .Where(subarray => subarray.Sum() <= k);
        }

        public static IEnumerable<IEnumerable<int>> OperationCombinations(this int n, int k)
        {
            IEnumerable<List<int>> seed = new[] { new List<int>() };
            return Enumerable
                .Range(1, n)
                .Aggregate(seed, (a, i) => a.SelectMany(s => new List<int>[]
                {
                    new List<int>(s) { i },
                    new List<int>(s) { -i },
                }))
                .Where(combination => combination.Sum() == k);
        }

        public static IEnumerable<IEnumerable<int>> GetTriplets(this int[] array)
        {
            return array
            .SelectMany((a, i) =>
                array.Skip(i + 1).SelectMany((b, j) =>
                array.Skip(i + j + 2).Where(c => Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2))
                     .Select(c => new[] { a, b, c })));          
        }

        public static IEnumerable<ProductFeature> AnyFeature(this IEnumerable<ProductFeature> products, IEnumerable<Feature> features)
        {
           return products.Where(product => product.Features.IntersectBy(features.Select(f => f.Id), f => f.Id).Any());
        }

        public static IEnumerable<ProductFeature> AllFeatures(this IEnumerable<ProductFeature> products, IEnumerable<Feature> features)
        {
            return products.Where(product => product.Features.UnionBy(features, f => f.Id).Count() == product.Features.Count);
        }

        public static IEnumerable<ProductFeature> NoneFeatures(this IEnumerable<ProductFeature> products, IEnumerable<Feature> features)
        {
            return products.Except(products.AnyFeature(features));
        }

        public static IEnumerable<Product> MergeGroups(this IEnumerable<Product> first, IEnumerable<Product> second)
        {
            return first.Concat(second)
                        .GroupBy(product => product.Name)
                        .Select(group => new Product(group.Key, group.Sum(p => p.Quantity)));
        }

        public static IEnumerable<TestResults> MaxTestResults(this IEnumerable<TestResults> testResults)
        {
            return testResults.GroupBy(result => result.FamilyId).Select(group => group.MaxBy(test => test.Score));
        }

        public static (IEnumerable<(string word, int count)>, int[] topThree) TopWords(this string text)
        {
            var wordCounts = text.ToLower()
                .Split(new char[] { ' ', '.', ',', ':', ';', '!', '?', '(', ')' }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(word => word)
                .OrderByDescending(group => group.Count())
                .Select(group => (word: group.Key, count: group.Count()));

            var topThree = wordCounts
                .Select(wordCount => wordCount.count)
                .Take(3)
                .ToArray();

            return (wordCounts, topThree);
        }

        public static bool SudokuValidator(this int[][] sudoku)
        {
            return Enumerable.Range(0, 9).All(i =>
                sudoku.ValidateRow(i) &&
                sudoku.ValidateColumn(i) &&
                sudoku.ValidateSubGrid(i));
        }

        public static double EvaluatePostfixExpression(this IEnumerable<string> expression)
        {
            var operators = ExecuteOperations();
            return expression.Aggregate(new List<int>(), (items, item) =>
            {
                if (int.TryParse(item, out int operand))
                {
                    items.Add(operand);
                }
                else
                {
                    int[] lastTwo = items.TakeLast(2).ToArray();
                    items.Add(operators[item[0]](lastTwo[0], lastTwo[1]));
                }
                return items;
            }).Last();
        }

        static private bool IsVowel(this char c)
        {
            var vowels = new HashSet<char>("aeiou");
            return vowels.Contains(char.ToLower(c));
        }

        static Dictionary<char, Func<int, int, int>> ExecuteOperations()
        {
            return new Dictionary<char, Func<int, int, int>>
            {
                {'+', (a, b) => a + b},
                {'-', (a, b) => a - b},
                {'*', (a, b) => a * b},
                {'/', (a, b) => a / b},
            };
        }
        
        static private bool ValidateRow(this int[][] sudoku, int row)
        {
            return sudoku[row]
                .GroupBy(num => num)
                .All(group => group.Count() == 1 && group.Key >= 1 && group.Key <= 9);
        }

        static private bool ValidateColumn(this int[][] sudoku, int column)
        {
            return Enumerable.Range(0, 9)
                    .Select(j => sudoku[j][column])
                    .GroupBy(num => num)
                    .All(group => group.Count() == 1);
        }

        static private bool ValidateSubGrid(this int[][] sudoku, int index)
        {
            return Enumerable.Range(index / 3 * 3, 3)
                    .SelectMany(row => Enumerable.Range(index % 3 * 3, 3),
                    (row, column) => sudoku[row][column])
                    .GroupBy(num => num)
                    .All(group => group.Count() == 1);
        }
    }
}