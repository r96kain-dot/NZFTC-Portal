using Microsoft.AspNetCore.Mvc;

namespace NZFTC_Portal.Controllers
{
    public class AdminController : Controller
    {
        // Loads the admin dashboard.
        public IActionResult Dashboard()
        {
            // Stops non-admin users from opening admin pages.
            IActionResult? accessResult = RequireAdminAccess();
            if (accessResult != null)
            {
                return accessResult;
            }

            // Loads shared header data for the dashboard.
            ApplySharedHeaderData("Dashboard");

            return View();
        }

        // Loads the admin leave page.
        public IActionResult Leave()
        {
            // Stops non-admin users from opening admin pages.
            IActionResult? accessResult = RequireAdminAccess();
            if (accessResult != null)
            {
                return accessResult;
            }

            // Loads shared header data for the leave page.
            ApplySharedHeaderData("Leave");

            return View();
        }

        // Loads the admin payroll page.
        public IActionResult Payroll()
        {
            // Stops non-admin users from opening admin pages.
            IActionResult? accessResult = RequireAdminAccess();
            if (accessResult != null)
            {
                return accessResult;
            }

            // Loads shared header data for the payroll page.
            ApplySharedHeaderData("Payroll");

            return View();
        }

        // Loads the admin employees page.
        public IActionResult Employees()
        {
            // Stops non-admin users from opening admin pages.
            IActionResult? accessResult = RequireAdminAccess();
            if (accessResult != null)
            {
                return accessResult;
            }

            // Loads shared header data for the employees page.
            ApplySharedHeaderData("Employees");

            return View();
        }

        // Loads the admin cases page.
        public IActionResult Cases()
        {
            // Stops non-admin users from opening admin pages.
            IActionResult? accessResult = RequireAdminAccess();
            if (accessResult != null)
            {
                return accessResult;
            }

            // Loads shared header data for the cases page.
            ApplySharedHeaderData("Cases");

            return View();
        }

        // Checks whether the current session belongs to an admin user.
        private IActionResult? RequireAdminAccess()
        {
            string? currentRole = HttpContext.Session.GetString("PortalRole");

            if (string.IsNullOrWhiteSpace(currentRole))
            {
                return RedirectToAction("Login", "Account");
            }

            if (currentRole != "Admin")
            {
                return RedirectToAction("Dashboard", "Employee");
            }

            return null;
        }

        // Applies shared header values for all admin pages.
        private void ApplySharedHeaderData(string activeTab)
        {
            ViewData["PortalUserName"] = HttpContext.Session.GetString("PortalUserName") ?? "Admin Name - ADM000";
            ViewData["PortalRole"] = "Admin";
            ViewData["ActiveTab"] = activeTab;
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "Employees", "Cases" };
        }
    }
}