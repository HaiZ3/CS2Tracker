using CsvHelper.Configuration.Attributes;
using CS2Tracker_Common.Helpers;
using System.ComponentModel;
using System;
using CsvTypeConverter = CsvHelper.Configuration.Attributes.TypeConverterAttribute;
using System.Collections.Generic;
using System.Text;
namespace CS2Tracker_DTOs.DTOs.AppDTOs
{
    public class EntryCsvDto
    {
        [Name("Item")]
        public string Name { get; set; } = string.Empty;

        [Name("Buy Price")]
        [CsvTypeConverter(typeof(CurrencyDecimalConverter))]
        public decimal BuyPrice { get; set; }

        [Name("Sell Price")]
        [CsvTypeConverter(typeof(CurrencyDecimalConverter))]
        public decimal SellPrice { get; set; }

        [Name("Hold Days")]
        public int? HoldDays { get; set; }

        [Name("Profit ")]
        [CsvTypeConverter(typeof(CurrencyDecimalConverter))]
        public decimal Profit { get; set; }

        [Name("Return %")]
        public decimal Return { get; set; }

        [Name("Daily Return %")]
        public decimal DailyReturn { get; set; }
    }
}
