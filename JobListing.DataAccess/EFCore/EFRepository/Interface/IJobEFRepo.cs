using JobListing.DataAccess.Repository.Interface;
using JobListingAppUI.DTOs;
using JobListingAppUI.Enums;
using JobListingAppUI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.DataAccess.EFCore.EFRepository.Interface
{
    public interface IJobEFRepo : ICrudEFRepo
    {
        //Task<IEnumerable<Job>> GetJobs();


        public Task<IEnumerable<Job>> GetJobs();
        public Task<IEnumerable<Job>> GetJobsByCategory(string categoryId);
        public Task<IEnumerable<Job>> GetJobsByLocation(Locations location);
        public Task<IEnumerable<Job>> GetJobsByIndustry(string industryId);
        public Task<IEnumerable<Job>> GetJobsByNature(JobNature jobNature);
        public Task<IEnumerable<Job>> GetJobsBySalaryRange(decimal minimum, decimal maximum);
        public Task<IEnumerable<Job>> GetJobsByName(string name);
        public Task<Job> GetJobById(string id);
        public Task<bool> JobExists(string name);
        
        
        

    }
}
