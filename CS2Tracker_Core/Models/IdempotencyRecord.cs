using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CS2Tracker_Core.Models
{
    [Table("Idempotency_Records")]
    public class IdempotencyRecord
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Key { get; set; } = null!;

        public string RequestHash { get; set; } = null!;

        public string Response { get; set; } = null!;

        public int StatusCode { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
