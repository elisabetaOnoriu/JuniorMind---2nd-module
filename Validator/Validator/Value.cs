namespace Validator
{
    public class Value
    {
        private readonly IPattern pattern;

        public Value()
        {
            var value = new Choice(
                            new String(),
                            new Number(),
                            new Text("true"),
                            new Text("false"),
                            new Text("null"));

            var whiteSpace = new Many(new Choice(new Character(' '), new Range('\t', '\n'), new Character('\r')));
            var separator = new Sequence(new Character(','), whiteSpace);
            var array = new Sequence(new Character('['),
                                     whiteSpace, new List(value, separator),
                                     new Character(']'));
            var objectUnit = new Sequence(new String(), new Character(':'), whiteSpace, value);
            var _object = new Sequence(new Character('{'),
                                      whiteSpace, new List(objectUnit, separator),
                                      new Character('}'));
            value.Add(array);
            value.Add(_object);
            pattern = new Sequence(whiteSpace, value, whiteSpace);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
