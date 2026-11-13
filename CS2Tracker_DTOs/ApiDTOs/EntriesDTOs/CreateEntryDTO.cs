using CS2Tracker_DTOs.DTOs.ApiDTOs.SkinInfoDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CS2Tracker_DTOs.DTOs.ApiDTOs.EntriesDTOs
{
    public class CreateEntryDTO
    {
        public int Quantity { get; set; }
        [Required]
        public DateTime DateBought { get; set;  }
        [Required]
        [Range(0,int.MaxValue)]
        public decimal BuyPrice { get; set; }
        [Required]
        public int SheetId { get; set; }
        [Required]
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
