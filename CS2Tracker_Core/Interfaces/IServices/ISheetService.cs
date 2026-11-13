using CS2Tracker_Common.Enums;
using CS2Tracker_Common.Responses;
using CS2Tracker_DTOs.ApiDTOs.SheetDTOs;
using CS2Tracker_DTOs.DTOs.ApiDTOs.SheetDTOs;
using CS2Tracker_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface ISheetService
    {
        public Task<ServiceResultGeneric<ICollection<SheetDisplayDTO>>> GetSheetsIdsByUserId(string userId);
        public Task<ServiceResult> CreateSheetAsync(string userId, string name, SheetType sheetType);
        public Task<ServiceResultGeneric<SheetDisplayWithEntriesDTO>> LoadSheetInfo(int sheetId, string userId);
    }
}
