using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project.Api.Models;
using Project.Domain.Services;

namespace Project.Api.Controllers
{
    [Route("api/project")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IConfiguration _configuration;

        public ProjectController(IProjectService projectService, IConfiguration configuration)
        {
            _projectService = projectService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequest request)
        {
            try
            {
                await _projectService.CreateProject(request.ProjectName, request.ProjectDescription, request.OwnerId);

                return StatusCode(201);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectById([FromHeader] long id, string token)
        {
            try
            {
                var project = await _projectService.GetProjectById(id, token);

                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectByName([FromHeader] string name, string token)
        {
            try
            {
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