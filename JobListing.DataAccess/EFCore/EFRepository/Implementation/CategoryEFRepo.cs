using JobListing.DataAccess.EFCore.EFRepository.Interface;
using JobListingAppUI.DbContexts;
using JobListingAppUI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.DataAccess.EFCore.EFRepository.Implementation
{
    public class CategoryEFRepo : ICategoryEFRepo
    {
        private readonly JobListingDbContext _context;

        public CategoryEFRepo(JobListingDbContext jobListingDbContext)
        {
            _context = jobListingDbContext;
        }
        public async Task<bool> Add<T>(T entity)
        {
            _context.Add(entity);
            return await SaveChanges();
        }

        public Task<bool> AddIndustry(JobIndustry industry)
        {
            
        }

        public async Task<bool> Delete<T>(T entity)
        {
            _context.Remove(entity);
            return await SaveChanges();
        }

        public async Task<JobCategory> GetCategory(string category)
        {
            return _context.Category.Wh
        }

        public Task<JobIndustry> GetIndustry(string industry)
        {
            throw new NotImplementedException();
        }

        public Task<int> RowCount()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >0;
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
