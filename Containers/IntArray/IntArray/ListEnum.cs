using System.Collections;

namespace Collections
{
    public class ListEnum<T> : IEnumerator
    {
        readonly List<T> items;
        int position = -1;

        public ListEnum(List<T> items)
        {
            this.items = items;
        }

        public object Current
        { 
            get => items[position];
        }
        
        public bool MoveNext()
        {
            position++;
            return position < items.Count;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
