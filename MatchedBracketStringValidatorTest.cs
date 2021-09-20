using Xunit;

namespace code_challenge
{
    public class MatchedBracketStringValidatorTest
    {
        private readonly MatchedBracketStringValidator validator = new();
        
        [Theory]
        [InlineData("Hello I’m [name] and I’m {age} years old!")]
        [InlineData("Hello I’m [(firstname) (lastname)] and I’m {age} years old!")]
        [InlineData("Hello I’m ((firstname) (lastname)) and I’m {age} years old!")]
        public void TestIsValid_IsOK(string input)
        {
            Assert.True(validator.IsValid(input));
        }
        
        [Theory]
        [InlineData("Hello I’m [(firstname) (lastname]) and I’m {age} years old!")]
        [InlineData("Hello I’m ((firstname) (lastname)and I’m {age} years old!")]
        public void TestIsValid_IsNotOK(string input)
        {
            Assert.False(validator.IsValid(input));
        }

        [Theory]
        [InlineData("Hello I’m [(firstname) (lastname]) and I’m {age} years old!")]
        public void TestIsValid_IsNotOK_WithWrongClosingOrderErrorMessage(string input)
        {
            Assert.False(validator.IsValid(input));
            Assert.Equal("Wrong closing order", validator.ErrorMessage);
        }
        
        [Theory]
        [InlineData("Hello I’m ((firstname) (lastname)and I’m {age} years old!")]
        public void TestIsValid_IsNotOK_WithMissingClosingBracketErrorMessage(string input)
        {
            Assert.False(validator.IsValid(input));
            Assert.Equal("Missing closing ')'", validator.ErrorMessage);
        }
    }
}