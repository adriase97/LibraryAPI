using Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Core.DTOs
{
    [TestClass]
    public class PublisherDTOTests
    {
        [TestMethod]
        public void PublisherDTO_Should_FailValidation_When_RequiredFieldsMissing()
        {
            // Arrange
            var publisher = new PublisherDTO();
            var context = new ValidationContext(publisher);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(publisher, context, results, true);

            // Assert
            Assert.IsTrue(results.Exists(r => r.MemberNames.Contains("Name")), "Name is required.");
            Assert.IsTrue(results.Exists(r => r.MemberNames.Contains("Country")), "Country is required.");
            Assert.IsTrue(results.Exists(r => r.MemberNames.Contains("Website")), "Website is required.");
        }

        [TestMethod]
        public void PublisherDTO_Should_PassValidation_When_AllFieldsAreValid()
        {
            // Arrange
            var publisher = new PublisherDTO
            {
                Id = 1,
                Name = "Gallimard",
                Country = "France",
                FoundedYear = 1911,
                Website = "https://www.gallimard.fr",
                BookPublishers = new List<BookPublisherDTO>
                {
                    new BookPublisherDTO { BookId = 1, PublisherId = 1, PublishedDate = new DateTime(1942, 6, 15) }
                }
            };

            var context = new ValidationContext(publisher);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(publisher, context, results, true);

            // Assert
            Assert.IsTrue(isValid, "Validation should pass when all required fields are provided.");
        }
    }
}
