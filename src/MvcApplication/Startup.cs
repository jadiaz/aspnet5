using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;

namespace MvcApplication.Web
{
    public class Startup
    {
        public Startup (IHostingEnvironment env) 
        {
            Configuration = new Configuration()
                .AddJsonFile("webConfig.json")
                .AddEnvironmentVariables();
        }
        
        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC to services container
            services.AddMvc();
        }

        public void ConfigureDevelopment(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // Add Console logging to container
            loggerFactory.AddConsole();
            // Use default error page
            app.UseErrorPage();
            // Show runtime information on a page (DEVELOPMENT)
            app.UseRuntimeInfoPage();

            Configure(app);
        }

        public void ConfigureProduction(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // Add Console logging to container
            loggerFactory.AddConsole();
            // Use custom error handler to catch exceptions and log them
            app.UseErrorHandler("/error.html");
            
            Configure(app);
        }

        public void Configure(IApplicationBuilder app)
        {
            // Add MVC to the request pipeline
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "api",
                    template: "{controller}/{id?}");
            });
        }
    }
}
