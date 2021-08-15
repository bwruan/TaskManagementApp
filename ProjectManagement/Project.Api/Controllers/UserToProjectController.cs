using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Models;
using Project.Domain.Services;
using Project.Infrastructure.UserManagement;

namespace Project.Api.Controllers
{
    
    [Route("api/usertoproject")]
    [ApiController]
    [Authorize]
    public class UserToProjectController : ControllerBase
    {
        private readonly IUserToProjectService _userToProjectService;

        public UserToProjectController(IUserToProjectService userToProjectService)
        {
            _userToProjectService = userToProjectService;
        }

        [HttpGet]
        [Route("project/{projectId}")]
        public async Task<IActionResult> GetAccountByProjectId(long projectId)
        {
            try
            {
                var token = "";

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    var jwt = (Request.Headers.FirstOrDefault(s => s.Key.Equals("Authorization"))).Value;

                    if (jwt.Count <= 0)
                    {
                        return StatusCode(400);
                    }

                    token = jwt[0].Replace("Bearer ", "");
                }

                var accountList = await _userToProjectService.GetAccountByProjectId(projectId, token);

                return Ok(accountList);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("account/{accountId}")]
        public async Task<IActionResult> GetProjectsByAccountId(long accountId)
        {
            try
            {
                var token = "";

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    var jwt = (Request.Headers.FirstOrDefault(s => s.Key.Equals("Authorization"))).Value;

                    if (jwt.Count <= 0)
                    {
                        return StatusCode(400);
                    }

                    token = jwt[0].Replace("Bearer ", "");
                }

                var projectList = await _userToProjectService.GetProjectsByAccountId(accountId, token);

                return Ok(projectList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("addMember")]
        public async Task<IActionResult> AddMember([FromBody] MemberRequest request)
        {
            try
            {
                var token = "";

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    var jwt = (Request.Headers.FirstOrDefault(s => s.Key.Equals("Authorization"))).Value;

                    if (jwt.Count <= 0)
                    {
                        return StatusCode(400);
                    }

                    token = jwt[0].Replace("Bearer ", "");
                }

                var account = await _userToProjectService.AddMember(request.ProjectId, request.Email, token);

                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{projectId}/{accountId}")]
        public async Task<IActionResult> RemoveProjectMember(long projectId, long accountId)
        {
            try
            {
                var token = "";

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    var jwt = (Request.Headers.FirstOrDefault(s => s.Key.Equals("Authorization"))).Value;

                    if (jwt.Count <= 0)
                    {
                        return StatusCode(400);
                    }

                    token = jwt[0].Replace("Bearer ", "");
                }

                await _userToProjectService.RemoveProjectMember(projectId, accountId, token);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}