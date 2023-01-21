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
            set {
                base[index] = value;
                InsertionSort();
            }
        }

        public override void Insert(int index, int element)
        {
            base.Insert(index, element);
            InsertionSort();
        }
        
        private void InsertionSort()
        {
            for (int i = 1; i < Count && Count > 1; i++)
            {
                for (int j = i; j >= 0 && this.numbers[j - 1] > this.numbers[j]; j--)
                {
                    (this.numbers[j - 1], this.numbers[j]) = (this.numbers[j], this.numbers[j - 1]);
                }
            }
        }
    }
}
