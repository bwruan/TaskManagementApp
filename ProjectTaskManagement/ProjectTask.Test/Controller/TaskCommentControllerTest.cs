using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectTask.Api.Controllers;
using ProjectTask.Api.Models;
using ProjectTask.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreComment = ProjectTask.Domain.Models.TaskComment;

namespace ProjectTask.Test.Controller
{
    [TestFixture]
    public class TaskCommentControllerTest
    {
        private Mock<ITaskCommentService> _taskCommentService;

        [SetUp]
        public void Setup()
        {
            _taskCommentService = new Mock<ITaskCommentService>();
        }

        [Test]
        public async Task CreateComment_Success()
        {
            _taskCommentService.Setup(c => c.CreateComment(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new TaskCommentController(_taskCommentService.Object);

            var response = await controller.CreateComment(new CommentRequest()
            {
                Comment = "comment1",
                CommenterId = 1
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(StatusCodeResult));

            var statusCode = (StatusCodeResult)response;

            Assert.AreEqual(statusCode.StatusCode, 201);
        }

        [Test]
        public async Task CreateComment_InternalServerError()
        {
            _taskCommentService.Setup(c => c.CreateComment(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskCommentController(_taskCommentService.Object);

            var response = await controller.CreateComment(new CommentRequest()
            {
                Comment = "comment1",
                CommenterId = 1
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task GetCommentByCommentId_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _taskCommentService.Setup(c => c.GetCommentByCommentId(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new CoreComment());

            var controller = new TaskCommentController(_taskCommentService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetCommentByCommentId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetCommentByCommentId_InternalServerError()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _taskCommentService.Setup(c => c.GetCommentByCommentId(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskCommentController(_taskCommentService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetCommentByCommentId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task UpdateComment_Success()
        {
            _taskCommentService.Setup(c => c.UpdateComment(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var controller = new TaskCommentController(_taskCommentService.Object);

            var response = await controller.UpdateComment(new UpdateCommentRequest()
            {
                CommentId = 1,
                NewComment = "new comment"
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkResult));

            var ok = (OkResult)response;

            Assert.AreEqual(ok.StatusCode, 200);
        }

        [Test]
        public async Task UpdateComment_InternalServerError()
        {
            _taskCommentService.Setup(c => c.UpdateComment(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskCommentController(_taskCommentService.Object);

            var response = await controller.UpdateComment(new UpdateCommentRequest()
            {
                CommentId = 1,
                NewComment = "new comment"
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task GetCommentsByTaskId_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _taskCommentService.Setup(c => c.GetCommentsByTaskId(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new List<CoreComment>());

            var controller = new TaskCommentController(_taskCommentService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetCommentsByTaskId(1, 1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetCommentsByTaskId_InternalServerError()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _taskCommentService.Setup(c => c.GetCommentsByTaskId(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskCommentController(_taskCommentService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetCommentsByTaskId(1, 1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }
    }
}
