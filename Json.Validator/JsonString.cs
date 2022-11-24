using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return HasContent(input)
                && IsDoubleQuoted(input)
                && ContainsValidChars(input);
        }

        private static bool ContainsControlCharacters(string input)
        {
            foreach (char c in input)
            {
                if (c < ' ')
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsValidChars(string input)
        {
            return !ContainsControlCharacters(input)
                && EscapedCharsAreValid(input)
                && UnicodeCharsAreValid(input);
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool IsDoubleQuoted(string input)
        {
            return input.Length >= 2 && input.StartsWith('"') && input.EndsWith('"');
        }

        private static bool EscapedCharsAreValid(string input)
        {
            const byte numberOfItemsToRemove = 2;
            const string allowedToBeEscaped = "\"\\/bfnrtu";
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == '\\' && (!allowedToBeEscaped.Contains(input[i + 1])
                    || (i + 1 == input.Length - 1)))
                {
                    return input[i - 1] == '\\';
                }
            }

            return true;
        }

        private static bool UnicodeCharsAreValid(string input)
        {
            const byte reversedSolidusAndULength = 2;
            if (!input.Contains("\\u"))
            {
                return true;
            }

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input.Substring(i, reversedSolidusAndULength) == "\\u" && !CheckValidityOfUnicodeDigits(input, i))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckValidityOfUnicodeDigits(string input, int index)
        {
            const int minimumLengthOfUnicodeWithLastQuote = 5;
            const byte reversedSolidusAndULength = 2;
            const int unicodeDigitsLength = 4;

            for (int j = index + 2; j < index + reversedSolidusAndULength + unicodeDigitsLength; j++)
            {
                int indexOfUnicodeDigits = index + 2;
                if (!IsHexChar(input[j])
                || input.Substring(indexOfUnicodeDigits).Length < minimumLengthOfUnicodeWithLastQuote)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsHexChar(char c)
        {
            if (int.TryParse(Convert.ToString(c), out _))
            {
                return true;
            }
            else if (char.ToUpper(c) < 'A' || char.ToUpper(c) > 'F')
            {
                return false;
            }

            return true;
        }
    }
}