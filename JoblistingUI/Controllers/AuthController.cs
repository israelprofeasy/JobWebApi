using JobListingAppUI.Commons;
using JobListingAppUI.DTOs;
using JobListingAppUI.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace JobListingAppUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;

        public AuthController(ILogger<AuthController> logger, IUserService user)
        {
            _logger = logger;
            _userService = user;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {

            try
            {
                var response = await _userService.Login(model.email, model.password);
                if (response.status)
                {
                    var res = Utilities.BuildResponse(true, "Logged in Successfull", null, response);
                    return Ok(res);
                }
                else
                {
                    var res = Utilities.BuildResponse(false, "Login Unsuceesfull", null, response);
                    return BadRequest(res);
                    
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }
        [HttpPost("Retrieve-Password")]
        public Task<IActionResult> RetreivePassword(string email)
        {
            throw new NotImplementedException();
        }

    }
}
