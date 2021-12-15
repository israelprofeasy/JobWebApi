using JobListingAppUI.Enums;
using Microsoft.AspNetCore.Http;

namespace JobListingAppUI.DTOs
{
    public class RegisterApplicantDto
    {
        public string UserId { get; set; }
        public Locations Location { get; set; }
        public string CurriculumVitae { get; set; }
        public IFormFile CV { get; set; }
    }
}
