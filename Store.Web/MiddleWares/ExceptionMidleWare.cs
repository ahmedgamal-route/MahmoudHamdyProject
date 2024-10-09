using Store.Service.HandleResponse;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace Store.Web.MiddleWares
{
    public class ExceptionMidleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMidleWare> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMidleWare(
            RequestDelegate next,
            IHostEnvironment environment,
            ILogger<ExceptionMidleWare> logger) 
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var resplonse = _environment.IsDevelopment()
                    ? new CustomExeption((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new CustomExeption((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(resplonse, options);
                await context.Response.WriteAsync(json);


            }
        }
    }
}
