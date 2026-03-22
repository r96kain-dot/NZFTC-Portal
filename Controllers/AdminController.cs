using Microsoft.AspNetCore.Mvc;

namespace NZFTC_Portal.Controllers
{
    public class AdminController : Controller
    {
        // Admin dashboard
        public IActionResult Dashboard()
        {
            // Shared header data
            ViewData["PortalUserName"] = "Admin Name - ADM000";
            ViewData["PortalRole"] = "Admin";
            ViewData["ActiveTab"] = "Dashboard";
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "Employees", "Cases" };

            return View();
        }

        // Admin leave management page
        public IActionResult Leave()
        {
            // Shared header data
            ViewData["PortalUserName"] = "Admin Name - ADM000";
            ViewData["PortalRole"] = "Admin";
            ViewData["ActiveTab"] = "Leave";
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "Employees", "Cases" };

            return View();
        }

        // Admin payroll management page
        public IActionResult Payroll()
        {
            // Shared header data
            ViewData["PortalUserName"] = "Admin Name - ADM000";
            ViewData["PortalRole"] = "Admin";
            ViewData["ActiveTab"] = "Payroll";
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "Employees", "Cases" };

            return View();
        }

        // Admin employee management page
        public IActionResult Employees()
        {
            // Shared header data
            ViewData["PortalUserName"] = "Admin Name - ADM000";
            ViewData["PortalRole"] = "Admin";
            ViewData["ActiveTab"] = "Employees";
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "Employees", "Cases" };

            return View();
        }

        // Admin case management page
        public IActionResult Cases()
        {
            // Shared header data
            ViewData["PortalUserName"] = "Admin Name - ADM000";
            ViewData["PortalRole"] = "Admin";
            ViewData["ActiveTab"] = "Cases";
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "Employees", "Cases" };

            return View();
        }
    }
}