namespace Collections
{
    public class IntArray
    {
        int[] array;

        public IntArray(int[] array)
        {
            Array.Resize(ref array, 4);
            this.array = array;    
        }

        public void Add(int element)
        {
            if (array[^1] != 0)
            {
                int nextPosition = array.Length;
                Array.Resize(ref array, array.Length * 2);
                array[nextPosition] = element;
            }
            else
            {
                array[Array.IndexOf(array, 0)] = element;
            }
        }

        public int Count()
        {
            return array.Contains(0) ? Array.IndexOf(array, 0) : array.Length;
        }

        public int Element(int index)
        {
            return array[index];
        }

        public void SetElement(int index, int element)
        {
            array[index] = element;
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

        public bool Contains(int element)
        {
            return this.IndexOf(element) > -1;
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
            if (this.IndexOf(element) > -1)
            {
                this.RemoveAt(this.IndexOf(element));
            }
        }

        public void RemoveAt(int index)
        {
            ShiftToLeft(index);
            array[^1] = 0;
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