using JobListingAppUI.DTOs;
using JobListingAppUI.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingAppUI.Services.JobServices.Interfaces
{
    public interface IJobServices
    {
        public Task<List<JobListingPreviewDto>> GetJobs();
        public Task<List<JobListingPreviewDto>> GetJobsByCategory(string categoryId);
        public Task<List<JobListingPreviewDto>> GetJobsByLocation(Locations location);
        public Task<List<JobListingPreviewDto>> GetJobsByIndustry(string industryId);
        public Task<List<JobListingPreviewDto>> GetJobsByNature(JobNature jobNature);
        public Task<List<JobListingPreviewDto>> GetJobsBySalaryRange(decimal minimum, decimal maximum);
        public Task<List<JobListingPreviewDto>> GetJobsByName(string name);
        public Task<JobDetailReturnedDto> GetJobById(string id);
        public Task<bool> JobExists(string name);



    }
}
