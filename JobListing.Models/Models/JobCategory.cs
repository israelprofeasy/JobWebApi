using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobListingAppUI.Models
{
    public class JobCategory : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public List<Job> JobListings { get; set; } = new List<Job>();
    }
}