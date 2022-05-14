using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate _next,
            ILogger<ExceptionMiddleware> _logger, IHostEnvironment _env)
        {
            this.next = _next; 
            this.logger = _logger;
            this.env = _env;
        }
        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
            //a func that can process a http request,
            //in case of no exception it will make the middleware
            //to move to the next piece of middleware
                await next(context);
            }
            //if there's an exception(catch it)
            //log it to the logger (console here)
            //write response into the context response so that we can send it to the client 
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = env.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError,
                    ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}; 
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
                    
            }
        }
    }
}
