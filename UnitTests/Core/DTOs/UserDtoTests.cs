using Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Core.DTOs
{
    [TestClass]
    public class UserDtoTests
    {
        [TestMethod]
        public void UserDto_ShouldBeValid_WhenAllFieldsAreCorrect()
        {
            // Arrange
            var dto = new UserDto
            {
                Id = "123",
                UserName = "TestUser",
                Email = "test@example.com"
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.AreEqual(0, results.Count);
        }
    }

}
