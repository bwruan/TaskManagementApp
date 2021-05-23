using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTask.Api.Models;
using ProjectTask.Domain.Services;

namespace ProjectTask.Api.Controllers
{
    [Route("api/task")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateTask([FromBody] TaskRequest request)
        {
            try
            {
                await _taskService.CreateTask(request.TaskName, request.TaskDescription, request.ProjectId, request.TaskeeId);

                return StatusCode(201);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskByName([FromQuery] string taskName)
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

                var task = await _taskService.GetTaskByName(taskName, token);

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet] 
        [Route("task/{taskId}")]
        public async Task<IActionResult> GetTaskByTaskId(long taskId)
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

                var task = await _taskService.GetTaskByTaskId(taskId, token);

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("project/{projectId}")]
        public async Task<IActionResult> GetTasksByProjectId(long projectId)
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

                var taskList = await _taskService.GetTasksByProjectId(projectId, token);

                return Ok(taskList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskRequest request)
        {
            try
            {
                await _taskService.UpdateTask(request.TaskId, request.NewName, request.NewDescription, request.NewTaskeeId, request.NewDueDate);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("complete")]
        public async Task<IActionResult> MarkComplete([FromBody] CompleteRequest request)
        {
            try
            {
                var completedDate = await _taskService.MarkComplete(request.TaskId);

                return Ok(new { completedDate });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}