using JobListing.Models.EFModels;
using JobListingAppUI.DbContexts;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.DataAccess.EFCore
{
    public class SeederClass
    {
        private readonly JobListingDbContext _ctx;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeederClass(JobListingDbContext ctx, UserManager<AppUser> userManager, RoleManager<IdentityRole> role)
        {
            _ctx = ctx;
            _userManager = userManager;
            _roleManager = role;
        }

        public async Task SeedMe()
        {
            _ctx.Database.EnsureCreated();
            try
            {

                    var roles = new string[] { "Applicant", "Admin" };
                    if (!_roleManager.Roles.Any())
                    {
                        foreach(var role in roles)
                        {
                            await _roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }

                    var data = System.IO.File.ReadAllText("../JobListing.DataAccess/EFCore/SeedData.json");
                        var listOfUsers = JsonConvert.DeserializeObject<List<AppUser>>(data);
                    if (!_userManager.Users.Any())
                    {
                        var count = 0;
                        var role = "";
                        foreach (var user in listOfUsers)
                        {
                            user.UserName = user.Email;
                           role = count < 1 ? roles[1]:roles[0];
                           var res = await _userManager.CreateAsync(user, "P@ssw0rd");
                        if(res.Succeeded)
                            await _userManager.AddToRoleAsync(user, role);
                        count++;
                        }
                    }
            }
            catch (DbException ex)
            {

            }
        }
    }
}
