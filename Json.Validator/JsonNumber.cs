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

            return input[..indexOfExponent];
        }

        private static bool IsInteger(string integer)
        {
            if (integer.Length > 1 && integer[0] == '0')
            {
                return false;
            }

            return integer.StartsWith('-') ? HasValidDigits(integer[1..]) : HasValidDigits(integer);
        }

        private static string Fraction(string input, int indexOfDot, int indexOfExponent)
        {
            if (indexOfDot != -1 && indexOfExponent == -1)
            {
                return input[indexOfDot..];
            }
            else if (indexOfDot != -1 && indexOfExponent != -1)
            {
                return input[indexOfDot..indexOfExponent];
            }

            return string.Empty;
        }

        private static bool IsFraction(string fraction)
        {
            if (fraction.EndsWith('.'))
            {
                return false;
            }

            return fraction == string.Empty || HasValidDigits(fraction[1..]);
        }

        private static string Exponent(string input, int indexOfExponent)
        {
            return indexOfExponent == -1 ? string.Empty : input[indexOfExponent..];
        }

        private static bool IsExponent(string exponent)
        {
            const byte indexOfFirstDigit = 2;
            if (exponent == string.Empty)
            {
                return true;
            }

            if (exponent.Length == 1 || exponent.EndsWith('-') || exponent.EndsWith('+'))
            {
                return false;
            }

            return exponent[1] == '+' || exponent[1] == '-' ?
                   HasValidDigits(exponent[indexOfFirstDigit..]) : HasValidDigits(exponent[1..]);
        }
    }
}