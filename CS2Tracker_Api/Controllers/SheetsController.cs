using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs.MarketPricesDTOs;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.SheetDTOs;
using CS2Tracker_Core.Services;
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
        private readonly IMarketplacePriceService _marketplacePriceService;
        public SheetsController(ISheetService sheetService,IMarketplacePriceService marketplacePriceService)
        {
            _sheetService = sheetService;
            _marketplacePriceService = marketplacePriceService;
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
        [HttpGet("{sheetId:int}")]
        public async Task<IActionResult> GetSheetById(int sheetId)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var tempResult = await _sheetService.GetSheetByIdAsync(sheetId, userId);

            if (!tempResult.Success)
            {
                return NotFound(tempResult);
            }

            Sheet sheet = tempResult.Data;

            EntryDisplayDto[] entries = new EntryDisplayDto[sheet.Entries.Count];

            int i = 0;

            foreach (Entry entry in sheet.Entries)
            {
                EntryDisplayDto entryDto = new EntryDisplayDto
                {
                    Id = entry.Id,
                    Name = entry.Name,
                    Quantity = entry.Quantity,
                    Status = entry.Status.ToString(),
                    DataSource = entry.DataSource.ToString(),
                    Profit = entry.Profit,
                    BuyPrice = entry.BuyPrice,
                    SellPrice = entry.SellPrice
                };

                var priceResult = await _marketplacePriceService
                    .GetSkinMarketplacePrice(entry.SkinInfo.MarketHashName);

                if (priceResult.Success && priceResult.Data != null && entry.Status == EntryStatus.Open)
                {
                    SkinPriceDTO skinPrice = priceResult.Data;

                    entryDto.CsFloatPrice = skinPrice.Prices.Csfloat?.Ask;
                    entryDto.YouPinPrice = skinPrice.Prices.Youpin?.Ask;
                }

                entries[i] = entryDto;
                i++;
            }

            SheetDisplayDto sheetDisplayDto = new SheetDisplayDto
            {
                Id = sheet.Id,
                UserId = sheet.UserId,
                Name = sheet.Name,
                Type = (int)sheet.SheetType,
                CreatedAt = sheet.CreatedAt,
                UpdatedAt = sheet.UpdatedAt,
                Entries = entries.ToArray()
            };

            ServiceResultGeneric<SheetDisplayDto> result = new ServiceResultGeneric<SheetDisplayDto>
            {
                Success = tempResult.Success,
                ErrorMessage = tempResult.ErrorMessage,
                Data = sheetDisplayDto
            };

            return Ok(result);
        }
        [HttpPost()]
        public async Task<IActionResult> AddNewSheet(AddNewSheetDTO request)
        {
            var userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if(request.Type > 3 || request.Type < 1)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return UnprocessableEntity();
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
