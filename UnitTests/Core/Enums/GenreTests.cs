using Core.Enums;

namespace UnitTests.Core.Enums
{
    [TestClass]
    public class GenreTests
    {
        [TestMethod]
        public void Genre_Should_Have_Expected_Values()
        {
            // Arrange
            var expectedGenres = new List<string>
            {
                "Fiction", "NonFiction", "Mystery", "Fantasy", "ScienceFiction",
                "Biography", "History", "Romance", "Horror", "PhilosophicalFiction",
                "Philosophy", "PsychologicalFiction", "ExistentialFiction", "Surrealism",
                "AbsurdistFiction", "ModernistFiction", "Tragedy", "Romanticism", "Bildungsroman"
            };

            // Act
            var actualGenres = Enum.GetNames(typeof(Genre));

            // Assert
            CollectionAssert.AreEquivalent(expectedGenres, actualGenres, "The Genre enum does not contain the expected values.");
        }

        [TestMethod]
        public void Genre_Should_Contain_Specific_Value()
        {
            // Assert
            Assert.IsTrue(Enum.IsDefined(typeof(Genre), "Fiction"), "Fiction should be a valid Genre.");
            Assert.IsTrue(Enum.IsDefined(typeof(Genre), "Philosophy"), "Philosophy should be a valid Genre.");
            Assert.IsTrue(Enum.IsDefined(typeof(Genre), "Romanticism"), "Romanticism should be a valid Genre.");
            Assert.IsFalse(Enum.IsDefined(typeof(Genre), "UnknownGenre"), "UnknownGenre should not exist in Genre.");
        }
    }
}
