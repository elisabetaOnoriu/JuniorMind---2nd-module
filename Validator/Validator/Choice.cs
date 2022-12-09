using System;

namespace Validator
{
    public class Choice : IPattern
    {
        readonly IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            foreach (var pattern in patterns)
            {
                var match = pattern.Match(text);
                if (!string.IsNullOrEmpty(text) && match.Success())
                {
                    return new SuccessMatch(text[1..]);
                }
            }

            return new FailedMatch(text);
        }

    }
}
