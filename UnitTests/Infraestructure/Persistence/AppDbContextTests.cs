using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infraestructure.Persistence
{
    [TestClass]
    public class AppDbContextTests
    {
        private DbContextOptions<AppDbContext> _options;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [TestMethod]
        public void AppDbContext_Should_Create_DatabaseSchema()
        {
            // Arrange & Act
            using var context = new AppDbContext(_options);
            context.Database.EnsureCreated();

            // Assert
            Assert.IsTrue(context.Authors.Any() == false, "Authors table should exist.");
            Assert.IsTrue(context.Books.Any() == false, "Books table should exist.");
            Assert.IsTrue(context.Publishers.Any() == false, "Publishers table should exist.");
            Assert.IsTrue(context.BookPublishers.Any() == false, "BookPublishers table should exist.");
        }

        [TestMethod]
        public void AppDbContext_Should_Have_IdentityTables()
        {
            // Arrange & Act
            using var context = new AppDbContext(_options);
            context.Database.EnsureCreated();

            // Assert
            var entityTypes = context.Model.GetEntityTypes().Select(e => e.Name).ToList();
            Assert.IsTrue(entityTypes.Contains(typeof(IdentityUser).FullName), "IdentityUser should exist.");
            Assert.IsTrue(entityTypes.Contains(typeof(IdentityRole).FullName), "IdentityRole should exist.");
        }
    }
}
