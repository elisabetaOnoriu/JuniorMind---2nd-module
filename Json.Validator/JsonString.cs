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
            if (!input.Contains("\\u"))
            {
                return true;
            }

            const string unicodeApprovedChars = "1234567890ABCDEFabcdef";
            const int minimumLengthOfUnicodeWithLastQuote = 5;
            const int unicodeDigitsLength = 4;
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input.Substring(i, 2) == "\\u")
                {
                    for (int j = i + 2; j < i + 2 + unicodeDigitsLength; j++)
                    {
                        int indexOfUnicodeDigits = i + 2;
                        if (!unicodeApprovedChars.Contains(input[j]) || input.Substring(indexOfUnicodeDigits).Length < minimumLengthOfUnicodeWithLastQuote)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}