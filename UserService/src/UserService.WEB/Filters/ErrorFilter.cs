using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using UserService.BLL.Infrastructure.Exceptions;

namespace UserService.WEB.Filters
{
    public class ErrorFilter : Attribute, IExceptionFilter
    {
        private readonly ILogger<ErrorFilter> _logger;

        public ErrorFilter(ILogger<ErrorFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            _logger.LogError($"Unhandled exception: {filterContext.Exception.Message} | " +
                             $"StackTrace: {filterContext.Exception.StackTrace}");

            var exception = filterContext.Exception;
            if (!filterContext.ExceptionHandled)
            {
                var notFoundException = exception as EntityNotFoundException;
                var serviceException = exception as ServiceException;

                if (notFoundException != null)
                {
                    filterContext.Result = new NotFoundObjectResult(new JsonResult(notFoundException.Message)
                    {
                        ContentType = notFoundException.Target,
                        StatusCode = (int)HttpStatusCode.NotFound
                    });
                }
                else if (serviceException != null)
                {
                    filterContext.Result = new BadRequestObjectResult(new JsonResult(serviceException.Message)
                    {
                        ContentType = serviceException.Target,
                        StatusCode = (int)HttpStatusCode.BadRequest
                    });
                }
                else
                {
                    filterContext.Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError);
                }

                filterContext.ExceptionHandled = true;
            }
        }
    }
}