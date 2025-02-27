using Core.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Core.DTOs
{
    [TestClass]
    public class LoginUserDtoTests
    {
        [TestMethod]
        public void LoginUserDto_ShouldBeInvalid_WhenEmailIsEmpty()
        {
            // Arrange
            var dto = new LoginUserDto
            {
                Email = "",
                Password = "ValidPassword123!"
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
        public void LoginUserDto_ShouldBeInvalid_WhenEmailIsNotValidFormat()
        {
            // Arrange
            var dto = new LoginUserDto
            {
                Email = "invalid-email",
                Password = "ValidPassword123!"
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
        public void LoginUserDto_ShouldBeInvalid_WhenPasswordIsEmpty()
        {
            // Arrange
            var dto = new LoginUserDto
            {
                Email = "user@example.com",
                Password = ""
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
        public void LoginUserDto_ShouldBeValid_WhenAllFieldsAreCorrect()
        {
            // Arrange
            var dto = new LoginUserDto
            {
                Email = "user@example.com",
                Password = "ValidPassword123!"
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
