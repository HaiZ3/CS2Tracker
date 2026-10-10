using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CS2Tracker_Core.Models
{
    public class DailyStat
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalDailyInvested { get; set; }

        public decimal TotalDailyProfit { get; set; }

        public decimal TotalDailyVolume { get; set; }

        public int TradeCount { get; set; }

        [NotMapped]
        public decimal DailyROI =>
            TotalDailyInvested == 0
                ? 0
                : TotalDailyProfit / TotalDailyInvested;
    }
}
