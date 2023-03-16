using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Dictionary
{
    public class MyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        Entry<TKey, TValue>[] entries;
        int[] buckets;
        int freeIndex = -1;
        int count;
        
        public MyDictionary(int capacity)
        {
            buckets = new int[capacity];
            Array.Fill(buckets, -1);
            entries = new Entry<TKey, TValue>[capacity];
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

            set
            {
                if (TryFindEntryIndex(key, out int index))
                {
                    entries[index].Value = value;
                }

                else
                {
                    Add(key, value);
                }
            }
        }

        private bool TryFindEntryIndex(TKey key, out int index)
        {
            for (int i = buckets[BucketIndex(key)]; i != -1; i = entries[i].Next)
            {
                if (entries[i].Key.Equals(key))
                {
                    index = i;
                    return true;
                }
            }

            index = default;
            return false;
        }

        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keys = new();
                foreach (var keyValue in this)
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
                foreach (var keyValue in this)
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

        internal int BucketIndex(TKey key)
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

            int index = GetNextFreePosition();
            int bucketIndex = BucketIndex(key);           
            Entry<TKey, TValue> newEntry = new(key, value);
            newEntry.Next  = buckets[bucketIndex];
            buckets[bucketIndex] = index;
            entries[index] = newEntry; 
            count++;   
        }

        private int GetNextFreePosition()
        {
            if (freeIndex != -1)
            {
                int index = freeIndex;
                freeIndex = entries[freeIndex].Next;
                return index;
            }

            return count;
        }

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            Array.Fill(buckets, -1);
            count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            for (int i = buckets[BucketIndex(item.Key)]; i != -1 ; i = entries[i].Next)
            {
                if (entries[i].KeyValue().Equals(item))
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
            for (int i = 0; i < buckets.Length; i++)
            {
                for (int j = buckets[i]; j != -1; j = entries[j].Next)
                {
                    yield return entries[j].KeyValue();                
                }
            }       
        }

        public bool Remove(TKey key)
        {
            ThrowExceptionIfArgumentIsNull(key);
            ThrowExceptionIfDictionaryIsReadOnly();
            var index = BucketIndex(key);
            bool found = TryFindEntryIndex(key, out int entryIndex);
            if (found)
            {
                if (entryIndex == buckets[index])
                {
                    buckets[index] = entries[entryIndex].Next;
                    entries[entryIndex].Key = default;
                    entries[entryIndex].Value = default;
                }

                entries[entryIndex].Next = freeIndex;
                freeIndex = entryIndex;
                count--;            
            }

            return found;           
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            ThrowExceptionIfArgumentIsNull(item);
            if (this[item.Key].Equals(item.Value))
            {
                return Remove(item.Key);
            }

            return false;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            ThrowExceptionIfArgumentIsNull(key);
            for (int i = buckets[BucketIndex(key)]; i != -1; i = entries[i].Next)
            {
                if (entries[i].Key.Equals(key))
                {
                    value = entries[i].Value;
                    return true;
                }
            }
            
            value = default;
            return false;                
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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