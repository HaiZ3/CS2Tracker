using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes
{
    [Table("Entries")]
    [Index(nameof(Status))]
    [Index(nameof(DateSold))]
    [Index(nameof(DateBought))]
    [Index(nameof(SheetId))]
    [Index(nameof(Status), nameof(DateSold))]
    public class Entry
    {
        public Entry()
        {
            
        }
        //Regular entry ctor
        public Entry(int quantity,DateTime? dateBought
            ,DateTime? dateSold,decimal buyPrice,decimal? sellPrice
            ,int sheetId,SkinInfo skinInfo,decimal sellOrderPrice)
        {
            Name = skinInfo.Name;
            Quantity = quantity;
            DateBought = dateBought;
            DateSold = dateSold;
            BuyPrice = buyPrice * quantity;
            SellPrice = sellPrice;
            SkinInfo = skinInfo;
            SheetId = sheetId;
            Status = EntryStatus.Open;
            CreatedAt = DateTime.UtcNow;
            DataSource = EntryDataSource.Regular;
            SellOrderPice = sellOrderPrice;
        }
        //Legacy entry ctor
        public Entry(int quantity, DateTime? dateBought
    , DateTime? dateSold, decimal buyPrice, decimal? sellPrice
    , int sheetId, SkinInfo skinInfo,EntryDataSource dataSource,decimal @return, decimal dailyReturn,EntryStatus status)
        {
            Name = skinInfo.Name;
            Quantity = quantity;
            DateBought = dateBought;
            DateSold = dateSold;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            SkinInfo = skinInfo;
            SheetId = sheetId;
            Status = EntryStatus.Open;
            CreatedAt = DateTime.UtcNow;
            DataSource = dataSource;
            Return = @return;
            DailyReturn = dailyReturn;
            Status = status;
            Profit = sellPrice - buyPrice;
        }
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public EntryStatus Status { get; set; } 
        public EntryDataSource DataSource { get; set; }
        public DateTime? DateBought { get; set; }
        public DateTime? DateSold { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? HoldDays { get; set; }
        public decimal? Return { get; set; }
        public decimal? DailyReturn { get; set; }
        public decimal? MarketPrice { get; set; }
        //After the sale is confirmed add the hold days to OverallStats
        public decimal? Profit { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public int SheetId { get; set; }
        public Sheet Sheet { get; set; }
        public int SkinInfoId { get; set; }
        public SkinInfo SkinInfo { get; set; }
        public decimal SellOrderPice { get; set; }
    }
}