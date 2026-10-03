using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.DTOs
{
    public class CSFloatResponseDto
    {
        [JsonPropertyName("data")]
        public List<CSFloatPriceDto> Data { get; set; }

    }
}
