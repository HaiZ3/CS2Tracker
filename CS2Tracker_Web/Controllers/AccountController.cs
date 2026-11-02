
using System.Net.Http.Json;
using System.Text.Json;
using CS2Tracker_Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CS2Tracker_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("CS2TrackerApi");

            try
            {
                var response = await client.PostAsJsonAsync(
                    "api/users/login",
                    new
                    {
                        model.Username,
                        model.Password
                    });

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();

                    ModelState.AddModelError(
                        string.Empty,
                        errorMessage);

                    return View(model);
                }

                var result = await response.Content
                    .ReadFromJsonAsync<LoginResponse>();

                if (string.IsNullOrWhiteSpace(result?.Token))
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "The API did not return a valid token.");

                    return View(model);
                }

                HttpContext.Session.SetString("JwtToken", result.Token);

                return RedirectToAction("Index", "Home");
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Could not connect to the API. Make sure it is running.");

                return View(model);
            }
        }

        private sealed class LoginResponse
        {
            public string Token { get; set; } = string.Empty;
        }
    }
}
