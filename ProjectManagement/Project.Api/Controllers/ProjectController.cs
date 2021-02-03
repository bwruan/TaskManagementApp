using System;
using System.Linq;
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
        //token does not come from any query or route parameters
        [HttpGet]
        public async Task<IActionResult> GetProjectById([FromHeader] long id, string token)
        {
            try
            {
                //this is how you extract token from a request
                var token = "";

                //Request object represents the request.
                //we want to check for the headers with the key "Authorization".
                //remember, everytime we send a request, we have header with key call authorization in postman?                
                if (Request.Headers.ContainsKey("Authorization"))
                {
                    //since we have verified, in if statement above, request headers contains "Authorization", we extract out the value stored in "Authorization" key.
                    //the value is the token
                    var jwt = (Request.Headers.FirstOrDefault(s => s.Key.Equals("Authorization"))).Value;

                    //this is just a check to make sure we have a value extracted
                    if (jwt.Count <= 0)
                    {
                        return StatusCode(400);
                    }

                    //since we have a value extracted, the token, remember, starts with "Bearer"?
                    //well we dont need the bearer part because in HTTP, we provide the schema "Bearer". 
                    //check your services classes and you'll see this:
                    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //as you can see, you tell http client "Bearer". Since you have explicitly told http client it is bearer, we dont need the Bearer portion in the extracted token.
                    //so this line replaces the "Bearer" portion of the extracted token with empty string leaving us with just the token.
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

        //name is not unique so you get it from query, not from header!
        //token does not come from any query or route parameters
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