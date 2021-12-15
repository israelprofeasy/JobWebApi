using JobListing.Models.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobListing.Models.DTOs
{
    public class PaginatedListDto<T>
    {
        public PageMeta MetaData { get; set; }
        public IEnumerable<T> Data { get; set; }
        public PaginatedListDto()
        {
            Data = new List<T>();
        }
    }
}
