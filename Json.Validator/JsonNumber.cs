using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            if (!HasContent(input))
            {
                return false;
            }

            var indexOfDot = input.IndexOf('.');
            var indexOfExponent = input.IndexOfAny("eE".ToCharArray());
            return IsInteger(Integer(input, indexOfDot, indexOfExponent));
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool HasValidDigits(string input)
        {
            if (input.Length == 1)
            {
                return true;
            }
            else if (input[0] == '0')
            {
                return false;
            }
            else if (input.StartsWith("-0"))
            {
                return true;
            }

            return true;
        }

        private static string Integer(string input, int indexOfDot, int indexOfExponent)
        {
            if (indexOfDot == -1 && indexOfExponent == -1)
            {
                return input;
            }
            else if (indexOfDot != -1)
            {
                return input[..^(indexOfDot + 1)];
            }
            else if (indexOfDot == -1 && indexOfExponent != -1)
            {
                return input[..^(indexOfExponent + 1)];
            }

            return string.Empty;
        }

        private static bool IsInteger(string integer)
        {
            return int.TryParse(integer, out _) && HasValidDigits(integer);
        }
    }
}
