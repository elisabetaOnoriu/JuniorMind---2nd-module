using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Dictionary
{
    public class MyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        Entry<TKey, TValue>[] entries;
        int[] buckets;
        int freeIndex;
        int count;
        
        public MyDictionary(int capacity)
        {
            buckets = new int[capacity];
            entries = new Entry<TKey, TValue>[capacity];
            SetInitialBuckets();
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
            int keyHash = Math.Abs(key.GetHashCode());
            return keyHash >= buckets.Length ? keyHash % buckets.Length : keyHash;
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
            newEntry.Next  = buckets[index];
            buckets[index] = count - 1;
            entries[count - 1] = newEntry;     
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
            for (var current = entries[buckets[GetIndex(item.Key)]]; current != default; current = entries[current.Next])
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

            int i = arrayIndex;
            foreach (var item in this)
            {
                array[i++] = item;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i] == default)
                {
                    break;
                }

                yield return entries[i].KeyValue();                
            }
        }

        public bool Remove(TKey key)
        {
            ThrowExceptionIfArgumentIsNull(key);
            ThrowExceptionIfDictionaryIsReadOnly();
            var index = GetIndex(key);
            for (var current = entries[buckets[index]]; current != default; current = entries[current.Next])
            {
                if (current == entries[buckets[index]] && current.Key.Equals(key))
                {
                    buckets[index] = current.Next;
                    entries[buckets[index]] = default;
                    freeIndex = Array.IndexOf(entries, current);
                }

                if (current.Key.Equals(key))
                {
                    buckets[index]--;
                    count--;
                    return true;
                }
            }

            return false;           
        }

        public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            ThrowExceptionIfArgumentIsNull(key);
            var index = GetIndex(key);
            if (buckets[index] == -1)
            {
                value = default;

                return false;
            }

            for (var current = entries[buckets[index]]; current != default; current = entries[current.Next])
            {
                if (current.Key.Equals(key))
                {
                    value = current.Value;
                    return true;
                }

                if (current.Next == -1)
                {
                    break;
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