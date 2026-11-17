using CS2Tracker_Models;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs;
using CS2Tracker_Common.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface IEntryService
    {
        public Task<ServiceResultGeneric<ICollection<Entry>>> GetEntriesBySheetAsync(string userId,int sheetId);
        public Task<ServiceResult> AddEntryAsync(string userId,CreateEntryDTO createEntryDTO);
        public Task<ServiceResult> CloseEntryAsync(string userId,CloseEntryDTO closeEntryDTO);
        public Task<ServiceResult> CancelEntryAsync(string userId,int entryId);
        public Task<ServiceResult> EditEntryAsync(string userId,EditEntryDTO editEntryDTO);
        public Task<ServiceResultGeneric<EntryDisplayDTO>> GetEntryBySheetIdAsync(string userId, int entryId, int sheetId);
        public Task<ServiceResultGeneric<Entry>> GetEntryByIdAsync(string userId,int entryId);
    }
}
