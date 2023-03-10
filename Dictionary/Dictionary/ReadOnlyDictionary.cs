using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Dictionary
{
    public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        readonly IDictionary<TKey, TValue> dictionary;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            this.dictionary = dictionary;
        }

        public TValue this[TKey key] { get => dictionary[key]; set => ThrowExceptionIfDictionaryIsReadOnly(); }

        public ICollection<TKey> Keys => dictionary.Keys;

        public ICollection<TValue> Values => dictionary.Values;

        public int Count => dictionary.Count;

        public bool IsReadOnly { get => true; }

        public void Add(TKey key, TValue value) => ThrowExceptionIfDictionaryIsReadOnly();

        public void Add(KeyValuePair<TKey, TValue> item) => ThrowExceptionIfDictionaryIsReadOnly();

        public void Clear() => ThrowExceptionIfDictionaryIsReadOnly();

        public bool Contains(KeyValuePair<TKey, TValue> item) => dictionary.Contains(item);

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            dictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            ThrowExceptionIfDictionaryIsReadOnly();
            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            ThrowExceptionIfDictionaryIsReadOnly();
            return false;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void ThrowExceptionIfDictionaryIsReadOnly()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }
        }
    }
}