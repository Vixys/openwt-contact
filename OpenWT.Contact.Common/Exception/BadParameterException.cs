using System;

namespace OpenWT.Contact.Common.Exception
{
    public class BadParameterException : System.Exception
    {
        public BadParameterException(string message) : base(message)
        {
        }

        public BadParameterException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}