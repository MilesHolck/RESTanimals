using RESTanimals.Models;

namespace RESTanimalsTests
{
    public class AnimalTests
    {
        [Fact]
        public void Validate_ValidAnimal_DoesNotThrow()
        {
            //Arrange
            Animal a = new Animal { Name = "Fido", Age = 3, PrimaryColor = "Brun" };
            //Act & Assert
            a.Validate(); // kaster den, fejler testen automatisk
        }

        [Fact]
        public void ValidateName_Null_Throws()
        {
            Animal a = new Animal { Name = null, Age = 3, PrimaryColor = "Brun" };
            Assert.Throws<ArgumentNullException>(() => a.ValidateName());
        }

        [Fact]
        public void ValidateName_TooShort_Throws()
        {
            Animal a = new Animal { Name = "F", Age = 3, PrimaryColor = "Brun" };
            Assert.Throws<ArgumentException>(() => a.ValidateName());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(20)]
        public void ValidateAge_ZeroOrPositive_DoesNotThrow(int age)
        {
            Animal a = new Animal { Name = "Fido", Age = age, PrimaryColor = "Brun" };
            a.ValidateAge();
        }

        [Fact]
        public void ValidateAge_Negative_Throws()
        {
            Animal a = new Animal { Name = "Fido", Age = -1, PrimaryColor = "Brun" };
            Assert.Throws<ArgumentOutOfRangeException>(() => a.ValidateAge());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ValidatePrimaryColor_Empty_Throws(string? color)
        {
            Animal a = new Animal { Name = "Fido", Age = 3, PrimaryColor = color };
            Assert.Throws<ArgumentException>(() => a.ValidatePrimaryColor());
        }

        [Fact]
        public void ToString_ContainsAllProperties()
        {
            Animal a = new Animal { Id = 1, Name = "Fido", Age = 3, PrimaryColor = "Brun" };
            Assert.Equal("Animal Id: 1, Name: Fido, Age: 3, PrimaryColor: Brun", a.ToString());
        }
    }
}
