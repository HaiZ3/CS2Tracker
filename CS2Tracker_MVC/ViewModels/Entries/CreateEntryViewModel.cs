using System.ComponentModel.DataAnnotations;
using CS2Tracker_Common.Enums;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs;
using CS2Tracker_DTOs.SharedDTOs.SkinInfoDTOs;
using static CS2Tracker_Common.Validations.ModelValidations;

namespace CS2Tracker_MVC.ViewModels.Entries
{
    public class CreateEntryViewModel
    {
        [Required]
        [Display(Name = "Sheet")]
        public int SheetId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date bought")]
        public DateTime DateBought { get; set; } = DateTime.Today;

        [Required]
        [Range(0, int.MaxValue)]
        [DataType(DataType.Currency)]
        [Display(Name = "Buy price")]
        public decimal BuyPrice { get; set; }

        [Required]
        [MaxLength(SkinInfoNameMaxLen), MinLength(SkinInfoNameMinLen)]
        [Display(Name = "Item name")]
        public string SkinName { get; set; } = string.Empty;

        [Range(SkinInfoFloatMin, SkinInfoFloatMax)]
        [Display(Name = "Float")]
        public decimal? Float { get; set; }

        [Required]
        [Display(Name = "Variant")]
        public SkinVariant SkinVariant { get; set; }

        [Required]
        [Display(Name = "Condition")]
        public SkinCondition SkinCondition { get; set; }

        public CreateEntryDTO ToDto() =>
            new CreateEntryDTO(
                DateBought,
                BuyPrice,
                SheetId,
                new SkinInfoRequestDTO(SkinName, Float, SkinVariant, SkinCondition));
    }
}