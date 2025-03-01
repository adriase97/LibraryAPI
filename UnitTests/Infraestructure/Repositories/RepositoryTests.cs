using Core.Entities;
using Core.Specifications;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infraestructure.Repositories
{
    [TestClass]
    public class RepositoryTests
    {
        private AppDbContext _context;
        private Repository<Author> _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _repository = new Repository<Author>(_context);

            var authors = new List<Author>
            {
                new Author { Id = 1, Name = "George Orwell", BirthDate = new DateTime(1903, 6, 25), Nationality = "British" },
                new Author { Id = 2, Name = "Aldous Huxley", BirthDate = new DateTime(1894, 7, 26), Nationality = "British" }
            };

            _context.Authors.AddRange(authors);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task GetByIdAsync_Should_Return_Entity()
        {
            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("George Orwell", result.Name);
        }

        [TestMethod]
        public async Task GetAllAsync_Should_Return_All_Entities()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetBySpecificationAsync_Should_Return_Filtered_Entities()
        {
            // Arrange
            var spec = new AuthorSpecification("George Orwell");

            // Act
            var result = await _repository.GetBySpecificationAsync(spec);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("George Orwell", result.First().Name);
        }

        [TestMethod]
        public async Task AddAsync_Should_Add_New_Entity()
        {
            // Arrange
            var newAuthor = new Author { Id = 3, Name = "Ray Bradbury", BirthDate = new DateTime(1920, 8, 22), Nationality = "American" };

            // Act
            await _repository.AddAsync(newAuthor);
            var result = await _repository.GetByIdAsync(3);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Ray Bradbury", result.Name);
        }

        [TestMethod]
        public async Task UpdateAsync_Should_Update_Entity()
        {
            // Arrange
            var author = await _repository.GetByIdAsync(1);
            author.Name = "George Orwell Updated";

            // Act
            await _repository.UpdateAsync(author);
            var updatedAuthor = await _repository.GetByIdAsync(1);

            // Assert
            Assert.AreEqual("George Orwell Updated", updatedAuthor.Name);
        }

        [TestMethod]
        public async Task DeleteAsync_ById_Should_Remove_Entity()
        {
            // Act
            await _repository.DeleteAsync(1);
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task DeleteAsync_ByEntity_Should_Remove_Entity()
        {
            // Arrange
            var author = await _repository.GetByIdAsync(2);

            // Act
            await _repository.DeleteAsync(author);
            var result = await _repository.GetByIdAsync(2);

            // Assert
            Assert.IsNull(result);
        }

    }
}
