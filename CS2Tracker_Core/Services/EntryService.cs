using CS2Tracker_Data.Context;
using CS2Tracker_Common.Enums;
using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Models;
using CS2Tracker_DTOs.DTOs.ApiDTOs.EntriesDTOs;
using CS2Tracker_DTOs.DTOs.ApiDTOs.EntriesDTOs.MarketPricesDTOs;
using CS2Tracker_Common.Responses;
using Microsoft.EntityFrameworkCore;
using CS2Tracker_DTOs.DTOs.ApiDTOs.SheetDTOs;
using CS2Tracker_DTOs.ApiDTOs.SheetDTOs;

namespace CS2Tracker_Core.Services
{
    public class EntryService : IEntryService
    {
        private readonly CS2TrackerContext _context;
        private readonly ISheetService _sheetService;
        private readonly IMarketplacePriceService _marketplacePriceService;
        public EntryService(CS2TrackerContext context, ISheetService sheetService, IMarketplacePriceService marketplacePriceService)
        {
            _context = context;
            _sheetService = sheetService;
            _marketplacePriceService = marketplacePriceService;
        }
        public async Task<ServiceResult> AddEntryAsync(string userId,CreateEntryDTO createEntryDTO)
        {
            Sheet? sheet = await _context.Sheets
                .FirstOrDefaultAsync(s =>
                s.Id == createEntryDTO.SheetId &&
                s.UserId == userId);

            if (sheet == null)
            {
                return ServiceResult.Fail("No such sheet exists");
            }
            try
            {
                SkinInfo skinInfo = new SkinInfo(createEntryDTO.SkinInfo); ;

                Entry entry = new Entry(createEntryDTO, skinInfo);


                skinInfo.SetMarketHashName();
                skinInfo.SetItemType();

                entry.DataSource = EntryDataSource.Regular;

                sheet.UpdatedAt = DateTime.UtcNow;

                _context.Add(entry);
                await _context.SaveChangesAsync();

                return ServiceResult.Ok();
            }
            catch (Exception)
            {
                return ServiceResult.Fail("Market hash name or item type failed to set!");
            }
        }

        public async Task<ServiceResultGeneric<ICollection<Entry>>> GetEntriesBySheetAsync(int sheetId)
        {
            ICollection<Entry>? entries = await _context.Entries
                .Where(e => e.SheetId == sheetId)
                .Include(e => e.SkinInfo)
                .ToListAsync();
            if (entries == null || entries.Count == 0)
            {
                return ServiceResultGeneric<ICollection<Entry>>.Fail("No entries found for this sheet.");
            }
            else
            {
                return ServiceResultGeneric<ICollection<Entry>>.Ok(entries);
            }
        }
        public async Task<ServiceResult> CloseEntryAsync(string userId, CloseEntryDTO request)
        {
            Entry? entry = await _context.Entries
                .Include(s => s.Sheet)
                .FirstOrDefaultAsync
                (e => e.Id == request.EntryId
                && e.Sheet.UserId == userId);

            if (entry == null)
            {
                return ServiceResult.Fail("No such entry exists");
            }
            else
            {
                entry.SellPrice = request.SellPrice * entry.Quantity; // might need to be edited
                entry.Status = EntryStatus.Closed;
                entry.Profit = entry.SellPrice - entry.BuyPrice;
                entry.Return = ((entry.SellPrice - entry.BuyPrice) / entry.BuyPrice) * 100;
                entry.DateSold = request.DateSold;
                entry.HoldDays = (entry.DateSold - entry.DateBought).Value.Days;
                entry.DailyReturn = entry.Return / entry.HoldDays;
                _context.Update(entry);
                await _context.SaveChangesAsync();
                return ServiceResult.Ok();
            }
        }
        public async Task<ServiceResult> CancelEntryAsync(string userId, CancelEntryDTO request)
        {
            Entry? entry = await _context.Entries
                .FirstOrDefaultAsync(
                e => e.Id == request.EntryId
                &&
                e.Sheet.UserId == userId

                );
            if (entry == null)
            {
                return ServiceResult.Fail("Entry not found.");
            }
            else
            {
                _context.Remove(entry);
                await _context.SaveChangesAsync();
                return ServiceResult.Ok();
            }
        }
        public async Task<ServiceResultGeneric<EntryDisplayDto>> GetEntryBySheetIdAsync(string userId, int entryId, int sheetId)
        {
            var sheetRes = await _sheetService.LoadSheetInfo(sheetId, userId);

            if (!sheetRes.Success)
            {
                return ServiceResultGeneric<EntryDisplayDto>.Fail("No such sheet exists");
            }

            SheetDisplayWithEntriesDTO sheet = sheetRes.Data!;

            var entryRes = sheet.Entries.FirstOrDefault(x => x.Id == entryId);

            if (entryRes == null)
            {
                return ServiceResultGeneric<EntryDisplayDto>.Fail("No such entry exists");
            }

            var entry = entryRes;

            var serviceResultGenericSkinPrice = await _marketplacePriceService.GetSkinMarketplacePrice(entry.Name);

            SkinPriceDTO skinPriceDTO = serviceResultGenericSkinPrice.Data!;

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

            return ServiceResultGeneric<EntryDisplayDto>.Ok(res);
        }
    }
}
