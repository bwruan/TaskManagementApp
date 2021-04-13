using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTask.Domain.Services;

namespace ProjectTask.Api.Controllers
{
    [Route("api/usertotask")]
    [ApiController]
    [Authorize]
    public class UserToTaskController : ControllerBase
    {
        private readonly IUserToTaskService _userToTaskService;

        public UserToTaskController(IUserToTaskService userToTaskService)
        {
            _userToTaskService = userToTaskService;
        }

        [HttpGet]
        [Route("{accountId}")]
        public async Task<IActionResult> GetTasksByAccountId(long accountId)
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

                var taskList = await _userToTaskService.GetTasksByAccountId(accountId, token);

                return Ok(taskList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}