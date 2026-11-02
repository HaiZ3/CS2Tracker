using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_Core.Models.DTOs.AppDTOs
{
    public class CSFloatPriceDto
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
