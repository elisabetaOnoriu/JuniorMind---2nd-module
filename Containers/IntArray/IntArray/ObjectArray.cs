namespace Collections
{
    public class ObjectArray
    {
        object[] objects;
        int count;

        public ObjectArray()
        {
            Array.Resize(ref objects, 4);
        }

        public virtual void Add(object element)
        {
            EnsureCapacity();
            objects[count] = element;
            count++;
        }

        public int Count { get => count; }

        public virtual object this[int index]
        {
            get => objects[index];
            set => objects[index] = value;
        }

        public int IndexOf(object element)
        {
            for (int i = 0; i < count; i++)
            {       
                if (objects[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Contains(object element)
        {
            return this.IndexOf(element) > -1;
        }

        public virtual void Insert(int index, object element)
        {
            EnsureCapacity();
            ShiftToRight(index);
            objects[index] = element;
            count++;
        }

        public void Clear()
        {
            Array.Resize(ref objects, 0);
            count = 0;
        }

        public virtual void Remove(object element)
        {
            int elementIndex = IndexOf(element);
            if (elementIndex > -1)
            {
                RemoveAt(elementIndex);
            }
        }

        public virtual void RemoveAt(int index)
        {
            ShiftToLeft(index);
            objects[^1] = 0;
            count--;
        }

        private void ShiftToLeft(int index)
        {
            for (int i = index; i < count - 1; i++)
            {
                objects[i] = objects[i + 1];
            }
        }

        private void ShiftToRight(int index)
        {
            for (int i = count; i > index; i--)
            {
                objects[i] = objects[i - 1];
            }
        }

        private void EnsureCapacity()
        {
            if (objects.Length == count)
            {
                Array.Resize(ref objects, count * 2);
            }
        }
    }
}
