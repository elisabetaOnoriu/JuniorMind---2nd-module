namespace Validator
{
    public class Text : IPattern
    {
        readonly string prefix;

        public Text(string prefix)
        {
            this.prefix = prefix;
        }

        public IMatch Match(string text)
        {
            return !string.IsNullOrEmpty(text) && text.StartsWith(prefix) ?
                   new SuccessMatch(text[prefix.Length..]) :
                   new FailedMatch(text);
        }
    }
}
