using CS2Tracker_Common.Enums;
using CS2Tracker_Models.Helpers;
using CS2Tracker_Common.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CS2Tracker_Common.Validations.ModelValidations;
using CS2Tracker_DTOs.DTOs.ApiDTOs.SkinInfoDTOs;

namespace CS2Tracker_Models
{

    [Table("SkinInfos")]
    public class SkinInfo
    {
        public SkinInfo()
        {

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
        [Required]
        [MaxLength(SkinInfoNameMaxLen),MinLength(SkinInfoNameMinLen)]
        public string Name { get; set; } = string.Empty;
        public string MarketHashName { get; set; } = string.Empty;

        public decimal? Float
        {
            get;
            set
            {
                if (value is < 0m or > 1m)
                    throw new ArgumentOutOfRangeException(nameof(value), "Float must be between 0 and 1.");
                field = value;
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
