using System;
using System.Collections.Generic;
using System.Text;

namespace JobListing.Models.DTOs
{
    public class RegisterSuccessDto
    {
        public bool Status { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
