using ChattingApp.Domain.Errors;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;

namespace ChattingApp.Helper.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware (RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env )
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }


        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await next(httpContext);

            }
            catch (Exception ex)
            {

                logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var ErrorResponse = env.IsDevelopment() ? new ApiException(httpContext.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(httpContext.Response.StatusCode, "Internal Server error ", "");
                var Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(ErrorResponse, Options);
                await httpContext.Response.WriteAsync(json);
            }
        }

    }
}
