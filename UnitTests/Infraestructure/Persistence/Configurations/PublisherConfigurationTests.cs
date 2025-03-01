using Core.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infraestructure.Persistence.Configurations
{
    [TestClass]
    public class PublisherConfigurationTests
    {
        [TestMethod]
        public void PublisherConfiguration_Should_Set_Correct_Constraints()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var modelBuilder = new ModelBuilder();
                var configuration = new PublisherConfiguration();

                // Act
                configuration.Configure(modelBuilder.Entity<Publisher>());
                var entityType = modelBuilder.Model.FindEntityType(typeof(Publisher));

                // Assert
                Assert.IsNotNull(entityType);

                // Verify primary key
                var primaryKey = entityType.FindPrimaryKey();
                Assert.IsNotNull(primaryKey);
                Assert.AreEqual("Id", primaryKey.Properties[0].Name);

                // Check property restrictions
                Assert.IsFalse(entityType.FindProperty("Name").IsNullable);
                Assert.AreEqual(150, entityType.FindProperty("Name").GetMaxLength());

                Assert.IsTrue(entityType.FindProperty("Country").IsNullable);
                Assert.AreEqual(100, entityType.FindProperty("Country").GetMaxLength());

                Assert.IsFalse(entityType.FindProperty("FoundedYear").IsNullable);

                Assert.IsTrue(entityType.FindProperty("Website").IsNullable);
                Assert.AreEqual(255, entityType.FindProperty("Website").GetMaxLength());

                // Check relationships
                var bookPublisherNavigation = entityType.GetNavigations().FirstOrDefault(n => n.Name == "BookPublishers");
                Assert.IsNotNull(bookPublisherNavigation);
                Assert.IsTrue(bookPublisherNavigation.IsCollection);
            }
        }
    }
}
