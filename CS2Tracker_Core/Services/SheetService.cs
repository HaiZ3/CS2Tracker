using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Context;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.Responses;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Services
{
    public class SheetService : ISheetService
    {
        private CS2TrackerContext _context { get; set; }
        public SheetService(CS2TrackerContext context)
        {
            _context = context;
        }
        public async Task<ServiceResult> CreateSheetAsync(int userId, string name, SheetType sheetType)
        {
            Sheet sheet = new Sheet(userId, name, sheetType);

            if (string.IsNullOrEmpty(name))
            {
                return ServiceResult.Fail("Failed to add the sheet to the database!");
            }
            await _context.Sheets.AddAsync(sheet);
            await _context.SaveChangesAsync();
            return ServiceResult.Ok();
        }
        
        public async Task<ServiceResultGeneric<ICollection<Sheet>>> LoadSheetsAsync(int userId)
        {
            ICollection<Sheet>? sheets = _context.Sheets.Where(s => s.UserId == userId).ToList();
            if(sheets is null || sheets.Count == 0)
            {
                return ServiceResultGeneric<ICollection<Sheet>>.Fail("Failed to load the sheets or there are no existing sheets!");
            }
            return ServiceResultGeneric<ICollection<Sheet>>.Ok(sheets);
        }

        public async Task<ServiceResultGeneric<Sheet>> GetSheetByIdAsync(int sheetId,int userId)
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
                return ServiceResultGeneric<Sheet>.Fail("No such sheet exists!");
            }
            return ServiceResultGeneric<Sheet>.Ok(sheet);
        }
    }
}
