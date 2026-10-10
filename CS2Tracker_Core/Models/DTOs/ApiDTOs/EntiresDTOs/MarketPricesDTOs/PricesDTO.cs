using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs.MarketPricesDTOs
{
    public class PricesDTO
    {
        [JsonPropertyName("youpin")]
        public MarketplacePriceDTO Youpin { get; set; } = null!;

        [JsonPropertyName("csfloat")]
        public MarketplacePriceDTO Csfloat { get; set; } = null!;
    }
}
