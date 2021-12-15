using JobListing.Models.DTOs;
using JobListingAppUI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Core.Services.RoleService.Interfaces
{
    public interface IRoleServices
    {
        Task<bool> Add(RoleDto newRole);
        Task<List<RoleReturnedDto>> GetRoles();
        Task<RoleReturnedDto> GetRoleById(string id);
        Task<RoleReturnedDto> GetRoleByName(string name);
        Task<bool> Update(RoleReturnedDto updateRoleDto);
        Task<bool> Delete(string id);
        public Task<List<string>> GetUserRoles(string userid);
    }
}
