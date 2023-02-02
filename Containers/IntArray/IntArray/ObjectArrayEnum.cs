using System.Collections;

namespace Collections
{
    public class ObjectArrayEnum : IEnumerator
    {
        readonly ObjectArray objects;
        int position = -1;

        public ObjectArrayEnum(ObjectArray objects)
        {
            this.objects = objects;
        }

        public object Current
        { 
            get => objects[position];
        }
        
        public bool MoveNext()
        {
            position++;
            return position < objects.Count;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
