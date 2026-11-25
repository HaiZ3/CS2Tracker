using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_DTOs.SharedDTOs.DashBoardDTOs
{
    public class DashboardSheetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal RealizedProfit { get; set; }
        public int OpenEntries { get; set; }
        public decimal CapitalInOpenEntries { get; set; }
        public decimal Allocation { get; set; }
    }
}
