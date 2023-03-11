using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Dictionary
{
    public class MyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        int[] buckets;
        int count;
        Entry<TKey, TValue>[] entries;      
        Entry<TKey, TValue> freeEntries;

        public MyDictionary(int capacity)
        {
            buckets = new int[capacity];
            SetInitialBuckets();
            entries = new Entry<TKey, TValue>[capacity];
        }

        private void SetInitialBuckets()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = -1;
            }
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
    
            int index = GetIndex(key);
            buckets[index] = count;
            Entry<TKey, TValue> newEntry = new(key, value);
            newEntry.Next  = entries[count];
            entries[count] = newEntry;
            count++;
        }

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = -1;
                entries[i] = null;
            }

            count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            for (var current = entries[buckets[GetIndex(item.Key)]]; current != null; current = current.Next)
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
            int entryIndex = buckets[GetIndex(key)];
            if (entryIndex == -1)
            {
                return false;
            }

            for (var current = entries[entryIndex]; current != null; current = current.Next)
            {
                if (current.Key.Equals(key))
                {
                    return true;
                }
            }

            return false;
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

        public IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i <= count; i++)
            {
                for (var current = entries[i]; current != null; current = current.Next)
                {
                    yield return current.KeyValue();
                }
            }
        }

        public bool Remove(TKey key)
        {
            ThrowExceptionIfArgumentIsNull(key);
            ThrowExceptionIfDictionaryIsReadOnly();
            if (!ContainsKey(key))
            {
                return false;
            }

            var index = buckets[GetIndex(key)];
            if (this[key].Equals(entries[index].Value))
            {
                entries[index] = entries[index].Next;
                buckets[GetIndex(key)]--;
                count--;
            }

            for (var current = entries[index]; current != null; current = current.Next)
            {
                if (current.Key.Equals(key))
                {
                    current = null;
                    return true;
                }
            }

            return false;           
        }

        public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (ContainsKey(key))
            {
                value =  entries[buckets[GetIndex(key)]].Value;
                return true;
            }

            value = default;
            return false;        
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

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