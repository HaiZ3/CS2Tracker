using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Core.Models;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs.MarketPricesDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CS2Tracker_Api.Controllers
{
    [Route("api/entries")]
    [ApiController]
    [Authorize]
    public class EntriesContoller : ControllerBase
    {
        private readonly IEntryService _entryService;
        private readonly IMarketplacePriceService _marketplacePriceService;

        public EntriesContoller(IEntryService entryService,IMarketplacePriceService marketplacePriceService)
        {
            _entryService = entryService;
            _marketplacePriceService = marketplacePriceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEntryFromSheet(GetEntryDTO request)
        {
            var userId = int.Parse(
                   User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var entryRes = await _entryService.GetEntryFromSheetAsync(userId, request.EntryId, request.SheetId);

            if (!entryRes.Success)
            {
                return NotFound(entryRes.ErrorMessage);
            }

            Entry entry = entryRes.Data!;

            var serviceResultGenericSkinPrice = await _marketplacePriceService.GetSkinMarketplacePrice(entry.SkinInfo.MarketHashName);

            SkinPriceDTO skinPriceDTO = serviceResultGenericSkinPrice.Data;

            EntryDisplayDto res = new EntryDisplayDto
            {
                Id = entry.Id,
                Name = entry.Name,
                Quantity = entry.Quantity,
                Status = entry.Status.ToString(),
                DataSource = entry.DataSource.ToString(),
                Profit = entry.Profit,
                BuyPrice = entry.BuyPrice,
                SellPrice = entry.SellPrice,
                CsFloatPrice = skinPriceDTO.Prices.Csfloat?.Ask,
                YouPinPrice = skinPriceDTO.Prices.Youpin?.Ask
            };

            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEntry(CreateEntryDTO request)
        {
            var userId = int.Parse(
       User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            try
            {
                SkinInfo skinInfo = new SkinInfo(request.SkinInfo);;

                Entry entry = new Entry(request, skinInfo);

                var result = await _entryService.AddEntryAsync(entry, skinInfo,userId);

                if (!result.Success)
                {
                    return BadRequest(result.ErrorMessage);
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> CloseEntry(CloseEntryDTO request)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _entryService.CloseEntryAsync(userId,request);

            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> CancelEntry(CancelEntryDTO request)
        {
                    var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _entryService.CancelEntryAsync(userId,request);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result);
        }
    }
}
