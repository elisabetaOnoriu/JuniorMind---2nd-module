namespace Validator
{
    public class Any : IPattern
    {
        readonly string accepted;

        public Any(string accepted)
        {
            this.accepted = accepted;
        }

        public IMatch Match(string text)
        {
            return !string.IsNullOrEmpty(text) && accepted.Contains(text[0]) ?
                   new SuccessMatch(text[1..]) :
                   new FailedMatch(text);
        }
    }
}
