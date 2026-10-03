using CsvHelper.Configuration.Attributes;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.DTOs
{
    public class EntryCsvDto
    {
        [Name("Item")]
        public string Name { get; set; } = string.Empty;

        [Name("Buy Price")]
        [TypeConverter(typeof(CurrencyDecimalConverter))]
        public decimal BuyPrice { get; set; }

        [Name("Sell Price")]
        [TypeConverter(typeof(CurrencyDecimalConverter))]
        public decimal SellPrice { get; set; }

        [Name("Hold Days")]
        public int? HoldDays { get; set; }

        [Name("Profit ")]
        [TypeConverter(typeof(CurrencyDecimalConverter))]
        public decimal Profit { get; set; }

        [Name("Return %")]
        public decimal Return { get; set; }

        [Name("Daily Return %")]
        public decimal DailyReturn { get; set; }
    }
}
