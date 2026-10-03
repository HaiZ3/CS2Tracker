using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes
{

    [Table("SkinInfos")]
    public class SkinInfo
    {
        public SkinInfo()
        {

        }
        public SkinInfo(string name, decimal? itemFloat, SkinVariant? skinVariant
            , SkinCondition? skinCondition)
        {
            this.Name = name;
            ItemFloat = itemFloat;
            SkinVariant = skinVariant;
            SkinCondition = skinCondition;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MarketHashName { get; set; } = string.Empty;
        public decimal? ItemFloat
        {
            get;
            set
            {
                if (value >= 1m)
                {
                    throw new Exception("The float is higher or equal to 1");
                }
            }
        }
        public ItemType? ItemType { get; set; }
        public SkinCondition? SkinCondition { get; set; }
        public SkinVariant? SkinVariant { get; set; }
        public int EntryId { get; set; }
        public Entry Entry { get; set; }

        public void SetItemType()
        {
            ItemType = Cs2ItemTypeHelper.GetItemType(Name);
        }
        public void SetMarketHashName()
        {
            MarketHashName = Cs2MarketHashNameHelper.BuildMarketHashName(this);
        }
    }
}
