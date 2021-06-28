using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.ExceptionFilters
{
    public class NotImplementedExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();

            if (exceptionType != typeof(NotImplementedException)) return;

            var message = "A server error occurred.";
            var status = HttpStatusCode.NotImplemented;

            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            var err = message;
            response.WriteAsync(err);
        }
    }
}
