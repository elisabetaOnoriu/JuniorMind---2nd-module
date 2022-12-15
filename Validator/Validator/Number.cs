namespace Validator
{
    public class Number : IPattern
    {
        readonly IPattern pattern;

        public Number()
        {
            var digit = new Choice(new Character('0'), new Range('1', '9'));
            var integer = new Sequence(new Optional(new Character('-')),
                                       new Choice(new OneOrMore(new Range('1', '9')),
                                                  digit));
            var fraction = new Sequence(
                            new Character('.'),
                            new OneOrMore(digit));
            var exponent = new Sequence(
                            new Any("Ee"),
                            new Optional(new Any("+-")),
                            new OneOrMore(digit));

            pattern = new Sequence(
                    integer,
                    new Optional(new Sequence(
                        new Choice(
                            new Sequence(fraction, new Optional(exponent)),
                            exponent))));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
