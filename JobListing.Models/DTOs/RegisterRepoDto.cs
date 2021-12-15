using System;
using System.Collections.Generic;
using System.Text;

namespace JobListing.Models.DTOs
{
    public class RegisterRepoDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Location { get; set; }
        
        public string DateCreated { get; set; } 
        public string DateUpdated { get; set; } 
    }
}
