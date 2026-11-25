using CS2Tracker_Common.Responses;
using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.BatchItemDTOs;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.SingleItemDTOs;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;
using System.Text.Json;


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
        public async Task<ServiceResultGeneric<SkinPriceSingleDTO>> GetSkinMarketplacePrice(string marketHashName)
        {
            string cacheKey = $"marketplace-price:{marketHashName}";

            if (_memoryCache.TryGetValue(
                cacheKey, out SkinPriceSingleDTO? cachedPrice)
                && cachedPrice != null
                )
            {
                return ServiceResultGeneric<SkinPriceSingleDTO>.Ok(cachedPrice);
            }

            var url =
                $"https://api.openskin.dev/v1/prices?item={Uri.EscapeDataString(marketHashName)}";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return ServiceResultGeneric<SkinPriceSingleDTO>.Fail(
                    "unsuccessful response");
            }

            var json = await response.Content.ReadAsStringAsync();

            SkinPriceSingleDTO? data =
                JsonSerializer.Deserialize<SkinPriceSingleDTO>(json);

            if (data == null)
            {
                return ServiceResultGeneric<SkinPriceSingleDTO>.Fail(
                    "Failed to deserialize marketplace prices");
            }

            _memoryCache.Set(
                cacheKey,
                data,
                TimeSpan.FromMinutes(10));

            return ServiceResultGeneric<SkinPriceSingleDTO>.Ok(data);
        }
        public async Task<ServiceResult> GetSkinsMarketPlacePrices(
            string[] marketHashNames)
        {
            // Remove duplicates and invalid names
            var uniqueMarketHashNames = marketHashNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .ToList();

            // Identify cache misses
            var uncachedItems = uniqueMarketHashNames
                .Where(item => !_memoryCache.TryGetValue(
                    $"marketplace-price:{item}", out SkinPriceSingleDTO? cached)
                    || cached == null)
                .ToList();

            // Fetch missing prices in batches of 500
            foreach (var batch in uncachedItems.Chunk(500))
            {
                var requestDto = new SkinPriceBatchRequestDTO
                {
                    Items = batch.ToList()
                };

                using var response = await _httpClient.PostAsJsonAsync(
                    "https://api.openskin.dev/v1/prices/batch",
                    requestDto);

                if (!response.IsSuccessStatusCode)
                {
                    return ServiceResult.Fail(
                        "Failed to retrieve marketplace prices");
                }

                var batchResponse =
                    await response.Content.ReadFromJsonAsync<SkinPriceBatchResponseDTO>();

                if (batchResponse == null)
                {
                    return ServiceResult.Fail(
                        "Failed to deserialize marketplace prices");
                }

                // Cache each individual skin
                foreach (var item in batchResponse.Data)
                {
                    string cacheKey = $"marketplace-price:{item.Key}";

                    SkinPriceBatchItemDTO skinPriceBatchItemDTO = item.Value;

                    SkinPriceSingleDTO skinPrice = new SkinPriceSingleDTO()
                    {
                        Name = item.Key,
                        Links = new SkinPriceSingleLinksDTO
                        {
                            Csfloat = skinPriceBatchItemDTO.Links?.CsFloat,
                            Youpin = skinPriceBatchItemDTO.Links?.Youpin
                        },
                        Prices = new SkinPriceSinglePricesDTO
                        {
                            Csfloat = new SkinPriceSingleMarketplacePriceDTO
                            {
                                Ask = skinPriceBatchItemDTO.CsFloat?.Ask,
                                UpdatedAt = skinPriceBatchItemDTO.UpdatedAt
                            },
                            Youpin = new SkinPriceSingleMarketplacePriceDTO
                            {
                                Ask = skinPriceBatchItemDTO.Youpin?.Ask,
                                UpdatedAt = skinPriceBatchItemDTO.UpdatedAt
                            }
                        }
                        
                    };

                    _memoryCache.Set(
                        cacheKey,
                        skinPrice,
                        TimeSpan.FromMinutes(10));
                }
            }
            return ServiceResult.Ok();
        }

    }
}
