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
            var characterChoice = new Any(" \n\t\r");
            var whiteSpace = new Many(characterChoice);
            var element = new Sequence(whiteSpace, value, whiteSpace);
            var separator = new Sequence(new Character(','), whiteSpace);
            var array = new Sequence(
                              new Character('['),
                              whiteSpace, new List(element, separator),
                              new Character(']'));
            var member = new Sequence(                            
                              new String(), new Character(':'), element);
            var @object = new Sequence(
                              new Character('{'),
                              whiteSpace,
                              new List(member, separator),
                              new Character('}'));
            value.Add(array);
            value.Add(@object);
            pattern = element;
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
