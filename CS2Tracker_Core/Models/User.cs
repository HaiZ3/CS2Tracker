using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            
        }
        public User(string username,string email,string passwordHash)
        {
            this.Username = username;
            this.Email = email;
            this.PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Sheet> Sheets { get; set; }
    }
}