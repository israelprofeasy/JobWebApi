using JobListing.DataAccess.EFCore.EFRepository.Interface;
using JobListingAppUI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.DataAccess.EFCore.EFRepository.Implementation
{
    public class CategoryEFRepo : ICategoryEFRepo
    {
        public Task<bool> Add<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddIndustry(JobIndustry industry)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<JobCategory> GetCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Task<JobIndustry> GetIndustry(string industry)
        {
            throw new NotImplementedException();
        }

        public Task<int> RowCount()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategory(string id, JobCategory category)
        {
            throw new NotImplementedException();
        }
    }
}
