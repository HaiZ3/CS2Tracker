using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices
{
    public interface IEntryService
    {
        //public Task<ServiceResult> AddEntryAsync(int sheetId, string entryName, int quantity
        //    , DateTime dateBought, DateTime? dateSold, decimal buyPrice, decimal? sellPrice, decimal? itemFloat
        //    , SkinCondition skinCondition, SkinVariant skinVariant,ItemType itemType);
        public Task<ServiceResultGeneric<ICollection<Entry>>> GetEntriesBySheetAsync(int sheetId);
        public Task<ServiceResult> AddEntryAsync(Entry entry,SkinInfo skinInfo,int userId);
        public Task<ServiceResult> CloseEntryAsync(int userId,CloseEntryDTO closeEntryDTO);
        public Task<ServiceResult> CancelEntryAsync(int entryId,CancelEntryDTO cancelEntryDTO);
        //public Task<ServiceResultGeneric<int[]>> ImportFromCsvToSheetAsync(int sheetId);
        public Task<ServiceResultGeneric<Entry>> GetEntryFromSheetAsync(int userId, int entryId, int sheetId);
    }
}