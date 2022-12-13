namespace Validator
{
    public class Optional : IPattern
    {
        readonly IPattern pattern;

        public Optional(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            var match = pattern.Match(text);
            return new SuccessMatch(match.RemainingText());
        }
    }
}
