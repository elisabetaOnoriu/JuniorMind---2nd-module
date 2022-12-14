namespace Validator
{
    public class OneOrMore : IPattern
    {
        readonly IPattern pattern;

        public OneOrMore(IPattern pattern)
        {
            IPattern patternConstructed = new Sequence(pattern, new Many(pattern));
            this.pattern = patternConstructed;
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
