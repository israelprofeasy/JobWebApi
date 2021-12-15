using JobListing.Core.Services.RoleService.Interfaces;
using JobListing.DataAccess.Repository.Interface;
using JobListing.Models.DTOs;
using JobListingAppUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Core.Services.RoleService.Implementation
{
    public class RoleService : IRoleServices

    {
        private readonly IRoleRepository _roleRepo;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepo = roleRepository;
        }
        public async Task<bool> Add(RoleDto newRole)
        {
            bool res = false;
            var check = await _roleRepo.GetRoleByName(newRole.Name);
            if(check != null)
            {
                return res;
            }
            try
            {
                Role role = new Role();
                role.Name = newRole.Name;

                bool result = await _roleRepo.Add(role);
                if (result)
                {
                    res = true;
                }
               
               
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
            return res;
        }

        public async Task<bool> Delete(string id)
        {
            bool res = false;
            var check = await _roleRepo.GetRoleById(id);
            if (check == null)
            {
                return res;
            }
            try
            {
                 bool result = await _roleRepo.Delete(id);
                if (result)
                {
                  res = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        public async Task<RoleReturnedDto> GetRoleById(string id)
        {
            RoleReturnedDto res = new RoleReturnedDto();
            try
            {
                RoleReturnedDto role = await _roleRepo.GetRoleById(id);
                if (role != null)
                {
                   res.Id = role.Id;
                    res.Name = role.Name;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        public async Task<RoleReturnedDto> GetRoleByName(string name)
        {
            RoleReturnedDto res = new RoleReturnedDto();
            try
            {
                RoleReturnedDto role = await _roleRepo.GetRoleByName(name);
                if (role != null)
                {
                    res.Id = role.Id;
                    res.Name = role.Name;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        public async Task<List<RoleReturnedDto>> GetRoles()
        {
            List< RoleReturnedDto > res = new List<RoleReturnedDto>();
            try
            {
                List<RoleReturnedDto> roles = await _roleRepo.GetAllRoles();
                foreach (RoleReturnedDto role in roles)
                {
                    res.Add(role);
                }

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        public async Task<List<string>> GetUserRoles(string userid)
        {
            List<string> res = new List<string>();
            try
            {
                List<string> roles = await _roleRepo.GetUserRoles(userid);
                foreach (var role in roles)
                {
                    res.Add(role);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }
        public async Task<bool> Update(RoleReturnedDto updateRoleDto)
        {
            bool res = false;
            var check = await _roleRepo.GetRoleById(updateRoleDto.Id);
            if(check == null)
            {
                return res;
            }
            try
            {
                bool result = await _roleRepo.Update(updateRoleDto);

                if (result)
                {
                    res = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }
    }
}
