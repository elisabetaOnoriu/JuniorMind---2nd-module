namespace Validator
{
    public class List : IPattern
    {
        readonly IPattern pattern;

        public List(IPattern element, IPattern separator)
        {
            this.pattern = new Optional(
                new Sequence(
                    element,
                    new Many(new Sequence(separator, element))));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
