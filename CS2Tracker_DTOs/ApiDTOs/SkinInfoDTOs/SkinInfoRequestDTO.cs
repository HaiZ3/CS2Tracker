using CS2Tracker_Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static CS2Tracker_Common.Validations.ModelValidations;

namespace CS2Tracker_DTOs.DTOs.ApiDTOs.SkinInfoDTOs
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
        [Required]
        [MaxLength(SkinInfoNameMaxLen),MinLength(SkinInfoNameMinLen)]
        public string Name { get; set; }

        [Range(SkinInfoFloatMin,SkinInfoFloatMax)]
        public decimal? Float { get; set; }
        [Required]
        [AllowedValues(typeof(SkinVariant))]
        public SkinVariant SkinVariant { get; set; }
        [Required]
        [AllowedValues(typeof(SkinCondition))]
        public SkinCondition SkinCondition { get; set; }
    }
}
