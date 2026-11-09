using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.EntriesDTOs
{
    public class CloseEntryDTO
    {
        public int EntryId { get; set; }
        public DateTime DateSold { get; set; }
        public decimal SellPrice { get; set; }
    }
}
