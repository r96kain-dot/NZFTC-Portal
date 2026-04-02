using Microsoft.AspNetCore.Mvc;

namespace NZFTC_Portal.Controllers
{
    public class AdminController : Controller
    {
        // Check if user is admin
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("Role") == "Admin"
                || HttpContext.Session.GetString("Role") == "Administrator";
        }

        // Reusable header setup
        private void SetAdminViewData(string activeTab)
        {
            var fullName = HttpContext.Session.GetString("FullName") ?? "Admin";
            var userId = HttpContext.Session.GetInt32("UserId") ?? 0;

            ViewData["PortalUserName"] = $"{fullName} - ADM{userId}";
            ViewData["PortalRole"] = "Admin";
            ViewData["ActiveTab"] = activeTab;
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "Employees", "Cases" };
        }

        // Admin dashboard
        public IActionResult Dashboard()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            SetAdminViewData("Dashboard");
            return View();
        }

        // Admin leave management page
        public IActionResult Leave()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            SetAdminViewData("Leave");
            return View();
        }

        // Admin payroll management page
        public IActionResult Payroll()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            SetAdminViewData("Payroll");
            return View();
        }

        // Admin employee management page
        public IActionResult Employees()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            SetAdminViewData("Employees");
            return View();
        }

        // Admin case management page
        public IActionResult Cases()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            SetAdminViewData("Cases");
            return View();
        }
    }
}