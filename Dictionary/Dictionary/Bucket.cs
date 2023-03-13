namespace Dictionary
{
    internal class Bucket<TKey, TValue>
    {
        int firstElementIndex = -1;
        Entry<TKey, TValue> entry;

        internal Entry<TKey, TValue> Entry { get => entry; set => entry = value; }

        internal int FirstElementIndex {  get => firstElementIndex; set => firstElementIndex = value; }
    }
}
