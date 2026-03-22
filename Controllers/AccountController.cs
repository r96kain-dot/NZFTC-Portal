using Microsoft.AspNetCore.Mvc;

namespace NZFTC_Portal.Controllers
{
    public class AccountController : Controller
    {
        // Login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login form submission
        [HttpPost]
        public IActionResult Login(string email, string password, string role)
        {
            // Route employee login
            if (role == "Employee")
            {
                return RedirectToAction("Dashboard", "Employee");
            }

            // Route admin login
            if (role == "Administrator")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            // Fallback back to login if no role is selected
            return RedirectToAction("Login", "Account");
        }

        // Logout action
        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}