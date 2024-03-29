﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectTask.Api.Controllers;
using ProjectTask.Api.Models;
using ProjectTask.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreTask = ProjectTask.Domain.Models.Task;
using CoreAccount = ProjectTask.Domain.Models.Account;
using ProjectTask.Infrastructure.UserManagement;

namespace ProjectTask.Test.Controller
{
    [TestFixture]
    public class TaskControllerTest
    {
        private Mock<ITaskService> _taskService;
        private Mock<IUserService> _userService;

        [SetUp]
        public void Setup()
        {
            _taskService = new Mock<ITaskService>();
            _userService = new Mock<IUserService>();
        }

        [Test]
        public async Task CreateTask_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _taskService.Setup(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .ReturnsAsync(1);

            var controller = new TaskController(_taskService.Object, _userService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.CreateTask(new TaskRequest()
            {
                TaskName = "Task1",
                TaskDescription = "Task1 description.",
                TaskeeId = 1,
                DueDate = DateTime.Parse("6/28/2021")
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task CreateTask_InternalServerError()
        {
            _taskService.Setup(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskController(_taskService.Object, _userService.Object);

            var response = await controller.CreateTask(new TaskRequest()
            {
                TaskName = "Task1",
                TaskDescription = "Task1 description.",
                TaskeeId = 1,
                DueDate = DateTime.Parse("6/28/2021")
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

            var controller = new TaskController(_taskService.Object, _userService.Object)
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

            var controller = new TaskController(_taskService.Object, _userService.Object)
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

            var controller = new TaskController(_taskService.Object, _userService.Object)
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

            var controller = new TaskController(_taskService.Object, _userService.Object)
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
        public async Task GetTasksByProjectId_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _taskService.Setup(t => t.GetTasksByProjectId(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new List<CoreTask>());

            var controller = new TaskController(_taskService.Object, _userService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };            

            var response = await controller.GetTasksByProjectId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetTasksByProjectId_InternalServerError()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _taskService.Setup(t => t.GetTasksByProjectId(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskController(_taskService.Object, _userService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetTasksByProjectId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task MarkComplete_Success()
        {
            _taskService.Setup(t => t.MarkComplete(It.IsAny<long>()))
                .ReturnsAsync(new DateTime());

            var controller = new TaskController(_taskService.Object, _userService.Object);

            var response = await controller.MarkComplete(new BaseTaskRequest()
            {
                TaskId = 1
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var ok = (OkObjectResult)response;

            Assert.AreEqual(ok.StatusCode, 200);
        }

        [Test]
        public async Task MarkComplete_InternalServerError()
        {
            _taskService.Setup(t => t.MarkComplete(It.IsAny<long>()))
                .ThrowsAsync(new Exception());

            var controller = new TaskController(_taskService.Object, _userService.Object);

            var response = await controller.MarkComplete(new BaseTaskRequest()
            {
                TaskId = 1
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

            var controller = new TaskController(_taskService.Object, _userService.Object);

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

            var controller = new TaskController(_taskService.Object, _userService.Object);

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

        [Test]
        public async Task DeleteTask_Success()
        {
            _taskService.Setup(t => t.GetTaskByTaskId(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new CoreTask());

            _taskService.Setup(t => t.RemoveTask(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new TaskController(_taskService.Object, _userService.Object);

            var response = await controller.RemoveTask(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkResult));

            var ok = (OkResult)response;

            Assert.AreEqual(ok.StatusCode, 200);
        }

        [Test]
        public async Task DeleteTask_InternalServerError()
        {
            _taskService.Setup(t => t.GetTaskByTaskId(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException());

            _taskService.Setup(t => t.RemoveTask(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var controller = new TaskController(_taskService.Object, _userService.Object);

            var response = await controller.RemoveTask(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task DeleteAllTaskFromProject_Success()
        {
            _taskService.Setup(t => t.RemoveAllTaskFromProject(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new TaskController(_taskService.Object, _userService.Object);

            var response = await controller.RemoveAllTaskFromProject(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkResult));

            var ok = (OkResult)response;

            Assert.AreEqual(ok.StatusCode, 200);
        }
    }
}
