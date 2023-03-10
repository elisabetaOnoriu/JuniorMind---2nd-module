using Dictionary;
namespace DictionaryFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData(2, "Ana")]
        [InlineData(1, "Any")]
        public void Add_ChainsNewItemsToBucket(int key, string value)
        {
            MyDictionary<int, string> dictionary = new(3);
            dictionary.Add(2, "Ana");
            dictionary.Add(1, "Any");
            Assert.Equal(value, dictionary[key]);
        }

        [Theory]
        [InlineData(2, "Ana")]
        [InlineData(1, "Any")]
        public void SetIndexProperty_ChainsNewItemsToBucket(int key, string value)
        {
            MyDictionary<int, string> dictionary = new(3);
            dictionary[2] = "Ana";
            dictionary[1] = "Any";
            Assert.Equal(value, dictionary[key]);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void Keys_GetsKeys(int key)
        {
            MyDictionary<int, string> dictionary = new(3);
            dictionary.Add(2, "Ana");
            dictionary.Add(1, "Any");
            var keys = dictionary.Keys;
            Assert.True(keys.Contains(key));
        }

        [Theory]
        [InlineData("Ana")]
        [InlineData("Any")]
        public void Values_GetsValues(string value)
        {
            MyDictionary<int, string> dictionary = new(3);
            dictionary.Add(2, "Ana");
            dictionary.Add(1, "Any");
            var values = dictionary.Values;
            Assert.True(values.Contains(value));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void Clear_ClearsDictionary(int key)
        {
            MyDictionary<int, string> dictionary = new(3);
            dictionary.Add(2, "Ana");
            dictionary.Add(1, "Any");
            dictionary.Clear();
            Assert.Equal(0, dictionary.Count);
            Assert.Throws<KeyNotFoundException>(() => dictionary[key]);
        }

        [Fact]
        public void Enumerator_EnumeratesThroughItems()
        {
            KeyValuePair<int, string> keyValue = new(2, "Ana");
            KeyValuePair<int, string> keyValue2 = new(1, "Any");
            KeyValuePair<int, string> keyValue3 = new(6, "Ema");
            MyDictionary<int, string> dictionary = new(3) { keyValue, keyValue2, keyValue3 };
            var enumerator = dictionary.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(dictionary[2], enumerator.Current.Value);
            enumerator.MoveNext();
            Assert.Equal(dictionary[1], enumerator.Current.Value);
            enumerator.MoveNext();
            Assert.Equal(dictionary[6], enumerator.Current.Value);
        }
    }
}