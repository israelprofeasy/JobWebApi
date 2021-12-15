using JobListing.Models.DTOs;
using JobListingAppUI.DTOs;
using JobListingAppUI.Models;
using System.Threading.Tasks;

namespace JobListingAppUI.Services.UserServices.Interfaces
{
    public interface IApplicantService : IUserService
    {
        public Task<bool> ApplyJob(string id);
        public Task<bool> RequestDeactivation();
        public Task<RegisterSuccessDto> RegisterUser(UserDto user, string password);
        Task<UserReturnedDto> EditUser(UserReturnedDto user);
    }
}
