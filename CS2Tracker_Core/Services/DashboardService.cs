using CS2Tracker_Common.Enums;
using CS2Tracker_Common.Responses;
using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Data.Context;
using CS2Tracker_DTOs.SharedDTOs.DashBoardDTOs;
using CS2Tracker_Models;
using Microsoft.EntityFrameworkCore;

namespace CS2Tracker_Core.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly CS2TrackerContext _context;
        private readonly ISheetService _sheetService;

        public DashboardService(CS2TrackerContext context, ISheetService sheetService)
        {
            _context = context;
            _sheetService = sheetService;
        }

        public async Task<ServiceResultGeneric<DashBoardSummaryDTO>> GetSheetDashboardAsync(string userId)
        {
            ICollection<Sheet> sheets = await _context.Sheets
                .Include(s => s.Entries)
                .ThenInclude(e => e.SkinInfo)
                .Where(s => s.UserId == userId)
                .ToListAsync();

            List<DashboardSheetDTO> dashboardSheetDTOs = new List<DashboardSheetDTO>();

            decimal totalProfit = 0;
            decimal totalUnrealizedProfit = 0;
            int openEntries = 0;
            decimal totalCapitalInOpenEntries = 0;
            decimal cost = 0;
            int closedEntries = 0;
            int profitableEntries = 0;
            decimal weightedReturn = 0;
            decimal winRate = 0;

            decimal totalCapitalInSheets = sheets
                .SelectMany(s => s.Entries)
                .Where(e => e.Status == EntryStatus.Open)
                .Sum(e => e.BuyPrice * e.Quantity);

            foreach (var sheet in sheets)
            {
                var res = await _sheetService.GetSheetEntriesCurrentMarketPricesAndAllocationPerEntry(sheet);

                decimal? capitalInSheet = sheet.Entries
                    .Where(e => e.Status == EntryStatus.Open)
                    .Sum(e => e.BuyPrice * e.Quantity);

                decimal? allocationPerSheet = totalCapitalInSheets > 0
                    ? capitalInSheet / totalCapitalInSheets
                    : null;

                dashboardSheetDTOs.Add(new DashboardSheetDTO()
                {
                    Id = sheet.Id,
                    Name = sheet.Name,
                    CapitalInOpenEntries = capitalInSheet ?? 0,
                    Allocation = allocationPerSheet ?? 0,
                    RealizedProfit = sheet.Entries.Sum(e => e.Profit ?? 0),
                    OpenEntries = sheet.Entries.Count(e => e.Status == EntryStatus.Open)
                });

                foreach (var entry in res.Data)
                {
                    if (entry.Status == EntryStatus.Open.ToString())
                    {
                        totalCapitalInOpenEntries += entry.BuyPrice;
                        openEntries++;
                        totalUnrealizedProfit += entry.UnrealizedProfit ?? 0;
                    }
                    else
                    {
                        totalProfit += entry.Profit ?? 0;
                        cost += entry.BuyPrice * entry.Quantity;
                        closedEntries++;
                        if (entry.Profit > 0)
                        {
                            profitableEntries++;
                        }
                    }
                }
            }



            if (cost > 0)
            {
                weightedReturn = totalProfit / cost;
            }
            if(closedEntries > 0)
            {
                winRate = (decimal)profitableEntries / closedEntries;
            }

            DashBoardSummaryDTO sheetDisplayDashboardDTO = new DashBoardSummaryDTO()
            {
                TotalProfit = totalProfit,
                TotalUnrealizedProfit = totalUnrealizedProfit,
                TotalCapitalInOpenEntries = totalCapitalInOpenEntries,
                OpenEntries = openEntries,
                WeightedReturn = weightedReturn,
                WinRate = winRate,
                Sheets = dashboardSheetDTOs
            };

            return ServiceResultGeneric<DashBoardSummaryDTO>.Ok(sheetDisplayDashboardDTO);
        }
    }
}
