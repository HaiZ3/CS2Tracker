using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CS2Tracker_DTOs.DTOs.ApiDTOs.EntriesDTOs
{
    public class CloseEntryDTO
    {
        [Required]
        public int EntryId { get; set; }
        [Required]
        public DateTime DateSold { get; set; }
        [Required]
        [Range(0,int.MaxValue)]
        public decimal SellPrice { get; set; }
    }
}
