using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CS2Tracker_Core.Models.DTOs.AppDTOs
{
    public class CSFloatResponseDto
    {
        [JsonPropertyName("data")]
        public List<CSFloatPriceDto> Data { get; set; }

    }
}
