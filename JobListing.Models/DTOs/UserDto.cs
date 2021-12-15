using JobListingAppUI.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobListingAppUI.DTOs
{
    public class UserDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage ="Name must not be less than 3 characters and not more than 20 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 characters and not more than 20 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Enter a valid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        
        [Phone(ErrorMessage ="Enter a valid phone number")]
        //    [DataType(DataType.PhoneNumber)]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6, ErrorMessage ="Password must be atleast 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be atleast 6 characters")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set;}

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 characters and not more than 20 characters")]
        //public Locations Location { get; set; }
        public string Location { get; set; }

    }
}
