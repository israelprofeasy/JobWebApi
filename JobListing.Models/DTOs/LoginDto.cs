using System.ComponentModel.DataAnnotations;

namespace JobListingAppUI.DTOs
{
    public class LoginDto
    {
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string password { get; set; }
    }
}
