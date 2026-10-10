using CS2Tracker_Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Models.DTOs.AppDTOs
{
    public class EntryDisplayDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string BuyPrice { get; set; }
        public string? SellPrice { get; set; }
        public string? Profit { get; set; }
        public string? ReturnPercent { get; set; }
        public DateTime? DateBought { get; set; }
        public DateTime? DateSold { get; set; }
        public EntryStatus Status { get; set; }
        public string LowestSellOrder { get; set; }
    }
}
