using Microsoft.AspNetCore.Mvc;

namespace NZFTC_Portal.Controllers
{
    public class EmployeeController : Controller
    {
        // Employee dashboard
        public IActionResult Dashboard()
        {
            // Shared header data
            ViewData["PortalUserName"] = "Employee Name - EMP000";
            ViewData["PortalRole"] = "Employee";
            ViewData["ActiveTab"] = "Dashboard";
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "My Info" };

            return View();
        }

        // Employee leave page
        public IActionResult Leave()
        {
            // Shared header data
            ViewData["PortalUserName"] = "Employee Name - EMP000";
            ViewData["PortalRole"] = "Employee";
            ViewData["ActiveTab"] = "Leave";
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "My Info" };

            return View();
        }

        // Employee payroll page
        public IActionResult Payroll()
        {
            // Shared header data
            ViewData["PortalUserName"] = "Employee Name - EMP000";
            ViewData["PortalRole"] = "Employee";
            ViewData["ActiveTab"] = "Payroll";
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "My Info" };

            return View();
        }

        // Employee information page
        public IActionResult MyInfo()
        {
            // Shared header data
            ViewData["PortalUserName"] = "Employee Name - EMP000";
            ViewData["PortalRole"] = "Employee";
            ViewData["ActiveTab"] = "My Info";
            ViewData["PortalNavItems"] = new[] { "Dashboard", "Leave", "Payroll", "My Info" };

            return View();
        }
    }
}