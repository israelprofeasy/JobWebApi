using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobListingAppUI.Models
{
    public class JobIndustry : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public List<Job> JobListings { get; set; } = new List<Job>();
    }
}
