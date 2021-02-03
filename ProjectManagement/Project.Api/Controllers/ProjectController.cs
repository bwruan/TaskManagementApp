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
        private readonly IConfiguration _configuration; //not use, remove

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

        //remember, id is unique. when we have uniqueness, we do route parameters. Also, if you are doing query parameters, it is [FromQuery], not [FromHeader]
        //token does not come from any query or route parameters - i will show u later how to grab token
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

        //name is not unique so you get it from query, not from header!
        //token does not come from any query or route parameters - i will show u later how to grab token
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