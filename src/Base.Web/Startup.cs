using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Base.Web.Middleware.Exceptions;

namespace Base.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            app.UseUnhandledExceptionCatching();
            app.UseMvc();
            loggerFactory.AddConsole();
        }
    }
}