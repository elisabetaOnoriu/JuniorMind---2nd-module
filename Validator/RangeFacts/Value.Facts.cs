using Validator;

namespace ValidatorValue.Facts
{
    public class TestProgram
    {
        [Fact]
        public void IsJsonString()
        {
            Value jsonStringValue = new();
            string text = " \"abc\" ";
            var match = jsonStringValue.Match(text);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void IsJsonString_FalseCase()
        {
            Value jsonStringValue = new();
            string text = " \"abc ";
            var match = jsonStringValue.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }

        [Fact]
        public void IsJsonNumber()
        {
            Value jsonNumberValue = new();
            string text = " 123.4 ";
            var match = jsonNumberValue.Match(text);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void IsJsonNumber_FalseCase()
        {
            Value jsonNumberValue = new();
            string text = " 123e4.4 ";
            var match = jsonNumberValue.Match(text);
            Assert.True(match.Success());
            Assert.Equal(".4 ", match.RemainingText());
        }

        [Theory]
        [InlineData(" true")]
        [InlineData("\t false ")]
        public void IsJsonBooleanValue(string text)
        {
            Value jsonBooleanValue = new();
            var match = jsonBooleanValue.Match(text);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void IsJsonNullValue()
        {
            Value jsonNullValue = new();
            string text = "null";
            var match = jsonNullValue.Match(text);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void IsJsonArray()
        {
            Value jsonArrayValue = new();
            string text = "[ \"abc\\u45B3\", 12.3e+7, true]";
            var match = jsonArrayValue.Match(text);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void IsJsonArray_FalseCase()
        {
            Value jsonArrayValue = new();
            string text = "[ \"abc\\u4g3\", 12.37, true]";
            var match = jsonArrayValue.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }

        [Fact]
        public void IsJsonObject()
        {
            Value jsonArrayValue = new();
            string text = "{\n\"abc\"" + ":" +
                          " \"def\",\t\"valid\"" + ":" + " true}";
            var match = jsonArrayValue.Match(text);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void IsJsonObject_FalseCase()
        {
            Value jsonArrayValue = new();
            string text = "{\n\"abc\"" + ":" +
                          " \"def\",\t\"valid" + ":" + " true}";
            var match = jsonArrayValue.Match(text);
            Assert.False(match.Success());
            Assert.Equal(text, match.RemainingText());
        }
    }
}
