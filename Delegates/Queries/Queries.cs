namespace Queries
{
    public static class Queries
    {
        public static (int, int) CountVowelsAndConsonant(this string input)
        {
            int consonantsCount = input.Where(c => IsConsonant(c)).Count();
            int vowelsCount = input.Where(c => IsVowel(c)).Count();
            return (vowelsCount, consonantsCount);
        }

        public static char FindFirstUniqueChar(this string input) => input.Where(a => input.IndexOf(a) == input.LastIndexOf(a)).First();

        public static int StringToInt(this string input) => input.Aggregate(0, (number, next) => number * 10 + (next - '0'));

        public static char GetsRepeatedTheMost(this string input) => input.GroupBy(c => c).MaxBy(g => g.Count()).Key;

        public static string[] Palindromes(this string input)
        {
            return Enumerable
                .Range(1, input.Length)
                .SelectMany(length => Enumerable.Range(0, input.Length - length + 1)
                                                .Select(a => input.Substring(a, length)))
                                                .Where(b => b.SequenceEqual(b.Reverse()))
                                                .ToArray();
        }

        public static List<List<int>> SubarraySum(int[] array, int k)
        {
            return Enumerable
                .Range(0, array.Length)
                .SelectMany(i => Enumerable.Range(i, array.Length - i)
                    .Aggregate(new List<List<int>>(), (acc, j) =>
                    {
                        var subarray = array.Skip(i).Take(j - i + 1).ToList();
                        if (subarray.Sum() <= k)
                        {
                            acc.Add(subarray);
                        }

                        return acc;
                    }))
                .ToList();
        }

        static private bool IsConsonant(char c)
        {
            var consonants = new HashSet<char>() { 'b', 'c', 'd', 'f', 'g', 'h', 'j',
                'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z'};
            return consonants.Contains(char.ToLower(c));
        }

        static private bool IsVowel(char c)
        {
            var vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
            return vowels.Contains(char.ToLower(c));
        }
    }
}