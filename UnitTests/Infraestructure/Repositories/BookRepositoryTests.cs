using Core.Entities;
using Core.Enums;
using Core.Specifications;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infraestructure.Repositories
{
    [TestClass]
    public class BookRepositoryTests
    {
        private AppDbContext _context;
        private BookRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // BD única por test
                .Options;

            _context = new AppDbContext(options);
            _repository = new BookRepository(_context);

            var author = new Author
            {
                Id = 1,
                Name = "Albert Camus",
                BirthDate = new DateTime(1913, 11, 7),
                Nationality = "French"
            };

            var publisher = new Publisher
            {
                Id = 1,
                Name = "Gallimard",
                Country = "France",
                FoundedYear = 1911
            };

            var book = new Book
            {
                Id = 1,
                Title = "The Stranger",
                PublicationYear = 1942,
                ISBN = "9780141182506",
                Pages = 123,
                Genre = Genre.Fiction,
                Price = 19.99m,
                AuthorId = 1,
                Author = author
            };

            var bookPublisher = new BookPublisher
            {
                BookId = 1,
                PublisherId = 1,
                PublishedDate = new DateTime(1942, 1, 1),
                Book = book,
                Publisher = publisher
            };

            _context.Authors.Add(author);
            _context.Publishers.Add(publisher);
            _context.Books.Add(book);
            _context.BookPublishers.Add(bookPublisher);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetByIdWithIncludesAsync_Should_Return_Book_With_Author_And_Publishers()
        {
            // Act
            var result = await _repository.GetByIdWithIncludesAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.IsNotNull(result.Author);
            Assert.AreEqual("Albert Camus", result.Author.Name);
            Assert.IsNotNull(result.BookPublishers);
            Assert.AreEqual(1, result.BookPublishers.Count);
            Assert.AreEqual("Gallimard", result.BookPublishers.First().Publisher.Name);
        }

        [TestMethod]
        public async Task GetAllWithIncludesAsync_Should_Return_All_Books_With_Author_And_Publishers()
        {
            // Act
            var result = await _repository.GetAllWithIncludesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            var book = result.First();
            Assert.AreEqual("The Stranger", book.Title);
            Assert.IsNotNull(book.Author);
            Assert.AreEqual("Albert Camus", book.Author.Name);
            Assert.IsNotNull(book.BookPublishers);
            Assert.AreEqual(1, book.BookPublishers.Count);
            Assert.AreEqual("Gallimard", book.BookPublishers.First().Publisher.Name);
        }

        [TestMethod]
        public async Task GetBySpecificationWithIncludesAsync_Should_Return_Filtered_Books()
        {
            // Arrange
            var spec = new BookSpecification("The Stranger", null, null, null);

            // Act
            var result = await _repository.GetBySpecificationWithIncludesAsync(spec);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("The Stranger", result.First().Title);
        }

    }
}
