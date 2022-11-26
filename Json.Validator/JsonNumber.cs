using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            var indexOfDot = input.IndexOf('.');
            var indexOfExponent = input.IndexOfAny("eE".ToCharArray());
            return IsInteger(Integer(input, indexOfDot, indexOfExponent));
        }

        private static string Integer(string input, int indexOfDot, int indexOfExponent)
        {
            return indexOfDot == -1 && indexOfExponent == -1 ? input : string.Empty;
        }

        private static bool IsInteger(string integer)
        {
            return int.TryParse(integer, out var result);
        }
    }
}
