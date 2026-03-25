using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZFTC_Portal.Controllers
{
    public class EmployeeController : Controller
    {
        // Loads the employee dashboard.
        public IActionResult Dashboard()
        {
            // Stops non-employee users from opening employee pages.
            IActionResult? accessResult = RequireEmployeeAccess();
            if (accessResult != null)
            {
                return accessResult;
            }

            // Loads shared header data for the dashboard.
            ApplySharedHeaderData("Dashboard");

            // Sends holiday preview data and full holiday list data to the view.
            ApplyHolidayViewData();

            return View();
        }

        // Loads the employee leave page.
        public IActionResult Leave()
        {
            // Stops non-employee users from opening employee pages.
            IActionResult? accessResult = RequireEmployeeAccess();
            if (accessResult != null)
            {
                return accessResult;
            }

            // Loads shared header data for the leave page.
            ApplySharedHeaderData("Leave");

            return View();
        }

        // Loads the employee payroll page.
        public IActionResult Payroll()
        {
            // Stops non-employee users from opening employee pages.
            IActionResult? accessResult = RequireEmployeeAccess();
            if (accessResult != null)
            {
                return accessResult;
            }

            // Loads shared header data for the payroll page.
            ApplySharedHeaderData("Payroll");

            return View();
        }

        // Loads the employee information page.
        public IActionResult MyInfo()
        {
            // Stops non-employee users from opening employee pages.
            IActionResult? accessResult = RequireEmployeeAccess();
            if (accessResult != null)
            {
                return accessResult;
            }

            // Ensures the profile stump has default values to display.
            EnsureEmployeeProfileData();

            // Loads shared header data for the info page.
            ApplySharedHeaderData("My Info");

            // Sends the current profile values to the view.
            ApplyProfileViewData();

            return View();
        }

        // Saves an allowed employee profile field.
        [HttpPost]
        public IActionResult UpdateProfile(string updateField, string contactNumber, string emergencyContact)
        {
            // Stops non-employee users from posting to employee pages.
            IActionResult? accessResult = RequireEmployeeAccess();
            if (accessResult != null)
            {
                return accessResult;
            }

            // Ensures the profile stump exists before saving updates.
            EnsureEmployeeProfileData();

            // Updates the contact number when that field is submitted.
            if (updateField == "contactNumber")
            {
                HttpContext.Session.SetString("EmployeeContactNumber", CleanProfileValue(contactNumber));
            }

            // Updates the emergency contact when that field is submitted.
            if (updateField == "emergencyContact")
            {
                HttpContext.Session.SetString("EmployeeEmergencyContact", CleanProfileValue(emergencyContact));
            }

            return RedirectToAction("MyInfo");
        }

        // Loads the employee support page.
        public IActionResult Support()
        {
            // Stops non-employee users from opening employee pages.
            IActionResult? accessResult = RequireEmployeeAccess();
            if (accessResult != null)
            {
                return accessResult;
            }

            // Loads shared header data for the support page.
            ApplySharedHeaderData(string.Empty);

            return View();
        }

        // Checks whether the current session belongs to an employee user.
        private IActionResult? RequireEmployeeAccess()
        {
            string? currentRole = HttpContext.Session.GetString("PortalRole");

            if (string.IsNullOrWhiteSpace(currentRole))
            {
                return RedirectToAction("Login", "Account");
            }

            if (currentRole != "Employee")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            return null;
        }

        // Applies shared header values for all employee pages.
        private void ApplySharedHeaderData(string activeTab)
        {
            ViewData["PortalUserName"] = HttpContext.Session.GetString("PortalUserName") ?? "Employee Name - EMP000";
            ViewData["PortalRole"] = "Employee";
            ViewData["ActiveTab"] = activeTab;
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "My Info" };
        }

        // Creates default employee profile values for the sprint 1 stump.
        private void EnsureEmployeeProfileData()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("EmployeeId")))
            {
                HttpContext.Session.SetString("EmployeeId", "EMP000");
            }

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("EmployeeFullName")))
            {
                HttpContext.Session.SetString("EmployeeFullName", "Employee Name");
            }

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("EmployeeContactNumber")))
            {
                HttpContext.Session.SetString("EmployeeContactNumber", "--");
            }

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("EmployeeEmergencyContact")))
            {
                HttpContext.Session.SetString("EmployeeEmergencyContact", "--");
            }

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("EmployeeEmailAddress")))
            {
                HttpContext.Session.SetString(
                    "EmployeeEmailAddress",
                    HttpContext.Session.GetString("PortalEmail") ?? "employee@nzftc.co.nz");
            }

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("EmployeeDepartment")))
            {
                HttpContext.Session.SetString("EmployeeDepartment", "--");
            }

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("EmployeePosition")))
            {
                HttpContext.Session.SetString("EmployeePosition", "--");
            }

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("EmployeeJoinDate")))
            {
                HttpContext.Session.SetString("EmployeeJoinDate", "--");
            }

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("EmployeeStatus")))
            {
                HttpContext.Session.SetString("EmployeeStatus", "Active");
            }
        }

        // Sends the current profile values to the employee info view.
        private void ApplyProfileViewData()
        {
            ViewData["EmployeeId"] = HttpContext.Session.GetString("EmployeeId") ?? "--";
            ViewData["EmployeeFullName"] = HttpContext.Session.GetString("EmployeeFullName") ?? "--";
            ViewData["EmployeeContactNumber"] = HttpContext.Session.GetString("EmployeeContactNumber") ?? "--";
            ViewData["EmployeeEmergencyContact"] = HttpContext.Session.GetString("EmployeeEmergencyContact") ?? "--";
            ViewData["EmployeeEmailAddress"] = HttpContext.Session.GetString("EmployeeEmailAddress") ?? "--";
            ViewData["EmployeeDepartment"] = HttpContext.Session.GetString("EmployeeDepartment") ?? "--";
            ViewData["EmployeePosition"] = HttpContext.Session.GetString("EmployeePosition") ?? "--";
            ViewData["EmployeeJoinDate"] = HttpContext.Session.GetString("EmployeeJoinDate") ?? "--";
            ViewData["EmployeeStatus"] = HttpContext.Session.GetString("EmployeeStatus") ?? "Active";
        }

        // Sends holiday preview data and full holiday list data to the dashboard view.
        private void ApplyHolidayViewData()
        {
            string[][] allHolidays = GetHolidayList();

            // Sends the first four holidays to the dashboard preview list.
            ViewData["HolidayPreview"] = allHolidays.Take(4).ToArray();

            // Sends the full holiday list to the holiday pop-out.
            ViewData["HolidayFullList"] = allHolidays;
        }

        // Creates the temporary holiday list used for the sprint 1 stump.
        private string[][] GetHolidayList()
        {
            return new string[][]
            {
                new[] { "New Year’s Day", "1 January 2026" },
                new[] { "Day after New Year’s Day", "2 January 2026" },
                new[] { "Waitangi Day", "6 February 2026" },
                new[] { "Good Friday", "3 April 2026" },
                new[] { "Easter Monday", "6 April 2026" },
                new[] { "ANZAC Day", "25 April 2026" },
                new[] { "King’s Birthday", "1 June 2026" },
                new[] { "Matariki", "10 July 2026" }
            };
        }

        // Cleans an updated profile field before saving it to session.
        private string CleanProfileValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "--";
            }

            return value.Trim();
        }
    }
}