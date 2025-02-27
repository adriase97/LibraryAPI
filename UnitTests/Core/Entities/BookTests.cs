using Core.Entities;
using Core.Enums;

namespace UnitTests.Core.Entities
{
    [TestClass]
    public class BookTests
    {
        [TestMethod]
        public void Book_Should_Have_DefaultValues_When_Instantiated()
        {
            // Arrange & Act
            var book = new Book();

            // Assert
            Assert.IsNotNull(book.BookPublishers, "BookPublishers collection should be initialized.");
            Assert.AreEqual(string.Empty, book.Title, "Title should be empty by default.");
            Assert.AreEqual(string.Empty, book.ISBN, "ISBN should be empty by default.");
            Assert.AreEqual(default, book.PublicationYear, "PublicationYear should have a default value.");
            Assert.AreEqual(default, book.Pages, "Pages should have a default value.");
            Assert.AreEqual(default, book.Genre, "Genre should have a default value.");
            Assert.AreEqual(default, book.Price, "Price should have a default value.");
            Assert.AreEqual(default, book.AuthorId, "AuthorId should have a default value.");
            Assert.IsNull(book.Author, "Author should be null by default.");
        }

        [TestMethod]
        public void Book_Should_SetProperties_Correctly()
        {
            // Arrange
            var expectedTitle = "The Stranger";
            var expectedPublicationYear = 1942;
            var expectedISBN = "9780141182506";
            var expectedPages = 123;
            var expectedGenre = Genre.Fiction;
            var expectedPrice = 19.99m;
            var expectedAuthorId = 1;
            var expectedAuthor = new Author { Name = "Albert Camus" };
            var bookPublishers = new List<BookPublisher>
        {
            new BookPublisher { PublisherId = 1, PublishedDate = new DateTime(1942, 6, 1) }
        };

            // Act
            var book = new Book
            {
                Title = expectedTitle,
                PublicationYear = expectedPublicationYear,
                ISBN = expectedISBN,
                Pages = expectedPages,
                Genre = expectedGenre,
                Price = expectedPrice,
                AuthorId = expectedAuthorId,
                Author = expectedAuthor,
                BookPublishers = bookPublishers
            };

            // Assert
            Assert.AreEqual(expectedTitle, book.Title, "Title was not set correctly.");
            Assert.AreEqual(expectedPublicationYear, book.PublicationYear, "PublicationYear was not set correctly.");
            Assert.AreEqual(expectedISBN, book.ISBN, "ISBN was not set correctly.");
            Assert.AreEqual(expectedPages, book.Pages, "Pages was not set correctly.");
            Assert.AreEqual(expectedGenre, book.Genre, "Genre was not set correctly.");
            Assert.AreEqual(expectedPrice, book.Price, "Price was not set correctly.");
            Assert.AreEqual(expectedAuthorId, book.AuthorId, "AuthorId was not set correctly.");
            Assert.IsNotNull(book.Author, "Author should not be null.");
            Assert.AreEqual(expectedAuthor.Name, book.Author.Name, "Author name was not set correctly.");
            Assert.AreEqual(1, book.BookPublishers.Count, "BookPublishers collection does not have the expected number of items.");
        }
    }
}
