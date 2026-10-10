using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs.MarketPricesDTOs
{
    public class SkinPriceDTO
    {
        [JsonPropertyName("item")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("prices")]
        public PricesDTO Prices { get; set; } = null!;

    }
}
