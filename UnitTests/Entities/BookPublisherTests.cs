using Core.Entities;

namespace UnitTests.Entities
{
    [TestClass]
    public class BookPublisherTests
    {
        [TestMethod]
        public void BookPublisher_Should_Have_DefaultValues_When_Instantiated()
        {
            // Arrange & Act
            var bookPublisher = new BookPublisher();

            // Assert
            Assert.AreEqual(default(int), bookPublisher.BookId, "BookId should have a default value.");
            Assert.AreEqual(default(int), bookPublisher.PublisherId, "PublisherId should have a default value.");
            Assert.AreEqual(default(DateTime), bookPublisher.PublishedDate, "PublishedDate should have a default value.");
            Assert.IsNull(bookPublisher.Book, "Book should be null by default.");
            Assert.IsNull(bookPublisher.Publisher, "Publisher should be null by default.");
        }

        [TestMethod]
        public void BookPublisher_Should_SetProperties_Correctly()
        {
            // Arrange
            var expectedBookId = 1;
            var expectedPublisherId = 2;
            var expectedPublishedDate = new DateTime(1942, 6, 1);
            var expectedBook = new Book { Title = "The Stranger", Author = new Author { Name = "Albert Camus" } };
            var expectedPublisher = new Publisher { Name = "Gallimard" };

            // Act
            var bookPublisher = new BookPublisher
            {
                BookId = expectedBookId,
                PublisherId = expectedPublisherId,
                PublishedDate = expectedPublishedDate,
                Book = expectedBook,
                Publisher = expectedPublisher
            };

            // Assert
            Assert.AreEqual(expectedBookId, bookPublisher.BookId, "BookId was not set correctly.");
            Assert.AreEqual(expectedPublisherId, bookPublisher.PublisherId, "PublisherId was not set correctly.");
            Assert.AreEqual(expectedPublishedDate, bookPublisher.PublishedDate, "PublishedDate was not set correctly.");
            Assert.IsNotNull(bookPublisher.Book, "Book should not be null.");
            Assert.AreEqual("The Stranger", bookPublisher.Book.Title, "Book title was not set correctly.");
            Assert.AreEqual("Albert Camus", bookPublisher.Book.Author.Name, "Book author was not set correctly.");
            Assert.IsNotNull(bookPublisher.Publisher, "Publisher should not be null.");
            Assert.AreEqual("Gallimard", bookPublisher.Publisher.Name, "Publisher name was not set correctly.");
        }
    }
}
