using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Dictionary
{
    public class MyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        int count;
        Bucket<TKey, TValue>[] buckets;      
        Entry<TKey, TValue>[] entries;
        Entry<TKey, TValue> freeEntries;

        public MyDictionary(int capacity)
        {
            buckets = new Bucket<TKey, TValue>[capacity];
        }

        public TValue this[TKey key]
        {
            get
            {
                ThrowExceptionIfArgumentIsNull(key);
                if (!TryGetValue(key, out TValue value))
                {
                    throw new KeyNotFoundException();
                }

                return value;
            }
            set => Add(key, value);
        }

        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keys = new();
                foreach (KeyValuePair<TKey, TValue> keyValue in this)
                {
                    keys.Add(keyValue.Key);
                }

                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> values = new();
                foreach (KeyValuePair<TKey, TValue> keyValue in this)
                {
                    values.Add(keyValue.Value);
                }

                return values;
            }
        }

        public int Count => count;

        public bool IsReadOnly { get => false; }

        public ReadOnlyDictionary<TKey, TValue> ReadOnly()
        {
            return new(this);
        }

        public int GetHashCode(TKey key)
        {
            int keyNumber = Math.Abs(Convert.ToInt32(key));
            return keyNumber >= buckets.Length ? key.GetHashCode() % buckets.Length : keyNumber;
        }

        public void Add(TKey key, TValue value)
        {
            ThrowExceptionIfArgumentIsNull(key);
            ThrowExceptionIfDictionaryIsReadOnly();
            if (ContainsKey(key))
            {
                throw new ArgumentException();
            }
    
            count++;
            int index = GetIndex(key);
            Entry<TKey, TValue> newEntry = new(key, value);
            newEntry.Next  = buckets[index].FirstElementIndex;
            buckets[index].FirstElementIndex = count - 1;
            buckets[index].Entry = newEntry;
            if (count == entries.Length)
            {
                //freeEntries
            }
            entries[count - 1] = newEntry;     
        }

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i].FirstElementIndex = -1;
                entries[i] = null;
            }

            count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            for (var current = buckets[GetIndex(item.Key)].Entry; current != default; current = entries[current.Next])
            {
                if (current.KeyValue().Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsKey(TKey key)
        {
            ThrowExceptionIfArgumentIsNull(key);
            return TryGetValue(key, out _);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ThrowExceptionIfArgumentIsNull(array);
            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException(null, nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException($"{arrayIndex}");
            }

            int i = 0;
            foreach (var item in this)
            {
                array[i++] = item;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i <= count; i++)
            {
                for (var current = buckets[i].Entry; current != default; current = entries[current.Next])
                {
                    yield return current.KeyValue();
                }
            }
        }

        public bool Remove(TKey key)
        {
            ThrowExceptionIfArgumentIsNull(key);
            ThrowExceptionIfDictionaryIsReadOnly();
            var index = GetIndex(key);
            for (var current = buckets[index].Entry; current != default; current = entries[current.Next])
            {
                if (current.Key.Equals(key))
                {
                    buckets[index].FirstElementIndex--;
                    //entries[current.Next]
                    count--;
                    return true;
                }
            }

            return false;           
        }

        public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            var index = GetIndex(key);
            if (buckets[index].FirstElementIndex == -1)
            {
                value = default;
                return false;
            }

            for (var current = buckets[index].Entry; current != default; current = entries[current.Next])
            {
                if (current.Key.Equals(key))
                {
                    value = current.Value;
                    return true;
                }
            }
            
            value = default;
            return false;                
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int GetIndex(TKey key) => GetHashCode(key);

        private void ThrowExceptionIfArgumentIsNull<TKey>(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }
        }

        private void ThrowExceptionIfDictionaryIsReadOnly()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }
        }
    }
}