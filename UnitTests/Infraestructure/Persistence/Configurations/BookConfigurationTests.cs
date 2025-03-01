using Core.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infraestructure.Persistence.Configurations
{
    [TestClass]
    public class BookConfigurationTests
    {
        [TestMethod]
        public void BookConfiguration_Should_Set_Correct_Constraints()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var modelBuilder = new ModelBuilder();
                var configuration = new BookConfiguration();

                // Act
                configuration.Configure(modelBuilder.Entity<Book>());
                var entityType = modelBuilder.Model.FindEntityType(typeof(Book));

                // Assert
                Assert.IsNotNull(entityType);

                // Properties
                Assert.IsTrue(entityType.FindProperty("Title").IsNullable == false);
                Assert.AreEqual(200, entityType.FindProperty("Title").GetMaxLength());

                Assert.IsTrue(entityType.FindProperty("PublicationYear").IsNullable == false);
                Assert.IsTrue(entityType.FindProperty("ISBN").IsNullable == false);
                Assert.AreEqual(13, entityType.FindProperty("ISBN").GetMaxLength());

                Assert.IsTrue(entityType.FindProperty("Pages").IsNullable == false);
                Assert.IsTrue(entityType.FindProperty("Genre").IsNullable == false);
                Assert.AreEqual(40, entityType.FindProperty("Genre").GetMaxLength());

                Assert.IsTrue(entityType.FindProperty("Price").IsNullable == false);
                Assert.AreEqual(10, entityType.FindProperty("Price").GetPrecision());
                Assert.AreEqual(2, entityType.FindProperty("Price").GetScale());

                // Relations
                var authorForeignKey = entityType.GetForeignKeys().FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(Author));
                Assert.IsNotNull(authorForeignKey);
                Assert.AreEqual(DeleteBehavior.Restrict, authorForeignKey.DeleteBehavior);

                var bookPublisherNavigation = entityType.GetNavigations().FirstOrDefault(n => n.Name == "BookPublishers");
                Assert.IsNotNull(bookPublisherNavigation);
                Assert.IsTrue(bookPublisherNavigation.IsCollection);

            }
        }
    }
}
