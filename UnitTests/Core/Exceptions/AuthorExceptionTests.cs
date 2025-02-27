using Core.Exceptions;

namespace UnitTests.Core.Exceptions
{
    [TestClass]
    public class AuthorExceptionTests
    {
        [TestMethod]
        public void AuthorException_Should_Have_DefaultConstructor()
        {
            // Act
            var exception = new AuthorException();

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception is Exception);
            Console.WriteLine($"Exception message: '{exception.Message}'");
            Assert.AreEqual("Exception of type 'Core.Exceptions.AuthorException' was thrown.", exception.Message);
        }

        [TestMethod]
        public void AuthorException_Should_Set_Message_Correctly()
        {
            // Arrange
            var expectedMessage = "An error occurred in the Author module.";

            // Act
            var exception = new AuthorException(expectedMessage);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestMethod]
        public void AuthorException_Should_Set_Message_And_InnerException_Correctly()
        {
            // Arrange
            var expectedMessage = "Author-related error.";
            var innerException = new Exception("Inner exception message");

            // Act
            var exception = new AuthorException(expectedMessage, innerException);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.AreEqual(innerException, exception.InnerException);
        }
    }
}
