using Microsoft.AspNet.Mvc;
using MvcApplication.Models;
using Microsoft.Framework.Logging;

namespace MvcApplication.Controllers
{
    public class HomeController : Controller
    {
        public HomeController (ILoggerFactory loggerFactory) 
        {
            _logger = loggerFactory.Create(typeof(HomeController).FullName);
        }

        public IActionResult Index()
        {
            _logger.WriteInformation("Index action activated!");

            return View(CreateUser());
        }

        public User CreateUser()
        {
            User user = new User()
            {
                Name = "My name",
                Address = "My address"
            };

            return user;
        }
        private readonly ILogger _logger;
    }
}