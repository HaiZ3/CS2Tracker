using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Core.Services;
using CS2Tracker_MVC.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CS2Tracker_MVC.Controllers
{
    [AllowAnonymous]
    public class AccountsController : Controller
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Home()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Sheets");
            }

            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Sheets");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Sheets");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userService.LoginUserAsync(
                model.Username,
                model.Password
            );

            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(model);
            }

            return RedirectToAction("All", "Sheets");
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Sheets");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Sheets");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await _userService.CreateUserAsync(model.Username, model.Email, model.Password);
            if (!res.Success)
            {
                return View(model);
            }
            return RedirectToAction(nameof(Login));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUserAsync();

            return RedirectToAction("Home", "Accounts");
        }
    }
}
