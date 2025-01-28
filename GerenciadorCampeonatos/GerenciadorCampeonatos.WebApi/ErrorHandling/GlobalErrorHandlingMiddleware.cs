using System.Net;
using System.Text.Json;

namespace GerenciadorCampeonatos.WebApi.ErrorHandling
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message;

            switch (exception)
            {
                case ArgumentNullException _:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "A required argument was not provided.";
                    break;
                case UnauthorizedAccessException _:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "You are not authorized to access this resource.";
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                Details = exception.Message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
