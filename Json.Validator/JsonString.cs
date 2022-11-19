using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return HasContent(input)
                && IsDoubleQuoted(input)
                && !ContainsControlCharacters(input);
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

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool IsDoubleQuoted(string input)
        {
            return input.Length >= 2 && input[0] == '"' && input[input.Length - 1] == '"';
        }
    }
}