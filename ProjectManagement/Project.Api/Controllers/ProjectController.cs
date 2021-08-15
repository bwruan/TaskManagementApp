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
    [Route("api/project")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IUserToProjectService _userToProjectService;

        public ProjectController(IProjectService projectService, IUserToProjectService userToProjectService)
        {
            _projectService = projectService;
            _userToProjectService = userToProjectService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequest request)
        {
            try
            {
                var projectId = await _projectService.CreateProject(request.ProjectName, request.ProjectDescription, request.OwnerId, request.StartDate, request.EndDate);

                return Ok( new { projectId});
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{projectId}")]
        public async Task<IActionResult> DeleteProject(long projectId)
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

                await _projectService.DeleteProject(projectId, token);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProjectById(long id)
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

                var project = await _projectService.GetProjectById(id, token);

                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectByName([FromQuery] string name)
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

                var project = await _projectService.GetProjectByName(name, token);

                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request)
        {
            try
            {
                await _projectService.UpdateProject(request.ProjectId, request.NewName, request.NewDescription, request.NewOwnerId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}