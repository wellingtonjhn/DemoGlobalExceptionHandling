using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace DemoGlobalExceptionHandling.Api.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");

                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        logger.LogError($"Unexpected error: {exceptionHandlerFeature.Error}");

                        var json = new
                        {
                            context.Response.StatusCode,
                            Message = "An error occurred whilst processing your request",
                            Detailed = exceptionHandlerFeature.Error
                        };

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
                    }
                });
            });
        }
    }
}