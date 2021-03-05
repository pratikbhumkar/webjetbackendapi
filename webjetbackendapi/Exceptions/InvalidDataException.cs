using System;

namespace webjetbackendapi.Exceptions
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message) : base(message)
        {

        }
    }
}
