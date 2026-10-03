using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.DTOs;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices
{
    public interface ICSFloatService
    {
        public Task<ServiceResultGeneric<decimal>> GetLowestPriceListingAsync(string marketHashName);
        public Task<ServiceResult> RefreshEntriesSellOrderPricesAsync(Entry[] entries);

    }
}
