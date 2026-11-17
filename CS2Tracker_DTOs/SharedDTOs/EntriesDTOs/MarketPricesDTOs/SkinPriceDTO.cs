using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs
{
    public class SkinPriceDTO
    {
        [JsonPropertyName("item")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("prices")]
        public PricesDTO Prices { get; set; } = null!;
        [JsonPropertyName("links")]
        public LinksDTO? Links { get; set; }
    }
}
