using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CS2Tracker_Common.Enums
{
    public enum SkinCondition
    {
        [Display(Name = "Select a condition")]
        Unknown = 0,

        [Display(Name = "Factory New")]
        FactoryNew = 1,

        [Display(Name = "Minimal Wear")]
        MinimalWear = 2,

        [Display(Name = "Field-Tested")]
        FieldTested = 3,

        [Display(Name = "Well-Worn")]
        WellWorn = 4,

        [Display(Name = "Battle-Scarred")]
        BattleScarred = 5
    }
}
