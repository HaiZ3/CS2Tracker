using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.SingleItemDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.BatchItemDTOs
{
    public class SkinPriceBatchResponseDTO
    {
        [JsonPropertyName("data")]
        public Dictionary<string, SkinPriceBatchItemDTO> Data { get; set; } = new();
    }
}
