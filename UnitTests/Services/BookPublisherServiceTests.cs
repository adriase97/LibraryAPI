using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Services;
using Moq;

namespace UnitTests.Services
{
    [TestClass]
    public class BookPublisherServiceTests
    {
        private Mock<IBookPublisherRepository> _bookPublisherRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private BookPublisherService _bookPublisherService;

        [TestInitialize]
        public void Setup()
        {
            _bookPublisherRepositoryMock = new Mock<IBookPublisherRepository>();
            _mapperMock = new Mock<IMapper>();
            _bookPublisherService = new BookPublisherService(_bookPublisherRepositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnMappedBookPublishers()
        {
            // Arrange
            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher { BookId = 1, PublisherId = 1, PublishedDate = new DateTime(1942, 5, 1) }
            };

            var bookPublisherDTOs = new List<BookPublisherDTO>
            {
                new BookPublisherDTO { BookId = 1, PublisherId = 1, PublishedDate = new DateTime(1942, 5, 1) }
            };

            _bookPublisherRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(bookPublishers);

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<BookPublisherDTO>>(bookPublishers))
                .Returns(bookPublisherDTOs);

            // Act
            var result = await _bookPublisherService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            _bookPublisherRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<BookPublisherDTO>>(bookPublishers), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnMappedBookPublisher_WhenFound()
        {
            // Arrange
            var bookPublisher = new BookPublisher { BookId = 1, PublisherId = 1, PublishedDate = new DateTime(1942, 5, 1) };
            var bookPublisherDTO = new BookPublisherDTO { BookId = 1, PublisherId = 1, PublishedDate = new DateTime(1942, 5, 1) };

            _bookPublisherRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<object[]>()))
                .ReturnsAsync(bookPublisher);

            _mapperMock.Setup(mapper => mapper.Map<BookPublisherDTO>(bookPublisher))
                .Returns(bookPublisherDTO);

            // Act
            var result = await _bookPublisherService.GetByIdAsync(1, 1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.BookId);
            Assert.AreEqual(1, result.PublisherId);
            _bookPublisherRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<object[]>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<BookPublisherDTO>(bookPublisher), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BookPublisherException))]
        public async Task GetByIdAsync_ShouldThrowException_WhenNotFound()
        {
            // Arrange
            _bookPublisherRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<object[]>()))
                .ReturnsAsync((BookPublisher)null);

            // Act
            await _bookPublisherService.GetByIdAsync(1, 1);

            // Assert (Handled by ExpectedException)
        }

        [TestMethod]
        public async Task AddAsync_ShouldCallRepository_WhenValidBookPublisherDTO()
        {
            // Arrange
            var bookPublisherDTO = new BookPublisherDTO { BookId = 1, PublisherId = 1, PublishedDate = new DateTime(1942, 5, 1) };
            var bookPublisher = new BookPublisher { BookId = 1, PublisherId = 1, PublishedDate = new DateTime(1942, 5, 1) };

            _mapperMock.Setup(mapper => mapper.Map<BookPublisher>(bookPublisherDTO))
                .Returns(bookPublisher);

            _bookPublisherRepositoryMock.Setup(repo => repo.AddAsync(bookPublisher))
                .Returns(Task.CompletedTask);

            // Act
            await _bookPublisherService.AddAsync(bookPublisherDTO);

            // Assert
            _mapperMock.Verify(mapper => mapper.Map<BookPublisher>(bookPublisherDTO), Times.Once);
            _bookPublisherRepositoryMock.Verify(repo => repo.AddAsync(bookPublisher), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BookPublisherException))]
        public async Task AddAsync_ShouldThrowException_WhenBookPublisherDTOIsNull()
        {
            // Act
            await _bookPublisherService.AddAsync(null);

            // Assert (Handled by ExpectedException)
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldCallRepository_WhenBookPublisherExists()
        {
            // Arrange
            var bookPublisher = new BookPublisher { BookId = 1, PublisherId = 1, PublishedDate = new DateTime(1942, 5, 1) };

            _bookPublisherRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<object[]>()))
                .ReturnsAsync(bookPublisher);

            _bookPublisherRepositoryMock.Setup(repo => repo.DeleteAsync(bookPublisher))
                .Returns(Task.CompletedTask);

            // Act
            await _bookPublisherService.DeleteAsync(1, 1);

            // Assert
            _bookPublisherRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<object[]>()), Times.Once);
            _bookPublisherRepositoryMock.Verify(repo => repo.DeleteAsync(bookPublisher), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BookPublisherException))]
        public async Task DeleteAsync_ShouldThrowException_WhenBookPublisherNotFound()
        {
            // Arrange
            _bookPublisherRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<object[]>()))
                .ReturnsAsync((BookPublisher)null);

            // Act
            await _bookPublisherService.DeleteAsync(1, 1);

            // Assert (Handled by ExpectedException)
        }

        [TestMethod]
        public async Task DeleteByBookOrPublisherAsync_ShouldCallRepositoryMethod()
        {
            // Arrange
            int bookId = 1;
            int publisherId = 2;

            _bookPublisherRepositoryMock.Setup(repo => repo.DeleteByBookOrPublisherAsync(bookId, publisherId))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            await _bookPublisherService.DeleteByBookOrPublisherAsync(bookId, publisherId);

            // Assert
            _bookPublisherRepositoryMock.Verify(repo => repo.DeleteByBookOrPublisherAsync(bookId, publisherId), Times.Once);
        }

        [TestMethod]
        public async Task AddRangeAsync_ShouldThrowException_WhenBookPublisherDTOsIsNull()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<BookPublisherException>(async () =>
            {
                await _bookPublisherService.AddRangeAsync(null);
            });
        }

        [TestMethod]
        public async Task AddRangeAsync_ShouldCallRepository_WhenValidDataIsProvided()
        {
            // Arrange
            var bookPublisherDTOs = new List<BookPublisherDTO>
            {
                new BookPublisherDTO { BookId = 1, PublisherId = 1 },
                new BookPublisherDTO { BookId = 2, PublisherId = 2 }
            };

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher { BookId = 1, PublisherId = 1 },
                new BookPublisher { BookId = 2, PublisherId = 2 }
            };

            _mapperMock.Setup(m => m.Map<IEnumerable<BookPublisher>>(bookPublisherDTOs))
                .Returns(bookPublishers);

            _bookPublisherRepositoryMock.Setup(repo => repo.AddRangeAsync(bookPublishers))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            await _bookPublisherService.AddRangeAsync(bookPublisherDTOs);

            // Assert
            _bookPublisherRepositoryMock.Verify(repo => repo.AddRangeAsync(bookPublishers), Times.Once);
        }
    }
}
