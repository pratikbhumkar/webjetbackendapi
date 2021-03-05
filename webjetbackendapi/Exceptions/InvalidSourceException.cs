using System;

namespace webjetbackendapi.Exceptions
{
    public class InvalidSourceException : Exception
    {
        public InvalidSourceException(string message) : base(message)
        {
            
        }
    }
}
