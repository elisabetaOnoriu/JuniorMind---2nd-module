namespace Collections
{
    public class IntArray
    {
        int[] array;

        public IntArray(int[] array)
        {
            this.array = array;
        }

        public void Add(int element)
        {
            Array.Resize(ref array, array.Length + 1);
            array[^1] = element;
        }

        public int Count()
        {
            return array.Length;
        }

        public int Element(int index)
        {
            return array[index];
        }

        public void SetElement(int index, int element)
        {
            array[index] = element;
        }

        public bool Contains(int element)
        {
            foreach (var item in array)
            {
                if (item == element)
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(int element)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, int element)
        {
            Array.Resize(ref array, array.Length + 1);
            ShiftToRight(index, element);
        }

        public void Clear()
        {
            Array.Resize(ref array, 0);
        }

        public void Remove(int element)
        {
            RemoveAt(this.IndexOf(element));
        }

        public void RemoveAt(int index)
        {
            ShiftToLeft(index);
            Array.Resize(ref array, array.Length - 1);
        }

        private void ShiftToLeft(int index)
        {
            for (int i = index; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }
        }

        private void ShiftToRight(int index, int element)
        {
            for (int i = array.Length - 1; i >= index; i--)
            {
                array[i] = i == index ? element : array[i - 1];
            }
        }
    }
}