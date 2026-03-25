using Microsoft.AspNetCore.Mvc;

namespace NZFTC_Portal.Controllers
{
    public class AccountController : Controller
    {
        // Demo admin account used for sprint 1 login testing.
        private const string DemoAdminEmail = "admin@nzftc.co.nz";
        private const string DemoAdminPassword = "Admin123!";
        private const string DemoAdminName = "Admin Name - ADM000";

        // Demo employee account used for sprint 1 login testing.
        private const string DemoEmployeeEmail = "employee@nzftc.co.nz";
        private const string DemoEmployeePassword = "Employee123!";
        private const string DemoEmployeeName = "Employee Name - EMP000";

        // Loads the login page.
        [HttpGet]
        public IActionResult Login()
        {
            // Redirects users away from login if they are already signed in.
            if (IsLoggedIn())
            {
                return RedirectToPortalHome();
            }

            return View();
        }

        // Processes the login form submission.
        [HttpPost]
        public IActionResult Login(string email, string password, string role)
        {
            // Normalizes the selected role from the login form.
            string selectedRole = NormalizeRole(role);

            // Checks the submitted details against the demo admin account.
            if (IsValidAdminLogin(email, password, selectedRole))
            {
                // Stores the signed-in admin details in session.
                HttpContext.Session.SetString("PortalUserName", DemoAdminName);
                HttpContext.Session.SetString("PortalRole", "Admin");
                HttpContext.Session.SetString("PortalEmail", DemoAdminEmail);

                return RedirectToAction("Dashboard", "Admin");
            }

            // Checks the submitted details against the demo employee account.
            if (IsValidEmployeeLogin(email, password, selectedRole))
            {
                // Stores the signed-in employee details in session.
                HttpContext.Session.SetString("PortalUserName", DemoEmployeeName);
                HttpContext.Session.SetString("PortalRole", "Employee");
                HttpContext.Session.SetString("PortalEmail", DemoEmployeeEmail);

                return RedirectToAction("Dashboard", "Employee");
            }

            // Sends a simple error message back to the login page.
            TempData["LoginError"] = "Invalid login details or role selection.";

            return RedirectToAction("Login");
        }

        // Clears the login session and returns the user to login.
        public IActionResult Logout()
        {
            // Removes all stored session values for the current user.
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        // Checks whether the current session already has a signed-in role.
        private bool IsLoggedIn()
        {
            return !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("PortalRole"));
        }

        // Redirects the signed-in user to the correct dashboard.
        private IActionResult RedirectToPortalHome()
        {
            string? currentRole = HttpContext.Session.GetString("PortalRole");

            if (currentRole == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            if (currentRole == "Employee")
            {
                return RedirectToAction("Dashboard", "Employee");
            }

            return RedirectToAction("Login");
        }

        // Checks whether the submitted login matches the demo admin account.
        private bool IsValidAdminLogin(string email, string password, string selectedRole)
        {
            return selectedRole == "Admin"
                && string.Equals(email, DemoAdminEmail, StringComparison.OrdinalIgnoreCase)
                && password == DemoAdminPassword;
        }

        // Checks whether the submitted login matches the demo employee account.
        private bool IsValidEmployeeLogin(string email, string password, string selectedRole)
        {
            return selectedRole == "Employee"
                && string.Equals(email, DemoEmployeeEmail, StringComparison.OrdinalIgnoreCase)
                && password == DemoEmployeePassword;
        }

        // Normalizes the role values coming from the login form.
        private string NormalizeRole(string role)
        {
            if (string.Equals(role, "Administrator", StringComparison.OrdinalIgnoreCase))
            {
                return "Admin";
            }

            if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                return "Admin";
            }

            if (string.Equals(role, "Employee", StringComparison.OrdinalIgnoreCase))
            {
                return "Employee";
            }

            return string.Empty;
        }
    }
}