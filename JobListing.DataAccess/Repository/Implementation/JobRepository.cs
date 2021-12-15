using JobListing.DataAccess.Repository.Interface;
using JobListingAppUI.DataAccess.Repository.Interface;
using JobListingAppUI.DTOs;
using JobListingAppUI.Enums;
using JobListingAppUI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace JobListingAppUI.DataAccess.Repository.Implementation
{
    public class JobRepository : IJobRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IADOOperations _ado;

        public JobRepository(IConfiguration configuration, IADOOperations aDOOperations)
        {
            _configuration = configuration;
            _ado = aDOOperations;
        }
        public async Task<bool> AddCategory(JobCategory category)
        {

            var stmt = $"INSERT INTO {_configuration.GetSection("Tables:CategoryTable").Value} values" +
                        $"('{category.Id}','{category.Name}')";
            try
            {
                if (await _ado.ExecuteForQuery(stmt))
                {
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }

            return false;
        }

        public async Task<bool> AddIndustry(JobIndustry industry)
        {
            var stmt = $"INSERT INTO {_configuration.GetSection("Tables:IndustryTable").Value} values" +
                      $"('{industry.Id}','{industry.Name}')";
            try
            {
                if (await _ado.ExecuteForQuery(stmt))
                {
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }

            return false;
        }

        public async Task<bool> AddJob(Job jobDetails)
        {
            
            var stmt = $"INSERT INTO {_configuration.GetSection("Tables:JobTable").Value} values" +
                      $"('{jobDetails.Id}','{jobDetails.JobTitle}','{jobDetails.Company}','{jobDetails.Location}','{jobDetails.JobNature}','{jobDetails.JobDescription}','{jobDetails.MinimumSalary}','{jobDetails.MaximumSalary}','{jobDetails.JobCategoryId}','{jobDetails.JobIndustryId}','{DateTime.Now}','{DateTime.Now}',{jobDetails.Deadline}')";
            try
            {
                if (await _ado.ExecuteForQuery(stmt))
                {
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }

            return false;
        }


       public async Task<List<JobListingPreviewDto>> GetJobsByName(string name)
        {
            List<JobListingPreviewDto> jobs = new List<JobListingPreviewDto>();
            var stmt = $"SELECT * FROM {_configuration.GetSection("Tables:JobTable").Value} WHERE JobTitle LIKE %{name}% OR Company LIKE %{name}%";
            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "jobtitle", "company", "location", "jobNature", "minimumsalary", "maximumsalary", "Datecreated", "deadline");
                if(response.Count <=0)
                {
                    return null;
                }
                foreach (var item in response)
                {
                    jobs.Add(new JobListingPreviewDto
                    {
                        Id = item.Values[0],
                        JobTitle = item.Values[1],
                        Company = item.Values[2],
                        Location = item.Values[3],
                        JobNature = item.Values[4],
                        SalaryRange = $"{item.Values[5]} to {item.Values[6]}",
                        DateCreated = DateTime.Parse(item.Values[7]),
                        Deadline = DateTime.Parse(item.Values[8])
                    });
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            return jobs;
        }

        public async Task<JobDetailReturnedDto> GetJobById(string id)
        {
            JobDetailReturnedDto job = new JobDetailReturnedDto();
            var stmt = $"SELECT * FROM {_configuration.GetSection("Tables:JobTable").Value} WHERE Id = '{id}'";
            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "jobtitle", "company", "location", "jobNature", "jobdescription", "minimumsalary", "maximumsalary", "Datecreated", "deadline");
                if (response.Count <= 0)
                {
                    return null;
                }
               
                    job = new JobDetailReturnedDto
                    {
                        Id = response[0].Values[0],
                        JobTitle = response[0].Values[1],
                        Company = response[0].Values[2],
                        Location = response[0].Values[3],
                        JobNature = response[0].Values[4],
                        JobDescription = response[0].Values[5],
                        SalaryRange = $"{response[0].Values[6]} to {response[0].Values[7]}",
                        DateCreated = DateTime.Parse(response[0].Values[8]),
                        Deadline = DateTime.Parse(response[0].Values[9])
                    };
                
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            return job;
        }

        public async Task<List<JobListingPreviewDto>> GetJobs()
        {
            List<JobListingPreviewDto> jobs = new List<JobListingPreviewDto>();
            var stmt = $"SELECT * FROM {_configuration.GetSection("Tables:JobTable").Value}";
            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "jobtitle", "company", "location", "jobNature", "minimumsalary", "maximumsalary", "Datecreated", "deadline");
                if (response.Count <= 0)
                {
                    return null;
                }
                foreach (var item in response)
                {
                    jobs.Add(new JobListingPreviewDto
                    {
                        Id = item.Values[0],
                        JobTitle = item.Values[1],
                        Company = item.Values[2],
                        Location = item.Values[3],
                        JobNature = item.Values[4],
                        SalaryRange = $"{item.Values[5]} to {item.Values[6]}",
                        DateCreated = DateTime.Parse(item.Values[7]),
                        Deadline = DateTime.Parse(item.Values[8])
                    });
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            return jobs;
        }

        public async Task<List<JobListingPreviewDto>> GetJobsByCategory(string categoryId)
        {
            List<JobListingPreviewDto> jobs = new List<JobListingPreviewDto>();
            var stmt = $"SELECT * FROM {_configuration.GetSection("Tables:JobTable").Value} WHERE JobCatogoryId = '{categoryId}'";
            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "jobtitle", "company", "location", "jobNature", "minimumsalary", "maximumsalary", "Datecreated", "deadline");
                if (response.Count <= 0)
                {
                    return null;
                }
                foreach (var item in response)
                {
                    jobs.Add(new JobListingPreviewDto
                    {
                        Id = item.Values[0],
                        JobTitle = item.Values[1],
                        Company = item.Values[2],
                        Location = item.Values[3],
                        JobNature = item.Values[4],
                        SalaryRange = $"{item.Values[5]} to {item.Values[6]}",
                        DateCreated = DateTime.Parse(item.Values[7]),
                        Deadline = DateTime.Parse(item.Values[8])
                    });
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            return jobs;
        }

        public async Task<List<JobListingPreviewDto>> GetJobsByIndustry(string industryId)
        {
            List<JobListingPreviewDto> jobs = new List<JobListingPreviewDto>();
            var stmt = $"SELECT * FROM {_configuration.GetSection("Tables:JobTable").Value} WHERE JobIndustryId = '{industryId}'";
            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "jobtitle", "company", "location", "jobNature", "minimumsalary", "maximumsalary", "Datecreated", "deadline");
                if (response.Count <= 0)
                {
                    return null;
                }
                foreach (var item in response)
                {
                    jobs.Add(new JobListingPreviewDto
                    {
                        Id = item.Values[0],
                        JobTitle = item.Values[1],
                        Company = item.Values[2],
                        Location = item.Values[3],
                        JobNature = item.Values[4],
                        SalaryRange = $"{item.Values[5]} to {item.Values[6]}",
                        DateCreated = DateTime.Parse(item.Values[7]),
                        Deadline = DateTime.Parse(item.Values[8])
                    });
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            return jobs;
        }

        public async Task<List<JobListingPreviewDto>> GetJobsByLocation(string location)
        {
            List<JobListingPreviewDto> jobs = new List<JobListingPreviewDto>();
            var stmt = $"SELECT * FROM {_configuration.GetSection("Tables:JobTable").Value} WHERE Location = '{location}'";
            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "jobtitle", "company", "location", "jobNature", "minimumsalary", "maximumsalary", "Datecreated", "deadline");
                if (response.Count <= 0)
                {
                    return null;
                }
                foreach (var item in response)
                {
                    jobs.Add(new JobListingPreviewDto
                    {
                        Id = item.Values[0],
                        JobTitle = item.Values[1],
                        Company = item.Values[2],
                        Location = item.Values[3],
                        JobNature = item.Values[4],
                        SalaryRange = $"{item.Values[5]} to {item.Values[6]}",
                        DateCreated = DateTime.Parse(item.Values[7]),
                        Deadline = DateTime.Parse(item.Values[8])
                    });
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            return jobs;
        }

        public async Task<List<JobListingPreviewDto>> GetJobsByNature(string jobNature)
        {
            List<JobListingPreviewDto> jobs = new List<JobListingPreviewDto>();
            var stmt = $"SELECT * FROM {_configuration.GetSection("Tables:JobTable").Value} WHERE JobNature = '{jobNature}'";
            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "jobtitle", "company", "location", "jobNature", "minimumsalary", "maximumsalary", "Datecreated", "deadline");
                if (response.Count <= 0)
                {
                    return null;
                }
                foreach (var item in response)
                {
                    jobs.Add(new JobListingPreviewDto
                    {
                        Id = item.Values[0],
                        JobTitle = item.Values[1],
                        Company = item.Values[2],
                        Location = item.Values[3],
                        JobNature = item.Values[4],
                        SalaryRange = $"{item.Values[5]} to {item.Values[6]}",
                        DateCreated = DateTime.Parse(item.Values[7]),
                        Deadline = DateTime.Parse(item.Values[8])
                    });
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            return jobs;
        }

        public async Task<List<JobListingPreviewDto>> GetJobsBySalaryRange(decimal minimum, decimal maximum)
        {
            List<JobListingPreviewDto> jobs = new List<JobListingPreviewDto>();
            var stmt = $"SELECT * FROM {_configuration.GetSection("Tables:JobTable").Value} WHERE minimumsalary >= '{minimum}' AND maximumsalary <= '{maximum}'";
            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "jobtitle", "company", "location", "jobNature", "minimumsalary", "maximumsalary", "Datecreated", "deadline");
                if (response.Count <= 0)
                {
                    return null;
                }
                foreach (var item in response)
                {
                    jobs.Add(new JobListingPreviewDto
                    {
                        Id = item.Values[0],
                        JobTitle = item.Values[1],
                        Company = item.Values[2],
                        Location = item.Values[3],
                        JobNature = item.Values[4],
                        SalaryRange = $"{item.Values[5]} to {item.Values[6]}",
                        DateCreated = DateTime.Parse(item.Values[7]),
                        Deadline = DateTime.Parse(item.Values[8])
                    });
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            return jobs;
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

        public Task<bool> UpdateCategory(string id, JobCategory category)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateJob(JobDetailReturnedDto jobDetails)
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
    }
}
