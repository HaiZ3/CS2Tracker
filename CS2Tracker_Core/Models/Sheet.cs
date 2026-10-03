using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes
{
    [Table("Sheets")]
    public class Sheet
    {
        public Sheet()
        {
            
        }
        public Sheet(int userId, string name, SheetType sheetType)
        {
            this.UserId = userId;
            this.Name = name;
            this.SheetType = sheetType;
            CreatedAt = DateTime.Now.ToUniversalTime();
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; } = string.Empty;
        public SheetType SheetType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Entry> Entries { get; set; }

    }
}
