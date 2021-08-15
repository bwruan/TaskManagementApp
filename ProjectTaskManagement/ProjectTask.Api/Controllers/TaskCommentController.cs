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
    [Route("api/comment")]
    [ApiController]
    [Authorize]
    public class TaskCommentController : ControllerBase
    {
        private readonly ITaskCommentService _taskCommentService;

        public TaskCommentController(ITaskCommentService taskCommentService)
        {
            _taskCommentService = taskCommentService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateComment([FromBody] CommentRequest request)
        {
            try
            {
                await _taskCommentService.CreateComment(request.Comment, request.CommenterId, request.TaskId);
                var lastPage = await _taskCommentService.GetLastPageOfCommentsList(request.TaskId);

                return Ok(lastPage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{commentId}")]
        public async Task<IActionResult> GetCommentByCommentId(long commentId)
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

                var comment = await _taskCommentService.GetCommentByCommentId(commentId, token);

                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("get/{taskId}/{page}")]
        public async Task<IActionResult> GetCommentsByTaskId(long taskId, int page)
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

                var comments = await _taskCommentService.GetCommentsByTaskId(taskId, page, token);

                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("lastPage/{taskId}")]
        public async Task<IActionResult> GetLastPageOfCommentsList(long taskId)
        {
            try
            {
                var lastPage = await _taskCommentService.GetLastPageOfCommentsList(taskId);

                return Ok(lastPage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentRequest request)
        {
            try
            {
                await _taskCommentService.UpdateComment(request.CommentId, request.NewComment);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}