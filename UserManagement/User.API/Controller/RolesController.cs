using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.API.Model;
using User.Domain.Services;

namespace User.API.Controller
{
    [Route("api/roles")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpPost]
        [Route("addrole")]
        public async Task<IActionResult> AddRole([FromBody] RolesRequest request)
        {
            try
            {
                await _rolesService.AddRole(request.Name);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("roles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var rolesList = await _rolesService.GetRoles();

                return Ok(rolesList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}