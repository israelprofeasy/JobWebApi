using JobListingAppUI.DataAccess.Repository.Interface;
using JobListingAppUI.DTOs;
using JobListingAppUI.Enums;
using JobListingAppUI.Services.JobServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingAppUI.Services.JobServices.Implementation
{
    public class JobServices : IJobServices, IPriviledgeJobServices
    {
        private readonly IJobRepository _jobRepo;
        public JobServices(IJobRepository jobRepository)
        {
            _jobRepo = jobRepository;
        }

        public Task<bool> AddCategory(CategoryDto category)
        {
            bool res = false;
            return Task.FromResult(res);

        }

        public Task<bool> AddJob(JobDetailDto jobDetails)
        {
            throw new System.NotImplementedException();
        }

        public Task<CategoryDto> EditCategory(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<JobDetailReturnedDto> EditJob(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobListingPreviewDto>> GetJobsByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<JobDetailReturnedDto> GetJobById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobListingPreviewDto>> GetJobs()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobListingPreviewDto>> GetJobsByCategory(string categoryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobListingPreviewDto>> GetJobsByIndustry(string industryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobListingPreviewDto>> GetJobsByLocation(Locations location)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobListingPreviewDto>> GetJobsByNature(JobNature jobNature)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobListingPreviewDto>> GetJobsBySalaryRange(decimal minimum, decimal maximum)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> JobExists(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveCategory(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveJob(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateCategory(string id, CategoryDto category)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateJob(string id, JobDetailDto jobDetails)
        {
            throw new System.NotImplementedException();
        }
    }
}
