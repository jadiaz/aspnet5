using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;

namespace MvcApplication.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup (IHostingEnvironment env) 
        {
            Configuration = new Configuration()
                .AddJsonFile("webConfig.json")
                .AddEnvironmentVariables();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.EnvironmentName == "Development") 
            {
                // Use default error page
                app.UseErrorPage();
                // Show runtime information on a page (DEVELOPMENT)
                app.UseRuntimeInfoPage();
            }

            // Use custom error handler to catch exceptions and log them
            app.UseErrorHandler("/error.html");

            // Add services to pipeline
            app.UseServices(services =>
            {
                services.AddMvc();
            });

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
