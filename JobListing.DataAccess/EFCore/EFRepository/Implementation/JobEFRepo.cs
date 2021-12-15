using JobListing.DataAccess.EFCore.EFRepository.Interface;
using JobListingAppUI.DbContexts;
using JobListingAppUI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.DataAccess.EFCore.EFRepository.Implementation
{
    public class JobEFRepo : IJobEFRepo
    {
        private readonly JobListingDbContext _context;

        public JobEFRepo(JobListingDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add<T>(T entity)
        {
            _context.Add(entity);
            return await SaveChanges();
        }

        public async Task<bool> Delete<T>(T entity)
        {
            _context.Remove(entity);
            return await SaveChanges();
        }

        public async Task<Job> GetJobById(string id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            return await _context.Jobs.ToListAsync();
        }

        public Task<IEnumerable<Job>> GetJobsByCategory(string categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Job>> GetJobsByIndustry(string industryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Job>> GetJobsByLocation(string location)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Job>> GetJobsByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Job>> GetJobsByNature(string jobNature)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Job>> GetJobsBySalaryRange(decimal minimum, decimal maximum)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> JobExists(string id)
        {
            return await _context.Jobs.AnyAsync(a => a.Id == id);
        }

        public async Task<int> RowCount()
        {
            return await _context.Jobs.CountAsync();
        }

        public async  Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >=0;
        }

        public Task<bool> Update<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
