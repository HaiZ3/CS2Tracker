using CS2Tracker_Common.Enums;
using CS2Tracker_Common.Responses;
using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Data.Context;
using CS2Tracker_DTOs.ApiDTOs.SheetDTOs;
using CS2Tracker_DTOs.DTOs.ApiDTOs.EntriesDTOs;
using CS2Tracker_DTOs.DTOs.ApiDTOs.EntriesDTOs.MarketPricesDTOs;
using CS2Tracker_DTOs.DTOs.ApiDTOs.SheetDTOs;
using CS2Tracker_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CS2Tracker_Core.Services
{
    public class SheetService : ISheetService
    {
        private readonly CS2TrackerContext _context;
        private readonly IMarketplacePriceService _marketplacePriceService;
        public SheetService(CS2TrackerContext context, IMarketplacePriceService marketplacePriceService)
        {
            _context = context;
            _marketplacePriceService = marketplacePriceService;
        }
        public async Task<ServiceResult> CreateSheetAsync(string userId, string name, SheetType sheetType)
        {

            if (string.IsNullOrEmpty(name))
            {
                return ServiceResult.Fail("Failed to add the sheet to the database!");
            }
            Sheet sheet = new Sheet(userId, name, sheetType);

            await _context.Sheets.AddAsync(sheet);
            await _context.SaveChangesAsync();


            return ServiceResult.Ok();
        }
        
        public async Task<ServiceResultGeneric<ICollection<SheetDisplayDTO>>> GetSheetsIdsByUserId(string userId)
        {
            ICollection<SheetDisplayDTO>? sheets = _context.Sheets.Where(s => s.UserId == userId).Select(x => new SheetDisplayDTO
            {
                Name = x.Name,
                Entries = x.Entries.Count,
                CreatedAt = x.CreatedAt,
                Type = x.SheetType.ToString(),
                UpdatedAt = x.UpdatedAt,
                Id = x.Id

            }).ToList();

            return ServiceResultGeneric<ICollection<SheetDisplayDTO>>.Ok(sheets);
        }

        public async Task<ServiceResultGeneric<SheetDisplayWithEntriesDTO>> LoadSheetInfo(int sheetId,string userId)
        {
            Sheet? sheet = await _context.Sheets
                .Include(s => s.Entries)
                .ThenInclude(s => s.SkinInfo)
                .FirstOrDefaultAsync(
                s => s.Id == sheetId
                && s.UserId == userId
                );

            if(sheet == null)
            {
                return ServiceResultGeneric<SheetDisplayWithEntriesDTO>.Fail("No such sheet exists!");
            }

            ICollection<EntryDisplayDto> entryDisplayDtos = sheet.Entries.Where(e => e.SheetId == sheetId)
                .Select(e => new EntryDisplayDto
                {
                    Id = e.Id,
                    Name = e.SkinInfo.MarketHashName,
                    Quantity = e.Quantity,
                    Status = e.Status.ToString(),
                    DataSource = e.DataSource.ToString(),
                    Profit = e.Profit,
                    BuyPrice = e.BuyPrice,
                    SellPrice = e.SellPrice
                })
                .ToList();

            foreach (var item in entryDisplayDtos)
            {
                ServiceResultGeneric<SkinPriceDTO> res =  await _marketplacePriceService.GetSkinMarketplacePrice(item.Name);

                item.YouPinPrice = res.Data.Prices.Youpin.Ask;
                item.CsFloatPrice = res.Data.Prices.Csfloat.Ask;

            }

            SheetDisplayWithEntriesDTO sheetDisplayDto = new(sheet.Id, sheet.Name, sheet.SheetType.ToString(), sheet.CreatedAt, sheet.UpdatedAt, entryDisplayDtos);

            return ServiceResultGeneric<SheetDisplayWithEntriesDTO>.Ok(sheetDisplayDto);
        }
    }
}
