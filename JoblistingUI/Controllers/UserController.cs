using AutoMapper;
using JobListing.Models.DTOs;
using JobListing.Models.EFModels;
using JobListing.Models.Helper;
using JobListingAppUI.Commons;
using JobListingAppUI.DTOs;
using JobListingAppUI.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobListingAppUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly ILogger<UserController> _logger;

        private readonly IAdmin _admin;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(IApplicantService applicant, ILogger<UserController> logger, IAdmin admin, UserManager<AppUser> userManager, IMapper mapper)
        {
            _applicantService = applicant;
            _logger = logger;
              _admin = admin;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers(int page, int perPage)
        {
            //var users = await _admin.GetUsers();
            var users = _userManager.Users;
          List<UserReturnedDto> listOfUsers = new List<UserReturnedDto>();
            if (users != null)
            {
                foreach (var user in users)
                {

                    listOfUsers.Add(_mapper.Map<UserReturnedDto>(user));
                }
                var pagedList = PageList<UserReturnedDto>.Paginate(listOfUsers, page, perPage);
                var res = new PaginatedListDto<UserReturnedDto>
                {
                    MetaData = pagedList.MetaData,
                    Data = listOfUsers
                };

                //var res = Utilities.BuildResponse(true, "list of users", null, pagedList);
                return Ok(Utilities.BuildResponse(true, "list of users", null, res));
            }
            else
            {
                ModelState.AddModelError("NotFound", "There was no record of users found!");
                var res = Utilities.BuildResponse<List<UserReturnedDto>>(false, "No result found", ModelState, null);
                return NotFound(res);
            }
        }

        [HttpGet("GetUsers/{name}")]
        public async Task<IActionResult> GetUsersByName(string name)
        {
            var users = await _admin.GetUsersByName(name);
            if (users != null)
            {
                var res = Utilities.BuildResponse(true, "list of users", null, users);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("NotFound", "There was no record of users found!");
                var res = Utilities.BuildResponse<List<UserReturnedDto>>(false, "No result found", ModelState, null);
                return NotFound(res);
            }
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _admin.GetUserbyId(id);
            if (user != null)
            {
                var res = Utilities.BuildResponse(true, "Users' Details", null, user);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("NotFound", "There was no record of users found!");
                var res = Utilities.BuildResponse<List<UserReturnedDto>>(false, "No result found", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("GetUser-by-email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            // var user = await _admin.GetUserbyEmail(email);
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var res = Utilities.BuildResponse(true, "Users' Details", null, user);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("NotFound", $"There was no record of users found with {user.Email}!");
                var res = Utilities.BuildResponse<List<UserReturnedDto>>(false, "No result found", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet]
        [Route("DeactivateUser/{id}")]
        public async Task<IActionResult> DeactivateUser(string id)
        {
            bool result = await _admin.DeactivateUser(id);
            if (result)
            {
                var res = Utilities.BuildResponse(true, "User has been successfully deactivated!", null, result);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Failed", "Deactivation unsucessful");
                var res = Utilities.BuildResponse(false, "Invalid User Id", ModelState, result);
                return BadRequest(res);
            }

        }

        [HttpGet("ActivateUser/{id}")]
        public async Task<IActionResult> ActivateUser(string id)
        {
            bool result = await _admin.ActivateUser(id);
            if (result)
            {
                var res = Utilities.BuildResponse(true, "User has been successfully activated!", null, result);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Failed", "Activation unsucessful");
                var res = Utilities.BuildResponse(false, "Invalid User Id", ModelState, result);
                return BadRequest(res);
            }
        }

        [HttpGet("ApplyJob")]
        public IActionResult ApplyJob(string name)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("InValid Details");
                return BadRequest(ModelState);

            }
            return Ok(_applicantService.ApplyJob(name));
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserDto model)
        {
            //   var response = await _applicantService.RegisterUser(model, model.Password);
            var user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsActive = false,


            };
            var response = await _userManager.CreateAsync(user, model.Password);
            if (!response.Succeeded)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                    return BadRequest(Utilities.BuildResponse<string>(false, "Failed to add user!", ModelState, null));
            }
            var res = await _userManager.AddToRoleAsync(user, "Applicant");
            if (!res.Succeeded)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                    return BadRequest(Utilities.BuildResponse<string>(false, "Failed to add role!", ModelState, null));
            }
            var details = _mapper.Map<RegisterSuccessDto>(model);
           
            return Ok(Utilities.BuildResponse(true, "User sucessfully added!", null, details));

            #region code to ignore
            if (true)
            {
                var resp = Utilities.BuildResponse(true, "User sucessfully added!", null, response);
                return Ok(resp);
            }
            else
            {
                ModelState.AddModelError("Invalid", "Email already exist");
                var resp = Utilities.BuildResponse<RegisterSuccessDto>(false, "New User was not added", ModelState, null);
                return NotFound(resp);
            }
            #endregion

        }
        [Authorize]
        [HttpPut("Update-user/{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserReturnedDto model)
        {
            // check if user logged is the one making the changes - only works for system using Auth tokens
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!id.Equals(currentUserId))
            {
                ModelState.AddModelError("Denied", "You are not allowed to edit another user's details");
                var result2 = Utilities.BuildResponse<List<UserReturnedDto>>(false, "Access denied!", ModelState, null);
                return BadRequest(result2);
            }

           var response = await _applicantService.EditUser(model);
            if (response != null)
            {
               
                var result = Utilities.BuildResponse(true, "User updated sucessfully!", null, response);
                return Ok(result);
            }

            ModelState.AddModelError("Failed", "User not updated");
            var res = Utilities.BuildResponse<List<UserReturnedDto>>(false, "Could not update details of user!", ModelState, null);
            return BadRequest(res);

        }
        [HttpPost("Delete-user")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            bool result = await _admin.DeleteUser(id);
            if (result)
            {
                var res = Utilities.BuildResponse(true, "User has been successfully deleted!", null, result);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Failed", "User does not exist");
                var res = Utilities.BuildResponse(false, "Invalid User Id", ModelState, result);
                return BadRequest(res);
            }
        }
        [HttpPost("Request-Account-Deactivation")]
        public IActionResult RequestDeactivation()
        {
            throw new NotImplementedException();
        }
    }
}
