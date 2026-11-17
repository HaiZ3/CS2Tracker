using System.ComponentModel.DataAnnotations;

namespace CS2Tracker_MVC.ViewModels.Entries
{
    public class CloseEntryViewModel
    {
        public int EntryId { get; set; }
        public int SheetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateBought { get; set; }
        public decimal BuyPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date sold")]
        public DateTime DateSold { get; set; } = DateTime.Today;

        [Required]
        [Range(0, int.MaxValue)]
        [DataType(DataType.Currency)]
        [Display(Name = "Sell price")]
        public decimal SellPrice { get; set; }
    }
}