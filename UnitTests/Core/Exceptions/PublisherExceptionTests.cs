using Core.Exceptions;

namespace UnitTests.Core.Exceptions
{
    [TestClass]
    public class PublisherExceptionTests
    {
        [TestMethod]
        public void PublisherException_Should_Have_DefaultConstructor()
        {
            // Act
            var exception = new PublisherException();

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception is Exception);
            Console.WriteLine($"Exception message: '{exception.Message}'");
            Assert.AreEqual("Exception of type 'Core.Exceptions.PublisherException' was thrown.", exception.Message);
        }

        [TestMethod]
        public void PublisherException_Should_Set_Message_Correctly()
        {
            // Arrange
            var expectedMessage = "An error occurred in the Publisher module.";

            // Act
            var exception = new PublisherException(expectedMessage);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestMethod]
        public void PublisherException_Should_Set_Message_And_InnerException_Correctly()
        {
            // Arrange
            var expectedMessage = "Publisher-related error.";
            var innerException = new Exception("Inner exception message");

            // Act
            var exception = new PublisherException(expectedMessage, innerException);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.AreEqual(innerException, exception.InnerException);
        }
    }
}
