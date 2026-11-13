using System.ComponentModel.DataAnnotations;
using static CS2Tracker_Common.Validations.ModelValidations;

namespace CS2Tracker_MVC.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(UsernameMaxLen), MinLength(UsernameMinLen)]
        public string Username { get; set; } = "";
        [Required]
        [EmailAddress]
        [MaxLength(UserEmailMaxLen)]
        public string Email { get; set; } = "";
        [Required]
        [MaxLength(UserPasswordMaxLen), MinLength(UserPasswordMinLen)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = "";
    }
}
