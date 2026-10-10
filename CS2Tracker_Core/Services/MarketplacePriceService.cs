using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs.MarketPricesDTOs;
using CS2Tracker_Core.Models.DTOs.AppDTOs;
using CS2Tracker_Core.Models.Responses;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace CS2Tracker_Core.Services
{
    public class MarketplacePriceService : IMarketplacePriceService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        public MarketplacePriceService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }
        public async Task<ServiceResultGeneric<SkinPriceDTO>> GetSkinMarketplacePrice(string marketHashName)
        {
            string cacheKey = $"marketplace-price:{marketHashName}";

            if (_memoryCache.TryGetValue(
                cacheKey, out SkinPriceDTO? cachedPrice)
                && cachedPrice != null
                )
            {
                return ServiceResultGeneric<SkinPriceDTO>.Ok(cachedPrice);
            }

            var url =
                $"https://api.openskin.dev/v1/prices?item={Uri.EscapeDataString(marketHashName)}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return ServiceResultGeneric<SkinPriceDTO>.Fail(
                    "unsuccessful response");
            }

            var json = await response.Content.ReadAsStringAsync();

            SkinPriceDTO? data =
                JsonSerializer.Deserialize<SkinPriceDTO>(json);

            if (data == null)
            {
                return ServiceResultGeneric<SkinPriceDTO>.Fail(
                    "Failed to deserialize marketplace prices");
            }

            _memoryCache.Set(
                cacheKey,
                data,
                TimeSpan.FromMinutes(10));

            return ServiceResultGeneric<SkinPriceDTO>.Ok(data);
        }
    }
}
