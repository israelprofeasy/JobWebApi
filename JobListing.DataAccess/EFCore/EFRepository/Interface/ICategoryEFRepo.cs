using JobListingAppUI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.DataAccess.EFCore.EFRepository.Interface
{
    public interface ICategoryEFRepo : ICrudEFRepo
    {
        
        public Task<JobCategory> GetCategory(string category);
        public Task<JobIndustry> GetIndustry(string industry);
        public Task<bool> AddIndustry(JobIndustry industry);
         public Task<bool> UpdateCategory(string id, JobCategory category);
    }
}
