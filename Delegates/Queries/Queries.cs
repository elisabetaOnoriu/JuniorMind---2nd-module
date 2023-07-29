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
            IEnumerable<int[]> seed = new[] { Array.Empty<int>() };
            return
                Enumerable
                    .Range(1, n)
                    .Aggregate(seed, (a, i) => a.SelectMany(s => new[]
                    {
                     s.Append(i).ToArray(),
                     s.Append(-i).ToArray(),
                    }))
                    .Where(combination => combination.Sum() == k);
        }

        public static IEnumerable<IEnumerable<int>> GetTriplets(this int[] array)
        {
            return array
            .SelectMany((a, i) =>
                array.Skip(i + 1).SelectMany((b, j) =>
                    array.Skip(i + j + 2).Select(c => new[] { a, b, c })))
            .Where(triplet => Math.Pow(triplet[0], 2) + Math.Pow(triplet[1], 2) == Math.Pow(triplet[2], 2));
        }

        public static IEnumerable<ProductFeature> AnyFeature(this IEnumerable<ProductFeature> products, IEnumerable<Feature> features)
        {
            return products.Where(product => product.Features.Any(feature => features.Any(f => f.Id == feature.Id)));
        }

        public static IEnumerable<ProductFeature> AllFeatures(this IEnumerable<ProductFeature> products, IEnumerable<Feature> features)
        {
            return products.Where(product => features.All(feature => product.Features.Any(f => f.Id == feature.Id)));
        }

        public static IEnumerable<ProductFeature> NoneFeatures(this IEnumerable<ProductFeature> products, IEnumerable<Feature> features)
        {
            return products.Where(product => !product.Features.Any(f => features.Any(feat => feat.Id == f.Id)));
        }

        public static IEnumerable<Product> MergeGroups(this IEnumerable<Product> first, IEnumerable<Product> second)
        {
            return first.Concat(second)
                        .GroupBy(product => product.Name)
                        .Select(group => new Product( group.Key, group.Sum(p => p.Quantity)));
        }

        public static IEnumerable<TestResults> MaxTestResults(this IEnumerable<TestResults> testResults)
        {
            return testResults.GroupBy(result => result.FamilyId).Select(group => group.MaxBy(test => test.Score));
        }

        public static IEnumerable<string> TopWords(this string text)
        {
            return text.ToLower().Split(new char[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 .GroupBy(word => word).OrderByDescending(group => group.Count())
                                 .Select(group => group.Key);
        }

        public static bool SudokuValidator(this int[][] sudoku)
        {
            return Enumerable.Range(0, 9).All(i =>
                sudoku[i].GroupBy(num => num).All(group => group.Count() == 1 && group.Key >= 1 && group.Key <= 9) &&
                Enumerable.Range(0, 9)
                    .Select(j => sudoku[j][i])
                    .GroupBy(num => num)
                    .All(group => group.Count() == 1) &&
                Enumerable.Range(i / 3 * 3, 3)
                    .SelectMany(row => Enumerable.Range(i % 3 * 3, 3).Select(col => sudoku[row][col]))
                    .GroupBy(num => num)
                    .All(group => group.Count() == 1));
        }

        public static int EvaluatePostfixExpression(this IEnumerable<string> expression)
        {
            var operators = ExecuteOperations();
            return expression.Aggregate(new Stack<int>(), (stack, item) =>
            {
                if (int.TryParse(item, out int operand))
                {
                    stack.Push(operand);
                }
                else
                {
                    int item1 = stack.Pop();
                    int item2 = stack.Pop();
                    stack.Push(operators[item[0]](item2, item1));
                }
                return stack;
            }).Pop();
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
    }
}