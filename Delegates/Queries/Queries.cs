namespace Queries
{
    public static class Queries
    {
        public static (int, int) CountVowelsAndConsonant(this string input)
        {
            return input.Where(c => char.IsLetter(c)).Aggregate((vowels: 0, consonants: 0), (counts, c) =>
               IsVowel(c) 
                ? (counts.vowels + 1, counts.consonants)
                : (counts.vowels, counts.consonants + 1));
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
               .SelectMany(i => Enumerable.Range(i + 1, array.Length - i),
               (i, j) => array[i..j])
               .Where(subarray => subarray.Sum() <= k);
        }

        public static IEnumerable<IEnumerable<int>> OperationCombinations(this int n, int k)
        {
            IEnumerable<IEnumerable<int>> seed = new[] { new int[] { }  };
            return Enumerable
                .Range(1, n)
                .Aggregate(seed, (a, i) => a.SelectMany(s => new []
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
                array.Skip(i + j + 2).Where(c => IsPythagorean(a, b, c))
                     .Select(c => new[] { a, b, c })));          
        }

        public static IEnumerable<ProductFeature> AnyFeature(this IEnumerable<ProductFeature> products, IEnumerable<Feature> features)
        {
           return products.Where(product => product.Features.Intersect(features).Any());
        }

        public static IEnumerable<ProductFeature> AllFeatures(this IEnumerable<ProductFeature> products, IEnumerable<Feature> features)
        {
            return products.Where(product => product.Features.Union(features).Count() == product.Features.Count);
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

        public static IEnumerable<(string word, int count)> TopWords(this string text, int count = 10)
        {
            return text.ToLower()
                .Split(" .,:;!?()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(word => word)
                .OrderByDescending(group => group.Count())
                .Take(count)
                .Select(group => (word: group.Key, count: group.Count()));
        }

        public static bool SudokuValidator(this int[][] sudoku)
        {
            return Enumerable.Range(0, 9).All(i =>
            new int[][] { sudoku.Row(i), sudoku.Column(i), sudoku.SubGrid(i) }.All(items => items.Validate())); 
        }

        public static double EvaluatePostfixExpression(this IEnumerable<string> expression)
        {
            IEnumerable<double> items = new double[] { };
            return expression.Aggregate(items, (items, item) =>
                double.TryParse(item, out double operand)
               ? items.Append(operand)
               : items.SkipLast(2).Append(Calculate(items.TakeLast(2).ToArray(), item))).Last();
        }

        static private bool IsVowel(this char c)
        {
            var vowels = new HashSet<char>("aeiou");
            return vowels.Contains(char.ToLower(c));
        }

        static private bool IsPythagorean(int a, int b, int c)
        {
            return Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2) ||
                Math.Pow(b, 2) + Math.Pow(c, 2) == Math.Pow(a, 2) ||
                Math.Pow(a, 2) + Math.Pow(c, 2) == Math.Pow(b, 2);
        }

        static double Calculate(double[] elements, string operand)
        {
            return operand switch
            {
                "+" => elements[0] + elements[1],
                "-" => elements[0] - elements[1],
                "*" => elements[0] * elements[1],
                "/" => elements[0] / elements[1],
                _ => throw new Exception(),
            };
        }
        
        static private int[] Row(this int[][] sudoku, int row) =>sudoku[row].ToArray();

        static private int[] Column(this int[][] sudoku, int column)
        {
            return Enumerable.Range(0, 9).Select(j => sudoku[j][column]).ToArray();
        }

        static private int[] SubGrid(this int[][] sudoku, int index)
        {
            return Enumerable.Range(index / 3 * 3, 3)
                    .SelectMany(row => Enumerable.Range(index % 3 * 3, 3),
                    (row, column) => sudoku[row][column]).ToArray();
        }

        static private bool Validate(this int[] array)
        {
            return array.GroupBy(num => num).All(group => group.Count() == 1 && group.Key >= 1 && group.Key <= 9);
        }
    }
}