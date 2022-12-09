using System;

namespace Validator
{
    public class Choice : IPattern
    {
        readonly IPattern[] patterns;

        public Choice(params IPattern[] patterns)        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            if(string.IsNullOrEmpty(text))
            {
                return new FailedMatch(text);
            }

            foreach (var pattern in patterns)
            {
                var match = pattern.Match(text);
                if (match.Success())
                {
                    return new SuccessMatch(text[1..]);
                }
            }

            return new FailedMatch(text);
        }

    }
}
