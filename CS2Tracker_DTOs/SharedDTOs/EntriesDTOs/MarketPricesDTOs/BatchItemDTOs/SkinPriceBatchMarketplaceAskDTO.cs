using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.BatchItemDTOs
{
    public class SkinPriceBatchMarketplaceAskDTO
    {
        [JsonPropertyName("ask")]
        public decimal? Ask { get; set; }
    }
}
