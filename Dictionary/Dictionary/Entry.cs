namespace Dictionary
{
    internal class Entry<TKey, TValue>
    {
        Entry<TKey, TValue> next = null;

        internal Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        internal TKey Key { get; set; }

        internal TValue Value { get; set; }

        internal Entry<TKey,TValue> Next { get => next; set => next = value; }

        internal KeyValuePair<TKey, TValue> KeyValue()
        {
            KeyValuePair<TKey, TValue> keyValuePair = new(Key, Value);
            return keyValuePair;
        }
    }
}
