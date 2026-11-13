using CS2Tracker_DTOs.DTOs.ApiDTOs.EntriesDTOs;

namespace CS2Tracker_DTOs.DTOs.ApiDTOs.SheetDTOs
{
    public class SheetDisplayDTO
    {
        public SheetDisplayDTO()
        {
            
        }
        public SheetDisplayDTO(int id, string name, string type, DateTime createdAt, DateTime updatedAt, int entries)
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
        public int Entries { get; set; }
    }
}
