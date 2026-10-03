using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.DTOs
{
    public class CurrencyInfoDto
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
