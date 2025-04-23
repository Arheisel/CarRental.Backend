using CarRental.API.Application.DTOs;
using CarRental.API.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace UADE.Extensions.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> _logger;
        protected readonly IHostEnvironment _env;

        public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var instance = Guid.NewGuid().ToString();
                var statusCode = HandleException(instance, e, out string? message);

                var error = new ErrorDto
                {
                    Instance = instance,
                    Status = (int)statusCode,
                    Title = statusCode.ToString(),
                    Detail = message ?? "An error has ocurred",
                    Path = context.Request.Path
                };
                
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(error, options));
            }
        }

        private HttpStatusCode HandleException(string instance, Exception e, out string? message)
        {
            _logger?.LogError(e, "{instance}: {message}", instance, e.Message);

            if (_env.IsDevelopment() || e is NotFoundException || e is ApplicationException)
                message = e.Message;
            else
                message = null;

            if (e.InnerException != null)
            {
                HandleException(instance, e.InnerException, out string? im);
                if (!string.IsNullOrEmpty(im)) message = $"{message}: {im}";
            }

            if (e is NotFoundException)
            {
                return HttpStatusCode.NotFound;
            }
            else if (e is ApplicationException)
            {
                return HttpStatusCode.BadRequest;
            }
            else if (e is AggregateException ae)
            {
                var messages = ae.InnerExceptions.Select(ex =>
                {
                    HandleException(instance, ex, out string? im);
                    return im;
                });

                var join = string.Join("; ", messages.Where(m => m != null));

                if (!string.IsNullOrEmpty(join)) message = $"{message}: {join}";

                return HttpStatusCode.InternalServerError;
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }

    public static class ExceptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionsMiddleware>();
        }
    }
}
