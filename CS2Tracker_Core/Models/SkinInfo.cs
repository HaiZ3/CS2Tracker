using CS2Tracker_Core.Models.DTOs.ApiDTOs.SkinInfoDTOs;
using CS2Tracker_Core.Enums;
using CS2Tracker_Core.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS2Tracker_Core.Models
{

    [Table("SkinInfos")]
    public class SkinInfo
    {
        public SkinInfo()
        {

        }
        public SkinInfo(string name, decimal? itemFloat, SkinVariant skinVariant
            , SkinCondition skinCondition)
        {
            this.Name = name;
            Float = itemFloat;
            SkinVariant = skinVariant;
            SkinCondition = skinCondition;
        }
        public SkinInfo(SkinInfoRequestDTO skinInfoRequestDTO)
        {
            Name = skinInfoRequestDTO.Name;
            Float = skinInfoRequestDTO.Float;
            SkinVariant = skinInfoRequestDTO.SkinVariant;
            SkinCondition = skinInfoRequestDTO.SkinCondition;
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string MarketHashName { get; set; } = string.Empty;

        public decimal? Float
        {
            get;
            set
            {
                if (value >= 1m)
                    throw new Exception("The float is higher or equal to 1");
                
            }
        }

        public ItemType ItemType { get; set; }
        public SkinCondition SkinCondition { get; set; }
        public SkinVariant SkinVariant { get; set; }

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
