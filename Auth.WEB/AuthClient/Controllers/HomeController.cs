using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;


namespace AuthClient.Controllers
{
    public class HomeController : Controller
    {
		ILogger<HomeController> logger;
		public HomeController(ILogger<HomeController> logger)
	    {
			this.logger = logger;
		}
		public IActionResult Index()
        {
			logger.LogInformation("Test");

			return View();
        }

        public IActionResult Admin()
        {
            ViewData["Message"] = "Hello, Admin!";

			logger.LogInformation("Test");

			return View();
        }

        public IActionResult Users()
        {
            ViewData["Message"] = "Hello, users!";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
