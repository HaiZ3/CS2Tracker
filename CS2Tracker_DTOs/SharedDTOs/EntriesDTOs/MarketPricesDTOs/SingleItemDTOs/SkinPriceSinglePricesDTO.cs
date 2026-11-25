using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.SingleItemDTOs
{
    public class SkinPriceSinglePricesDTO
    {
        [JsonPropertyName("youpin")]
        public SkinPriceSingleMarketplacePriceDTO Youpin { get; set; } = null!;

        [JsonPropertyName("csfloat")]
        public SkinPriceSingleMarketplacePriceDTO Csfloat { get; set; } = null!;
    }
}
