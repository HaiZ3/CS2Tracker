using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CS2Tracker_DTOs.SharedDTOs.EntriesDTOs
{
    public class EditEntryDTO
    {
        public EditEntryDTO(int entryId, DateTime dateBought, decimal buyPrice)
        {
            Id = entryId;
            DateBought = dateBought;
            BuyPrice = buyPrice;
        }

        public int Id { get; set; }

        [Required]
        public DateTime DateBought { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal BuyPrice { get; set; }
    }
}
