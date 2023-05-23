namespace Delegates
{
    internal class MultiComparer<TKey, TKey1> : IComparer<Tuple<TKey, TKey1>>
    {
        IComparer<TKey> previousComparer;
        IComparer<TKey1> actualComparer;

        internal MultiComparer(IComparer<TKey> previousComparer, IComparer<TKey1> actualComparer)
        {
            this.previousComparer = previousComparer;
            this.actualComparer = actualComparer;
        }

        public int Compare(Tuple<TKey, TKey1> x, Tuple<TKey, TKey1> y)
        {
            int firstComparer = previousComparer.Compare(x.Item1, y.Item1);
            int secondComparer = actualComparer.Compare(x.Item2, y.Item2);
            if (firstComparer != 0)
            {
                return firstComparer;
            }

            return secondComparer;
        }
    }
}
