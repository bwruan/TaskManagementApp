using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Project.Api.Controllers;
using Project.Api.Models;
using Project.Domain.Models;
using Project.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreProject = Project.Domain.Models.Project;

namespace Project.Test.Controller
{
    [TestFixture]
    public class UserToProjectControllerTest
    {
        private Mock<IUserToProjectService> _userToProjectService;

        [SetUp]
        public void Setup()
        {
            _userToProjectService = new Mock<IUserToProjectService>();
        }

        [Test]
        public async Task GetAccountByProjectId_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _userToProjectService.Setup(u => u.GetAccountByProjectId(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new List<Account>());

            var controller = new UserToProjectController(_userToProjectService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetAccountByProjectId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetAccountByProjectId_InternalServerError()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _userToProjectService.Setup(u => u.GetAccountByProjectId(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new UserToProjectController(_userToProjectService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetAccountByProjectId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task GetProjectsByAccountId_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _userToProjectService.Setup(u => u.GetProjectsByAccountId(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new List<CoreProject>());

            var controller = new UserToProjectController(_userToProjectService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetProjectsByAccountId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetProjectsByAccountId_InternalServerError()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _userToProjectService.Setup(u => u.GetProjectsByAccountId(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new UserToProjectController(_userToProjectService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetProjectsByAccountId(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task AddMember_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _userToProjectService.Setup(u => u.AddMember(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Account());

            var controller = new UserToProjectController(_userToProjectService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.AddMember(new MemberRequest()
            {
                ProjectId = 1,
                Email = "email@email.com"
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task AddMember_InternalServerError()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _userToProjectService.Setup(u => u.AddMember(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new UserToProjectController(_userToProjectService.Object);

            var response = await controller.AddMember(new MemberRequest()
            {
                ProjectId = 1,
                Email = "email@email.com"
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }
    }
}
