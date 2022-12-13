namespace Validator
{
    public class Optionally : IPattern
    {
        readonly IPattern pattern;

        public Optionally(IPattern pattern)
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
