using CS2Tracker_Common.Enums;
using CS2Tracker_Common.Responses;
using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Data.Context;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs;
using CS2Tracker_DTOs.SharedDTOs.SheetDTOs;
using CS2Tracker_Models;
using static CS2Tracker_Common.ErrorMessages.SheetErrorMessages;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ServiceResultGeneric<Sheet>> GetSheetById(string userId,int sheetId)
        {
            Sheet? sheet = await _context.Sheets.FirstOrDefaultAsync(s => s.Id == sheetId && s.UserId == userId);

            if(sheet == null)
            {
                return ServiceResultGeneric<Sheet>.Fail(SheetNotFound);
            }

            return ServiceResultGeneric<Sheet>.Ok(sheet);
        }
        public async Task<ServiceResult> CreateSheetAsync(string userId, string name, SheetType sheetType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return ServiceResult.Fail(SheetNameIsEmpty);
            }

            Sheet sheet = new Sheet(userId, name, sheetType);

            await _context.Sheets.AddAsync(sheet);
            await _context.SaveChangesAsync();


            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteSheetAsync(string userId, int sheetId)
        {
            Sheet? sheet = await _context.Sheets.FirstOrDefaultAsync(s => s.Id == sheetId && s.UserId == userId);
            if (sheet == null)
            {
                return ServiceResult.Fail(SheetNotFound);
            }

            _context.Sheets.Remove(sheet);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> EditSheetAsync(string userId, SheetEditDTO sheetDTO)
        {
            Sheet? sheet = await _context.Sheets.FirstOrDefaultAsync(s => s.Id == sheetDTO.Id && s.UserId == userId);
            if (sheet == null)
            {
                return ServiceResult.Fail(SheetNotFound);
            }

            sheet.Name = sheetDTO.Name.Trim();
            sheet.SheetType = Enum.Parse<SheetType>(sheetDTO.Type);

            await _context.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResultGeneric<SheetEditDTO>> GetSheetForEditAsync(string userId, int sheetId)
        {
            Sheet? res = await _context.Sheets.FirstOrDefaultAsync(s => s.Id == sheetId && s.UserId == userId);

            if (res == null)
            {
                return ServiceResultGeneric<SheetEditDTO>.Fail(SheetNotFound);
            }

            SheetEditDTO sheet = new SheetEditDTO
            {
                Id = res.Id,
                Name = res.Name,
                Type = res.SheetType.ToString()
            };


            return ServiceResultGeneric<SheetEditDTO>.Ok(sheet);
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

        public async Task<ServiceResultGeneric<SheetDisplayWithEntriesDTO>> LoadSheetInfo(int sheetId, string userId)
        {
            Sheet? sheet = await _context.Sheets
                .Include(s => s.Entries)
                .ThenInclude(s => s.SkinInfo)
                .FirstOrDefaultAsync(
                s => s.Id == sheetId
                && s.UserId == userId
                );

            if (sheet == null)
            {
                return ServiceResultGeneric<SheetDisplayWithEntriesDTO>.Fail(SheetNotFound);
            }

            ICollection<EntryDisplayDTO> entryDisplayDtos = sheet
                .Entries
                .Where(e => e.SheetId == sheetId)
                .OrderBy(e => e.Status)
                .ThenByDescending(e => e.DateBought)
                .Select(e =>
                    new EntryDisplayDTO
                    (
                    e.Id,
                    e.SkinInfo.MarketHashName,
                    e.Quantity,
                    e.Status.ToString(),
                    e.Profit,
                    e.Return,
                    e.DateBought,
                    e.DateSold,
                    e.HoldDays,
                    e.BuyPrice,
                    e.SellPrice,
                    null,
                    null
                    )
                )
                .ToList();

            foreach (var item in entryDisplayDtos)
            {
                if(item.Status == EntryStatus.Closed.ToString())
                {
                    continue;
                }

                ServiceResultGeneric<SkinPriceDTO> res = await _marketplacePriceService.GetSkinMarketplacePrice(item.Name);

                if (!res.Success)
                {
                    item.YouPinPrice = null;
                    item.CsFloatPrice = null;
                    item.YouPinUrl = null;
                    item.CsFloatUrl = null;
                }
                else
                {
                    item.YouPinPrice = res.Data.Prices.Youpin?.Ask;
                    item.CsFloatPrice = res.Data.Prices.Csfloat?.Ask;
                    item.YouPinUrl = res.Data.Links?.Youpin;
                    item.CsFloatUrl = res.Data.Links?.Csfloat;
                }
            }

            SheetDisplayWithEntriesDTO sheetDisplayDto = new(sheet.Id, sheet.Name, sheet.SheetType.ToString(), sheet.CreatedAt, sheet.UpdatedAt, entryDisplayDtos);

            return ServiceResultGeneric<SheetDisplayWithEntriesDTO>.Ok(sheetDisplayDto);
        }
    }
}
