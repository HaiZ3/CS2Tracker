using CS2Tracker_Core.Models;
using CS2Tracker_Core.Models.Enums;

namespace CS2Tracker_Core.Helpers
{
    public static class Cs2MarketHashNameHelper
    {
        public static string BuildMarketHashName(SkinInfo skinInfo)
        {
            string marketHashName = string.Empty;
            if (skinInfo.ItemType == ItemType.Sticker)
            {
                return marketHashName = $"Sticker | {skinInfo.Name.Trim()}";
            }
            if(skinInfo.ItemType == ItemType.Patch)
            {
                return marketHashName = $"Patch | {skinInfo.Name.Trim()}";
            }
            if(skinInfo.ItemType == ItemType.Charm)
            {
                return marketHashName = $"Charm | {skinInfo.Name.Trim()}";
            }

            if (skinInfo.ItemType == ItemType.MusicKit)
            {
                if (skinInfo.SkinVariant == SkinVariant.Normal)
                {
                    marketHashName = $"Music Kit | {skinInfo.Name.Trim()}";
                }
                else
                {
                    marketHashName = $"StatTrak™ Music Kit | {skinInfo.Name.Trim()}";
                }
                return marketHashName;
            }
            string skinVariant = string.Empty;
            switch (skinInfo.SkinCondition)
            {
                case SkinCondition.FactoryNew:
                    skinVariant = "Factory New";
                    break;
                case SkinCondition.MinimalWear:
                    skinVariant = "Minimal Wear";
                    break;
                case SkinCondition.FieldTested:
                    skinVariant = "Field-Tested";
                    break;
                case SkinCondition.WellWorn:
                    skinVariant = "Well-Worn";
                    break;
                case SkinCondition.BattleScarred:
                    skinVariant = "Battle-Scarred";
                    break;
                default:
                    return skinInfo.Name;
            }
            if (skinInfo.Name.StartsWith("★"))
            {
                if (skinInfo.SkinVariant == SkinVariant.Normal)
                {
                    marketHashName = $"{skinInfo.Name.Trim()} ({skinVariant})";
                }
                else
                {
                    string temp = skinInfo.Name.Remove(0, 1).Trim();
                    marketHashName = $"★ {skinInfo.SkinVariant}™ {temp.Trim()} ({skinVariant})";
                }
            }
            else
            {
                if (skinInfo.SkinVariant == SkinVariant.Normal)
                {
                    marketHashName = $"{skinInfo.Name.Trim()} ({skinVariant})";
                }
                else if (skinInfo.SkinVariant == SkinVariant.StatTrak)
                {
                    marketHashName = $"StatTrak™ {skinInfo.Name.Trim()} ({skinVariant})";
                }
                else if (skinInfo.SkinVariant == SkinVariant.Souvenir)
                {
                    marketHashName = $"Souvenir {skinInfo.Name.Trim()} ({skinVariant})";
                }
            }

            return marketHashName;
        }
    }
}
