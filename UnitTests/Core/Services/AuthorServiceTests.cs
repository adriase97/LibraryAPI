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
    public class AuthorServiceTests
    {
        private Mock<IAuthorRepository> _mockAuthorRepository;
        private Mock<IMapper> _mockMapper;
        private AuthorService _authorService;

        [TestInitialize]
        public void Setup()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _mockMapper = new Mock<IMapper>();
            _authorService = new AuthorService(_mockAuthorRepository.Object, _mockMapper.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnMappedAuthors()
        {
            // Arrange
            var authors = new List<Author> { new Author { Id = 1, Name = "Albert Camus" } };
            var authorDTOs = new List<AuthorDTO> { new AuthorDTO { Id = 1, Name = "Albert Camus" } };

            _mockAuthorRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(authors);
            _mockMapper.Setup(m => m.Map<IEnumerable<AuthorDTO>>(authors)).Returns(authorDTOs);

            // Act
            var result = await _authorService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Albert Camus", result.First().Name);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnAuthor_WhenExists()
        {
            // Arrange
            var author = new Author { Id = 1, Name = "Albert Camus" };
            var authorDTO = new AuthorDTO { Id = 1, Name = "Albert Camus" };

            _mockAuthorRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(author);
            _mockMapper.Setup(m => m.Map<AuthorDTO>(author)).Returns(authorDTO);

            // Act
            var result = await _authorService.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Albert Camus", result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public async Task GetByIdAsync_ShouldThrowException_WhenAuthorNotFound()
        {
            // Arrange
            _mockAuthorRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Author)null);

            // Act
            await _authorService.GetByIdAsync(1);

            // Assert (Handled by ExpectedException)
        }

        [TestMethod]
        public async Task AddAsync_ShouldCallRepository_WhenValidAuthor()
        {
            // Arrange
            var authorDTO = new AuthorDTO { Id = 1, Name = "Albert Camus" };
            var author = new Author { Id = 1, Name = "Albert Camus" };

            _mockMapper.Setup(m => m.Map<Author>(authorDTO)).Returns(author);
            _mockAuthorRepository.Setup(repo => repo.AddAsync(author)).Returns(Task.CompletedTask);

            // Act
            await _authorService.AddAsync(authorDTO);

            // Assert
            _mockAuthorRepository.Verify(repo => repo.AddAsync(author), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public async Task AddAsync_ShouldThrowException_WhenAuthorDTOIsNull()
        {
            // Act
            await _authorService.AddAsync(null);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldCallRepository_WhenAuthorExists()
        {
            // Arrange
            var authorDTO = new AuthorDTO { Id = 1, Name = "Albert Camus" };
            var author = new Author { Id = 1, Name = "Albert Camus" };

            _mockAuthorRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(author);
            _mockMapper.Setup(mapper => mapper.Map(authorDTO, author));
            _mockAuthorRepository.Setup(repo => repo.UpdateAsync(author)).Returns(Task.CompletedTask);

            // Act
            await _authorService.UpdateAsync(authorDTO);

            // Assert
            _mockAuthorRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Author>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public async Task UpdateAsync_ShouldThrowException_WhenAuthorDoesNotExist()
        {
            // Arrange
            var authorDTO = new AuthorDTO { Id = 1, Name = "Albert Camus" };
            _mockAuthorRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Author)null);

            // Act
            await _authorService.UpdateAsync(authorDTO);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldCallRepository_WhenAuthorExists()
        {
            // Arrange
            var author = new Author { Id = 1, Name = "Albert Camus" };

            _mockAuthorRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(author);
            _mockAuthorRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _authorService.DeleteAsync(1);

            // Assert
            _mockAuthorRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public async Task DeleteAsync_ShouldThrowException_WhenAuthorNotFound()
        {
            // Arrange
            _mockAuthorRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Author)null);

            // Act
            await _authorService.DeleteAsync(1);
        }
    }
}
