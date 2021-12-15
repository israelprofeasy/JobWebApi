using JobListingAppUI.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobListingAppUI.Models
{
    public class Applicant
    {
        [Key]
        public string UserId { get; set; }
       // public Locations Location { get; set; }
        public string Location { get; set; }
        public string CurriculumVitae { get; set; }
        public User User { get; set; }

        public List<Job> JobsApplied { get; set; } = new List<Job>();
    }
}
