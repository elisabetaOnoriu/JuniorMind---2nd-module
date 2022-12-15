namespace Validator
{
    public class Number : IPattern
    {
        readonly IPattern pattern;

        public Number()
        {
            var digit = new Range('0', '9');
            var digits = new OneOrMore(digit);
            var integer = new Sequence(new Optional(new Character('-')),
                                       new Choice(new Character('0'),
                                                  digits));
            var fraction = new Sequence(
                            new Character('.'),
                            digits);
            var exponent = new Sequence(
                            new Any("Ee"),
                            new Optional(new Any("+-")),
                            digits);

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
