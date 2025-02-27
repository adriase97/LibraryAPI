using Core.Entities;

namespace UnitTests.Entities
{
    [TestClass]
    public class AuthorTests
    {
        [TestMethod]
        public void Author_Should_Have_DefaultValues_When_Instantiated()
        {
            // Arrange & Act
            var author = new Author();

            // Assert
            Assert.IsNotNull(author.Books, "Books collection should be initialized.");
            Assert.AreEqual(string.Empty, author.Name, "Name should be empty by default.");
            Assert.AreEqual(string.Empty, author.Nationality, "Nationality should be empty by default.");
            Assert.AreEqual(string.Empty, author.Biography, "Biography should be empty by default.");
            Assert.AreEqual(default(DateTime), author.BirthDate, "BirthDate should have a default value.");
        }

        [TestMethod]
        public void Author_Should_SetProperties_Correctly()
        {
            // Arrange
            var expectedName = "Albert Camus";
            var expectedNationality = "French";
            var expectedBirthDate = new DateTime(1913, 11, 7);
            var expectedBiography = "Author of 'The Stranger' and 'The Plague'";
            var books = new List<Book>
        {
            new Book { Title = "The Stranger" },
            new Book { Title = "The Plague" }
        };

            // Act
            var author = new Author
            {
                Name = expectedName,
                Nationality = expectedNationality,
                BirthDate = expectedBirthDate,
                Biography = expectedBiography,
                Books = books
            };

            // Assert
            Assert.AreEqual(expectedName, author.Name, "Name was not set correctly.");
            Assert.AreEqual(expectedNationality, author.Nationality, "Nationality was not set correctly.");
            Assert.AreEqual(expectedBirthDate, author.BirthDate, "BirthDate was not set correctly.");
            Assert.AreEqual(expectedBiography, author.Biography, "Biography was not set correctly.");
            Assert.AreEqual(2, author.Books.Count, "Books collection does not have the expected number of items.");
        }
    }
}
