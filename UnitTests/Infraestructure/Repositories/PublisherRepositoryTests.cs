using Core.Entities;
using Core.Enums;
using Core.Specifications;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infraestructure.Repositories
{
    [TestClass]
    public class PublisherRepositoryTests
    {
        private AppDbContext _context;
        private PublisherRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _repository = new PublisherRepository(_context);

            var author = new Author
            {
                Id = 1,
                Name = "George Orwell",
                BirthDate = new DateTime(1903, 6, 25),
                Nationality = "British"
            };

            var publisher = new Publisher
            {
                Id = 1,
                Name = "Secker & Warburg",
                Country = "United Kingdom",
                FoundedYear = 1935
            };

            var book = new Book
            {
                Id = 1,
                Title = "1984",
                PublicationYear = 1949,
                ISBN = "9780451524935",
                Pages = 328,
                Genre = Genre.ScienceFiction,
                Price = 15.99m,
                AuthorId = 1,
                Author = author
            };

            var bookPublisher = new BookPublisher
            {
                BookId = 1,
                PublisherId = 1,
                PublishedDate = new DateTime(1949, 6, 8),
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
        public async Task GetByIdWithIncludesAsync_Should_Return_Publisher_With_Books_And_Authors()
        {
            // Act
            var result = await _repository.GetByIdWithIncludesAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.IsNotNull(result.BookPublishers);
            Assert.AreEqual(1, result.BookPublishers.Count);
            Assert.AreEqual("1984", result.BookPublishers.First().Book.Title);
            Assert.AreEqual("George Orwell", result.BookPublishers.First().Book.Author.Name);
        }

        [TestMethod]
        public async Task GetAllWithIncludesAsync_Should_Return_All_Publishers_With_Books_And_Authors()
        {
            // Act
            var result = await _repository.GetAllWithIncludesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            var publisher = result.First();
            Assert.AreEqual("Secker & Warburg", publisher.Name);
            Assert.IsNotNull(publisher.BookPublishers);
            Assert.AreEqual(1, publisher.BookPublishers.Count);
            Assert.AreEqual("1984", publisher.BookPublishers.First().Book.Title);
            Assert.AreEqual("George Orwell", publisher.BookPublishers.First().Book.Author.Name);
        }

        [TestMethod]
        public async Task GetBySpecificationWithIncludesAsync_Should_Return_Filtered_Publishers()
        {
            // Arrange
            var spec = new PublisherSpecification("Secker & Warburg", null);

            // Act
            var result = await _repository.GetBySpecificationWithIncludesAsync(spec);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Secker & Warburg", result.First().Name);
        }

    }
}
