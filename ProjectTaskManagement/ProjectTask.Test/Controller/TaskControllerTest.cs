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
using CoreTask = ProjectTask.Domain.Models.Task;

namespace ProjectTask.Test.Controller
{
    [TestFixture]
    public class TaskControllerTest
    {
        private Mock<ITaskService> _taskService;

        [SetUp]
        public void Setup()
        {
            _taskService = new Mock<ITaskService>();
        }

        [Test]
        public async Task CreateTask_Success()
        {
            _taskService.Setup(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new TaskController(_taskService.Object);

            var response = await controller.CreateTask(new TaskRequest()
            {
                TaskName = "Task1",
                TaskDescription = "Task1 description.",
                TaskeeId = 1
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(StatusCodeResult));

            var statusCode = (StatusCodeResult)response;

            Assert.AreEqual(statusCode.StatusCode, 201);
        }

        [Test]
        public async Task CreateTask_InternalServerError()
        {
            _taskService.Setup(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskController(_taskService.Object);

            var response = await controller.CreateTask(new TaskRequest()
            {
                TaskName = "Task1",
                TaskDescription = "Task1 description.",
                TaskeeId = 1
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task GetTaskByName_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _taskService.Setup(t => t.GetTaskByName(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new CoreTask());

            var controller = new TaskController(_taskService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetTaskByName("Task1");

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetTaskByName_InternalServerError()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _taskService.Setup(t => t.GetTaskByName(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskController(_taskService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetTaskByName("Task1");

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task GetTaskByTaskId_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _taskService.Setup(t => t.GetTaskByTaskId(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new CoreTask());

            var controller = new TaskController(_taskService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetTaskByTaskId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetTaskById_InternalServerError()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _taskService.Setup(t => t.GetTaskByTaskId(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskController(_taskService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetTaskByTaskId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task MarkComplete_Success()
        {
            _taskService.Setup(t => t.MarkComplete(It.IsAny<long>(), It.IsAny<bool>()))
                .Returns(Task.CompletedTask);

            var controller = new TaskController(_taskService.Object);

            var response = await controller.MarkComplete(new CompleteRequest()
            {
                TaskId = 1,
                IsComplete = true
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkResult));

            var ok = (OkResult)response;

            Assert.AreEqual(ok.StatusCode, 200);
        }

        [Test]
        public async Task MarkComplete_InternalServerError()
        {
            _taskService.Setup(t => t.MarkComplete(It.IsAny<long>(), It.IsAny<bool>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskController(_taskService.Object);

            var response = await controller.MarkComplete(new CompleteRequest()
            {
                TaskId = 1,
                IsComplete = true
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task UpdateTask_Success()
        {
            _taskService.Setup(t => t.UpdateTask(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);

            var controller = new TaskController(_taskService.Object);

            var response = await controller.UpdateTask(new UpdateTaskRequest()
            {
                TaskId = 1,
                NewName = "NewTask1",
                NewDescription = "NewTask1 Description",
                NewDueDate = DateTime.Parse("6/1/2021"),
                NewTaskeeId = 2
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkResult));

            var ok = (OkResult)response;

            Assert.AreEqual(ok.StatusCode, 200);
        }

        [Test]
        public async Task UpdateTask_InternalServerError()
        {
            _taskService.Setup(t => t.UpdateTask(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskController(_taskService.Object);

            var response = await controller.UpdateTask(new UpdateTaskRequest()
            {
                TaskId = 1,
                NewName = "NewTask1",
                NewDescription = "NewTask1 Description",
                NewDueDate = DateTime.Parse("6/1/2021"),
                NewTaskeeId = 2
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }
    }
}
