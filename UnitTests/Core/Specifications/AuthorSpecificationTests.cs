using Core.Entities;
using Core.Specifications;

namespace UnitTests.Core.Specifications
{
    [TestClass]
    public class AuthorSpecificationTests
    {
        [TestMethod]
        public void AuthorSpecification_Should_Filter_By_Name()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { Name = "Albert Camus" },
                new Author { Name = "Fyodor Dostoyevsky" },
                new Author { Name = "Friedrich Nietzsche" },
                new Author { Name = "Franz Kafka" }
            };

            string filterName = "Albert";
            var specification = new AuthorSpecification(filterName);

            // Act
            var filteredAuthors = authors.AsQueryable().Where(specification.Criteria).ToList();

            // Assert
            Assert.AreEqual(1, filteredAuthors.Count);
            Assert.IsTrue(filteredAuthors.Any(a => a.Name == "Albert Camus"));
        }

        [TestMethod]
        public void AuthorSpecification_Should_Return_All_When_Name_Is_Null_Or_Empty()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { Name = "Albert Camus" },
                new Author { Name = "Fyodor Dostoyevsky" },
                new Author { Name = "Friedrich Nietzsche" },
                new Author { Name = "Franz Kafka" }
            };

            var specification = new AuthorSpecification(null); // No filtro

            // Act
            var filteredAuthors = authors.AsQueryable().Where(specification.Criteria).ToList();

            // Assert
            Assert.AreEqual(authors.Count, filteredAuthors.Count);
        }
    }
}
