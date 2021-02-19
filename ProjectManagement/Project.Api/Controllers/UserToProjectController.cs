using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Services;

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

        //is projectId unique?
        [HttpGet]
        public async Task<IActionResult> GetAccountByProjectId([FromQuery] long projectId)
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

        //is accountId unique?
        [HttpGet]
        public async Task<IActionResult> GetProjectsByAccountId([FromQuery] long accountId)
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
    }
}