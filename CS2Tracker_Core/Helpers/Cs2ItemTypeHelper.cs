using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Helpers
{
    public static class Cs2ItemTypeHelper
    {
        public static ItemType GetItemType(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                return ItemType.Unknown;

            // knives and gloves begin with a star
            if (itemName.StartsWith("★"))
            {
                if (itemName.Contains("Gloves") || itemName.Contains("Hand Wraps"))
                    return ItemType.Gloves;
                return ItemType.Knife;
            }

            // cases, packages
            if (itemName.EndsWith("Case") ||
                itemName.Contains("Weapon Case") ||
                (itemName.Contains("Operation") && itemName.Contains("Case")))
                return ItemType.Case;

            if (itemName.Contains("Souvenir Package"))
                return ItemType.SouvenirPackage;

            if (itemName.Contains("Collection Package") || itemName.EndsWith("Collection"))
                return ItemType.CollectionPackage;

            // capsules and autograph capsules
            if (itemName.Contains("Capsule") || itemName.Contains("Autograph Capsule"))
                return ItemType.Capsule;

            // cosmetic items
            if (itemName.StartsWith("Sticker |"))
                return ItemType.Sticker;

            if (itemName.StartsWith("Patch |"))
                return ItemType.Patch;

            if (itemName.StartsWith("Charm |"))
                return ItemType.Charm;

            if (itemName.StartsWith("Sealed Graffiti |") || itemName.StartsWith("Graffiti |"))
                return ItemType.Graffiti;

            if (itemName.StartsWith("Music Kit |"))
                return ItemType.MusicKit;

            // items with a pipe: weapons or agents
            if (itemName.Contains(" | "))
            {
                if (IsKnownAgent(itemName))
                    return ItemType.Agent;

                string baseName = itemName.Split('|')[0].Trim();
                return baseName switch
                {
                    "Glock-18" or "USP-S" or "P2000" or "P250" or "Five-SeveN" or
                    "Tec-9" or "CZ75-Auto" or "Desert Eagle" or "R8 Revolver" or "Zeus x27"
                        => ItemType.Pistol,

                    "AK-47" or "M4A4" or "M4A1-S" or "FAMAS" or "Galil AR" or "AUG" or "SG 553"
                        => ItemType.Rifle,

                    "AWP" or "SSG 08" or "SCAR-20" or "G3SG1"
                        => ItemType.SniperRifle,

                    "MP9" or "MAC-10" or "MP7" or "MP5-SD" or "UMP-45" or "P90" or "PP-Bizon"
                        => ItemType.SMG,

                    "Nova" or "XM1014" or "MAG-7" or "Sawed-Off"
                        => ItemType.Shotgun,

                    "M249" or "Negev"
                        => ItemType.MachineGun,
                };
            }

            //pins
            if (itemName.EndsWith("Pin"))
            {
                return ItemType.Pin;
            }

            return ItemType.Unknown;
        }

        private static bool IsKnownAgent(string itemName)
        {
            return itemName.Contains("| SEAL Frogman")
                || itemName.Contains("| FBI")
                || itemName.Contains("| SWAT")
                || itemName.Contains("| KSK")
                || itemName.Contains("| Sabre")
                || itemName.Contains("| Phoenix")
                || itemName.Contains("| NSWC SEAL")
                || itemName.Contains("| Operator")
                || itemName.Contains("| Ground Rebel")
                || itemName.Contains("| Elite Crew")
                || itemName.Contains("| Guerrilla Warfare")
                || itemName.Contains("| The Professionals")
                || itemName.Contains("| Kumicho Security")
                || itemName.Contains("| Sabre Footsoldier")
                || itemName.Contains("| Street Soldier");
        }
    }
}