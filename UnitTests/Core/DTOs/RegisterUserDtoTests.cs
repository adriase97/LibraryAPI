using Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Core.DTOs
{
    [TestClass]
    public class RegisterUserDtoTests
    {
        [TestMethod]
        public void RegisterUserDto_ShouldBeInvalid_WhenUserNameIsEmpty()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                UserName = "",
                Email = "user@example.com",
                Password = "ValidPassword123!",
                ConfirmPassword = "ValidPassword123!"
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Exists(x => x.MemberNames.Contains(nameof(dto.UserName))));
        }

        [TestMethod]
        public void RegisterUserDto_ShouldBeInvalid_WhenEmailIsEmpty()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                UserName = "TestUser",
                Email = "",
                Password = "ValidPassword123!",
                ConfirmPassword = "ValidPassword123!"
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Exists(x => x.MemberNames.Contains(nameof(dto.Email))));
        }

        [TestMethod]
        public void RegisterUserDto_ShouldBeInvalid_WhenEmailFormatIsIncorrect()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                UserName = "TestUser",
                Email = "invalid-email",
                Password = "ValidPassword123!",
                ConfirmPassword = "ValidPassword123!"
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Exists(x => x.MemberNames.Contains(nameof(dto.Email))));
        }

        [TestMethod]
        public void RegisterUserDto_ShouldBeInvalid_WhenPasswordIsEmpty()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                UserName = "TestUser",
                Email = "user@example.com",
                Password = "",
                ConfirmPassword = ""
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Exists(x => x.MemberNames.Contains(nameof(dto.Password))));
        }

        [TestMethod]
        public void RegisterUserDto_ShouldBeInvalid_WhenPasswordsDoNotMatch()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                UserName = "TestUser",
                Email = "user@example.com",
                Password = "ValidPassword123!",
                ConfirmPassword = "DifferentPassword456!"
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Exists(x => x.MemberNames.Contains(nameof(dto.ConfirmPassword))));
        }

        [TestMethod]
        public void RegisterUserDto_ShouldBeValid_WhenAllFieldsAreCorrect()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                UserName = "TestUser",
                Email = "user@example.com",
                Password = "ValidPassword123!",
                ConfirmPassword = "ValidPassword123!"
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
