using CS2Tracker_Common.Enums;
using CS2Tracker_Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Tests.HelpersTests
{
    public class Cs2ItemTypeHelper_Tests
    {
        [Theory]
        // Rifles
        [InlineData("AK-47 | Fire Serpent", ItemType.Rifle)]
        [InlineData("M4A4 | Howl", ItemType.Rifle)]
        [InlineData("M4A1-S | Printstream", ItemType.Rifle)]

        // Pistols
        [InlineData("Glock-18 | Fade", ItemType.Pistol)]
        [InlineData("Desert Eagle | Blaze", ItemType.Pistol)]
        [InlineData("USP-S | Kill Confirmed", ItemType.Pistol)]

        // Sniper rifles
        [InlineData("AWP | Dragon Lore", ItemType.SniperRifle)]
        [InlineData("SSG 08 | Blood in the Water", ItemType.SniperRifle)]

        // SMGs
        [InlineData("MP9 | Starlight Protector", ItemType.SMG)]
        [InlineData("P90 | Asiimov", ItemType.SMG)]

        // Shotguns
        [InlineData("Nova | Antique", ItemType.Shotgun)]
        [InlineData("XM1014 | Tranquility", ItemType.Shotgun)]

        // Machine guns
        [InlineData("M249 | Nebula Crusader", ItemType.MachineGun)]
        [InlineData("Negev | Mjölnir", ItemType.MachineGun)]

        // Knives
        [InlineData("★ Karambit | Doppler", ItemType.Knife)]
        [InlineData("★ Butterfly Knife | Fade", ItemType.Knife)]

        // Gloves
        [InlineData("★ Sport Gloves | Vice", ItemType.Gloves)]
        [InlineData("★ Hand Wraps | Slaughter", ItemType.Gloves)]

        // Cases
        [InlineData("Dreams & Nightmares Case", ItemType.Case)]
        [InlineData("Operation Broken Fang Case", ItemType.Case)]

        // Packages
        [InlineData("2024 Copenhagen Souvenir Package", ItemType.SouvenirPackage)]
        [InlineData("Anubis Collection Package", ItemType.CollectionPackage)]

        // Capsules
        [InlineData("Paris 2023 Legends Capsule", ItemType.Capsule)]
        [InlineData("Autograph Capsule | s1mple", ItemType.Capsule)]

        // Cosmetics
        [InlineData("Sticker | Crown (Foil)", ItemType.Sticker)]
        [InlineData("Patch | Chicken Lover", ItemType.Patch)]
        [InlineData("Charm | Lil' Monster", ItemType.Charm)]
        [InlineData("Sealed Graffiti | Recoil", ItemType.Graffiti)]
        [InlineData("Music Kit | EZ4ENCE", ItemType.MusicKit)]

        // Agents
        [InlineData("Special Agent Ava | FBI", ItemType.Agent)]
        [InlineData("Sir Bloody Miami Darryl | The Professionals", ItemType.Agent)]

        // Pins
        [InlineData("Guardian Elite Pin", ItemType.Pin)]

        // Unknown
        [InlineData("Some Random Item", ItemType.Unknown)]
        [InlineData("", ItemType.Unknown)]
        [InlineData(null, ItemType.Unknown)]
        [InlineData("   ", ItemType.Unknown)]

        public void CsItemTypeHelper_ReturnsCorrectItemType(string itemName,ItemType expectedItemType)
        {
            ItemType itemType = Cs2ItemTypeHelper.GetItemType(itemName);

            Assert.Equal(expectedItemType, itemType);
        }
    }
}
