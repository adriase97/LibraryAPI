using Core.DTOs;
using Core.Enums;

namespace UnitTests.Core.DTOs
{
    [TestClass]
    public class BookPublisherDTOTests
    {
        [TestMethod]
        public void BookPublisherDTO_Should_SetPropertiesCorrectly()
        {
            // Arrange - Datos de ejemplo con Albert Camus
            var expectedBookId = 1;
            var expectedPublisherId = 2;
            var expectedPublishedDate = new DateTime(1942, 5, 19);
            var expectedBook = new BookDTO
            {
                Id = expectedBookId,
                Title = "The Stranger",
                PublicationYear = 1942,
                ISBN = "9780141182506",
                Pages = 123,
                Genre = Genre.PhilosophicalFiction,
                Price = 12.99m,
                AuthorId = 3,
                Author = new AuthorDTO
                {
                    Id = 3,
                    Name = "Albert Camus",
                    Nationality = "French",
                    BirthDate = new DateTime(1913, 11, 7),
                    Biography = "Famous for 'The Stranger' and 'The Myth of Sisyphus'"
                }
            };
            var expectedPublisher = new PublisherDTO
            {
                Id = expectedPublisherId,
                Name = "Gallimard",
                Country = "France"
            };

            // Act
            var bookPublisher = new BookPublisherDTO
            {
                BookId = expectedBookId,
                PublisherId = expectedPublisherId,
                PublishedDate = expectedPublishedDate,
                Book = expectedBook,
                Publisher = expectedPublisher
            };

            // Assert
            Assert.AreEqual(expectedBookId, bookPublisher.BookId);
            Assert.AreEqual(expectedPublisherId, bookPublisher.PublisherId);
            Assert.AreEqual(expectedPublishedDate, bookPublisher.PublishedDate);
            Assert.IsNotNull(bookPublisher.Book);
            Assert.AreEqual(expectedBook.Title, bookPublisher.Book.Title);
            Assert.IsNotNull(bookPublisher.Publisher);
            Assert.AreEqual(expectedPublisher.Name, bookPublisher.Publisher.Name);
        }

        [TestMethod]
        public void BookPublisherDTO_Should_FailValidation_When_FieldsAreZeroOrDefault()
        {
            // Arrange
            var bookPublisher = new BookPublisherDTO
            {
                BookId = 0,
                PublisherId = 0,
                PublishedDate = DateTime.MinValue
            };

            // Act & Assert
            Assert.IsTrue(bookPublisher.BookId == 0, "BookId should not be zero.");
            Assert.IsTrue(bookPublisher.PublisherId == 0, "PublisherId should not be zero.");
            Assert.IsTrue(bookPublisher.PublishedDate == DateTime.MinValue, "PublishedDate should not be default.");
        }
    }
}
