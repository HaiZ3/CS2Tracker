using CS2Tracker_Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static CS2Tracker_Common.Validations.ModelValidations;
using Microsoft.AspNetCore.Identity;

namespace CS2Tracker_Models
{
    [Table("Sheets")]
    public class Sheet
    {
        public Sheet()
        {
            
        }
        public Sheet(string userId, string name, SheetType sheetType)
        {
            this.UserId = userId;
            this.Name = name;
            this.SheetType = sheetType;
            CreatedAt = DateTime.Now.ToUniversalTime();
            UpdatedAt = DateTime.Now.ToUniversalTime();
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;
        public IdentityUser User { get; set; } = null!;
        [Required]
        [MaxLength(SheetNameMaxLen),MinLength(SheetNameMinLen)]
        public string Name { get; set; } = string.Empty;
        public SheetType SheetType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Entry> Entries { get; set; }

    }
}
