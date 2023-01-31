using System.Collections;

namespace Collections
{
    public class ObjectArrayEnum : IEnumerator
    {
        object[] objects;
        int position = -1;

        public ObjectArrayEnum(object[] objects)
        {
            this.objects = objects;
        }

        public object Current
        {
            get
            {
                try
                {
                    return objects[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return position < objects.Length;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
