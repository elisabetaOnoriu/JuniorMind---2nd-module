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
                if (!input.Contains('\\'))
                {
                    return true;
                }

                if (input[i] == '\\' && allowedToBeEscaped.Contains(input[i + 1])
                    && i + 1 != input.Length - 1)
                {
                    input = input.Remove(i, numberOfItemsToRemove);
                    EscapedCharsAreValid(input);
                    return true;
                }
            }

            return false;
        }

        private static bool UnicodeCharsAreValid(string input)
        {
            if (!input.Contains("\\u"))
            {
                return true;
            }

            const string unicodeApprovedChars = "1234567890ABCDEFabcdef";
            const int minimumLengthOfUnicodeWithLastQuote = 6;
            const int unicodeDigitsLength = 4;
            if (input.Substring(input.IndexOf("\\u") + 1).Length < minimumLengthOfUnicodeWithLastQuote)
            {
                return false;
            }

            int indexOfUnicode = input.IndexOf("\\u") + 2;
            for (int i = indexOfUnicode; i < indexOfUnicode + unicodeDigitsLength; i++)
            {
                if (!unicodeApprovedChars.Contains(input[i]))
                {
                    return false;
                }
            }

            input = input.Remove(input.IndexOf("\\u"), unicodeDigitsLength);
            UnicodeCharsAreValid(input);
            return true;
        }
    }
}