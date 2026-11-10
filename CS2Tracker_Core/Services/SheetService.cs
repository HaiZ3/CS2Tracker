using CS2Tracker_Models;
using CS2Tracker_Common.Enums;
using CS2Tracker_Data.Context;
using CS2Tracker_Core.Interfaces.IServices;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using CS2Tracker_Common.Responses;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.SheetDTOs;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntriesDTOs;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntriesDTOs.MarketPricesDTOs;

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
        public async Task<ServiceResult> CreateSheetAsync(int userId, string name, int sheetType)
        {

            if (string.IsNullOrEmpty(name))
            {
                return ServiceResult.Fail("Failed to add the sheet to the database!");
            }
            Sheet sheet = new Sheet(userId, name, (SheetType)sheetType);

            await _context.Sheets.AddAsync(sheet);
            await _context.SaveChangesAsync();


            return ServiceResult.Ok();
        }
        
        public async Task<ServiceResultGeneric<ICollection<int>>> GetSheetsIdsByUserId(int userId)
        {
            ICollection<int>? sheets = _context.Sheets.Where(s => s.UserId == userId).Select(x => x.Id).ToList();

            if(sheets is null || sheets.Count == 0)
            {
                return ServiceResultGeneric<ICollection<int>>.Fail("Failed to load the sheets or there are no existing sheets!");
            }

            return ServiceResultGeneric<ICollection<int>>.Ok(sheets);
        }

        public async Task<ServiceResultGeneric<SheetDisplayDto>> LoadSheetInfo(int sheetId,int userId)
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
                return ServiceResultGeneric<SheetDisplayDto>.Fail("No such sheet exists!");
            }

            ICollection<EntryDisplayDto> entryDisplayDtos = sheet.Entries
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

            SheetDisplayDto sheetDisplayDto = new(sheet.Id, sheet.Name, sheet.SheetType.ToString(), sheet.CreatedAt, sheet.UpdatedAt, entryDisplayDtos);

            return ServiceResultGeneric<SheetDisplayDto>.Ok(sheetDisplayDto);
        }
    }
}
