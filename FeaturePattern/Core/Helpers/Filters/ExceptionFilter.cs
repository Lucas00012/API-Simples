using API.Core.Helpers.Exceptions.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Net;

namespace API.Core.Helpers.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            if (ex is BaseHttpException httpException)
            {
                var statusCode = (int)httpException.StatusCode;
                var errorModel = new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "Mensagens", new[] { ex.Message } }
                })
                {
                    Status = statusCode,
                    Title = $"ERRO: {httpException.GetType().Name}"
                };

                context.Exception = null;
                context.HttpContext.Response.StatusCode = statusCode;
                context.Result = new JsonResult(errorModel);

                return;
            }
            else
            {
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var errorModel = new ValidationProblemDetails
                {
                    Status = statusCode,
                    Title = $"ERRO: {ex.Message}",
                    Detail = ex.StackTrace,
                };

                context.HttpContext.Response.StatusCode = statusCode;
                context.Result = new JsonResult(errorModel);
            }
        }
    }
}
