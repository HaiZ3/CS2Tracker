using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Helpers
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
                case SkinCondition.Factory_New:
                    skinVariant = "Factory New";
                    break;
                case SkinCondition.Minimal_Wear:
                    skinVariant = "Minimal Wear";
                    break;
                case SkinCondition.Field_Tested:
                    skinVariant = "Field-Tested";
                    break;
                case SkinCondition.Well_Worn:
                    skinVariant = "Well-Worn";
                    break;
                case SkinCondition.Battle_Scarred:
                    skinVariant = "Battle-Scarred";
                    break;
                case SkinCondition.Vanilla:
                    marketHashName = skinInfo.Name;
                    return marketHashName;
                case null:
                    marketHashName = skinInfo.Name;
                    return marketHashName;
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
