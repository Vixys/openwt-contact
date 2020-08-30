﻿using System;

namespace OpenWT.Contact.Common.Exception
{
    public class UnauthorizedException : System.Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}