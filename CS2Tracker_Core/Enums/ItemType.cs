using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Enums
{
    public enum ItemType 
    {
        Unknown = 0,

        Pistol = 1,
        Rifle = 2,
        SMG = 3,
        SniperRifle = 4,
        Shotgun = 5,
        MachineGun = 6,

        Knife = 10,
        Gloves = 11,

        Case = 20,
        Capsule = 21,   // covers ALL sticker/autograph/event capsules
        SouvenirPackage = 22,
        CollectionPackage = 23,

        Sticker = 30,
        Patch = 31,
        Charm = 32,

        Agent = 40,

        MusicKit = 50,
        Graffiti = 51,
        Pin = 52
    }
}
