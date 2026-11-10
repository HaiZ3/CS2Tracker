using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CS2Tracker_Models
{
    public class User : IdentityUser<int>
    {
        public User()
        {
        }

        public User(string username,string email,string passwordHash)
        {
            UserName = username;
            Email = email;
            this.PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Sheet> Sheets { get; set; } = new List<Sheet>();
    }
}
