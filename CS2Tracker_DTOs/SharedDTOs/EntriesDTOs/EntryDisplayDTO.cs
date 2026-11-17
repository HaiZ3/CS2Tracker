using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs
{
    public class EntryDisplayDTO
    {
        public EntryDisplayDTO(int id, string name, int quantity, string status, decimal? profit, decimal? ret,DateTime dateBought,DateTime? dateSold, int? holdDays, decimal buyPrice, decimal? sellPrice, decimal? youPinPrice, decimal? csFloatPrice)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Status = status;
            Profit = profit;
            ReturnPercent = ret * 100;
            DateBought = dateBought;
            DateSold = dateSold;
            HoldDays = holdDays;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            YouPinPrice = youPinPrice;
            CsFloatPrice = csFloatPrice;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public string Status { get; set; } = "";
        public decimal? Profit { get; set; }
        public decimal? ReturnPercent { get; set; }
        public DateTime DateBought { get; set; }
        public DateTime? DateSold { get; set; } 
        public int? HoldDays { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? YouPinPrice { get; set; }
        public decimal? CsFloatPrice { get; set; }
        public decimal? UnrealizedProfit
        {
            get
            {
                if (SellPrice is not null)
                {
                    return null;
                }

                var perUnitProfits = new[] { YouPinPrice, CsFloatPrice }
                    .Where(p => p is not null)
                    .Select(p => p!.Value - BuyPrice)
                    .ToList();

                if (perUnitProfits.Count == 0)
                {
                    return null;
                }

                return perUnitProfits.Max() * Quantity;
            }
        }
        public string? YouPinUrl {  get; set; }
        public string? CsFloatUrl { get; set; }
    }
}
