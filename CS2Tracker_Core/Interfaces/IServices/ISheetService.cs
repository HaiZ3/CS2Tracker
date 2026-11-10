using CS2Tracker_Common.Responses;
using CS2Tracker_Models;
using CS2Tracker_Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.SheetDTOs;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface ISheetService
    {
        public Task<ServiceResultGeneric<ICollection<int>>> GetSheetsIdsByUserId(int userId);
        public Task<ServiceResult> CreateSheetAsync(int userId, string name, int sheetType);
        public Task<ServiceResultGeneric<SheetDisplayDto>> LoadSheetInfo(int sheetId, int userId);
    }
}
