using CS2Tracker_Common.Responses;
using CS2Tracker_Models;
using CS2Tracker_Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface ISheetService
    {
        public Task<ServiceResultGeneric<ICollection<Sheet>>> LoadSheetsAsync(int userId);
        public Task<ServiceResult> CreateSheetAsync(int userId, string name, SheetType sheetType);
        public Task<ServiceResultGeneric<Sheet>> GetSheetByIdAsync(int sheetId,int userId);
    }
}
