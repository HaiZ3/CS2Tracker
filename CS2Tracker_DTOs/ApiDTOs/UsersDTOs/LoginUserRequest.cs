using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static CS2Tracker_Common.Validations.ModelValidations;

namespace CS2Tracker_DTOs.DTOs.ApiDTOs.UsersDTOs
{
    public class LoginUserRequest
    {
        [Required]
        [MaxLength(UsernameMaxLen),MinLength(UsernameMinLen)]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
    }
}
