using JobListing.DataAccess.Repository.Interface;
using JobListing.Models.DTOs;
using JobListingAppUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingAppUI.DataAccess.Repository.Interface
{
    public interface IUserRepository : ICrudRepo
    {
        public Task<bool> ApplyJob(string id);
        public Task<List<UserReturnedDto>> GetUsers();
        public Task<List<UserReturnedDto>> GetUsersByName(string name);
        public Task<UserReturnedDto> GetUsersById(string id);
        public Task<User> GetUserByEmail(string email);
        public Task<bool> DeactivateUser(string id);
        public Task<bool> ActivateUser(string id);
        

    }
}
