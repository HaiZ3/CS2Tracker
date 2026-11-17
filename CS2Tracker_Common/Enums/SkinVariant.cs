using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CS2Tracker_Common.Enums
{
    public enum SkinVariant
    {
        [Display(Name = "Select a variant")]
        Unknown = 0,
        [Display(Name = "Normal")]
        Normal = 1,
        [Display(Name = "StatTrak™")]
        StatTrak = 2,
        [Display(Name = "Souvenir")]
        Souvenir = 3
    }
}
