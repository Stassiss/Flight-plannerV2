using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.ExceptionFilters
{
    public class UnexpectedExceptionsFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            var exceptionType = context.Exception.GetType();

            if (exceptionType != typeof(Exception)) return;

            var message = context.Exception.Message;
            Console.WriteLine(message + "\n" + context.Exception.StackTrace);

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";

            var err = message;
            response.WriteAsync(err);
        }
    }
}
