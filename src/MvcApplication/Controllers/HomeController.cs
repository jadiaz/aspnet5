using Microsoft.AspNet.Mvc;
using MvcApplication.Models;
using Microsoft.Framework.Logging;
using System.Linq;

namespace MvcApplication.Controllers
{
    public class HomeController : Controller
    {
        public HomeController (ILoggerFactory loggerFactory, AppDbContext context) 
        {
            _logger = loggerFactory.Create(typeof(HomeController).FullName);
            _dbContext = context;
        }

        public IActionResult Index()
        {
            _logger.WriteInformation("Index action activated!");
            _logger.WriteInformation("No. of Users: {0}", _dbContext.Users.Count());

            return View(_dbContext.Users.ToList());
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
        private readonly AppDbContext _dbContext;
    }
}