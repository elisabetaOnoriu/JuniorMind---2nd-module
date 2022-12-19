namespace Validator
{
    public class String : IPattern
    {
        readonly IPattern pattern;

        public String()
        {
            var digit = new Range('0', '9');
            var hex = new Choice(digit, new Range('A', 'F'), new Range('a', 'f'));
            var unicode = new Sequence(new Character('u'), hex, hex, hex, hex);
            var validStringUnit = new Choice(new Range(' ', '!'), new Range('#', '['), new Range(']', char.MaxValue));
            var escapedSequence = new Sequence(new Character('\\'), new Choice(new Any("\"\\/bfnrt"), unicode));
            this.pattern = new Sequence(new Character('"'),
                                   new Many(new Choice(validStringUnit, escapedSequence)),
                                   new Character('"'));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
