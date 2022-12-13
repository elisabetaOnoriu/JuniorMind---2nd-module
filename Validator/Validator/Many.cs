namespace Validator
{
    public class Many : IPattern
    {
        readonly IPattern pattern;

        public Many(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            IMatch match = new SuccessMatch(text);
            while (match.Success())
            {
                match = pattern.Match(match.RemainingText());
            }

            return match.RemainingText() == text ?
                new SuccessMatch(text) :
                new SuccessMatch(match.RemainingText());
        }
    }
}
