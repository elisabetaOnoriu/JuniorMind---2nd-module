namespace Validator
{
    public class SuccessMatch : IMatch
    {
        readonly string remainingText;

        public SuccessMatch(string remainingText)
        {
            this.remainingText = remainingText;
        }

        public string RemainingText()
        {
            return remainingText;
        }

        public bool Success()
        {
            return true;
        }
    }
}
