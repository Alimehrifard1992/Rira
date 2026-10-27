using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Rira.Application.Helper;

namespace Rira.WebApi.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, message, title) = exception switch
            {
                CustomException ex => (
                    ex.Error.Status,
                    ex.Error.Massage,
                    "خطا در درخواست"),

                ArgumentException => (
                    StatusCodes.Status400BadRequest,
                    exception.Message,
                    "درخواست نامعتبر"),

                KeyNotFoundException => (
                    StatusCodes.Status404NotFound,
                    exception.Message,
                    "موردی پیدا نشد"),

                _ => (
                    StatusCodes.Status500InternalServerError,
                    "خطایی در سرور رخ داد. لطفاً دوباره تلاش کنید.",
                    "خطای داخلی سرور")
            };

            httpContext.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = message
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
