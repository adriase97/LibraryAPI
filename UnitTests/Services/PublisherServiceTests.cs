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
    public class PublisherServiceTests
    {
        private Mock<IPublisherRepository> _publisherRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private PublisherService _publisherService;

        [TestInitialize]
        public void Setup()
        {
            _publisherRepositoryMock = new Mock<IPublisherRepository>();
            _mapperMock = new Mock<IMapper>();
            _publisherService = new PublisherService(
                _publisherRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnMappedPublishers()
        {
            // Arrange
            var publishers = new List<Publisher> { new Publisher { Id = 1, Name = "Penguin" } };
            var publisherDTOs = new List<PublisherDTO> { new PublisherDTO { Id = 1, Name = "Penguin" } };

            _publisherRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(publishers);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<PublisherDTO>>(publishers)).Returns(publisherDTOs);

            // Act
            var result = await _publisherService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Penguin", result.First().Name);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnMappedPublisher_WhenPublisherExists()
        {
            // Arrange
            var publisher = new Publisher { Id = 1, Name = "Penguin" };
            var publisherDTO = new PublisherDTO { Id = 1, Name = "Penguin" };

            _publisherRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(publisher);
            _mapperMock.Setup(mapper => mapper.Map<PublisherDTO>(publisher)).Returns(publisherDTO);

            // Act
            var result = await _publisherService.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Penguin", result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(PublisherException))]
        public async Task GetByIdAsync_ShouldThrowException_WhenPublisherDoesNotExist()
        {
            // Arrange
            _publisherRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Publisher)null);

            // Act
            await _publisherService.GetByIdAsync(1);
        }

        [TestMethod]
        public async Task AddAsync_ShouldCallRepository_WhenPublisherIsValid()
        {
            // Arrange
            var publisherDTO = new PublisherDTO { Id = 1, Name = "Penguin" };
            var publisher = new Publisher { Id = 1, Name = "Penguin" };

            _mapperMock.Setup(mapper => mapper.Map<Publisher>(publisherDTO)).Returns(publisher);
            _publisherRepositoryMock.Setup(repo => repo.AddAsync(publisher)).Returns(Task.CompletedTask);

            // Act
            await _publisherService.AddAsync(publisherDTO);

            // Assert
            _publisherRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Publisher>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(PublisherException))]
        public async Task AddAsync_ShouldThrowException_WhenPublisherIsNull()
        {
            // Act
            await _publisherService.AddAsync(null);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldCallRepository_WhenPublisherExists()
        {
            // Arrange
            var publisherDTO = new PublisherDTO
            {
                Id = 1,
                Name = "Penguin"                
            };

            var publisher = new Publisher { Id = 1, Name = "Penguin" };

            _mapperMock.Setup(mapper => mapper.Map<Publisher>(publisherDTO)).Returns(publisher);
            _publisherRepositoryMock.Setup(repo => repo.UpdateAsync(publisher)).Returns(Task.CompletedTask);

            // Act
            await _publisherService.UpdateAsync(publisherDTO);

            // Assert
            _publisherRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Publisher>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(PublisherException))]
        public async Task UpdateAsync_ShouldThrowException_WhenPublisherIsNull()
        {
            // Act
            await _publisherService.UpdateAsync(null);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldCallRepository_WhenPublisherExists()
        {
            // Arrange
            var publisher = new Publisher { Id = 1, Name = "Penguin" };
            _publisherRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(publisher);
            _publisherRepositoryMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _publisherService.DeleteAsync(1);

            // Assert
            _publisherRepositoryMock.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(PublisherException))]
        public async Task DeleteAsync_ShouldThrowException_WhenPublisherDoesNotExist()
        {
            // Arrange
            _publisherRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Publisher)null);

            // Act
            await _publisherService.DeleteAsync(1);
        }
    }
}
