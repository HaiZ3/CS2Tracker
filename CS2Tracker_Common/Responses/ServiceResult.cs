using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Common.Responses
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        public string? UserId { get; set; }
        public string? Username { get; set; }

        public static ServiceResult Ok()
            => new ServiceResult { Success = true };

        public static ServiceResult Ok(string userId, string username)
            => new ServiceResult
            {
                Success = true,
                UserId = userId,
                Username = username
            };

        public static ServiceResult Fail(string message)
            => new ServiceResult
            {
                Success = false,
                ErrorMessage = message
            };
    }
}
