using JobListing.Core.Services.RoleService.Interfaces;
using JobListing.Models.DTOs;
using JobListingAppUI.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingAppUI.Controllers
{

   // [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleService;

        public RoleController(IRoleServices roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("Add-role")]
        public async Task<IActionResult> AddRole(RoleDto newRole)
        {
            var result = await _roleService.Add(newRole);
            if (result)
            {
                var res = Utilities.BuildResponse(true, "Roles Added", null, newRole.Name);
                return Ok(res);

            }
            else
            {
                ModelState.AddModelError("Invalid", "Role already exist");
                var res = Utilities.BuildResponse<string>(false, "Role already exist", ModelState, null);
                return BadRequest(res);

            }
            
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRoles();
            if(roles != null)
            {
                var res = Utilities.BuildResponse(true, "list of roles", null, roles);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("NotFound", "There was no record of roles found!");
                var res = Utilities.BuildResponse<List<RoleReturnedDto>>(false, "No result found", ModelState, null);
                return NotFound(res);
            }
        }

        [HttpGet("Get-role/{id}")]
        public async Task<IActionResult> GetSingle(string id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role != null)
            {
                var res = Utilities.BuildResponse(true, "Role Details", null, role);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("NotFound", "Role details not found");
                var res = Utilities.BuildResponse<RoleReturnedDto>(false, "No result found", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("GetRoles/{name}")]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            var role = await _roleService.GetRoleByName(name);
            if (role != null)
            {
                var res = Utilities.BuildResponse(true, "Role Details", null, role);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("NotFound", $"Role details not found for the role {role.Name}");
                var res = Utilities.BuildResponse<RoleReturnedDto>(false, "No result found with", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpPut("update-role")]
        public async Task<IActionResult> Update(RoleReturnedDto updateRoleDto)
        {

            bool result = await _roleService.Update(updateRoleDto);
            if (result)
            {
                var res = Utilities.BuildResponse(true, "Role successfully updated!", null, updateRoleDto.Name);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Failed", "Role could not be Updated");
                var res = Utilities.BuildResponse(false, "Role does not exist", ModelState, updateRoleDto);
                return BadRequest(res);
            }
        }

        [HttpDelete("Delete-role/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool result = await _roleService.Delete(id);
            if (result)
            {
                var res = Utilities.BuildResponse(true, "Role successfully deleted!", null, "Sucessful");
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Failed", "Role could not be deleted");
                var res = Utilities.BuildResponse(false, "Role does not exist", ModelState, "Unsucessful!");
                return BadRequest(res);
            }
        }
    }
}
