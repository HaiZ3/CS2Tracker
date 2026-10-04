using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.SheetDTOs;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
                return NotFound(result);

            return Ok(result);
        }
        [HttpGet("{sheetId}")]
        public async Task<IActionResult> GetSheetById(GetSheetByIdDTO request)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var tempResult = await _sheetService.GetSheetByIdAsync(request.Id, userId);

            if (!tempResult.Success)
            {
                return NotFound(tempResult);
            }

            Sheet sheet = tempResult.Data;

            SheetDisplayDto sheetDisplayDto = new SheetDisplayDto
            {
                Id = sheet.Id,
                UserId = sheet.UserId,
                Name = sheet.Name,
                Type = (int)sheet.SheetType,
                CreatedAt = sheet.CreatedAt,
                UpdatedAt = sheet.UpdatedAt,
                Entries = sheet.Entries.Select(s => new EntryDisplayDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Quantity = s.Quantity,
                    Status = s.Status.ToString(),
                    DataSource = s.DataSource.ToString(),
                    SellOrderPice = s.SellOrderPice,
                    Profit = s.Profit,
                    BuyPrice = s.BuyPrice,
                    SellPrice = s.SellPrice
                }).ToArray()
            };

            ServiceResultGeneric<SheetDisplayDto> result = new ServiceResultGeneric<SheetDisplayDto>
            {
                Success = tempResult.Success,
                ErrorMessage = tempResult.ErrorMessage,
                UserId = tempResult.UserId,
                Username = tempResult.Username,
                Data = sheetDisplayDto
            };

            return Ok(result);
        }
        [HttpPost()]
        public async Task<IActionResult> AddNewSheet(AddNewSheetDTO request)
        {
            var userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if(request.Type > 3 && request.Type < 1)
            {
                return BadRequest();
            }

            SheetType sheetType = (SheetType)request.Type;

            var result = await _sheetService.CreateSheetAsync(userId, request.Name, sheetType);

            if(!result.Success)
            {
                return Conflict();
            }

            return Ok(result);
        }
    }
}
