using System;
namespace Validator
{
    public class Sequence : IPattern
    {
        readonly IPattern[] patterns;

        public Sequence(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new FailedMatch(text);
            }

            bool hasInnerSequence = false;
            int indexOfPattern = 0;
            int indexOfInnerPattern = 0;
            int textIndex = 0;
            while (indexOfPattern < patterns.Length)
            {
                var pattern = patterns[indexOfPattern];
                var match = pattern.Match(text[textIndex..]);
                if (pattern.GetType() == typeof(Sequence))
                {
                    hasInnerSequence = true;
                    Sequence innerSequence = (Sequence)pattern;
                    while (indexOfInnerPattern < innerSequence.patterns.Length)
                    {
                        match = innerSequence.patterns[indexOfInnerPattern].Match(text[textIndex..]);
                        if (!match.Success())
                        {
                            return new FailedMatch(text);
                        }

                        textIndex++;
                        indexOfInnerPattern++;
                    }

                    textIndex--;
                }

                if (!match.Success())
                {
                    return new FailedMatch(text);
                }

                indexOfPattern++;
                textIndex++;
            }

            return hasInnerSequence ? new SuccessMatch(text[textIndex..]) : new SuccessMatch(text[indexOfPattern..]);
        }
    }
}
