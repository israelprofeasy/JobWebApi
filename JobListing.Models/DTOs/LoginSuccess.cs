using System;
using System.Collections.Generic;
using System.Text;

namespace JobListing.Models.DTOs
{
    public class LoginSuccess
    {
        public string Id { get; set; }
        public string token { get; set; }

        public bool status { get; set; } = false;
    }
}
