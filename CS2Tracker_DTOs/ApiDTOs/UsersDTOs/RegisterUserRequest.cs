using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static CS2Tracker_Common.Validations.ModelValidations;

namespace CS2Tracker_DTOs.DTOs.ApiDTOs.UsersDTOs
{
    public class RegisterUserRequest
    {
        [Required]
        [MaxLength(UsernameMaxLen),MinLength(UsernameMinLen)]
        public string Username { get; set; } = "";
        [Required]
        [EmailAddress]
        [MaxLength(UserEmailMaxLen)]
        public string Email { get; set; } = "";
        [Required]
        [MaxLength(UserPasswordMaxLen),MinLength(UserPasswordMinLen)]
        public string Password { get; set; } = "";
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = "";
    }
}
