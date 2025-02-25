namespace Core.Exceptions
{
    public class BookPublisherException : Exception
    {
        public BookPublisherException() { }
        public BookPublisherException(string message) : base(message) { }
        public BookPublisherException(string message, Exception innerException) : base(message, innerException) { }
    }
}
