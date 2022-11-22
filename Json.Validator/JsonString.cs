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
            const int firstValidCharForJsonString = 32;
            for (int i = 0; i < input.Length; i++)
            {
                if (Convert.ToChar(input[i]) < firstValidCharForJsonString)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsValidChars(string input)
        {
            return !ContainsControlCharacters(input)
                && !ContainsExceptedChar(input);
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool IsDoubleQuoted(string input)
        {
            return input.Length >= 2 && input[0] == '"' && input[input.Length - 1] == '"';
        }

        private static bool ContainsExceptedChar(string input)
        {
            char[] allowedToBeEscaped = { '"', '\\', '/', 'b', 'f', 'n', 'r', 't', 'u' };
            if (!input.Contains('\\'))
            {
                return false;
            }

            for (int i = 0; i < allowedToBeEscaped.Length - 1; i++)
            {
                if (input[input.IndexOf(@"\") + 1] == allowedToBeEscaped[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}