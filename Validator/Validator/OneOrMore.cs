namespace Validator
{
    public class OneOrMore : IPattern
    {
        readonly IPattern pattern;

        public OneOrMore(IPattern pattern)
        {
            this.pattern = new Many(pattern);
        }

        public IMatch Match(string text)
        {
            var match = pattern.Match(text);
            return match.RemainingText() == text ? new FailedMatch(text) : match;
        }
    }
}
