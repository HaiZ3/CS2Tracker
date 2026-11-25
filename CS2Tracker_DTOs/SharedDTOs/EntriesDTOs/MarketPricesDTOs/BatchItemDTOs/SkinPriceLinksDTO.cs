using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.BatchItemDTOs
{
    public class SkinPriceLinksDTO
    {
        [JsonPropertyName("youpin")]
        public string? Youpin { get; set; }

        [JsonPropertyName("csfloat")]
        public string? CsFloat { get; set; }
    }
}
