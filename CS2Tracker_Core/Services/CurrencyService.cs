using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Context;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.DTOs;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Services
{
    public class CurrencyService : ICurrencyService
    {
        HttpClient _httpClient;
        CS2TrackerContext _context;
        public CurrencyService(HttpClient httpClient, CS2TrackerContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }
        public async Task<ServiceResult> FetchCurrencyInfoAndSaveRatesAsync()
        {
            _httpClient.BaseAddress = new Uri("https://api.exchangerate-api.com/v4/latest/");

            CurrencyInfo? lastCurrencyInfoUpdate = await _context.CurrencyInfos.FirstOrDefaultAsync();
            if (lastCurrencyInfoUpdate != null)
            {
                if (lastCurrencyInfoUpdate.LastUpdate.Date == DateTime.UtcNow.Date)
                {
                    return ServiceResult.Fail("Conversion rates are up to date");
                }
            }

            _context.CurrencyInfos.RemoveRange(_context.CurrencyInfos);

            const string usd = "USD";
            const string eur = "EUR";
            const string cny = "CNY";

            var usdInfo = await _httpClient.GetFromJsonAsync<CurrencyInfoDto>(_httpClient.BaseAddress + usd);
            var eurInfo = await _httpClient.GetFromJsonAsync<CurrencyInfoDto>(_httpClient.BaseAddress + eur);
            var cnyInfo = await _httpClient.GetFromJsonAsync<CurrencyInfoDto>(_httpClient.BaseAddress + cny);

            DateTime lastUpdate = usdInfo.Date;

            CurrencyInfo currencyInfoUsdToCny = new CurrencyInfo
            {
                FromCurrency = usd,
                ToCurrency = cny,
                ConversionRate = usdInfo.Rates[cny],
                LastUpdate = lastUpdate
            };

            CurrencyInfo currencyInfoUsdToEur = new CurrencyInfo
            {
                FromCurrency = usd,
                ToCurrency = eur,
                ConversionRate = usdInfo.Rates[eur],
                LastUpdate = lastUpdate
            };

            CurrencyInfo currencyInfoEurToCny = new CurrencyInfo
            {
                FromCurrency = eur,
                ToCurrency = cny,
                ConversionRate = eurInfo.Rates[cny],
                LastUpdate = lastUpdate
            };

            CurrencyInfo currencyInfoEurToUsd = new CurrencyInfo
            {
                FromCurrency = eur,
                ToCurrency = usd,
                ConversionRate = eurInfo.Rates[usd],
                LastUpdate = lastUpdate
            };

            CurrencyInfo currencyInfoCnyToUsd = new CurrencyInfo
            {
                FromCurrency = cny,
                ToCurrency = usd,
                ConversionRate = cnyInfo.Rates[usd],
                LastUpdate = lastUpdate
            };

            CurrencyInfo currencyInfoCnyToEur = new CurrencyInfo
            {
                FromCurrency = cny,
                ToCurrency = eur,
                ConversionRate = cnyInfo.Rates[eur],
                LastUpdate = lastUpdate
            };

            _context.CurrencyInfos.Add(currencyInfoUsdToCny);
            _context.CurrencyInfos.Add(currencyInfoUsdToEur);

            _context.CurrencyInfos.Add(currencyInfoEurToCny);
            _context.CurrencyInfos.Add(currencyInfoEurToUsd);

            _context.CurrencyInfos.Add(currencyInfoCnyToUsd);
            _context.CurrencyInfos.Add(currencyInfoCnyToEur);

            await _context.SaveChangesAsync();

            return ServiceResult.Ok();
        }
        public async Task<ServiceResultGeneric<decimal>> ConvertCurrencyAsync(string fromCurrency, string toCurrency, decimal amount)
        {
            CurrencyInfo? currencyInfo = await _context.CurrencyInfos
                .FirstOrDefaultAsync(x => x.FromCurrency == fromCurrency && x.ToCurrency == toCurrency);
            if (currencyInfo == null)
            {
                return ServiceResultGeneric<decimal>.Fail("Fetch the currency rates again");
            }

            decimal rate = currencyInfo.ConversionRate;

            decimal result = amount * rate;

            return ServiceResultGeneric<decimal>.Ok(result);

        }
    }
}
