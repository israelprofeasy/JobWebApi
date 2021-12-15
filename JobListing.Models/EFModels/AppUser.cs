using JobListingAppUI.Models;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobListing.Models.EFModels
{
    public  class AppUser : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string password { get; set; }

     //   public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
