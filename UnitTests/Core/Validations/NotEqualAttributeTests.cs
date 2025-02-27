using Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Core.Validations
{
    [TestClass]
    public class NotEqualAttributeTests
    {
        private class TestModel
        {
            public string OtherProperty { get; set; }

            [NotEqual("OtherProperty")]
            public string NewPassword { get; set; }
        }

        private bool IsModelValid(TestModel model, out List<ValidationResult> results)
        {
            var context = new ValidationContext(model, null, null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, results, true);
        }

        [TestMethod]
        public void NotEqualAttribute_ShouldBeInvalid_WhenValuesAreEqual()
        {
            // Arrange
            var model = new TestModel
            {
                OtherProperty = "SamePassword",
                NewPassword = "SamePassword"
            };

            // Act
            var isValid = IsModelValid(model, out var results);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Any(r => r.ErrorMessage == "The new password must be different from the current one."));
        }

        [TestMethod]
        public void NotEqualAttribute_ShouldBeValid_WhenValuesAreDifferent()
        {
            // Arrange
            var model = new TestModel
            {
                OtherProperty = "OldPassword",
                NewPassword = "NewPassword"
            };

            // Act
            var isValid = IsModelValid(model, out var results);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void NotEqualAttribute_ShouldBeValid_WhenNewPasswordIsNull()
        {
            // Arrange
            var model = new TestModel
            {
                OtherProperty = "OldPassword",
                NewPassword = null
            };

            // Act
            var isValid = IsModelValid(model, out var results);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NotEqualAttribute_ShouldBeValid_WhenOtherPropertyIsNull()
        {
            // Arrange
            var model = new TestModel
            {
                OtherProperty = null,
                NewPassword = "NewPassword"
            };

            // Act
            var isValid = IsModelValid(model, out var results);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NotEqualAttribute_ShouldThrowError_WhenPropertyDoesNotExist()
        {
            // Arrange
            var attribute = new NotEqualAttribute("NonExistentProperty");
            var model = new TestModel { NewPassword = "NewPassword" };
            var context = new ValidationContext(model);

            // Act
            var result = attribute.GetValidationResult(model.NewPassword, context);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Property NonExistentProperty not found.", result.ErrorMessage);
        }
    }

}
