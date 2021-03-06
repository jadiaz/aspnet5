using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using Microsoft.Data.Entity;
using System.Threading.Tasks;

using MvcApplication.Models;

namespace MvcApplication
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

        /// The purpose of this method is to setup dependency injection
        /// Here the application learns about the services that will be
        /// supplied to the application
        public void ConfigureServices(IServiceCollection services)
        {
            // Add in-memory database store
            services.AddEntityFramework(Configuration)
                    .AddInMemoryStore()
                    .AddDbContext<AppDbContext>(options =>
                    {
                        options.UseInMemoryStore();
                    });

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

        /// Configure allows the application to opt-into the services that are needed
        /// We can also have separate environment configurations
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

            // Initialize database
            SampleData.InitializeUserTableAsync(app.ApplicationServices).Wait();
        }

    }
}
