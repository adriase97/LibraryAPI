using Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Core.DTOs
{
    [TestClass]
    public class ChangePasswordUserDtoTests
    {
        [TestMethod]
        public void ChangePasswordUserDto_ShouldBeInvalid_WhenCurrentPasswordIsEmpty()
        {
            // Arrange
            var dto = new ChangePasswordUserDto
            {
                CurrentPassword = "",
                NewPassword = "NewStrongPass123!"
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Exists(x => x.MemberNames.Contains(nameof(dto.CurrentPassword))));
        }

        [TestMethod]
        public void ChangePasswordUserDto_ShouldBeInvalid_WhenNewPasswordIsEqualToCurrentPassword()
        {
            // Arrange
            var dto = new ChangePasswordUserDto
            {
                CurrentPassword = "SamePassword123!",
                NewPassword = "SamePassword123!"
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Exists(x => x.ErrorMessage.Contains("The new password must be different from the current one.")));
        }


        [TestMethod]
        public void ChangePasswordUserDto_ShouldBeValid_WhenCorrectDataIsProvided()
        {
            // Arrange
            var dto = new ChangePasswordUserDto
            {
                CurrentPassword = "OldPassword123!",
                NewPassword = "NewStrongPass123!"
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
