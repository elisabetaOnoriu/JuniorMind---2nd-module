namespace Queries
{
    public class Queries
    {
        public (int, int) CountVowelsAndConsonant(string input)
        {
            int consonantsCount = input.Where(c => IsConsonant(c)).Count();
            int vowelsCount = input.Where(c => IsVowel(c)).Count();
            return (vowelsCount, consonantsCount);
        }

        public char FindFirstUniqueChar(string input) => input.Where(a => input.IndexOf(a) == input.LastIndexOf(a)).First();

        public int StringToInt(string input) => input.Aggregate(0, (number, next) => number * 10 + (next - '0'));

        public char GetsRepeatedTheMost(string input) => input.GroupBy(c => c).MaxBy(g => g.Count()).Key;

        public string[] Palindromes(string input)
        {
            return Enumerable
                .Range(1, input.Length)
                .SelectMany(length => Enumerable.Range(0, input.Length - length + 1)
                                                .Select(a => input.Substring(a, length)))
                                                .Where(b => b.SequenceEqual(b.Reverse()))
                                                .ToArray();
        }

        public List<List<int>> SubarraySum(int[] array, int k)
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

        private bool IsConsonant(char c)
        {
            var consonants = new HashSet<char>() { 'b', 'c', 'd', 'f', 'g', 'h', 'j',
                'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z'};
            return consonants.Contains(char.ToLower(c));
        }

        private bool IsVowel(char c)
        {
            var vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
            return vowels.Contains(char.ToLower(c));
        }
    }
}