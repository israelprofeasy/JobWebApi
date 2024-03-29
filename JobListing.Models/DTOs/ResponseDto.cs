﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobListing.Models.DTOs
{
    public class ResponseDto<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<ErrorItem> Errors { get; set; }
        public T Data { get; set; }

        public ResponseDto()
        {
            Errors = new List<ErrorItem>();
        }
    }
    public class ErrorItem
    {
        public string Key { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
    
}
