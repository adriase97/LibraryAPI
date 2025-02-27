using Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Core.DTOs
{
    [TestClass]
    public class ReplaceClaimDtoTests
    {
        [TestMethod]
        public void ReplaceClaimDto_ShouldBeInvalid_WhenOldTypeIsEmpty()
        {
            // Arrange
            var dto = new ReplaceClaimDTO
            {
                OldType = "",
                OldValue = "OldValue123",
                NewType = "NewType123",
                NewValue = "NewValue123"
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Exists(x => x.MemberNames.Contains(nameof(dto.OldType))));
        }

        [TestMethod]
        public void ReplaceClaimDto_ShouldBeInvalid_WhenOldValueIsEmpty()
        {
            // Arrange
            var dto = new ReplaceClaimDTO
            {
                OldType = "OldType123",
                OldValue = "",
                NewType = "NewType123",
                NewValue = "NewValue123"
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Exists(x => x.MemberNames.Contains(nameof(dto.OldValue))));
        }

        [TestMethod]
        public void ReplaceClaimDto_ShouldBeInvalid_WhenNewTypeIsEmpty()
        {
            // Arrange
            var dto = new ReplaceClaimDTO
            {
                OldType = "OldType123",
                OldValue = "OldValue123",
                NewType = "",
                NewValue = "NewValue123"
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Exists(x => x.MemberNames.Contains(nameof(dto.NewType))));
        }

        [TestMethod]
        public void ReplaceClaimDto_ShouldBeInvalid_WhenNewValueIsEmpty()
        {
            // Arrange
            var dto = new ReplaceClaimDTO
            {
                OldType = "OldType123",
                OldValue = "OldValue123",
                NewType = "NewType123",
                NewValue = ""
            };

            // Act
            var validationContext = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, results, true);

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Exists(x => x.MemberNames.Contains(nameof(dto.NewValue))));
        }

        [TestMethod]
        public void ReplaceClaimDto_ShouldBeValid_WhenAllFieldsAreCorrect()
        {
            // Arrange
            var dto = new ReplaceClaimDTO
            {
                OldType = "OldType123",
                OldValue = "OldValue123",
                NewType = "NewType123",
                NewValue = "NewValue123"
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
