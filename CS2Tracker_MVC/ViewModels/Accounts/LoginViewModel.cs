using System.ComponentModel.DataAnnotations;


namespace CS2Tracker_MVC.ViewModels.Accounts
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = "";
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
}
