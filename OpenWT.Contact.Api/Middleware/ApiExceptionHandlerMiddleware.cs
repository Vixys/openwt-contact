using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OpenWT.Contact.Common.Exception;

namespace OpenWT.Contact.Api.Middleware
{
    public class ApiExceptionHandlerMiddleware : IMiddleware
    {
        private readonly IWebHostEnvironment _env;

        public ApiExceptionHandlerMiddleware(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        #region Helpers

        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetStatusCodeFromException(e);

            var resp = new ApiExceptionModel()
            {
                ErrorId = Guid.NewGuid(),
                StatusCode = context.Response.StatusCode,
                Message = GetExceptionMessage(e),
                StackTrace = string.Equals(this._env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase) ? e.StackTrace : null
            };

            // TODO: Log error here

            return context.Response.WriteAsync(JsonSerializer.Serialize(resp, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }

        private static HttpStatusCode GetStatusCodeFromException(Exception exception)
        {
            switch (exception)
            {
                case UnauthorizedException _:
                    return HttpStatusCode.Unauthorized;
                case BadParameterException _:
                    return HttpStatusCode.BadRequest;
                case NotFoundException _:
                    return HttpStatusCode.NotFound;
                case TechnicalException _:
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }

        private static string GetExceptionMessage(Exception exception)
        {
            // TODO: Add condition to not show technical and unhandled exception message in production env
            
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            return exception.Message;
        }

        #endregion
    }
}