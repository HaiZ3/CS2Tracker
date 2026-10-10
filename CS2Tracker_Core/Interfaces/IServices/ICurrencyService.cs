using CS2Tracker_Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface ICurrencyService
    {
        public Task<ServiceResult> FetchCurrencyInfoAndSaveRatesAsync();
        public Task<ServiceResultGeneric<decimal>> ConvertCurrencyAsync(string fromCurrency, string toCurrency, decimal amount);
    }
}
