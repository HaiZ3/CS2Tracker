using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices
{
    public interface ISheetService
    {
        public Task<ServiceResultGeneric<ICollection<Sheet>>> LoadSheetsAsync(int userId);
        public Task<ServiceResult> CreateSheetAsync(int userId, string name, SheetType sheetType);
        public Task<ServiceResultGeneric<Sheet>> GetSheetByIdAsync(int? sheetId);
    }
}
