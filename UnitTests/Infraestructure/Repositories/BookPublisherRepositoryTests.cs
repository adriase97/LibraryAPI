using Core.Entities;
using Core.Enums;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infraestructure.Repositories
{
    [TestClass]
    public class BookPublisherRepositoryTests
    {
        private AppDbContext _context;
        private BookPublisherRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // BD única por test
                .Options;

            _context = new AppDbContext(options);
            _repository = new BookPublisherRepository(_context);

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
        public async Task GetByIdAsync_Should_Return_Correct_BookPublisher()
        {
            // Act
            var result = await _repository.GetByIdAsync(1, 1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.BookId);
            Assert.AreEqual(1, result.PublisherId);
            Assert.AreEqual(new DateTime(1942, 1, 1), result.PublishedDate);
        }

        [TestMethod]
        public async Task DeleteByBookOrPublisherAsync_Should_Delete_Correct_Entries()
        {
            // Act
            await _repository.DeleteByBookOrPublisherAsync(bookId: 1);
            var result = await _repository.GetByIdAsync(1, 1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddRangeAsync_Should_Add_BookPublishers()
        {
            // Arrange
            var newBookPublisher = new List<BookPublisher>
            {
                new BookPublisher { BookId = 2, PublisherId = 1, PublishedDate = new DateTime(1951, 1, 1) },
                new BookPublisher { BookId = 3, PublisherId = 1, PublishedDate = new DateTime(1960, 1, 1) }
            };

            // Act
            await _repository.AddRangeAsync(newBookPublisher);
            var count = _context.BookPublishers.Count();

            // Assert
            Assert.AreEqual(3, count);
        }


    }
}
