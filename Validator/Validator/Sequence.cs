using System;
namespace Validator
{
    public class Sequence : IPattern
    {
        readonly IPattern[] patterns;

        public Sequence(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            string textInFirstInstance = text;
            foreach (var pattern in patterns)
            {
                var match = pattern.Match(text);
                if (!match.Success())
                {
                    return new FailedMatch(textInFirstInstance);
                }
                
                text = match.RemainingText();
            }

            return new SuccessMatch(text);
        }
    }
}
