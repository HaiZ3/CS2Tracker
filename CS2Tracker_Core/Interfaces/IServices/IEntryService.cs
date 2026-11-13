using CS2Tracker_Models;
using CS2Tracker_DTOs.DTOs.ApiDTOs.EntriesDTOs;
using CS2Tracker_Common.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface IEntryService
    {
        public Task<ServiceResultGeneric<ICollection<Entry>>> GetEntriesBySheetAsync(int sheetId);
        public Task<ServiceResult> AddEntryAsync(string userId,CreateEntryDTO createEntryDTO);
        public Task<ServiceResult> CloseEntryAsync(string userId,CloseEntryDTO closeEntryDTO);
        public Task<ServiceResult> CancelEntryAsync(string userId,CancelEntryDTO cancelEntryDTO);
        public Task<ServiceResultGeneric<EntryDisplayDto>> GetEntryBySheetIdAsync(string userId, int entryId, int sheetId);
    }
}
