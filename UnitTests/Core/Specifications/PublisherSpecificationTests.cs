using Core.Entities;
using Core.Specifications;

namespace UnitTests.Core.Specifications
{
    [TestClass]
    public class PublisherSpecificationTests
    {
        private List<Publisher> _publishers;

        [TestInitialize]
        public void Setup()
        {
            _publishers = new List<Publisher>
        {
            new Publisher { Name = "Gallimard", Country = "France" },
            new Publisher { Name = "Penguin Books", Country = "United Kingdom" },
            new Publisher { Name = "HarperCollins", Country = "United States" },
            new Publisher { Name = "Alianza Editorial", Country = "Spain" }
        };
        }

        [TestMethod]
        public void PublisherSpecification_Should_Filter_By_Name()
        {
            // Arrange
            var specification = new PublisherSpecification("Gallimard", null);

            // Act
            var filteredPublishers = _publishers.AsQueryable().Where(specification.Criteria).ToList();

            // Assert
            Assert.AreEqual(1, filteredPublishers.Count);
            Assert.AreEqual("Gallimard", filteredPublishers.First().Name);
        }

        [TestMethod]
        public void PublisherSpecification_Should_Filter_By_Country()
        {
            // Arrange
            var specification = new PublisherSpecification(null, "France");

            // Act
            var filteredPublishers = _publishers.AsQueryable().Where(specification.Criteria).ToList();

            // Assert
            Assert.AreEqual(1, filteredPublishers.Count);
            Assert.AreEqual("Gallimard", filteredPublishers.First().Name);
            Assert.AreEqual("France", filteredPublishers.First().Country);
        }

        [TestMethod]
        public void PublisherSpecification_Should_Filter_By_Name_And_Country()
        {
            // Arrange
            var specification = new PublisherSpecification("Gallimard", "France");

            // Act
            var filteredPublishers = _publishers.AsQueryable().Where(specification.Criteria).ToList();

            // Assert
            Assert.AreEqual(1, filteredPublishers.Count);
            Assert.AreEqual("Gallimard", filteredPublishers.First().Name);
            Assert.AreEqual("France", filteredPublishers.First().Country);
        }

        [TestMethod]
        public void PublisherSpecification_Should_Return_All_When_No_Filters_Are_Provided()
        {
            // Arrange
            var specification = new PublisherSpecification(null, null);

            // Act
            var filteredPublishers = _publishers.AsQueryable().Where(specification.Criteria).ToList();

            // Assert
            Assert.AreEqual(_publishers.Count, filteredPublishers.Count);
        }
    }
}
