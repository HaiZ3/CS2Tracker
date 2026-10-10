using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs
{
    public class EntryDisplayDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public string Status { get; set; } = "";
        public string DataSource { get; set; } = "";
        public decimal? Profit { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? YouPinPrice { get; set; }
        public decimal? CsFloatPrice { get; set; }
    }
}
