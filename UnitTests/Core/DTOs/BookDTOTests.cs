using Core.DTOs;
using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Core.DTOs
{
    [TestClass]
    public class BookDTOTests
    {
        [TestMethod]
        public void BookDTO_Should_SetPropertiesCorrectly()
        {
            // Arrange
            var expectedId = 1;
            var expectedTitle = "The Stranger";
            var expectedPublicationYear = 1942;
            var expectedISBN = "9780141182506";
            var expectedPages = 123;
            var expectedGenre = Genre.PhilosophicalFiction;
            var expectedPrice = 12.99m;
            var expectedAuthorId = 2;
            var expectedAuthor = new AuthorDTO
            {
                Id = expectedAuthorId,
                Name = "Albert Camus",
                Nationality = "French",
                BirthDate = new DateTime(1913, 11, 7),
                Biography = "Famous for 'The Stranger' and 'The Myth of Sisyphus'"
            };
            var expectedPublishers = new List<BookPublisherDTO>
            {
                new BookPublisherDTO { PublisherId = 1, BookId = 1, PublishedDate = DateTime.Now }
            };

            // Act
            var book = new BookDTO
            {
                Id = expectedId,
                Title = expectedTitle,
                PublicationYear = expectedPublicationYear,
                ISBN = expectedISBN,
                Pages = expectedPages,
                Genre = expectedGenre,
                Price = expectedPrice,
                AuthorId = expectedAuthorId,
                Author = expectedAuthor,
                BookPublishers = expectedPublishers
            };

            // Assert
            Assert.AreEqual(expectedId, book.Id);
            Assert.AreEqual(expectedTitle, book.Title);
            Assert.AreEqual(expectedPublicationYear, book.PublicationYear);
            Assert.AreEqual(expectedISBN, book.ISBN);
            Assert.AreEqual(expectedPages, book.Pages);
            Assert.AreEqual(expectedGenre, book.Genre);
            Assert.AreEqual(expectedPrice, book.Price);
            Assert.AreEqual(expectedAuthorId, book.AuthorId);
            Assert.IsNotNull(book.Author);
            Assert.AreEqual(expectedAuthor.Name, book.Author.Name);
            CollectionAssert.AreEqual(expectedPublishers, book.BookPublishers);
        }

        [TestMethod]
        public void BookDTO_Should_InitializeBookPublishersList()
        {
            // Act
            var book = new BookDTO();

            // Assert
            Assert.IsNotNull(book.BookPublishers);
            Assert.AreEqual(0, book.BookPublishers.Count);
        }

        [TestMethod]
        public void BookDTO_Should_FailValidation_When_PriceIsZeroOrNegative()
        {
            // Arrange
            var book = new BookDTO
            {
                Title = "The Fall",
                PublicationYear = 1956,
                ISBN = "9780141036599",
                Pages = 147,
                Genre = Genre.ExistentialFiction,
                Price = 0, // Invalid price
                AuthorId = 2
            };

            var context = new ValidationContext(book);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(book, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Exists(r => r.ErrorMessage == "Price must be greater than zero."));
        }
    }
}
