using CS2Tracker_Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static CS2Tracker_Common.Validations.ModelValidations;

namespace CS2Tracker_DTOs.SharedDTOs.SheetDTOs
{
    public class SheetEditDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(SheetNameMaxLen), MinLength(SheetNameMinLen)]
        public string Name { get; set; } = "";
        [Required]
        [EnumDataType(typeof(SheetType))]
        public string Type { get; set; }
    }
}
