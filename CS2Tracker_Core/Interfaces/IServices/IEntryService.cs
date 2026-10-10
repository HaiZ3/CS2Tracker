using CS2Tracker_Core.Models;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs;
using CS2Tracker_Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface IEntryService
    {
        public Task<ServiceResultGeneric<ICollection<Entry>>> GetEntriesBySheetAsync(int sheetId);
        public Task<ServiceResult> AddEntryAsync(Entry entry,SkinInfo skinInfo,int userId);
        public Task<ServiceResult> CloseEntryAsync(int userId,CloseEntryDTO closeEntryDTO);
        public Task<ServiceResult> CancelEntryAsync(int entryId,CancelEntryDTO cancelEntryDTO);
        public Task<ServiceResultGeneric<Entry>> GetEntryFromSheetAsync(int userId, int entryId, int sheetId);
    }
}
