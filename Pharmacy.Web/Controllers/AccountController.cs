using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Constants;
using Pharmacy.Core.Entities.General.Users;
using Pharmacy.Core.Entities.ViewModels.Identity;
using Pharmacy.Core.Entities.ViewModels.Requests.Patients;
using Pharmacy.Core.Interfaces.IServices;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IServicePool _service;

        public AccountController(IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IServicePool service)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service;
        }

        #region Login
        [HttpGet]
        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string? returnUrl = null)
        {
            if (_httpContextAccessor.HttpContext.User.IsInRole(ApplicationUserRoles.Admin)
                || _httpContextAccessor.HttpContext.User.IsInRole(ApplicationUserRoles.User))
            {
                return RedirectToAction("Index", "Home");
            }
            var request = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(request);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);

            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (string.IsNullOrEmpty(login.ReturnUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return LocalRedirect(login.ReturnUrl);
                        }
                    }
                }
                TempData["Error"] = "Wrong credentials. Please try again.";
                return View(login);
            }
            TempData["Error"] = "Wrong credentials. Please try again.";
            return View(login);
        }
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            if (_httpContextAccessor.HttpContext.User.IsInRole(ApplicationUserRoles.Admin)
                || _httpContextAccessor.HttpContext.User.IsInRole(ApplicationUserRoles.User))
            {
                return RedirectToAction("Index", "Home");
            }
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) return View(register);

            var user = await _userManager.FindByEmailAsync(register.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(register);
            }

            var patient = await _service.Patients.CreatePatientAsync(register.FirstName, register.LastName);

            var newUser = new ApplicationUser()
            {
                Email = register.EmailAddress,
                UserName = register.EmailAddress,
                Name = register.FirstName + " " + register.LastName,
                PatientId = patient.Id
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, register.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, ApplicationUserRoles.User);
                var addedUser = await _userManager.FindByEmailAsync(register.EmailAddress);
                await _signInManager.PasswordSignInAsync(addedUser, register.Password, false, false);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Access denied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion

    }
}
