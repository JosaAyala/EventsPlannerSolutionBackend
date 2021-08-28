using EventsPlanner.ContextDTO.RoleDtos;
using EventsPlanner.ContextDTO.UserDtos;
using EventsPlanner.WebAPI.AppServices.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlanner.WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserAppService _userAppService;
        public UserController(UserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAllUserAsync()
        {
            List<UserDto> users = await _userAppService.GetAllUserAsync();
            return Ok(users);
        }

        [HttpGet("by-filter")]
        public async Task<ActionResult> GetUsersByFilterAsync([FromQuery] GetUserRequest request)
        {
            List<UserDto> users = await _userAppService.GetUsersByFilterAsync(request);
            return Ok(users);
        }

        [HttpPost]
        public ActionResult CreateNewUser([FromBody] CreateEditUserRequest request)
        {
            UserDto response = _userAppService.CreateNewUser(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult> GetUserToLoginAsync([FromBody] LoginUserRequest request)
        {
            UserDto response = await _userAppService.GetUserToLoginAsync(request);
            return Ok(response);
        }

        [HttpPut]
        public ActionResult UpdateUser([FromBody] CreateEditUserRequest request)
        {
            UserDto response = _userAppService.UpdateUser(request);
            return Ok(response);
        }

        [HttpDelete]
        public ActionResult DeleteUser([FromBody] string userLogin)
        {
            UserDto response = _userAppService.DeleteUser(userLogin);
            return Ok(response);
        }
    }
}
