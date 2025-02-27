using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Services;
using Moq;

namespace UnitTests.Core.Services
{
    [TestClass]
    public class BookServiceTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private BookService _bookService;

        [TestInitialize]
        public void Setup()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapperMock = new Mock<IMapper>();
            _bookService = new BookService(_bookRepositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnMappedBooks()
        {
            // Arrange
            var books = new List<Book> { new Book { Id = 1, Title = "The Stranger" } };
            var bookDTOs = new List<BookDTO> { new BookDTO { Id = 1, Title = "The Stranger" } };

            _bookRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<BookDTO>>(books)).Returns(bookDTOs);

            // Act
            var result = await _bookService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("The Stranger", result.First().Title);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnMappedBook_WhenBookExists()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "The Stranger" };
            var bookDTO = new BookDTO { Id = 1, Title = "The Stranger" };

            _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);
            _mapperMock.Setup(mapper => mapper.Map<BookDTO>(book)).Returns(bookDTO);

            // Act
            var result = await _bookService.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("The Stranger", result.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(BookException))]
        public async Task GetByIdAsync_ShouldThrowException_WhenBookDoesNotExist()
        {
            // Arrange
            _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book)null);

            // Act
            await _bookService.GetByIdAsync(1);
        }

        [TestMethod]
        public async Task AddAsync_ShouldCallRepository_WhenBookIsValid()
        {
            // Arrange
            var bookDTO = new BookDTO { Id = 1, Title = "The Stranger" };
            var book = new Book { Id = 1, Title = "The Stranger" };

            _mapperMock.Setup(mapper => mapper.Map<Book>(bookDTO)).Returns(book);
            _bookRepositoryMock.Setup(repo => repo.AddAsync(book)).Returns(Task.CompletedTask);

            // Act
            await _bookService.AddAsync(bookDTO);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BookException))]
        public async Task AddAsync_ShouldThrowException_WhenBookIsNull()
        {
            // Act
            await _bookService.AddAsync(null);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldCallRepository_WhenBookExists()
        {
            // Arrange
            var bookDTO = new BookDTO { Id = 1, Title = "The Stranger" };
            var book = new Book { Id = 1, Title = "The Stranger" };

            _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);
            _mapperMock.Setup(mapper => mapper.Map(bookDTO, book));
            _bookRepositoryMock.Setup(repo => repo.UpdateAsync(book)).Returns(Task.CompletedTask);

            // Act
            await _bookService.UpdateAsync(bookDTO);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Book>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BookException))]
        public async Task UpdateAsync_ShouldThrowException_WhenBookDoesNotExist()
        {
            // Arrange
            var bookDTO = new BookDTO { Id = 1, Title = "The Stranger" };
            _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book)null);

            // Act
            await _bookService.UpdateAsync(bookDTO);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldCallRepository_WhenBookExists()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "The Stranger" };
            _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);
            _bookRepositoryMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _bookService.DeleteAsync(1);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BookException))]
        public async Task DeleteAsync_ShouldThrowException_WhenBookDoesNotExist()
        {
            // Arrange
            _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book)null);

            // Act
            await _bookService.DeleteAsync(1);
        }
    }
}
