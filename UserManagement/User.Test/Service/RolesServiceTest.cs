using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Domain.Models;
using User.Domain.Services;
using User.Infrastructure.Repository;
using EfRoles = User.Infrastructure.Repository.Entities.Roles;

namespace User.Test.Service
{
    [TestFixture]
    public class RolesServiceTest
    {
        private Mock<IRolesRepository> _rolesRepository;

        [SetUp]
        public void Setup()
        {
            _rolesRepository = new Mock<IRolesRepository>();
        }

        [Test]
        public void AddRole_NameEmpty()
        {
            _rolesRepository.Setup(r => r.AddRole(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException());

            var rolesService = new RolesService(_rolesRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => rolesService.AddRole(""));
        }

        [Test]
        public void AddRole_NameNull()
        {
            _rolesRepository.Setup(r => r.AddRole(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException());

            var rolesService = new RolesService(_rolesRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => rolesService.AddRole(null));
        }

        [Test]
        public async Task AddRole_Success()
        {
            _rolesRepository.Setup(r => r.AddRole(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var rolesService = new RolesService(_rolesRepository.Object);

            await rolesService.AddRole("Administrator");

            _rolesRepository.Verify(r => r.AddRole(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetRoles_Fail()
        {
            _rolesRepository.Setup(r => r.GetRoles())
                .ThrowsAsync(new ArgumentException());

            var rolesService = new RolesService(_rolesRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => rolesService.GetRoles());
        }

        [Test]
        public async Task GetRoles_Success()
        {
            _rolesRepository.Setup(r => r.GetRoles())
                .ReturnsAsync(new List<EfRoles>());

            var rolesService = new RolesService(_rolesRepository.Object);

            await rolesService.GetRoles();

            _rolesRepository.Verify(r => r.GetRoles(), Times.Once);
        }
    }
}
