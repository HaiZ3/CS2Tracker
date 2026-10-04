using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.SheetDTOs
{
    public class SheetDisplayDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<EntryDisplayDto> Entries { get; set; }
    }
}
