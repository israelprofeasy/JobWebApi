using JobListing.DataAccess.EFCore.EFRepository.Interface;
using JobListingAppUI.DbContexts;
using JobListingAppUI.Enums;
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

        public async Task<IEnumerable<Job>> GetJobsByCategory(string categoryId)
        {
            return await _context.Jobs.Where(x => x.JobCategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByIndustry(string industryId)
        {
            return await _context.Jobs.Where(x => x.JobIndustryId == industryId).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByLocation(Locations location)
        {
            return await _context.Jobs.Where(x => x.Location == location).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByName(string name)
        {
            return await _context.Jobs.Where(x => x.JobTitle == name || x.Company == name).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByNature(JobNature jobNature)
        {
            return await _context.Jobs.Where(x => x.JobNature == jobNature).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsBySalaryRange(decimal minimum, decimal maximum)
        {
            return await _context.Jobs.Where(x => x.MinimumSalary >= minimum && x.MaximumSalary <= maximum).OrderBy(x => x.MinimumSalary).ToListAsync();
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
            return await _context.SaveChangesAsync() >0;
        }

        public async Task<bool> Update<T>(T entity)
        {
             _context.Update(entity);
            return await SaveChanges();
        }
    }
}
