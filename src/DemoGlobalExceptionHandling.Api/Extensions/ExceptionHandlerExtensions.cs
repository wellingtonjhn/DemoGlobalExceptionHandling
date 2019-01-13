using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DemoGlobalExceptionHandling.Api.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                        logger.LogError($"Unexpected error: {exceptionHandlerFeature.Error}");

                        const int statusCode = StatusCodes.Status500InternalServerError;

                        var json = JsonConvert.SerializeObject(new
                        {
                            statusCode,
                            message = "An error occurred whilst processing your request",
                            detailed = exceptionHandlerFeature.Error
                        }, SerializerSettings.JsonSerializerSettings);

                        context.Response.StatusCode = statusCode;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(json);
                    }
                });
            });
        }
    }
}