using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.SheetDTOs
{
    public class AddNewSheetDTO
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 3)]
        public int Type { get; set; }
    }
}
