﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobListing.Models.Helper
{
    public class PageMeta
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
    }
}
