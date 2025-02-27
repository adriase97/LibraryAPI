using Core.Exceptions;

namespace UnitTests.Core.Exceptions
{
    [TestClass]
    public class BookPublisherExceptionTests
    {
        [TestMethod]
        public void BookPublisherException_Should_Have_DefaultConstructor()
        {
            // Act
            var exception = new BookPublisherException();

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception is Exception);
            Console.WriteLine($"Exception message: '{exception.Message}'");
            Assert.AreEqual("Exception of type 'Core.Exceptions.BookPublisherException' was thrown.", exception.Message);
        }

        [TestMethod]
        public void BookPublisherException_Should_Set_Message_Correctly()
        {
            // Arrange
            var expectedMessage = "An error occurred in the BookPublisher module.";

            // Act
            var exception = new BookPublisherException(expectedMessage);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestMethod]
        public void BookPublisherException_Should_Set_Message_And_InnerException_Correctly()
        {
            // Arrange
            var expectedMessage = "BookPublisher-related error.";
            var innerException = new Exception("Inner exception message");

            // Act
            var exception = new BookPublisherException(expectedMessage, innerException);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.AreEqual(innerException, exception.InnerException);
        }
    }
}
