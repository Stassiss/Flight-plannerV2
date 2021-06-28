using System.Net;
using Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.ExceptionFilters
{
    public class DateFormatExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();

            if (exceptionType != typeof(DateFormatException)) return;

            var message = context.Exception.Message;
            var status = HttpStatusCode.BadRequest;

            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";

            var err = message;
            response.WriteAsync(err);
        }
    }
}
