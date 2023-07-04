namespace Delegates
{
    internal class CustomComparer<TSource> : IComparer<TSource>
    { 
        internal delegate int GetComparison(TSource item1, TSource item2);
        GetComparison compared;
        internal IComparer<TSource> next;

        public CustomComparer(GetComparison compared)
        {
            this.compared = compared;
            this.next = null;
        }

        public int Compare(TSource x, TSource y)
        {
            int result = compared(x, y);
            return result == 0 ? next.Compare(x, y) : result;
        }
    }
}