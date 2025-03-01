using Core.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infraestructure.Persistence.Configurations
{
    [TestClass]
    public class AuthorConfigurationTests
    {
        [TestMethod]
        public void AuthorConfiguration_Should_Set_Correct_Constraints()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var modelBuilder = new ModelBuilder();
                var configuration = new AuthorConfiguration();

                // Act
                configuration.Configure(modelBuilder.Entity<Author>());
                var entityType = modelBuilder.Model.FindEntityType(typeof(Author));

                // Assert
                Assert.IsNotNull(entityType);
                Assert.IsTrue(entityType.FindProperty("Name").IsNullable == false);
                Assert.AreEqual(100, entityType.FindProperty("Name").GetMaxLength());

                Assert.AreEqual(50, entityType.FindProperty("Nationality").GetMaxLength());

                Assert.IsTrue(entityType.FindProperty("BirthDate").IsNullable == false);
                Assert.AreEqual(1000, entityType.FindProperty("Biography").GetMaxLength());
            }
        }
    }
}
