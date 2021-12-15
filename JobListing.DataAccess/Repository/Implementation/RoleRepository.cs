using JobListing.DataAccess.Repository.Interface;
using JobListing.Models.DTOs;
using JobListingAppUI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.DataAccess.Repository.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IADOOperations _ado;

        public RoleRepository(IConfiguration configuration, IADOOperations aDOOperations)
        {
            _configuration = configuration;
            _ado = aDOOperations;
        }
        //public async Task<bool> AddRole(Role role)
        public async Task<bool> Add<T>(T entity)
        {
            var role = entity as Role;
            var stmt = $"INSERT INTO {_configuration.GetSection("Tables:RolesTable").Value} values"+
                        $"('{role.Id}','{role.Name}')";
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

        public async Task<bool> Delete<T>(T entity)
        //public async Task<bool> DeleteRole(string id)
        {
            var id = entity as string;
            var stmt = $"DELETE FROM {_configuration.GetSection("Tables:RolesTable").Value} WHERE Id = {id}";
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

        public async Task<List<RoleReturnedDto>> GetAllRoles()
        {
            var listOfRoles = new List<RoleReturnedDto>();

            string stmt = $"SELECT * FROM {_configuration.GetSection("Tables:RolesTable").Value}";

            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "name");

                if (response.Count <= 0)
                {
                    return null;
                }

                foreach (var item in response)
                {
                    //var values = item.Values.ToArray();

                    listOfRoles.Add(new RoleReturnedDto
                    {
                        Id = item.Values[0],
                        Name = item.Values[1]
                    });
                       
                }

            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }

            return listOfRoles;
        }

        public async Task<RoleReturnedDto> GetRoleById(string id)
        {
            var role = new RoleReturnedDto();

            string stmt = $"SELECT * FROM {_configuration.GetSection("Tables:RolesTable").Value} WHERE Id = '{id}'";

            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "name");

                if (response.Count <= 0)
                {
                    return null;
                }

                foreach (var item in response)
                {
                    //var values = item.Values.ToArray();


                    role.Id = item.Values[0];
                    role.Name = item.Values[1];
                   

                }

            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }

            return role;
        }

        public async Task<List<string>> GetUserRoles(string userid)
        {
            var userRoles = new List<string>();
            string stmt = $"SELECT * FROM {_configuration.GetSection("Tables:UserRoleTable").Value} WHERE UserId = '{userid}'";

            try
            {
                var response = await _ado.ExecuteForReader(stmt, "rolename");

                if (response.Count <= 0)
                {
                    return null;
                }

                foreach (var item in response)
                {
                    //var values = item.Values.ToArray();


                    
                    userRoles.Add( item.Values[0]);


                }

            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }

            return userRoles;

        }
        public async Task<RoleReturnedDto> GetRoleByName(string name)
        {
            var role = new RoleReturnedDto();

            string stmt = $"SELECT * FROM {_configuration.GetSection("Tables:RolesTable").Value} WHERE Name = '{name}'";

            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "name");

                if (response.Count <= 0)
                {
                    return null;
                }

                foreach (var item in response)
                {
                    //var values = item.Values.ToArray();


                    role.Id = item.Values[0];
                    role.Name = item.Values[1];


                }

            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }

            return role;
        }

        //public async Task<bool> UpdateRole(RoleReturnedDto updateRoleDto)
        public async Task<bool> Update<T>(T entity)
        {
            var updateRoleDto = entity as RoleReturnedDto;
            var stmt = $"UPDATE {_configuration.GetSection("Tables:RolesTable").Value} SET Name = {updateRoleDto.Name} WHERE Id = {updateRoleDto.Id}";
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
    }
}
