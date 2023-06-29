using LeaveManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LeaveManagement.Web.Controllers
{
    public class DemoLoginController : Controller
    {
        private readonly SignInManager<Employee> signInManager;
        private readonly ILogger<DemoLoginController> _logger;

        public DemoLoginController(SignInManager<Employee> signInManager, ILogger<DemoLoginController> _logger)
        {
            this.signInManager = signInManager;
            this._logger = _logger;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin()
        {
            var returnUrl = Url.Content("~/");
            var adminUsername = "admin@localhost.com";
            var adminPwd = "aA!111";

            var result = await signInManager.PasswordSignInAsync(adminUsername, adminPwd, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("Demo Admin logged in.");
                return LocalRedirect(returnUrl);
            } else
            {
                return RedirectToAction("/");
            }

            //return View();
        }        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeLogin()
        {
            var returnUrl = Url.Content("~/");
            var employeeUsername = "user@localhost.com";
            var employeePwd = "aA!111";

            var result = await signInManager.PasswordSignInAsync(employeeUsername, employeePwd, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("Demo Admin logged in.");
                return LocalRedirect(returnUrl);
            } else
            {
                //return View();
                return RedirectToAction(returnUrl);
            }

            //return View();
        }
    }
}
