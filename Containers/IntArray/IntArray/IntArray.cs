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
            int[] result = new int[array.Length + 1];
            result[index] = element;
            int firstIndex = 0, secondIndex = 0;
            while (secondIndex < result.Length)
            {
                if (array[firstIndex] == element)
                {
                    secondIndex++;
                    continue;
                }

                result[secondIndex++] = array[firstIndex++];
                array = result;
            }
        }

        public void Clear()
        {
            Array.Resize(ref array, 0);
        }

        public void Remove(int element)
        {
            int[] result = new int[array.Length - 1];
            int firstIndex = 0, secondIndex = 0;
            bool check = false;
            while (secondIndex < array.Length)
            {
                if (array[firstIndex] == element && !check)
                {
                    secondIndex++;
                    check = true;
                }

                result[firstIndex++] = array[secondIndex++];
            }
            
            array = result;
        }

        public void RemoveAt(int index)
        {
            int[] result = new int[array.Length - 1];
            int firstIndex = 0, secondIndex = 0;
            while (secondIndex < array.Length)
            {
                if (firstIndex == index)
                {
                    secondIndex++;
                }

                result[firstIndex++] = array[secondIndex++];   
            }
            
            array = result;
        }
    }
}