using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Models.DTOs.ApiDTOs.UsersDTOs
{
    public class LoginUserRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
