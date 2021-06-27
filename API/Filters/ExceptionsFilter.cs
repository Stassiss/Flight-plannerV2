using System;
using System.Net;
using Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository.Exceptions;

namespace API.Filters
{
    public class ExceptionsFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;

            var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred.";
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(SameAirportException))
            {
                message = context.Exception.Message;
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(SameFlightException))
            {
                message = context.Exception.Message;
                status = HttpStatusCode.Conflict;
            }
            else if (exceptionType == typeof(DateFormatException))
            {
                message = context.Exception.Message;
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                message = context.Exception.Message;
                status = HttpStatusCode.NotFound;
            }
            else
            {
                message = context.Exception.Message;
                Console.WriteLine(message + "\n" + context.Exception.StackTrace);
            }

            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            var err = message;
            response.WriteAsync(err);
        }
    }
}
