using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes
{
    public class OverallStat
    {
        [Key]
        public int Id { get; set; }

        public decimal BuyVolume { get; set; }
        public decimal SellVolume { get; set; }

        [NotMapped]
        public decimal TotalVolume
        {
            get { return BuyVolume + SellVolume; }
        }

        [NotMapped]
        public decimal ProfitVolume
        {
            get { return SellVolume - BuyVolume; }
        }

        public decimal TotalProfit { get; set; }

        public int TradeCount { get; set; }

        // IMPORTANT: store TOTAL hold days (not average)
        public int TotalHoldDays { get; set; }

        [NotMapped]
        public decimal WeightedROI
        {
            get
            {
                return BuyVolume == 0
                    ? 0
                    : TotalProfit / BuyVolume;
            }
        }

        [NotMapped]
        public decimal AvgDailyReturn
        {
            get
            {
                return TotalHoldDays == 0
                    ? 0
                    : TotalProfit / TotalHoldDays;
            }
        }

        [NotMapped]
        public decimal AvgROI
        {
            get
            {
                return TradeCount == 0
                    ? 0
                    : TotalProfit / TradeCount;
            }
        }

        [NotMapped]
        public decimal AvgHoldDays
        {
            get
            {
                return TradeCount == 0
                    ? 0
                    : (decimal)TotalHoldDays / TradeCount;
            }
        }

        public DateTime LastUpdated { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
