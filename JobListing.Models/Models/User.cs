using JobListingAppUI.Enums;
using System.Collections.Generic;

namespace JobListingAppUI.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string IsActive { get; set; }

       // public List<UserRole> UserRoles { get; set; } = new List<UserRole>();


    }
}
