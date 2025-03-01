using Core.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infraestructure.Persistence.Configurations
{
    [TestClass]
    public class BookPublisherConfigurationTests
    {
        [TestMethod]
        public void BookPublisherConfiguration_Should_Set_Correct_Constraints()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var modelBuilder = new ModelBuilder();
                var configuration = new BookPublisherConfiguration();

                // Act
                configuration.Configure(modelBuilder.Entity<BookPublisher>());
                var entityType = modelBuilder.Model.FindEntityType(typeof(BookPublisher));

                // Assert
                Assert.IsNotNull(entityType);

                // Composite Primary Key
                var primaryKey = entityType.FindPrimaryKey();
                Assert.IsNotNull(primaryKey);
                Assert.AreEqual(2, primaryKey.Properties.Count);
                Assert.IsTrue(primaryKey.Properties.Any(p => p.Name == "BookId"));
                Assert.IsTrue(primaryKey.Properties.Any(p => p.Name == "PublisherId"));

                // Properties
                Assert.IsTrue(entityType.FindProperty("PublishedDate").IsNullable == false);

                // Relations
                var bookForeignKey = entityType.GetForeignKeys().FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(Book));
                Assert.IsNotNull(bookForeignKey);
                Assert.AreEqual(DeleteBehavior.Cascade, bookForeignKey.DeleteBehavior);

                var publisherForeignKey = entityType.GetForeignKeys().FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(Publisher));
                Assert.IsNotNull(publisherForeignKey);
                Assert.AreEqual(DeleteBehavior.Cascade, publisherForeignKey.DeleteBehavior);
            }
        }
    }
}
