using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        public static ServiceResult Ok()
            => new ServiceResult { Success = true };

        public static ServiceResult Fail(string message)
            => new ServiceResult { Success = false, ErrorMessage = message };
    }
}
