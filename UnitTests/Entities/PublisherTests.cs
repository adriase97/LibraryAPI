using Core.Entities;

namespace UnitTests.Entities
{
    [TestClass]
    public class PublisherTests
    {
        [TestMethod]
        public void Publisher_Should_Have_DefaultValues_When_Instantiated()
        {
            // Arrange & Act
            var publisher = new Publisher();

            // Assert
            Assert.AreEqual(string.Empty, publisher.Name, "Name should be empty by default.");
            Assert.AreEqual(string.Empty, publisher.Country, "Country should be empty by default.");
            Assert.AreEqual(default(int), publisher.FoundedYear, "FoundedYear should have a default value of 0.");
            Assert.AreEqual(string.Empty, publisher.Website, "Website should be empty by default.");
            Assert.IsNotNull(publisher.BookPublishers, "BookPublishers collection should not be null.");
            Assert.AreEqual(0, publisher.BookPublishers.Count, "BookPublishers collection should be empty by default.");
        }

        [TestMethod]
        public void Publisher_Should_SetProperties_Correctly()
        {
            // Arrange
            var expectedName = "Gallimard";
            var expectedCountry = "France";
            var expectedFoundedYear = 1911;
            var expectedWebsite = "https://www.gallimard.fr";

            // Act
            var publisher = new Publisher
            {
                Name = expectedName,
                Country = expectedCountry,
                FoundedYear = expectedFoundedYear,
                Website = expectedWebsite
            };

            // Assert
            Assert.AreEqual(expectedName, publisher.Name, "Name was not set correctly.");
            Assert.AreEqual(expectedCountry, publisher.Country, "Country was not set correctly.");
            Assert.AreEqual(expectedFoundedYear, publisher.FoundedYear, "FoundedYear was not set correctly.");
            Assert.AreEqual(expectedWebsite, publisher.Website, "Website was not set correctly.");
        }

        [TestMethod]
        public void Publisher_Should_Allow_Adding_BookPublishers()
        {
            // Arrange
            var publisher = new Publisher { Name = "Gallimard" };
            var bookPublisher = new BookPublisher
            {
                BookId = 1,
                PublisherId = 1,
                PublishedDate = new DateTime(1942, 6, 1),
                Book = new Book { Title = "The Stranger", Author = new Author { Name = "Albert Camus" } }
            };

            // Act
            publisher.BookPublishers.Add(bookPublisher);

            // Assert
            Assert.AreEqual(1, publisher.BookPublishers.Count, "BookPublishers collection should have one item.");
            Assert.AreEqual("The Stranger", publisher.BookPublishers.First().Book.Title, "Book title was not set correctly.");
            Assert.AreEqual("Albert Camus", publisher.BookPublishers.First().Book.Author.Name, "Book author was not set correctly.");
        }
    }
}
