namespace Validator
{
    public interface IMatch
    {
        bool Success();
        string RemainingText();
    }
}
