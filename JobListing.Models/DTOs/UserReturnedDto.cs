using JobListingAppUI.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobListing.Models.DTOs
{
    public class UserReturnedDto

    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public Locations Location { get; set; }
        public string Location { get; set; }
    }
}
