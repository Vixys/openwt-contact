using System;

namespace OpenWT.Contact.Common.Exception
{
    public class TechnicalException : System.Exception
    {
        public TechnicalException(string message) : base(message)
        {
        }

        public TechnicalException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}