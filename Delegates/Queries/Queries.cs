namespace Queries
{
    public static class Queries
    {
        public static (int, int) CountVowelsAndConsonant(this string input)
        {
            return input.Aggregate((0, 0), (counts, c) =>
            {
                if (char.IsLetter(c))
                {
                    return IsVowel(c)
                        ? (counts.Item1 + 1, counts.Item2)
                        : (counts.Item1, counts.Item2 + 1);
                }
                return counts;
            });
        }

        public static char FindFirstUniqueChar(this string input) => input.Distinct().First();

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
                .SelectMany(i => Enumerable.Range(i, array.Length - i)
                .Where(j => array.Skip(i).Take(j - i + 1).Sum() <= k)
                .Select(j => array.Skip(i).Take(j - i + 1))
            );
        }

        static private bool IsVowel(this char c)
        {
            var vowels = new HashSet<char>("aeiou");
            return vowels.Contains(char.ToLower(c));
        }
    }
}