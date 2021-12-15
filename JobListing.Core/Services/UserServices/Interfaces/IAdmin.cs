using JobListing.Models.DTOs;
using JobListingAppUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingAppUI.Services.UserServices.Interfaces
{
    public interface IAdmin : IUserService
    {
        
        public Task<List<UserReturnedDto>> GetUsersByName(string name);
        public Task<UserReturnedDto> GetUserbyId(string id);
        public Task<UserReturnedDto> GetUserbyEmail(string email);
        public Task<List<UserReturnedDto>> GetUsers();
        public Task<bool> DeactivateUser(string id);
        public Task<bool> ActivateUser(string id);
        public Task<bool> DeleteUser(string id);

    }
}
