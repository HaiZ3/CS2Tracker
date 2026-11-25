using CS2Tracker_DTOs.SharedDTOs.SkinInfoDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs
{
    public class CreateEntryDTO
    {
        [Required]
        public DateTime DateBought { get; set;  }
        [Required]
        [Range(0.01,int.MaxValue)]
        public decimal BuyPrice { get; set; }
        [Required]
        public int SheetId { get; set; }
        [Required]
        public SkinInfoRequestDTO SkinInfo { get; set; } = null!;
        public CreateEntryDTO(DateTime dateBought, decimal buyPrice, int sheetId, SkinInfoRequestDTO skinInfo)
        {
            DateBought = dateBought;
            BuyPrice = buyPrice;
            SheetId = sheetId;
            SkinInfo = skinInfo;
        }
    }
}
