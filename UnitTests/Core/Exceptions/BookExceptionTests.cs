using Core.Exceptions;

namespace UnitTests.Core.Exceptions
{
    [TestClass]
    public class BookExceptionTests
    {
        [TestMethod]
        public void BookException_Should_Have_DefaultConstructor()
        {
            // Act
            var exception = new BookException();

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception is Exception);
            Console.WriteLine($"Exception message: '{exception.Message}'");
            Assert.AreEqual("Exception of type 'Core.Exceptions.BookException' was thrown.", exception.Message);
        }

        [TestMethod]
        public void BookException_Should_Set_Message_Correctly()
        {
            // Arrange
            var expectedMessage = "An error occurred in the Book module.";

            // Act
            var exception = new BookException(expectedMessage);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestMethod]
        public void BookException_Should_Set_Message_And_InnerException_Correctly()
        {
            // Arrange
            var expectedMessage = "Book-related error.";
            var innerException = new Exception("Inner exception message");

            // Act
            var exception = new BookException(expectedMessage, innerException);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.AreEqual(innerException, exception.InnerException);
        }
    }
}
