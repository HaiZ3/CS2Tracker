using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.SkinInfoDTOs
{
    public class SkinInfoRequestDTO
    {
        public SkinInfoRequestDTO(string name, decimal? @float, SkinVariant skinVariant, SkinCondition skinCondition)
        {
            Name = name;
            Float = @float;
            SkinVariant = skinVariant;
            SkinCondition = skinCondition;
        }

        public string Name { get; set; }
        public decimal? Float { get; set; }
        public SkinVariant SkinVariant { get; set; }
        public SkinCondition SkinCondition { get; set; }
    }
}
