using EventsPlanner.ContextDTO.RoleDtos;
using EventsPlanner.WebAPI.AppServices.RoleServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlanner.WebAPI.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleAppService _roleAppService;
        public RoleController(RoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAllRolesAsync()
        {
            List<RoleDto> subcategories = await _roleAppService.GetAllRolesAsync();
            return Ok(subcategories);
        }

        [HttpGet("by-id")]
        public async Task<ActionResult> GetRoleByIdAsync([FromQuery] GetRoleRequest request)
        {
            RoleDto subcategories = await _roleAppService.GetRoleByIdAsync(request);
            return Ok(subcategories);
        }
    }
}
