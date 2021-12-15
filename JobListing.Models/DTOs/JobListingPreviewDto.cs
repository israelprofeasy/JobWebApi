using JobListingAppUI.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobListingAppUI.DTOs
{
    public class JobListingPreviewDto
    {
        public string Id { get; set; }
        public string JobTitle { get; set; }

       
        public string Company { get; set; }

        public string Location { get; set; }
        //public Locations Location { get; set; }

        public string JobNature { get; set; }
       // public JobNature JobNature { get; set; }

        public DateTime Deadline { get; set; }
        public DateTime DateCreated { get; set; }


        public string SalaryRange { get; set; }
        
    }
}
