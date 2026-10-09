using CS2Tracker_Core.Models.DTOs.ApiDTOs.SkinInfoDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs
{
    public class CreateEntryDTO
    {
        public int Quantity { get; set; }
        public DateTime DateBought { get; set;  }
        public decimal BuyPrice { get; set; }
        public int SheetId { get; set; }
        public SkinInfoRequestDTO SkinInfo { get; set; } = null!;
        public CreateEntryDTO(int quantity, DateTime dateBought, decimal buyPrice, int sheetId, SkinInfoRequestDTO skinInfo)
        {
            Quantity = quantity;
            DateBought = dateBought;
            BuyPrice = buyPrice;
            SheetId = sheetId;
            SkinInfo = skinInfo;
        }
    }
}
