using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs.MarketPricesDTOs.BatchItemDTOs
{
    public class SkinPriceBatchRequestDTO
    {
        public ICollection<string> Items { get; set; } = new List<string>();
    }
}
