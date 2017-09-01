using Microsoft.AspNetCore.Mvc;

namespace AuthClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Admin()
        {
            ViewData["Message"] = "Hello, Admin!";

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
