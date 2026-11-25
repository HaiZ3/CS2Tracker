using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_DTOs.SharedDTOs.DashBoardDTOs
{
    public class DashBoardSummaryDTO
    {
        public decimal TotalProfit { get; set; }
        public decimal TotalUnrealizedProfit {  get; set; }
        public int OpenEntries { get; set; }
        public decimal TotalCapitalInOpenEntries { get; set; }
        public decimal WinRate { get; set; }
        public decimal WeightedReturn { get; set; }
        public ICollection<DashboardSheetDTO> Sheets { get; set; } = new List<DashboardSheetDTO>();

    }
}
