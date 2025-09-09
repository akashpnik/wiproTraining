using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureTaskApp.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Index()
        {
            // Redirect to Dashboard if logged in
            if (User?.Identity?.IsAuthenticated == true)
                return RedirectToAction("Dashboard");
            return RedirectToAction("Login", "Account");
        }
    }
}
