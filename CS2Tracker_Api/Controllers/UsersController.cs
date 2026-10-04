using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.UsersDTOs;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS2Tracker_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UsersController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var result = await _userService.CreateUserAsync(
                request.Username,
                request.Email,
                request.Password);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            ServiceResult result = await _userService.LoginUserAsync(
                request.Username,
                request.Password
                );

            if (!result.Success)
            {
                return Unauthorized(result.ErrorMessage);
            }
            string token = _tokenService.CreateToken(
            result.UserId!.Value,
            result.Username!);

            return Ok(new
            {
                token
            });
        }

    }
}
