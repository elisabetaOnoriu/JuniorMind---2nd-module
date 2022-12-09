namespace Validator
{
    public class FailedMatch : IMatch
    {
        readonly string remainingText;

        public FailedMatch(string remainingText)
        {
            this.remainingText = remainingText;
        }

        public string RemainingText()
        {
            return remainingText;
        }

        public bool Success()
        {
            return false;
        }
    }
}
