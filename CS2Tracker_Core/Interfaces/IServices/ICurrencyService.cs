using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices
{
    public interface ICurrencyService
    {
        public Task<ServiceResult> FetchCurrencyInfoAndSaveRatesAsync();
        public Task<ServiceResultGeneric<decimal>> ConvertCurrencyAsync(string fromCurrency, string toCurrency, decimal amount);
    }
}
