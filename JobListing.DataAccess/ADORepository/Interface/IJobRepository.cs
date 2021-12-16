using JobListing.DataAccess.Repository.Interface;
using JobListingAppUI.DTOs;
using JobListingAppUI.Enums;
using JobListingAppUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingAppUI.DataAccess.Repository.Interface
{
    public interface IJobRepository 
    {
        public Task<List<JobListingPreviewDto>> GetJobs();
        public Task<List<JobListingPreviewDto>> GetJobsByCategory(string categoryId);
        public Task<List<JobListingPreviewDto>> GetJobsByLocation(string location);
        public Task<List<JobListingPreviewDto>> GetJobsByIndustry(string industryId);
        public Task<List<JobListingPreviewDto>> GetJobsByNature(string jobNature);
        public Task<List<JobListingPreviewDto>> GetJobsBySalaryRange(decimal minimum, decimal maximum);
        public Task<List<JobListingPreviewDto>> GetJobsByName(string name);
        public Task<JobDetailReturnedDto> GetJobById(string id);
        public Task<bool> JobExists(string name);
        public Task<bool> AddJob(Job jobDetails);
        public Task<bool> RemoveJob(string id);
        public Task<bool> UpdateJob(JobDetailReturnedDto jobDetails);
        public Task<bool> AddCategory(JobCategory category);
        public Task<JobCategory> GetCategory(string category);
        public Task<JobIndustry> GetIndustry(string industry);
        public Task<bool> AddIndustry(JobIndustry industry);
        public Task<bool> RemoveCategory(string id);
        
        public Task<bool> UpdateCategory(string id, JobCategory category);
    }
}
