using Microsoft.AspNetCore.Mvc;

namespace Order.Consumer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
