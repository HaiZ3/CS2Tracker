using CS2Tracker_Models;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs;
using CS2Tracker_Common.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface IEntryService
    {
        public Task<ServiceResultGeneric<ICollection<Entry>>> GetEntriesBySheetAsync(int sheetId);
        public Task<ServiceResult> AddEntryAsync(int userId,CreateEntryDTO createEntryDTO);
        public Task<ServiceResult> CloseEntryAsync(int userId,CloseEntryDTO closeEntryDTO);
        public Task<ServiceResult> CancelEntryAsync(int entryId,CancelEntryDTO cancelEntryDTO);
        public Task<ServiceResultGeneric<EntryDisplayDto>> GetEntryBySheetIdAsync(int userId, int entryId, int sheetId);
    }
}
