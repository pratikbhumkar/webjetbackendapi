using System;

namespace webjetbackendapi.Exceptions
{
    public class FetchException : Exception
    {
        public FetchException(string message) : base(message)
        {
        }
    }
}
