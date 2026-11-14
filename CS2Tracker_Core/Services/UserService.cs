
using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Common.Responses;
using Microsoft.AspNetCore.Identity;

namespace CS2Tracker_Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ServiceResult> CreateUserAsync(
            string username, string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            if(_userManager.Users.Any(u => u.UserName == username))
            {
                return ServiceResult.Fail($"The username: {username} exists!");
            }

            if (_userManager.Users.Any(u => u.Email == email))
            {
                return ServiceResult.Fail($"The email: {email} belongs to another user!");
            }

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return ServiceResult.Fail(
                    string.Join(" ", result.Errors.Select(e => e.Description)));

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> LoginUserAsync(
            string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(
                username,
                password,
                isPersistent: false,
                lockoutOnFailure: true
            );

            if (!result.Succeeded)
                return ServiceResult.Fail("Invalid login attempt.");

            var user = await _userManager.FindByNameAsync(username);

            return ServiceResult.Ok(
                user!.Id,
                user.UserName ?? username
            );
        }

        public async Task<ServiceResult> ValidateCredentialsAsync(
            string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return ServiceResult.Fail("Invalid login attempt.");

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                lockoutOnFailure: true);

            if (!result.Succeeded)
                return ServiceResult.Fail("Invalid login attempt.");

            return ServiceResult.Ok(
                user.Id,
                user.UserName ?? username
            );
        }

        public async Task<ServiceResult> LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
            return ServiceResult.Ok();
        }
    }
}
