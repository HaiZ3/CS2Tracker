using CS2Tracker_Common.Enums;
using CS2Tracker_Common.Responses;
using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Data.Context;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.SingleItemDTOs;
using CS2Tracker_Models;
using Microsoft.EntityFrameworkCore;
using static CS2Tracker_Common.ErrorMessages.EntryErrorMessages;
using static CS2Tracker_Common.ErrorMessages.SheetErrorMessages;

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
        public async Task<ServiceResultGeneric<ICollection<Entry>>> GetEntriesBySheetAsync(string userId, int sheetId)
        {
            ICollection<Entry>? entries = await _context.Entries
                .Include(e => e.Sheet)
                .Where(e => e.SheetId == sheetId && e.Sheet.UserId == userId)
                .Include(e => e.SkinInfo)
                .ToListAsync();

            return ServiceResultGeneric<ICollection<Entry>>.Ok(entries);
        }
        public async Task<ServiceResult> AddEntryAsync(string userId, CreateEntryDTO createEntryDTO)
        {
            var res = await _sheetService.GetSheetById(userId, createEntryDTO.SheetId);

            if (!res.Success)
            {
                return ServiceResult.Fail(SheetNotFound);
            }

            Sheet sheet = res.Data!;

            try
            {
                SkinInfo skinInfo = new SkinInfo(createEntryDTO.SkinInfo); ;

                skinInfo.SetMarketHashName();
                skinInfo.SetItemType();

                switch (skinInfo.ItemType)
                {
                    case ItemType.Pistol or ItemType.Rifle or ItemType.SMG or ItemType.SniperRifle
                           or ItemType.Shotgun or ItemType.MachineGun or ItemType.Knife or ItemType.Gloves:

                        if(skinInfo.SkinCondition == SkinCondition.Unknown || skinInfo.SkinVariant == SkinVariant.Unknown)
                        {
                            return ServiceResult.Fail("Invalid Condition or Variant");
                        }
                        break;
                    default:
                        break;
                }

                Entry entry = new Entry(createEntryDTO, skinInfo);

                sheet!.UpdatedAt = DateTime.UtcNow;

                _context.Add(entry);
                await _context.SaveChangesAsync();

                return ServiceResult.Ok();
            }
            catch (Exception)
            {
                return ServiceResult.Fail(InvalidItemTypeOrMarketHashName);
            }
        }
        public async Task<ServiceResult> CloseEntryAsync(string userId, CloseEntryDTO request)
        {
            var res = await GetEntryByIdAsync(userId, request.EntryId);

            if (!res.Success)
            {
                return ServiceResult.Fail(res.ErrorMessage!);
            }

            Entry entry = res.Data!;

            if (entry.Status != EntryStatus.Open)
            {
                return ServiceResult.Fail(EntryStatusOtherThanOpen);
            }

            var daysHeld = (request.DateSold - entry.DateBought).Days;

            if (daysHeld < EntryHoldDaysMin)
            {
                return ServiceResult.Fail($"Items can't be sold until {EntryHoldDaysMin} days after purchase (trade hold).");
            }

            entry.SellPrice = request.SellPrice * entry.Quantity;
            entry.Status = EntryStatus.Closed;
            entry.DateSold = request.DateSold;

            await _context.SaveChangesAsync();

            return ServiceResult.Ok();

        }
        public async Task<ServiceResult> CancelEntryAsync(string userId, int entryId)
        {
            var res = await GetEntryByIdAsync(userId, entryId);

            if (!res.Success)
            {
                return ServiceResult.Fail(res.ErrorMessage!);
            }

            Entry entry = res.Data!;


            _context.Remove(entry);
            await _context.SaveChangesAsync();
            return ServiceResult.Ok();
        }
        public async Task<ServiceResult> EditEntryAsync(string userId, EditEntryDTO editEntryDTO)
        {
            var res = await GetEntryByIdAsync(userId, editEntryDTO.Id);

            if (!res.Success)
            {
                return ServiceResult.Fail(res.ErrorMessage!);
            }

            Entry entry = res.Data!;

            entry.DateBought = editEntryDTO.DateBought;
            entry.BuyPrice = editEntryDTO.BuyPrice;
            await _context.SaveChangesAsync();

            return ServiceResult.Ok();
        }
        public async Task<ServiceResultGeneric<EntryDisplayDTO>> GetEntryBySheetIdAsync(string userId, int entryId, int sheetId)
        {
            var sheetRes = await _sheetService.GetSheetById(userId, sheetId);

            if (!sheetRes.Success)
            {
                return ServiceResultGeneric<EntryDisplayDTO>.Fail(sheetRes.ErrorMessage!);
            }

            EntryDisplayDTO? entry = sheetRes.Data!.Entries.Select(e => new EntryDisplayDTO
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
            .FirstOrDefault(e => e.Id == entryId);

            if (entry == null)
            {
                return ServiceResultGeneric<EntryDisplayDTO>.Fail(EntryNotFound);
            }

            ServiceResultGeneric<SkinPriceSingleDTO> res = await _marketplacePriceService.GetSkinMarketplacePrice(entry.Name);

            if (!res.Success)
            {
                entry.YouPinPrice = null;
                entry.CsFloatPrice = null;
            }
            else
            {
                entry.YouPinPrice = res.Data?.Prices.Youpin.Ask;
                entry.CsFloatPrice = res.Data?.Prices.Csfloat.Ask;
            }

            return ServiceResultGeneric<EntryDisplayDTO>.Ok(entry);

        }

        public async Task<ServiceResultGeneric<Entry>> GetEntryByIdAsync(string userId, int entryId)
        {
            Entry? entry = await _context
                .Entries
                .Include(e => e.Sheet)
                .Include(e => e.SkinInfo)
                .FirstOrDefaultAsync(e => e.Id == entryId && e.Sheet.UserId == userId);

            if (entry == null)
            {
                return ServiceResultGeneric<Entry>.Fail(EntryNotFound);
            }

            return ServiceResultGeneric<Entry>.Ok(entry);
        }
    }
}
