using AutoMapper;
using JobListing.Models.DTOs;
using JobListing.Models.EFModels;
using JobListingAppUI.DTOs;
using JobListingAppUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobListing.Models.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, UserReturnedDto>();
            CreateMap<AppUser, RegisterSuccessDto>();
            CreateMap<JobDetailDto, Job>();
            CreateMap<Job, JobListingPreviewDto>();
        }
    }
}
