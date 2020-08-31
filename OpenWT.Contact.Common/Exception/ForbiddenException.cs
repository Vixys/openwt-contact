using System;

namespace OpenWT.Contact.Common.Exception
{
    public class ForbiddenException : System.Exception
    {
        public ForbiddenException(string message) : base(message)
        {
        }

        public ForbiddenException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}