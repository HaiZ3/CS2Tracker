using CS2Tracker_Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static CS2Tracker_Common.Validations.ModelValidations;

namespace CS2Tracker_DTOs.DTOs.ApiDTOs.SheetDTOs
{
    public class AddNewSheetDTO
    {
        [Required]
        [MaxLength(SheetNameMaxLen),MinLength(SheetNameMinLen)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(SheetType))]
        public SheetType SheetType { get; set; }
    }
}
