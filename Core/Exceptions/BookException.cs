﻿namespace Core.Exceptions
{
    public class BookException : Exception
    {
        public BookException() { }
        public BookException(string message) : base(message) { }
        public BookException(string message, Exception innerException) : base(message, innerException) { }
    }
}
