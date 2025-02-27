using Core.Enums;

namespace UnitTests.Core.Enums
{
    [TestClass]
    public class RoleTests
    {
        [TestMethod]
        public void Role_Should_Have_Expected_Values()
        {
            // Arrange
            var expectedRoles = new List<string>
            {
                "Admin",
                "AuthorsBooksPublisher",
                "AuthorsBooks",
                "Publisher",
                "ViewAuthorsBooks"
            };

            // Act
            var actualRoles = Enum.GetNames(typeof(Role));

            // Assert
            CollectionAssert.AreEquivalent(expectedRoles, actualRoles, "The Role enum does not contain the expected values.");
        }

        [TestMethod]
        public void Role_Should_Contain_Specific_Value()
        {
            // Assert
            Assert.IsTrue(Enum.IsDefined(typeof(Role), "Admin"), "Admin should be a valid Role.");
            Assert.IsTrue(Enum.IsDefined(typeof(Role), "Publisher"), "Publisher should be a valid Role.");
            Assert.IsFalse(Enum.IsDefined(typeof(Role), "SuperAdmin"), "SuperAdmin should not exist in Role.");
        }
    }
}
