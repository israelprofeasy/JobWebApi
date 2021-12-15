using System;
using System.Collections.Generic;
using System.Text;

namespace JobListing.Models.Models
{
    public class ExecuterReaderResult
    {
        public List<string> Fields { get; set; }
        public List<string> Values { get; set; }
        public List<byte[]> ByteValues { get; set; }
        public ExecuterReaderResult()
        {
            Fields = new List<string>();
            Values = new List<string>();
            ByteValues = new List<byte[]>();
        }
    }
}
