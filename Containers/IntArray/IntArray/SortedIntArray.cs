using System.Xml.Linq;

namespace Collections
{
    public class SortedIntArray : IntArray
    {
        public SortedIntArray()
            : base()
        {
        }

        public override void Add(int element)
        {
            base.Add(element);
            InsertionSort();
        }

        public override int this[int index]
        {
            set
            {
                if (WillBeSortedAfterOperation(index + 1, index - 1, value))
                {
                    base[index] = value;
                }
            }
        }

        public override void Insert(int index, int element)
        {
            if (WillBeSortedAfterOperation(index, index - 1, element))
            {
                base.Insert(index, element);
            }
        }

        private void InsertionSort()
        {
            for (int i = 1; i < Count && Count > 1; i++)
            {
                for (int j = i; j > 0 && this.numbers[j - 1] > this.numbers[j]; j--)
                {
                    (this.numbers[j - 1], this.numbers[j]) = (this.numbers[j], this.numbers[j - 1]);
                }
            }
        }

        private bool WillBeSortedAfterOperation(int nextIndex, int previousIndex, int element)
        => (nextIndex > Count - 1 || element < this.numbers[nextIndex]) &&
        (previousIndex < 0 || element > this.numbers[previousIndex]);
    }
}
