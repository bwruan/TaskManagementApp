using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.API.Controller;
using User.API.Model;
using User.Domain.Models;
using User.Domain.Services;

namespace User.Test.Controller
{
    [TestFixture]
    public class RolesControllerTest
    {
        private Mock<IRolesService> _rolesService;

        [SetUp]
        public void Setup()
        {
            _rolesService = new Mock<IRolesService>();
        }

        [Test]
        public async Task AddRole_Success()
        {
            _rolesService.Setup(r => r.AddRole(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var controller = new RolesController(_rolesService.Object);

            var response = await controller.AddRole(new RolesRequest()
            {
                Name = "name"
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(StatusCodeResult));

            var statusCode = (StatusCodeResult)response;

            Assert.AreEqual(statusCode.StatusCode, 201);
        }

        [Test]
        public async Task AddRole_InternalServerError()
        {
            _rolesService.Setup(r => r.AddRole(It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new RolesController(_rolesService.Object);

            var response = await controller.AddRole(new RolesRequest()
            {
                Name = "name"
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task GetRoles_Success()
        {
            _rolesService.Setup(r => r.GetRoles())
                .ReturnsAsync(new List<Roles>());

            var controller = new RolesController(_rolesService.Object);

            var response = await controller.GetRoles();

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetRoles_InternalServerError()
        {
            _rolesService.Setup(r => r.GetRoles())
                .ThrowsAsync(new Exception());

            var controller = new RolesController(_rolesService.Object);

            var response = await controller.GetRoles();

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }
    }
}
