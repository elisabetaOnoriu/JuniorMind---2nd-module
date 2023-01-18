namespace Collections
{
    public class IntArray
    {
        int[] numbers;
        int count;

        public IntArray()
        {
            Array.Resize(ref numbers, 4);
        }

        public void Add(int element)
        {      
            if (count == 0 || count % 4 != 0)
            {
                numbers[count] = element;
            }
            else
            {
                Array.Resize(ref numbers, numbers.Length * 2);
                numbers[count] = element;
            }

            count++;
        }

        public int Count()
        {
            return count;
        }

        public int Element(int index)
        {
            return numbers[index];
        }

        public void SetElement(int index, int element)
        {
            numbers[index] = element;
        }

        public int IndexOf(int element)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if(numbers[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Contains(int element)
        {
            return this.IndexOf(element) > -1;
        }

        public void Insert(int index, int element)
        {        
            if (count >= 4 && count % 4 == 0)
            {
                Array.Resize(ref numbers, numbers.Length * 2);
            }

            ShiftToRight(index, element);
            count++;
        }

        public void Clear()
        {
            Array.Resize(ref numbers, 0);
            count = 0;
        }

        public void Remove(int element)
        {
            if (this.IndexOf(element) > -1)
            {
                this.RemoveAt(this.IndexOf(element));
            }
        }

        public void RemoveAt(int index)
        {
            ShiftToLeft(index);
            numbers[^1] = 0;
            count--;
        }

        private void ShiftToLeft(int index)
        {
            for (int i = index; i < numbers.Length - 1; i++)
            {
                numbers[i] = numbers[i + 1];
            }
        }

        private void ShiftToRight(int index, int element)
        {
            for (int i = numbers.Length - 1; i >= index; i--)
            {
                numbers[i] = i == index ? element : numbers[i - 1];
            }
        }
    }
}