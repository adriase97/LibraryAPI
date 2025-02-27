using Core.Enums;

namespace UnitTests.Core.Enums
{
    [TestClass]
    public class UserClaimsTests
    {
        [TestMethod]
        public void UserClaims_Should_Have_Expected_Values()
        {
            // Arrange
            var expectedClaims = new List<string>
            {
                "AuthorsAccess", "BooksAccess", "PublishersAccess",
                "AuthorsCreate", "AuthorsEdit", "AuthorsDelete",
                "BooksCreate", "BooksEdit", "BooksDelete",
                "PublishersCreate", "PublishersEdit", "PublishersDelete"
            };

            // Act
            var actualClaims = Enum.GetNames(typeof(UserClaims));

            // Assert
            CollectionAssert.AreEquivalent(expectedClaims, actualClaims, "The UserClaims enum does not contain the expected values.");
        }

        [TestMethod]
        public void UserClaims_Should_Contain_Specific_Value()
        {
            // Assert
            Assert.IsTrue(Enum.IsDefined(typeof(UserClaims), "AuthorsAccess"), "AuthorsAccess should be a valid UserClaim.");
            Assert.IsTrue(Enum.IsDefined(typeof(UserClaims), "BooksEdit"), "BooksEdit should be a valid UserClaim.");
            Assert.IsFalse(Enum.IsDefined(typeof(UserClaims), "SuperAdminAccess"), "SuperAdminAccess should not exist in UserClaims.");
        }
    }
}
