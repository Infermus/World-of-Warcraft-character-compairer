using Microsoft.AspNetCore.Mvc;

namespace WowCharComparerWebApp.Controllers.User
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}