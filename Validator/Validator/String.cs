namespace Validator
{
    public class String : IPattern
    {
        readonly IPattern pattern;

        public String(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
