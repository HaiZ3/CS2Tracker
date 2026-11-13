using CS2Tracker_DTOs.DTOs.ApiDTOs.EntriesDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_DTOs.ApiDTOs.SheetDTOs
{
    public class SheetDisplayWithEntriesDTO
    {
        public SheetDisplayWithEntriesDTO()
        {
            
        }
        public SheetDisplayWithEntriesDTO(int id, string name, string type, DateTime createdAt, DateTime updatedAt, ICollection<EntryDisplayDto> entries)
        {
            Id = id;
            Name = name;
            Type = type;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Entries = entries;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<EntryDisplayDto> Entries { get; set; }
    }
}
