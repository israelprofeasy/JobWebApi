using JobListingAppUI.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobListingAppUI.Models
{
    public class Job : BaseEntity 
    {
        [Required]
        public int JobValidDays { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string Company { get; set; }

        public string JobIndustryId { get; set; }
        public JobIndustry JobIndustry { get; set; }
        public string JobCategoryId { get; set; }
        public JobCategory JobCategory { get; set; }
        [Required]
        public Locations Location { get; set; }
        [Required]
        public JobNature JobNature { get; set; }
        [Required]
        public string JobDescription { get; set; }
        [Required]
        [Column(TypeName ="money(18,2)")]
        public decimal MinimumSalary { get; set; }
        [Required]
        [Column(TypeName = "money(18,2)")]
        public decimal MaximumSalary { get; set; }
        public string Deadline
        {
            get
            {
                return DateTime.Now.AddDays(JobValidDays).ToString();
            }
        }


    }
}
