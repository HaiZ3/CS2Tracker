using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs.MarketPricesDTOs
{
    public class MarketplacePriceDTO
    {
        [JsonPropertyName("ask")]
        public decimal? Ask { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
