using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Helpers
{
    public class CurrencyDecimalConverter : DecimalConverter
    {
        public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0m;

            text = text.Replace("$", "").Replace(",", "").Trim();

            // handle (34.14) => -34.14
            if (text.StartsWith("(") && text.EndsWith(")"))
                text = "-" + text[1..^1];

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                return result;

            return 0m;
        }
    }
}
