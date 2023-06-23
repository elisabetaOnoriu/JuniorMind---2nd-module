namespace Delegates
{
    internal class MultiComparer<TSource> : IComparer<TSource>
    {
        readonly IComparer<TSource> primary, secondary;

        public MultiComparer(IComparer<TSource> primary, IComparer<TSource> secondary)
        {
            this.primary = primary;
            this.secondary = secondary;
        }

        int IComparer<TSource>.Compare(TSource x, TSource y)
        {
            int result = primary.Compare(x, y);
            return result == 0 ? secondary.Compare(x, y) : result;
        }
    }
}