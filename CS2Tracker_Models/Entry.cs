using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs;
using CS2Tracker_Common.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static CS2Tracker_Common.Validations.ModelValidations;

namespace CS2Tracker_Models
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
        public Entry(CreateEntryDTO createEntryDTO,SkinInfo skinInfo)
        {
            DateBought = createEntryDTO.DateBought;
            BuyPrice = createEntryDTO.BuyPrice;
            SheetId = createEntryDTO.SheetId;
            SkinInfo = skinInfo;
            Name = skinInfo.Name;
            CreatedAt = DateTime.UtcNow;
        }
        //Legacy entry ctor
    //    public Entry(int quantity, DateTime? dateBought
    //, DateTime? dateSold, decimal buyPrice, decimal? sellPrice
    //, int sheetId, SkinInfo skinInfo,EntryDataSource dataSource,decimal @return, decimal dailyReturn,EntryStatus status)
    //    {
    //        Name = skinInfo.Name;
    //        Quantity = quantity;
    //        DateBought = dateBought;
    //        DateSold = dateSold;
    //        BuyPrice = buyPrice;
    //        SellPrice = sellPrice;
    //        SkinInfo = skinInfo;
    //        SheetId = sheetId;
    //        Status = EntryStatus.Open;
    //        CreatedAt = DateTime.UtcNow;
    //        DataSource = dataSource;
    //        Return = @return;
    //        DailyReturn = dailyReturn;
    //        Status = status;
    //        Profit = sellPrice - buyPrice;
    //    }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntryNameMaxLen),MinLength(EntryNameMinLen)]
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        [Required]
        [EnumDataType(typeof(EntryStatus))]
        public EntryStatus Status { get; set; }
        [Required]
        public DateTime DateBought { get; set; }
        public DateTime? DateSold { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [NotMapped]
        public int? HoldDays
        {
            get
            {
                int? holdDays = null;

                if(DateSold == null)
                {
                    return holdDays;
                }

                holdDays = (int)(DateSold - DateBought).Value.TotalDays;

                return holdDays;
            }
        }
        [NotMapped]
        public decimal? Return
        {
            get
            {
                decimal? ret = null;

                if(SellPrice == null)
                {
                    return ret;
                }

                ret = Profit / (BuyPrice * Quantity);

                return ret;
            }
        }
        [NotMapped]
        public decimal? Profit
        {
            get
            {
                decimal? profit = null;

                if(SellPrice == null)
                {
                    return profit;
                }

                profit = SellPrice - BuyPrice;
                    
                return profit;
            }
        }
        [Range (0,int.MaxValue)]
        [Required]
        public decimal BuyPrice { get; set; }
        [Range(0,int.MaxValue)]
        public decimal? SellPrice { get; set; }
        public int SheetId { get; set; }
        public Sheet Sheet { get; set; } = null!;
        public SkinInfo SkinInfo { get; set; } = null!;
    }
}
