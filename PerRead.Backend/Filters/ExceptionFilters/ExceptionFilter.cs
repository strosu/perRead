using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PerRead.Backend.Helpers.Errors;
using System.Net;

namespace PerReadPerRead.Backend.Filters 
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionFilter(IHostEnvironment hostEnvironment) =>
            _hostEnvironment = hostEnvironment;

        public void OnException(ExceptionContext context)
        {
            if (!_hostEnvironment.IsDevelopment())
            {
                // Don't display exception details unless running in Development.
                return;
            }

            var httpCode = HttpStatusCode.InternalServerError;

            if (context.Exception is NotFoundException)
            {
                httpCode = HttpStatusCode.NotFound;
            }

            if (context.Exception is MalformedDataException || context.Exception is ArgumentNullException)
            {
                httpCode = HttpStatusCode.BadRequest;
            }

            if (context.Exception is ConflictException)
            {
                httpCode = HttpStatusCode.Conflict;
            }


            if (context.Exception is UnauthorizedException)
            {
                httpCode = HttpStatusCode.Unauthorized;
            }

            context.Result = new JsonResult(context.Exception.Message)
            {
                StatusCode = (int)httpCode
            };

            //context.Response = context.Request.CreateResponse(httpCode, context.Exception.Message);

            //context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(context.Exception.Message) };
        }
    }
}