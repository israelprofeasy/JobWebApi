using JobListingAppUI.DTOs;
using System.Threading.Tasks;

namespace JobListingAppUI.Services.JobServices.Interfaces
{
    public interface IPriviledgeJobServices
    {
        public Task<bool> AddJob(JobDetailDto jobDetails);
        public Task<bool> RemoveJob(string id);
        public Task<JobDetailReturnedDto> EditJob(string id);
        public Task<bool> UpdateJob(string id, JobDetailDto jobDetails);
        public Task<bool> AddCategory(CategoryDto category);
        public Task<bool> RemoveCategory(string id);
        public Task<CategoryDto> EditCategory(string id);
        public Task<bool> UpdateCategory(string id, CategoryDto category);


    }
}
