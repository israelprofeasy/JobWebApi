using JobListing.DataAccess.Repository.Interface;
using JobListing.Models.DTOs;
using JobListing.Models.Models;
using JobListingAppUI.DataAccess.Repository.Interface;
using JobListingAppUI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JobListingAppUI.DataAccess.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IADOOperations _ado;
        private readonly SqlConnection _conn;

        public UserRepository(IConfiguration configuration, IADOOperations aDOOperations)
        {
            _configuration = configuration;
            _ado = aDOOperations;
            _conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        }
        public async Task<bool> Add<T>(T entity)
        {
            var user = entity as RegisterRepoDto;
           

                     var passHash = "0x" + String.Join("", user.PasswordHash.Select(n => n.ToString("X2")));
                     var passSalt = "0x" + String.Join("", user.PasswordSalt.Select(n => n.ToString("X2")));
            string stmt = $"INSERT INTO {_configuration.GetSection("Tables:UserTable").Value} values" +
                    $"('{user.Id}', '{user.FirstName}','{user.LastName}', '{user.Email}', '{user.PhoneNumber}', {passHash}, {passSalt}, '{DateTime.Now}','{DateTime.Now}', '{1}')";
                string stmt2 = $"INSERT INTO {_configuration.GetSection("Tables:ApplicantTable").Value} values('{user.Id}','{user.Location}');"+
                         $"INSERT INTO {_configuration.GetSection("Tables:UserRoleTable").Value} values('{user.RoleId}','{user.RoleName}','{user.Id}')";
            try
                {
                    if (await _ado.ExecuteForTransactionQuery(stmt,stmt2))
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

        public Task<bool> ApplyJob(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = new User();

            string stmt = $"SELECT * FROM {_configuration.GetSection("Tables:UserTable").Value} WHERE email = '{email}'";

            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "firstname", "lastname", "email","passwordHash", "passwordSalt", "isactive");

                if (response.Count <= 0)
                {
                    return null;
                }

                user = new User
                {
                    Id = response[0].Values[0],
                    LastName = response[0].Values[1],
                    FirstName = response[0].Values[2],
                    Email = response[0].Values[3],
                    PasswordHash = response[0].ByteValues[0],
                    PasswordSalt = response[0].ByteValues[1],
                    IsActive = response[0].Values[4]
                };

            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        public async Task<List<UserReturnedDto>> GetUsers()
        {

            var listOfUsers = new List<UserReturnedDto>();

                   string userTable = _configuration.GetSection("Tables:UserTable").Value;
                    string applicantTable = _configuration.GetSection("Tables:ApplicantTable").Value;
                    string stmt = $"SELECT * FROM {userTable} INNER JOIN {applicantTable} ON {userTable}.Id={applicantTable}.UserId";

            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "firstname", "lastname", "email", "Phonenumber", "location");

                if (response.Count <= 0)
                {
                    throw new Exception("No record found");
                }

                foreach (var item in response)
                {
                    //var values = item.Values.ToArray();

                    listOfUsers.Add(new UserReturnedDto
                    {
                        Id = item.Values[0],
                        LastName = item.Values[1],
                        FirstName = item.Values[2],
                        Email = item.Values[3],
                        PhoneNumber = item.Values[4],
                        Location = item.Values[5]
                    });
                }

            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }

            return listOfUsers;
          
        }

        public async Task<UserReturnedDto> GetUsersById(string id)
        {
            var user = new UserReturnedDto();
            string userTable = _configuration.GetSection("Tables:UserTable").Value;
            string applicantTable = _configuration.GetSection("Tables:ApplicantTable").Value;
            string stmt = $"SELECT * FROM {userTable}  INNER JOIN {applicantTable} ON {userTable}.Id={applicantTable}.UserId WHERE Id = '{id}'";

            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "firstname", "lastname", "email", "phonenumber", "location");

                if (response.Count <= 0)
                {
                    return null;
                }

                user = new UserReturnedDto
                {
                    Id = response[0].Values[0],
                    LastName = response[0].Values[1],
                    FirstName = response[0].Values[2],
                    Email = response[0].Values[3],
                    PhoneNumber = response[0].Values[4],
                    Location = response[0].Values[5]
                };
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        public async Task<List<UserReturnedDto>> GetUsersByName(string name)
        {
            var listOfUsers = new List<UserReturnedDto>();

            string userTable = _configuration.GetSection("Tables:UserTable").Value;
            string applicantTable = _configuration.GetSection("Tables:ApplicantTable").Value;
            string stmt = $"SELECT * FROM {userTable} INNER JOIN {applicantTable} ON {userTable}.Id={applicantTable}.UserId  WHERE firstname ='{name}' OR lastname ='{name}'";

            try
            {
                var response = await _ado.ExecuteForReader(stmt, "id", "firstname", "lastname", "email", "Phonenumber", "location");

                if (response.Count <= 0)
                {
                    return null;
                }

                foreach (var item in response)
                {
                    //var values = item.Values.ToArray();

                    listOfUsers.Add(new UserReturnedDto
                    {
                        Id = item.Values[0],
                        LastName = item.Values[1],
                        FirstName = item.Values[2],
                        Email = item.Values[3],
                        PhoneNumber = item.Values[4],
                        Location = item.Values[5]
                    });
                }

            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }

            return listOfUsers;

        }

        public async Task<bool> IsTableExist(string name)
        {
            int newProdID = 0;
          
            string sql = "SELECT count(*) as IsExists FROM dbo.sysobjects where id = object_id('[dbo].["+name+"]')";
           // try{
                await using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString")))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    newProdID = (int)cmd.ExecuteScalar();
                }
                catch (DbException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            //get the result value: 1-exist; 0-not exist;
           if(newProdID == 1) return true;
           return false;
        }

        public async Task<bool> Delete<T>(T entity)
        {
            var id = entity as string;
            var stmt2 = $"DELETE FROM {_configuration.GetSection("Tables:UserTable").Value} WHERE Id = '{id}'";
            var stmt = $"DELETE FROM {_configuration.GetSection("Tables:ApplicantTable").Value} WHERE userId ='{id}';"+
                        $"DELETE FROM { _configuration.GetSection("Tables:UserRoleTable")} WHERE userId ='{id}'";
            try
            {
                if (await _ado.ExecuteForTransactionQuery(stmt, stmt2))
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

        public async Task<bool> Update<T>(T entity)
        {
            var user = entity as UserReturnedDto;


            
            string stmt = $"UPDATE {_configuration.GetSection("Tables:UserTable").Value} SET" +
                    $" firstname ='{user.FirstName}', lastname ='{user.LastName}', email ='{user.Email}', phonenumber = '{user.PhoneNumber}', dateupdated = '{DateTime.Now}', '{1}' WHERE id ='{user.Id}'";
            string stmt2 = $"UPDATE {_configuration.GetSection("Tables:ApplicantTable").Value} SET location ='{user.Location}' WHERE userId = '{user.Id}'"; 
                     
            try
            {
                if (await _ado.ExecuteForTransactionQuery(stmt, stmt2))
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

        public async Task<bool> DeactivateUser(string id)
        {
            string stmt = $"UPDATE {_configuration.GetSection("Tables:UserTable")} SET IsActive = '{0}' WHERE Id = '{id}'";
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

        public async Task<bool> ActivateUser(string id)
        {
            string stmt = $"UPDATE {_configuration.GetSection("Tables:UserTable")} SET IsActive = 'True' WHERE Id = '{id}'";
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
