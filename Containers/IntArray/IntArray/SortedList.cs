namespace Collections
{
    public class SortedList<T> : List<T> where T : IComparable<T>
    {
        public SortedList()
            :base() 
        {
            InsertionSort();
        }

        public override void Add(T item)
        {
            base.Add(item);
            InsertionSort();
        }

        public override T this[int index]
        {
            set
            {
                if (WillBeSortedAfterOperation(index + 1, index - 1, value))
                {
                    base[index] = value;
                }
            }
        }

        public override void Insert(int index, T item)
        {
            if (WillBeSortedAfterOperation(index, index - 1, item))
            {
                base.Insert(index, item);
            }
        }

        private void InsertionSort()
        {
            for (int i = 1; i < Count && Count > 1; i++)
            {
                for (int j = i; j > 0 && this.items[j - 1].CompareTo(this.items[j]) > 0; j--)
                {
                    (this.items[j - 1], this.items[j]) = (this.items[j], this.items[j - 1]);
                }
            }
        }

        private bool WillBeSortedAfterOperation(int nextIndex, int previousIndex, T element)
        => (nextIndex > Count - 1 || element.CompareTo(this.items[nextIndex]) < 0) &&
        (previousIndex < 0 || element.CompareTo(this.items[previousIndex]) > 0);
    }
}
