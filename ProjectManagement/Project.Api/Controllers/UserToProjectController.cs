using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Services;

namespace Project.Api.Controllers
{
    //put authorize on this one - this needs to be protected too
    [Route("api/usertoproject")]
    [ApiController]
    public class UserToProjectController : ControllerBase
    {
        private readonly IUserToProjectService _userToProjectService;

        public UserToProjectController(IUserToProjectService userToProjectService)
        {
            _userToProjectService = userToProjectService;
        }

        //same comments as those on project controller
        [HttpGet]
        public async Task<IActionResult> GetAccountByProjectId([FromHeader] long projectId, string token)
        {
            try
            {
                var accountList = await _userToProjectService.GetAccountByProjectId(projectId, token);

                return Ok(accountList);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //same comments
        [HttpGet]
        public async Task<IActionResult> GetProjectsByAccountId([FromHeader] long accountId, string token)
        {
            try
            {
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