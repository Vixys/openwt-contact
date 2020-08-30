using System;

namespace OpenWT.Contact.Api.Middleware
{
    public class ApiExceptionModel
    {
        public Guid ErrorId { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}