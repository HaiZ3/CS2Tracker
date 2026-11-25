using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.SingleItemDTOs
{
    public class SkinPriceSingleMarketplacePriceDTO
    {
        [JsonPropertyName("ask")]
        public decimal? Ask { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
