using JobListing.Models.DTOs;
using JobListingAppUI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.DataAccess.Repository.Interface
{
    public interface IRoleRepository : ICrudRepo
    {
      //  Task<bool> AddRole(Role role);
        Task<List<RoleReturnedDto>> GetAllRoles();
        Task<RoleReturnedDto> GetRoleById(string id);
        Task<RoleReturnedDto> GetRoleByName(string name);
     //   Task<bool> UpdateRole(RoleReturnedDto updateRoleDto);
      //  Task<bool> DeleteRole(string id);
        public Task<List<string>> GetUserRoles(string userid);
    }
}
