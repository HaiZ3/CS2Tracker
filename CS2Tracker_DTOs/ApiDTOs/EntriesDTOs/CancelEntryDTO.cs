using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CS2Tracker_DTOs.DTOs.ApiDTOs.EntriesDTOs
{
    public class CancelEntryDTO
    {
        [Required]
        public int EntryId { get; set; }
    }
}
