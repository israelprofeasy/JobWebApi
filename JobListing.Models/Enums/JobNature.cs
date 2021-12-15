﻿using System.ComponentModel.DataAnnotations;

namespace JobListingAppUI.Enums
{
    public enum JobNature
    {
        [Display(Name = "Full Time")]
        FullTime,
        [Display(Name = "Part Time")]
        PartTime,
        Contract,
        Internship
    }
}
