using Core.DTOs;

namespace UnitTests.Core.DTOs
{
    [TestClass]
    public class ClaimDTOTests
    {
        [TestMethod]
        public void ClaimDTO_ShouldInitializeWithDefaultValues()
        {
            // Arrange & Act
            var claim = new ClaimDTO();

            // Assert
            Assert.AreEqual(string.Empty, claim.Type);
            Assert.AreEqual(string.Empty, claim.Value);
        }

        [TestMethod]
        public void ClaimDTO_ShouldStoreCorrectValues_WhenAssigned()
        {
            // Arrange
            var claim = new ClaimDTO
            {
                Type = "Role",
                Value = "Admin"
            };

            // Assert
            Assert.AreEqual("Role", claim.Type);
            Assert.AreEqual("Admin", claim.Value);
        }
    }

}
