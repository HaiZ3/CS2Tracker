using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models
{
    [Table("CurrencyInfos")]
    public class CurrencyInfo
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(3)]
        public string FromCurrency { get; set; }
        [MaxLength(3)]
        public string ToCurrency { get; set; }
        public decimal ConversionRate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
