using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Common.ErrorMessages
{
    public static class EntryErrorMessages
    {
        public const string InvalidItemTypeOrMarketHashName = "Item name or item type is an invalid value";
        public const string EntryNotFound = "No such entry exists.";
        public const int EntryHoldDaysMin = 7;
        public const string EntryStatusOtherThanOpen = "Only open entries can be closed.";
    }
}
