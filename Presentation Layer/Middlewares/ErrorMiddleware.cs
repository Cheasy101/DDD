using System.Net;
using System.Text.Json;
using Exceptions.Exceptions;
using EmployeeAlreadyExistsException = DDD.APP.Exceptions.EmployeeAlreadyExistsException;
using EmployeeNotFoundException = DDD.APP.Exceptions.EmployeeNotFoundException;

namespace DDD.APP.Presentation_Layer.Middlewares
{
    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing the request.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                EmployeeNotFoundException => (int)HttpStatusCode.NotFound,
                EmployeeAlreadyExistsException => (int)HttpStatusCode.Conflict,
                DuplicateDataException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new { message = exception.Message };
            var jsonResponse = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}