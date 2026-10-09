using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Context;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Services
{
    public class EntryService : IEntryService
    {
        private CS2TrackerContext _context;
        private ISheetService _sheetService;
        public EntryService(CS2TrackerContext context, ISheetService sheetService)
        {
            _context = context;
            _sheetService = sheetService;
        }
        public async Task<ServiceResult> AddEntryAsync(Entry entry, SkinInfo skinInfo, int userId)
        {
            bool sheetExists = await _context.Sheets
                .AnyAsync(s =>
                s.Id == entry.SheetId &&
                s.UserId == userId);

            if (!sheetExists)
            {
                return ServiceResult.Fail("No such sheet exists");
            }
            try
            {

                skinInfo.SetMarketHashName();
                skinInfo.SetItemType();

            }
            catch (Exception)
            {
                return ServiceResult.Fail("Market hash name or item type failed to set!");
            }

            entry.DataSource = EntryDataSource.Regular;

            _context.Add(entry);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok();
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
        public async Task<ServiceResult> CloseEntryAsync(int userId,CloseEntryDTO request)
        {
            Entry? entry = await _context.Entries
                .Include (s => s.Sheet)
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
        public async Task<ServiceResult> CancelEntryAsync(int userId,CancelEntryDTO request)
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
        public async Task<ServiceResultGeneric<Entry>> GetEntryFromSheetAsync(int userId, int entryId, int sheetId)
        {
            var sheetRes = await _sheetService.GetSheetByIdAsync(sheetId, userId);

            if (!sheetRes.Success)
            {
                return ServiceResultGeneric<Entry>.Fail("No such sheet exists");
            }

            Sheet sheet = sheetRes.Data!;

            var entryRes = sheet.Entries.FirstOrDefault(e => e.Id == entryId);

            if (entryRes == null)
            {
                return ServiceResultGeneric<Entry>.Fail("No such entry exists");
            }

            return ServiceResultGeneric<Entry>.Ok(entryRes);
        }
    }
}
//public async Task<ServiceResultGeneric<int[]>> ImportFromCsvToSheetAsync(int sheetId)
//{
//    string filePath =
//        @"C:\Users\skele\source\repos\FormsApp_CS2_Arbitrage_And_Investment_Tracker\FormsApp_CS2_Arbitrage_And_Investment_Tracker\CSVs\Skins_v2.csv";
//    List<EntryCsvDto> entriesFromCsv = new();

//    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
//    {
//        HeaderValidated = null,
//        MissingFieldFound = null,
//        BadDataFound = null,
//        ShouldSkipRecord = args => args.Row.Parser.Record?.All(string.IsNullOrWhiteSpace) ?? false,
//    };

//    using var reader = new StreamReader(filePath);
//    using var csv = new CsvHelper.CsvReader(reader, config);


//    var records = csv.GetRecordsAsync<EntryCsvDto>();
//    int[] recordsData = new int[2];
//    await foreach (var record in records)
//    {
//        try
//        {
//            if (record.Name == null || record.BuyPrice < 0 || record.SellPrice < 0 || string.IsNullOrEmpty(record.Name))
//            {
//                recordsData[0]++;
//                continue;
//            }
//            SkinInfo skinInfo = new SkinInfo(record.Name, null, null, null);
//            skinInfo.SetItemType();

//            Entry entry = new Entry(1, null, null, record.BuyPrice, record.SellPrice, 1
//                , skinInfo, EntryDataSource.Legacy, record.Return, record.DailyReturn, EntryStatus.Closed);

//            entry.SheetId = sheetId;
//            entry.Profit = entry.SellPrice - entry.BuyPrice;

//            _context.Entries.Add(entry);
//            recordsData[1]++;
//        }
//        catch (Exception)
//        {
//            recordsData[0]++;
//        }
//    }
//    await _context.SaveChangesAsync();
//    return ServiceResultGeneric<int[]>.Ok(recordsData);
//}

//public async Task<ServiceResult> AddEntryAsync(int sheetId, string entryName, int quantity
//    , DateTime dateBought, DateTime? dateSold, decimal buyPrice, decimal? sellPrice, decimal? itemFloat
//    , SkinCondition skinCondition, SkinVariant skinVariant, ItemType itemType)
//{
//    SkinInfo skinInfo = new SkinInfo(entryName, itemFloat, skinVariant
//        , skinCondition);

//    var cosmetics = new ItemType?[] { ItemType.Sticker, ItemType.Charm, ItemType.Patch };

//    if (!cosmetics.Contains(itemType))
//    {
//        skinInfo.SetItemType();
//    }
//    else
//    {
//        skinInfo.ItemType = itemType;
//    }

//    skinInfo.SetMarketHashName();

//    ServiceResultGeneric<decimal> sellOrderPriceResult = await _CSFloatService.GetLowestPriceListingAsync(skinInfo.MarketHashName);

//    if (!sellOrderPriceResult.Success)
//    {
//        return ServiceResult.Fail(sellOrderPriceResult.ErrorMessage);
//    }

//    decimal sellOrderPrice = sellOrderPriceResult.Data;

//    Entry entry = new Entry(quantity, dateBought, buyPrice, sheetId, skinInfo);
//    entry.DataSource = EntryDataSource.Regular;

//    _context.Add(entry);
//    await _context.SaveChangesAsync();

//    return ServiceResult.Ok();
//}