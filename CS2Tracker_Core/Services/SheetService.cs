using CS2Tracker_Common.Enums;
using CS2Tracker_Common.Responses;
using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Data.Context;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.SingleItemDTOs;
using CS2Tracker_DTOs.SharedDTOs.SheetDTOs;
using CS2Tracker_Models;
using Microsoft.EntityFrameworkCore;
using static CS2Tracker_Common.ErrorMessages.SheetErrorMessages;

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
        public async Task<ServiceResultGeneric<Sheet>> GetSheetById(string userId, int sheetId)
        {
            Sheet? sheet = await _context.Sheets.FirstOrDefaultAsync(s => s.Id == sheetId && s.UserId == userId);

            if (sheet == null)
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
            ICollection<SheetDisplayDTO>? sheets = await _context.Sheets.Where(s => s.UserId == userId).Select(x => new SheetDisplayDTO
            {
                Name = x.Name,
                Entries = x.Entries.Count,
                CreatedAt = x.CreatedAt,
                Type = x.SheetType.ToString(),
                UpdatedAt = x.UpdatedAt,
                Id = x.Id

            }).ToListAsync();

            return ServiceResultGeneric<ICollection<SheetDisplayDTO>>.Ok(sheets);
        }

        public async Task<ServiceResultGeneric<SheetDisplayWithEntriesDTO>> GetSheetDetailsById(string userId, int sheetId)
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

            var res = await GetSheetEntriesCurrentMarketPricesAndAllocationPerEntry(sheet);

            ICollection<EntryDisplayDTO> entryDisplayDtos = res.Data!;

            SheetDisplayWithEntriesDTO sheetDisplayDto = new(sheet.Id, sheet.Name, sheet.SheetType.ToString(), sheet.CreatedAt, sheet.UpdatedAt, entryDisplayDtos);

            return ServiceResultGeneric<SheetDisplayWithEntriesDTO>.Ok(sheetDisplayDto);
        }
        public async Task<ServiceResultGeneric<ICollection<EntryDisplayDTO>>> GetSheetEntriesCurrentMarketPricesAndAllocationPerEntry(Sheet sheet)
        {
            List<EntryDisplayDTO> entryDisplayDtos = sheet
                  .Entries
                  .OrderBy(e => e.Status)
                  .ThenByDescending(e => e.BuyPrice)
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

            decimal totalCapitalInOpenEntries = entryDisplayDtos
                .Where(e => e.Status == EntryStatus.Open.ToString())
                .Sum(e => e.Quantity * e.BuyPrice);


            string[] marketHashNames = new string[entryDisplayDtos.Count];

            for (int i = 0; i < entryDisplayDtos.Count; i++)
            {
                marketHashNames[i] = entryDisplayDtos[i].Name;
            }

            ServiceResult loadMarketpacePricesInCache = await _marketplacePriceService.GetSkinsMarketPlacePrices(marketHashNames);

            foreach (var entry in entryDisplayDtos)
            {
                if (entry.Status == EntryStatus.Closed.ToString())
                {
                    continue;
                }

                //Compute the allocation as a % per open entry as a total of all the entries
                decimal capitalInOpenEntry = entry.BuyPrice * entry.Quantity;

                decimal allocationInOpenEntry = capitalInOpenEntry / totalCapitalInOpenEntries;

                entry.Allocation = allocationInOpenEntry;

                ServiceResultGeneric<SkinPriceSingleDTO> res = await _marketplacePriceService.GetSkinMarketplacePrice(entry.Name);

                if (!res.Success)
                {
                    entry.YouPinPrice = null;
                    entry.CsFloatPrice = null;
                    entry.YouPinUrl = null;
                    entry.CsFloatUrl = null;
                }
                else
                {
                    entry.YouPinPrice = res.Data!.Prices.Youpin?.Ask;
                    entry.CsFloatPrice = res.Data!.Prices.Csfloat?.Ask;
                    entry.YouPinUrl = res.Data!.Links?.Youpin;
                    entry.CsFloatUrl = res.Data!.Links?.Csfloat;
                }
            }
            return ServiceResultGeneric<ICollection<EntryDisplayDTO>>.Ok(entryDisplayDtos);
        }
    }
}
