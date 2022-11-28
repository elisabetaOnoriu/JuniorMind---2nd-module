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
            return IsInteger(Integer(input, indexOfDot, indexOfExponent))
                && IsFraction(Fraction(input, indexOfDot, indexOfExponent))
                && IsExponent(Exponent(input, indexOfDot, indexOfExponent));
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool HasValidDigits(string input)
        {
            return int.TryParse(input, out _);
        }

        private static string Integer(string input, int indexOfDot, int indexOfExponent)
        {
            if (indexOfDot == -1 && indexOfExponent == -1)
            {
                return input;
            }
            else if (indexOfDot != -1)
            {
                return input[..^(input.Length - indexOfDot)];
            }
            else if (indexOfDot == -1 && indexOfExponent != -1)
            {
                return input[..^(input.Length - indexOfExponent)];
            }

            return string.Empty;
        }

        private static bool IsInteger(string integer)
        {
            if (integer.Length > 1 && integer[0] == '0')
            {
                return false;
            }

            return HasValidDigits(integer);
        }

        private static string Fraction(string input, int indexOfDot, int indexOfExponent)
        {
            if (indexOfDot != -1 && indexOfExponent == -1)
            {
                return input[indexOfDot..];
            }
            else if (indexOfDot != -1 && indexOfExponent != -1)
            {
                return input[indexOfDot..^(input.Length - indexOfExponent)];
            }
            else
            {
                return string.Empty;
            }
        }

        private static bool IsFraction(string fraction)
        {
            if (fraction == string.Empty)
            {
                return true;
            }

            fraction = fraction[1..];
            return HasValidDigits(fraction);
        }

        private static string Exponent(string input, int indexOfDot, int indexOfExponent)
        {
            return indexOfExponent == -1 ? string.Empty : input[indexOfExponent..];
        }

        private static bool IsExponent(string exponent)
        {
            return exponent == string.Empty || HasValidDigits(exponent[1..]);
        }
    }
}
