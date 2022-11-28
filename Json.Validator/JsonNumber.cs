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
                && IsExponent(Exponent(input, indexOfExponent));
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool HasValidDigits(string input)
        {
            

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return input.Length > 0;
        }

        private static string Integer(string input, int indexOfDot, int indexOfExponent)
        {
            if (indexOfDot != -1)
            {
                return input[..indexOfDot];
            }

            if (indexOfExponent != -1)
            {
                return input[..indexOfExponent];
            }

            return input;
        }

        private static bool IsInteger(string integer)
        {
            if (integer.Length > 1 && integer[0] == '0')
            {
                return false;
            }

            if (integer.StartsWith('-'))
            {
                integer = integer[1..];
            }

            return HasValidDigits(integer);
        }

        private static string Fraction(string input, int indexOfDot, int indexOfExponent)
        {
            if (indexOfDot != -1 && indexOfExponent == -1)
            {
                return input[indexOfDot..];
            }

            if (indexOfDot != -1 && indexOfExponent != -1)
            {
                return input[indexOfDot..indexOfExponent];
            }

            return string.Empty;
        }

        private static bool IsFraction(string fraction)
        {
            return fraction == string.Empty || HasValidDigits(fraction[1..]);
        }

        private static string Exponent(string input, int indexOfExponent)
        {
            if (indexOfExponent == -1)
            {
                return string.Empty;
            }

            return input[indexOfExponent..];
        }

        private static bool IsExponent(string exponent)
        {
            if (exponent == string.Empty)
            {
                return true;
            }

            if (exponent.Contains('-') || exponent.Contains('+'))
            {
                exponent = exponent[1..];
            }

            return HasValidDigits(exponent[1..]);
        }
    }
}