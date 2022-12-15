namespace Validator.NumberFacts
{
    public class TestProgram
    {
        [Theory]
        [InlineData("-0")]
        [InlineData("1234")]
        public void IntegerIsValid_TrueCases(string integer)
        {
            var number = new Number();
            var match = number.Match(integer);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void IntegerIsValid_IsDigit_FalseCases()
        {
            var number = new Number();
            var match = number.Match("a");
            Assert.False(match.Success());
            Assert.Equal("a", match.RemainingText());
        }

        [Fact]
        public void IntegerIsValid_DoesNotConsumeWholeText_TrueCase()
        {
            var number = new Number();
            var match = number.Match("07");
            Assert.True(match.Success());
            Assert.Equal("7", match.RemainingText());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NumberIsValid_FalseCases(string integer)
        {
            var number = new Number();
            var match = number.Match(integer);
            Assert.False(match.Success());
            Assert.Equal(integer, match.RemainingText());
        }

        [Theory]
        [InlineData("12.34")]
        [InlineData("0.000034")]
        public void FractionIsValid_TrueCases(string fraction)
        {
            var number = new Number();
            var match = number.Match(fraction);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Fact]
        public void FractionIsValid_FractionIsComplete_FalseCase()
        {
            var number = new Number();
            var match = number.Match("12.");
            Assert.True(match.Success());
            Assert.Equal(".", match.RemainingText());
        }

        [Fact]
        [InlineData()]
        public void FractionIsValid_FalseCases()
        {
            var number = new Number();
            var match = number.Match("12.34.56");
            Assert.True(match.Success());
            Assert.Equal(".56", match.RemainingText());
        }

        [Fact]
        public void FractionIsValid_DoesNotContainLetters_FalseCase()
        {
            var number = new Number();
            var match = number.Match("12.3x");
            Assert.True(match.Success());
            Assert.Equal("x", match.RemainingText());
        }

        [Theory]
        [InlineData("12e3")]
        [InlineData("12E3")]
        public void ExponentIsValid_TrueCases(string fraction)
        {
            var number = new Number();
            var match = number.Match(fraction);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Theory]
        [InlineData("12e-3")]
        [InlineData("12E+3")]
        public void ExponentIsValid_PlusMinusCases_TrueCases(string fraction)
        {
            var number = new Number();
            var match = number.Match(fraction);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }
        
        [Theory]
        [InlineData("22e3x3")]
        [InlineData("22e3e22")]
        public void ExponentIsValid_FalseCases(string exponent)
        {
            var number = new Number();
            var match = number.Match(exponent);
            Assert.True(match.Success());
            Assert.Equal(exponent[4..], match.RemainingText());
        }

        [Theory]
        [InlineData("22e")]
        [InlineData("22e+")]
        [InlineData("23E-")]
        public void ExponentIsAlwaysComplete_FalseCases(string exponent)
        {
            var number = new Number();
            var match = number.Match(exponent);
            Assert.True(match.Success());
            Assert.Equal(exponent[2..], match.RemainingText());
        }

        [Theory]
        [InlineData("12.34e3")]
        [InlineData("12.5E4")]
        public void FractionAndExponentAreValid_TrueCases(string fraction)
        {
            var number = new Number();
            var match = number.Match(fraction);
            Assert.True(match.Success());
            Assert.Equal("", match.RemainingText());
        }

        [Theory]
        [InlineData("12e3.3")]
        [InlineData("1E2.25")]
        public void FractionAndExponentAreValid_TheExponentIsAfterTheFraction_FalseCases(string fractionExponent)
        {
            var number = new Number();
            var match = number.Match(fractionExponent);
            Assert.True(match.Success());
            Assert.Equal(fractionExponent[fractionExponent.IndexOf('.')..], match.RemainingText());
        }
    }
}
