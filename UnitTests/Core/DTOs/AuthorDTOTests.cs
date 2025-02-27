using Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Core.DTOs
{
    [TestClass]
    public class AuthorDTOTests
    {
        [TestMethod]
        public void AuthorDTO_Should_SetPropertiesCorrectly()
        {
            // Arrange
            var expectedId = 1;
            var expectedName = "Albert Camus";
            var expectedNationality = "French";
            var expectedBirthDate = new DateTime(1913, 11, 7);
            var expectedBiography = "Famous for 'The Stranger' and 'The Myth of Sisyphus'";
            var expectedBooks = new List<BookDTO>
            {
                new BookDTO { Id = 1, Title = "The Stranger" },
                new BookDTO { Id = 2, Title = "The Myth of Sisyphus" }
            };

            // Act
            var author = new AuthorDTO
            {
                Id = expectedId,
                Name = expectedName,
                Nationality = expectedNationality,
                BirthDate = expectedBirthDate,
                Biography = expectedBiography,
                Books = expectedBooks
            };

            // Assert
            Assert.AreEqual(expectedId, author.Id);
            Assert.AreEqual(expectedName, author.Name);
            Assert.AreEqual(expectedNationality, author.Nationality);
            Assert.AreEqual(expectedBirthDate, author.BirthDate);
            Assert.AreEqual(expectedBiography, author.Biography);
            CollectionAssert.AreEqual(expectedBooks, author.Books);
        }

        [TestMethod]
        public void AuthorDTO_Should_InitializeBooksList()
        {
            // Act
            var author = new AuthorDTO();

            // Assert
            Assert.IsNotNull(author.Books);
            Assert.AreEqual(0, author.Books.Count);
        }

        [TestMethod]
        public void AuthorDTO_Should_FailValidation_When_RequiredFieldsMissing()
        {
            // Arrange
            var author = new AuthorDTO();
            var context = new ValidationContext(author);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(author, context, results, true);

            // Assert
            Assert.IsTrue(results.Exists(r => r.MemberNames.Contains("Name")), "Name is required.");
            Assert.IsTrue(results.Exists(r => r.MemberNames.Contains("Nationality")), "Nationality is required.");
            Assert.IsTrue(results.Exists(r => r.MemberNames.Contains("Biography")), "Biography is required.");
        }
    }
}
