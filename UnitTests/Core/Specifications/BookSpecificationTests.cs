using Core.Entities;
using Core.Enums;
using Core.Specifications;

namespace UnitTests.Core.Specifications
{
    [TestClass]
    public class BookSpecificationTests
    {
        private List<Book> _books;

        [TestInitialize]
        public void Setup()
        {
            _books = new List<Book>
            {
                new Book { Title = "The Stranger", Genre = Genre.PhilosophicalFiction, Price = 15.99m, Author = new Author { Name = "Albert Camus" } },
                new Book { Title = "One Hundred Years of Solitude", Genre = Genre.Fiction, Price = 20.50m, Author = new Author { Name = "Gabriel García Márquez" } },
                new Book { Title = "1984", Genre = Genre.ScienceFiction, Price = 12.99m, Author = new Author { Name = "George Orwell" } },
                new Book { Title = "The Old Man and the Sea", Genre = Genre.Tragedy, Price = 9.99m, Author = new Author { Name = "Ernest Hemingway" } }
            };
        }

        [TestMethod]
        public void BookSpecification_Should_Filter_By_Title()
        {
            // Arrange
            var specification = new BookSpecification("Stranger", null, null, null);

            // Act
            var filteredBooks = _books.AsQueryable().Where(specification.Criteria).ToList();

            // Assert
            Assert.AreEqual(1, filteredBooks.Count);
            Assert.AreEqual("The Stranger", filteredBooks.First().Title);
            Assert.AreEqual("Albert Camus", filteredBooks.First().Author.Name);
        }

        [TestMethod]
        public void BookSpecification_Should_Filter_By_Genre()
        {
            // Arrange
            var specification = new BookSpecification(null, Genre.PhilosophicalFiction, null, null);

            // Act
            var filteredBooks = _books.AsQueryable().Where(specification.Criteria).ToList();

            // Assert
            Assert.AreEqual(1, filteredBooks.Count);
            Assert.AreEqual("Albert Camus", filteredBooks.First().Author.Name);
        }

        [TestMethod]
        public void BookSpecification_Should_Filter_By_Price_Range()
        {
            // Arrange
            var specification = new BookSpecification(null, null, 10m, 16m);

            // Act
            var filteredBooks = _books.AsQueryable().Where(specification.Criteria).ToList();

            // Assert
            Assert.AreEqual(2, filteredBooks.Count);
            Assert.IsTrue(filteredBooks.Any(b => b.Title == "The Stranger" && b.Author.Name == "Albert Camus"));
            Assert.IsTrue(filteredBooks.Any(b => b.Title == "1984" && b.Author.Name == "George Orwell"));
        }

        [TestMethod]
        public void BookSpecification_Should_Return_All_When_No_Filters_Are_Provided()
        {
            // Arrange
            var specification = new BookSpecification(null, null, null, null);

            // Act
            var filteredBooks = _books.AsQueryable().Where(specification.Criteria).ToList();

            // Assert
            Assert.AreEqual(_books.Count, filteredBooks.Count);
        }
    }
}
