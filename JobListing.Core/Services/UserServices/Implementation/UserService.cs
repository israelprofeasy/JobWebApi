using JobListing.Models.DTOs;
using JobListingAppUI.DataAccess.Repository.Interface;
using JobListingAppUI.DTOs;
using JobListingAppUI.Models;
using JobListingAppUI.Services.UserServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using JobListingAppUI.Commons;
using JobListing.Core.Services.RoleService.Interfaces;

namespace JobListingAppUI.Services.UserServices.Implementation
{
    public class UserService : IApplicantService, IAdmin
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IRoleServices _role;

        public UserService(IUserRepository userRepository, IJwtService jwtService, IRoleServices roleServices)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _role = roleServices;
        }

        
        public async Task<bool> ActivateUser(string id)
        {
            bool res = false;
            var check = await _userRepository.GetUsersById(id);
            if (check == null)
            {
                return res;
            }
            try
            {
                var result = await _userRepository.ActivateUser(id);
                if(result)
                    res =  true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;

        }

        public Task<bool> ApplyJob(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeactivateUser(string id)
        {
            bool res = false;
            var check = await _userRepository.GetUsersById(id);
            if (check == null)
            {
                return res;
            }
            try
            {
                var result = await _userRepository.DeactivateUser(id);
                if (result)
                    res = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;

        }

        public async Task<bool> DeleteUser(string id)
        {
            bool res = false;
            var check = await _userRepository.GetUsersById(id);
            if (check == null)
            {
                return res;
            }
            try
            {
                var result = await _userRepository.Delete(id);
                if (result)
                    res = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        public async Task<UserReturnedDto> EditUser(UserReturnedDto user)
        {
            UserReturnedDto res = new UserReturnedDto();
            var check = await _userRepository.GetUsersById(user.Id);
            if(check == null)
            {
                return res;
            }
            try
            {
                var result = await _userRepository.Update(user);
                if (result)
                {
                    res.Id = user.Id;
                    res.FirstName = user.FirstName;
                    res.LastName = user.LastName;
                    res.Email = user.Email;
                    res.PhoneNumber = user.PhoneNumber;
                    res.Location = user.Location;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
            
        }

        public async Task<UserReturnedDto> GetUserbyEmail(string email)
        {
            UserReturnedDto res = new UserReturnedDto();

            try
            {
                var result = await _userRepository.GetUserByEmail(email);
                if (result != null)
                {
                    res.Id = result.Id;
                    res.FirstName = result.FirstName;
                    res.LastName = result.LastName;
                    res.Email = result.Email;
                    res.PhoneNumber = result.PhoneNumber;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        public async Task<UserReturnedDto> GetUserbyId(string id)
        {
            UserReturnedDto res = new UserReturnedDto();

            try
            {
                var result = await _userRepository.GetUsersById(id);
                if (result != null)
                {
                    res.Id = result.Id;
                    res.FirstName = result.FirstName;
                    res.LastName = result.LastName;
                    res.Email = result.Email;
                    res.PhoneNumber = result.PhoneNumber;
                    res.Location = result.Location;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        public async Task<List<UserReturnedDto>> GetUsers()
        {
            List<UserReturnedDto> res = new List<UserReturnedDto>();
            try
            {
                List<UserReturnedDto> users = await _userRepository.GetUsers();
                foreach (UserReturnedDto user in users)
                {
                    res.Add(user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        public async Task<List<UserReturnedDto>> GetUsersByName(string name)
        {
            List<UserReturnedDto> res = new List<UserReturnedDto>();
            try
            {
                List<UserReturnedDto> users = await _userRepository.GetUsersByName(name);
                foreach (UserReturnedDto user in users)
                {
                    res.Add(user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        public async Task<LoginSuccess> Login(string email, string password)
        {
            if (String.IsNullOrWhiteSpace(email))
                throw new Exception("Email is empty");
            if (String.IsNullOrWhiteSpace(password))
                throw new Exception("Password is empty");

            LoginSuccess success = new LoginSuccess();
           List<string> roles = new List<string>();
            try
            {
                var response = await _userRepository.GetUserByEmail(email);
                roles = await _role.GetUserRoles(response.Id);
                if (Utilities.CompareHash(password, response.PasswordHash, response.PasswordSalt))
                {
                    if (response.IsActive == "False")
                    {
                        success.status = false;
                        success.Id = "Account has being deactivated, Pls Contact the Admin";
                        return success;
                    }
                    else
                    {

                        success.status = true;
                        success.Id = response.Id;
                        success.token = _jwtService.JwtGen(response, roles);
                    }
                }


            }
            catch (Exception e)
            {
            
                throw new Exception(e.Message);
            }
            return success;

        }

       
        public async Task<RegisterSuccessDto> RegisterUser(UserDto user, string password)
        {
            var res = await _userRepository.GetUserByEmail(user.Email);
            var success = new RegisterSuccessDto();
            if (res != null)
            {
                return success;
            }

            //success.Status = false;
            var listOfHash = Utilities.HashGenerator(password);

            var role = await _role.GetRoleByName("Applicant");

            var newUser = new User { 
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    PasswordHash = listOfHash[0],
                    PasswordSalt = listOfHash[1],
                
                };
            var userDetails = new RegisterRepoDto();
            userDetails.Id = newUser.Id;
            userDetails.FirstName = newUser.FirstName;
            userDetails.LastName = newUser.LastName;
            userDetails.Email = newUser.Email;
            userDetails.PhoneNumber = newUser.PhoneNumber;
            userDetails.PasswordHash = newUser.PasswordHash;
            userDetails.PasswordSalt = newUser.PasswordSalt;
            userDetails.Location = user.Location;
            userDetails.RoleId = role.Id;
            userDetails.RoleName = role.Name;
            userDetails.DateCreated = newUser.DateCreated;
            userDetails.DateUpdated = newUser.DateUpdated;
            //var result = await _userRepository.AddUser(newUser, applicant);

            try
            {
                if (await _userRepository.Add(userDetails))
                {
                    success.Status = true;
                    success.FullName = $"{user.FirstName} {user.LastName}";
                    success.Email = user.Email;
                }
            }
            catch (Exception)
            {
                // Log Error
            }
            return success;
            
        }

        public Task<bool> RequestDeactivation()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RetreivePassword(string email)
        {
            throw new NotImplementedException();
        }
    }
}
