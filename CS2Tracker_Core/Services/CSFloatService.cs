using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Context;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.DTOs;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.Responses;
using Microsoft.Extensions.Configuration;
using System.Text.Json;


namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Services
{
    public class CSFloatService : ICSFloatService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly CS2TrackerContext _context;
        private readonly string _apiKey;

        public CSFloatService(HttpClient httpClient, IConfiguration configuration, CS2TrackerContext context)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;
            _apiKey = _configuration["CsFloat:ApiKey"];
        }

        public async Task<ServiceResultGeneric<decimal>> GetLowestPriceListingAsync(string marketHashName)
        {
            var url = $"https://csfloat.com/api/v1/listings?market_hash_name={Uri.EscapeDataString(marketHashName)}&type=buy_now&sort_by=lowest_price&limit=1";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", _apiKey);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return ServiceResultGeneric<decimal>.Fail("unsuccessful response");
            }
            var json = await response.Content.ReadAsStringAsync();

            CSFloatResponseDto data = JsonSerializer.Deserialize<CSFloatResponseDto>(json);

            try
            {
                decimal priceUsd = data.Data[0].Price / 100m;
                return ServiceResultGeneric<decimal>.Ok(priceUsd);
            }
            catch (Exception)
            {
                return ServiceResultGeneric<decimal>.Fail("Failed to get the lowest listing sell price try selecting the item type manually");
            }

        }
        public async Task<ServiceResult> RefreshEntriesSellOrderPricesAsync(Entry[] entries)
        {
            foreach (var entry in entries)
            {
                ServiceResultGeneric<decimal> serviceResult = await GetLowestPriceListingAsync(entry.SkinInfo.MarketHashName);
                if (!serviceResult.Success)
                {
                    return ServiceResult.Fail("Failed to set the lowest sell order price of 1 or more entries!");
                }
                entry.SellOrderPice = serviceResult.Data;
            }

            await _context.SaveChangesAsync();

            return ServiceResult.Ok();

        }

    }
}
