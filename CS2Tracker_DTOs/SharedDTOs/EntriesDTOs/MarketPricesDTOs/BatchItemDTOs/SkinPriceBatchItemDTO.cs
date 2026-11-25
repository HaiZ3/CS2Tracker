using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.BatchItemDTOs
{
    public class SkinPriceBatchItemDTO
    {
        [JsonPropertyName("youpin")]
        public SkinPriceBatchMarketplaceAskDTO? Youpin { get; set; }

        [JsonPropertyName("csfloat")]
        public SkinPriceBatchMarketplaceAskDTO? CsFloat { get; set; }

        [JsonPropertyName("links")]
        public SkinPriceLinksDTO? Links { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
