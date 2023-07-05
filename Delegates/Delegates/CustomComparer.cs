namespace Delegates
{
    internal class CustomComparer<TSource> : IComparer<TSource>
    { 
        internal Func<TSource, TSource, int> compared;

        public CustomComparer(Func<TSource, TSource, int> compared)
        {
            this.compared = compared;
        }

        public int Compare(TSource x, TSource y) => compared(x, y);
    }
}