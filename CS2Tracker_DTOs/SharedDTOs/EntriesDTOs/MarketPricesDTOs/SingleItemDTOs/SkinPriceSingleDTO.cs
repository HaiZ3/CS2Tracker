using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.SingleItemDTOs
{
    public class SkinPriceSingleDTO
    {
        [JsonPropertyName("item")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("prices")]
        public SkinPriceSinglePricesDTO Prices { get; set; } = null!;
        [JsonPropertyName("links")]
        public SkinPriceSingleLinksDTO? Links { get; set; }
    }
}
