using JobListing.Models.DTOs;
using JobListingAppUI.DataAccess.Repository.Interface;
using JobListingAppUI.DTOs;
using JobListingAppUI.Services.UserServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Core.Services.UserServices.Implementation
{
    public class AdminService 
    {
        private readonly IUserRepository _userRepository;

        public AdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> ActivateDeactivateUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserReturnedDto> GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserReturnedDto>> GetUsers(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserReturnedDto>> GetUsers()
        {
            try
            {

                return await _userRepository.GetUsers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Task<bool> Login(LoginDto login)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RetreivePassword(string email)
        {
            throw new NotImplementedException();
        }
    }
}
