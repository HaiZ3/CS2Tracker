using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.SheetDTOs
{
    public class AddNewSheetDTO
    {
        public string Name { get; set; } = "";
        public int Type { get; set; }
    }
}
