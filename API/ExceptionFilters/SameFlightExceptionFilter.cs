using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository.Exceptions;

namespace API.ExceptionFilters
{
    public class SameFlightExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();

            if (exceptionType != typeof(SameFlightException)) return;

            var message = context.Exception.Message;
            var status = HttpStatusCode.Conflict;

            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";

            var err = message;
            response.WriteAsync(err);
        }
    }
}
