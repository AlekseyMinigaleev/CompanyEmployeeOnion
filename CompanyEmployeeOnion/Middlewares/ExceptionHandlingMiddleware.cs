using Domain.Exceptions.BadRequests;
using Domain.Exceptions.NotFounds;
using System.Text.Json;

namespace CompanyEmployeeOnion.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(exception, context);
            }
        }

        private static async Task HandleExceptionAsync(Exception exception, HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode =
                exception switch
                {
                    BadRequestException => StatusCodes.Status400BadRequest,
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError,
                };

            var response = new
            {
                error = exception.Message,
            };

            await httpContext.Response
                .WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}