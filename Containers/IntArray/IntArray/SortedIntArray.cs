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
            this.numbers = Sort(this.numbers, 0, Count - 1);
        }

        public override int this[int index]
        {
            set {
                base[index] = value;
                this.numbers = Sort(this.numbers, 0, Count - 1);
            }
        }

        public override void Insert(int index, int element)
        {
            base.Insert(index, element);
            this.numbers = Sort(this.numbers, 0, Count - 1);
        }
        
        private int[] Sort(int[] array, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];
            while (i <= j)
            {
                while (array[i] < pivot)
                {
                    i++;
                }

                while (array[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                Sort(array, leftIndex, j);
            if (i < rightIndex)
                Sort(array, i, rightIndex);
            return array;
        }
    }
}
