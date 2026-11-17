using CS2Tracker_Common.Enums;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs;
using System.ComponentModel.DataAnnotations;

namespace CS2Tracker_MVC.ViewModels.Entries
{
    public class EditEntryViewModel
    {
        public int EntryId { get; set; }
        public int SheetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public SkinVariant SkinVariant { get; set; }
        public SkinCondition SkinCondition { get; set; }
        public decimal? Float { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date bought")]
        public DateTime DateBought { get; set; } = DateTime.Today;

        [Required]
        [Range(0, int.MaxValue)]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = "Buy price")]
        public decimal BuyPrice { get; set; }

        public EditEntryDTO ToDto() => new EditEntryDTO(EntryId, DateBought, BuyPrice);
    }
}