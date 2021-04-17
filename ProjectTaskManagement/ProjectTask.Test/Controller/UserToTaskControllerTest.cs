using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectTask.Api.Controllers;
using ProjectTask.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreTask = ProjectTask.Domain.Models.Task;

namespace ProjectTask.Test.Controller
{
    [TestFixture]
    public class UserToTaskControllerTest
    {
        private Mock<IUserToTaskService> _userToTaskService;

        [SetUp]
        public void Setup()
        {
            _userToTaskService = new Mock<IUserToTaskService>();
        }

        [Test]
        public async Task GetTasksByAccountId_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _userToTaskService.Setup(u => u.GetTasksByAccountId(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new List<CoreTask>());

            var controller = new UserToTaskController(_userToTaskService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetTasksByAccountId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetTasksByAccountId_InternalServerError()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _userToTaskService.Setup(u => u.GetTasksByAccountId(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new UserToTaskController(_userToTaskService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetTasksByAccountId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }
    }
}
