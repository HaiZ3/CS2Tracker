using Microsoft.AspNetCore.Mvc;

namespace CS2Tracker_Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
