using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.SheetDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS2Tracker_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SheetsController : ControllerBase
    {
        private readonly ISheetService _sheetService;
        public SheetsController(ISheetService sheetService)
        {
            _sheetService = sheetService;
        }
        [HttpGet]
        public async Task<IActionResult> GetSheets()
        {
            var userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _sheetService.LoadSheetsAsync(userId);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        [HttpGet("{sheetId:int}")]
        public async Task<IActionResult> GetSheetById(int sheetId)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _sheetService.GetSheetByIdAsync(sheetId, userId);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        [HttpPost()]
        public async Task<IActionResult> AddNewSheet(AddNewSheetDTO request)
        {
            var userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _sheetService.CreateSheetAsync(userId, request.Name, request.Type);

            if(!result.Success)
            {
                return Conflict(result.ErrorMessage);
            }

            return Ok();
        }
    }
}
