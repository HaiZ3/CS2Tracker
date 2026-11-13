using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Common.Validations
{
    public static class ModelValidations
    {
        public const int EntryNameMaxLen = 128;
        public const int EntryNameMinLen = 5;
        public const int EntryHoldDaysMin = 7;
        public const int SheetNameMaxLen = 128;
        public const int SheetNameMinLen = 3;
        public const int SkinInfoNameMaxLen = 128;
        public const int SkinInfoNameMinLen = 5;
        public const double SkinInfoFloatMin = 0.0;
        public const double SkinInfoFloatMax = 1.0;
        public const int UsernameMaxLen = 50;
        public const int UsernameMinLen = 3;
        public const int UserPasswordMaxLen = 128;
        public const int UserPasswordMinLen = 8;
        public const int UserEmailMaxLen = 254;
    }
}
