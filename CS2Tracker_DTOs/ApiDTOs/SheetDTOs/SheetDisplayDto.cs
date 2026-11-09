using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntriesDTOs;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.SheetDTOs
{
    public class SheetDisplayDto
    {
        public SheetDisplayDto(int id, string name, string type, DateTime createdAt, DateTime updatedAt, ICollection<EntryDisplayDto> entries)
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
