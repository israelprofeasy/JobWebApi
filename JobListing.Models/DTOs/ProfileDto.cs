using JobListingAppUI.Enums;
using JobListingAppUI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobListingAppUI.DTOs
{
    public class ProfileDto
    {

        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        
        public string Email { get; set; }

        
        public int PhoneNumbers { get; set; }

              
        public Locations Location { get; set; }

        public List<DocumentUpload> JobsApplied { get; set; }
    }
}
