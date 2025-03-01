using Core.Entities;
using Core.Enums;
using Core.Specifications;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infraestructure.Repositories
{
    [TestClass]
    public class AuthorRepositoryTests
    {
        private DbContextOptions<AppDbContext> _options;
        private AppDbContext _context;
        private AuthorRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(_options);
            _repository = new AuthorRepository(_context);

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
                AuthorId = 1
            };

            var author = new Author
            {
                Id = 1,
                Name = "Albert Camus",
                BirthDate = new DateTime(1913, 11, 7),
                Nationality = "French",
                Books = new List<Book> { book }
            };

            var bookPublisher = new BookPublisher
            {
                BookId = 1,
                PublisherId = 1,
                PublishedDate = new DateTime(1942, 1, 1)
            };

            _context.Publishers.Add(publisher);
            _context.Authors.Add(author);
            _context.Books.Add(book);
            _context.BookPublishers.Add(bookPublisher);
            _context.SaveChanges();
        }


        [TestMethod]
        public async Task GetByIdWithIncludesAsync_Should_Return_Author_With_Books_And_Publishers()
        {
            // Act
            var author = await _repository.GetByIdWithIncludesAsync(1);

            // Assert
            Assert.IsNotNull(author);
            Assert.AreEqual("Albert Camus", author.Name);

            // Check books
            Assert.IsNotNull(author.Books);
            Assert.AreEqual(1, author.Books.Count);
            Assert.AreEqual("The Stranger", author.Books.First().Title);

            // Verify that BookPublishers exist
            var bookPublishers = author.Books.First().BookPublishers;
            Assert.IsNotNull(bookPublishers);
            Assert.AreEqual(1, bookPublishers.Count);
            Assert.AreEqual("Gallimard", bookPublishers.First().Publisher.Name);
        }


        [TestMethod]
        public async Task GetAllWithIncludesAsync_Should_Return_All_Authors_With_Books()
        {
            // Act
            var authors = await _repository.GetAllWithIncludesAsync();

            // Assert
            Assert.AreEqual(1, authors.Count());
            Assert.AreEqual("Albert Camus", authors.First().Name);
            Assert.AreEqual(1, authors.First().Books.Count);
        }

        [TestMethod]
        public async Task GetBySpecificationWithIncludesAsync_Should_Return_Filtered_Authors()
        {
            // Arrange
            var specification = new AuthorSpecification("Albert Camus");

            // Act
            var authors = await _repository.GetBySpecificationWithIncludesAsync(specification);

            // Assert
            Assert.IsNotNull(authors);
            Assert.AreEqual(1, authors.Count());
            Assert.AreEqual("Albert Camus", authors.First().Name);
        }

    }
}
