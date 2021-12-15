using JobListing.Models.EFModels;
using System.ComponentModel.DataAnnotations;

namespace JobListingAppUI.Models
{
    public class DocumentUpload : BaseEntity
    {
        [Required]
        public string JobListingId { get; set; }
        public Job Job { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

    }
}
