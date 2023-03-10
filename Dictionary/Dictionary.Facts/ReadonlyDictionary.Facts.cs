global using Xunit;
using Dictionary;

namespace DictionaryFacts
{
    public class ReadOnlyDictionaryFacts
    {
        [Fact]
        public void Add_ThrowsException()
        {
            MyDictionary<int, string> dictionary = new(3);
            dictionary.Add(2, "Ana");
            dictionary.Add(1, "Any");
            ReadOnlyDictionary<int, string> readOnlyDictionary = dictionary.ReadOnly();
            Assert.Throws<NotSupportedException>(() => readOnlyDictionary.Add(0, "Bob"));
        }

        [Fact]
        public void SetIndexProperty_ThrowsException()
        {
            MyDictionary<int, string> dictionary = new(3);           
            ReadOnlyDictionary<int, string> readOnlyDictionary = dictionary.ReadOnly();
            Assert.Throws<NotSupportedException>(() => readOnlyDictionary[2] = "Ana");
        }

        [Fact]
        public void Add_KeyValuePair_ThrowsException()
        {
            MyDictionary<int, string> dictionary = new(3);
            KeyValuePair<int, string> keyValuePair = new(2, "abc");
            ReadOnlyDictionary<int, string> readOnlyDictionary = dictionary.ReadOnly();
            Assert.Throws<NotSupportedException>(() => readOnlyDictionary.Add(keyValuePair));
        }

        [Fact]
        public void Clear_ThrowsException()
        {
            MyDictionary<int, string> dictionary = new(3);
            dictionary.Add(2, "Ana");
            dictionary.Add(1, "Any");
            ReadOnlyDictionary<int, string> readOnlyDictionary = dictionary.ReadOnly();
            Assert.Throws<NotSupportedException>(() => readOnlyDictionary.Clear());
        }

        [Fact]
        public void Remove_ThrowsException()
        {
            MyDictionary<int, string> dictionary = new(3);
            dictionary.Add(2, "Ana");
            dictionary.Add(1, "Any");
            ReadOnlyDictionary<int, string> readOnlyDictionary = dictionary.ReadOnly();
            Assert.Throws<NotSupportedException>(() => readOnlyDictionary.Remove(2));
        }

        [Fact]
        public void Remove_KeyValuePair_ThrowsException()
        {
            MyDictionary<int, string> dictionary = new(3);
            KeyValuePair<int, string> keyValuePair = new(2, "abc");
            dictionary.Add(keyValuePair);
            ReadOnlyDictionary<int, string> readOnlyDictionary = dictionary.ReadOnly();
            Assert.Throws<NotSupportedException>(() => readOnlyDictionary.Remove(keyValuePair));
        }

    }
}