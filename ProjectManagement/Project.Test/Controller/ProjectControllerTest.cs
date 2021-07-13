using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Project.Api.Controllers;
using Project.Api.Models;
using Project.Domain.Services;
using CoreProject = Project.Domain.Models.Project;
using System;
using System.Threading.Tasks;

namespace Project.Test.Controller
{
    [TestFixture]
    public class ProjectControllerTest
    {
        private Mock<IProjectService> _projectService;


        [SetUp]
        public void Setup()
        {
            _projectService = new Mock<IProjectService>();
        }

        [Test]
        public async Task CreateProject_Success()
        {
            _projectService.Setup(p => p.CreateProject(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(1);

            var controller = new ProjectController(_projectService.Object);

            var response = await controller.CreateProject(new ProjectRequest()
            {
                ProjectName = "Project1",
                ProjectDescription = "Description",
                OwnerId = 1,
                StartDate = Convert.ToDateTime("05/30/2021"),
                EndDate = Convert.ToDateTime("12/18/2021")
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task CreateProject_InternalServerError()
        {
            _projectService.Setup(p => p.CreateProject(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception());

            var controller = new ProjectController(_projectService.Object);

            var response = await controller.CreateProject(new ProjectRequest()
            {
                ProjectName = "Project1",
                ProjectDescription = "Description",
                OwnerId = 1,
                StartDate = Convert.ToDateTime("05/30/2021"),
                EndDate = Convert.ToDateTime("12/18/2021")
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task Test_GetProjectById_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _projectService.Setup(p => p.GetProjectById(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new CoreProject());

            var controller = new ProjectController(_projectService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetProjectById(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetProjectById_InternalServerError()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _projectService.Setup(p => p.GetProjectById(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new ProjectController(_projectService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetProjectById(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task Test_GetProjectByName_Success()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _projectService.Setup(p => p.GetProjectByName(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new CoreProject());

            var controller = new ProjectController(_projectService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetProjectByName("Project1");

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetProjectByName_InternalServerError()
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Authorization"] = "Bearer testtoken";

            _projectService.Setup(p => p.GetProjectByName(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new ProjectController(_projectService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var response = await controller.GetProjectByName("Project1");

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task UpdateProject_Success()
        {
            _projectService.Setup(p => p.UpdateProject(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new ProjectController(_projectService.Object);

            var response = await controller.UpdateProject(new UpdateProjectRequest()
            {
                ProjectId = 1,
                NewName = "Project1.1",
                NewDescription = "New Description",
                NewOwnerId = 2
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkResult));

            var ok = (OkResult)response;

            Assert.AreEqual(ok.StatusCode, 200);
        }

        [Test]
        public async Task UpdateAccountInfo_InternalServerError()
        {
            _projectService.Setup(p => p.UpdateProject(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new Exception());

            var controller = new ProjectController(_projectService.Object);

            var response = await controller.UpdateProject(new UpdateProjectRequest()
            {
                ProjectId = 1,
                NewName = "Project1.1",
                NewDescription = "New Description",
                NewOwnerId = 2
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }
    }
}
