namespace Validator
{
    public interface IPattern
    {
        IMatch Match(string text);
    }
}
