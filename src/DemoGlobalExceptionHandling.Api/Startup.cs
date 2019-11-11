using DemoGlobalExceptionHandling.Api.Data;
using DemoGlobalExceptionHandling.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DemoGlobalExceptionHandling.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<FakeData>();

            //services.AddGlobalExceptionHandlerMiddleware();
            services.AddMvc()
                .AddJsonOptions(
                    options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.ConfigureProblemDetailsModelState();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole();

            //app.UseGlobalExceptionHandlerMiddleware();
            //app.UseGlobalExceptionHandler(loggerFactory);
            app.UseProblemDetailsExceptionHandler(loggerFactory);
            app.UseMvc();
        }
    }
}
