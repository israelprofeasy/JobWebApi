using JobListingAppUI.Enums;
using JobListingAppUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobListing.Models.EFModels
{
    public class ApplicantUser
    {
        [Key]
        public string UserId { get; set; }
        public Locations Location { get; set; }
        
        public string CurriculumVitae { get; set; }
        public AppUser AppUser { get; set; }
        public List<Job> JobsApplied { get; set; } = new List<Job>();
    }
}
