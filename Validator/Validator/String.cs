namespace Validator
{
    public class String : IPattern
    {
        readonly IPattern pattern;

        public String()
        {
            var validStringUnit = new Choice(new Range(' ', '!'), new Range('#', '['), new Range(']', '~'));
            var escapedSequence = new Sequence(new Character('\\'), new Any("\"\\/bfnrt"));
            var digit = new Range('0', '9');
            var hex = new Choice(digit, new Range('A', 'F'), new Range('a', 'f'));
            var unicode = new Sequence(new Text("\\u"), hex, hex, hex, hex);
            this.pattern = new Sequence(new Character('"'),
                                   new Many(new Choice(validStringUnit, escapedSequence, unicode)),
                                   new Character('"'));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
