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
            IMatch match = new SuccessMatch(text);
            foreach (var pattern in patterns)
            {
                match = pattern.Match(match.RemainingText());
                if (!match.Success())
                {
                    return new FailedMatch(text);
                }
            }

            return new SuccessMatch(match.RemainingText());
        }
    }
}
