using Moq;
using NUnit.Framework;
using Project.Domain.Services;
using Project.Infrastructure.Repository;
using Project.Infrastructure.UserManagement;
using Project.Infrastructure.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbProject = Project.Infrastructure.Repository.Entities.Project;


namespace Project.Test.Service
{
    [TestFixture]
    public class UserToProjectServiceTest
    {
        private Mock<IUserToProjectRepository> _userToProjectRepository;
        private Mock<IProjectRepository> _projectRepository;
        private Mock<IUserService> _userService;

        [SetUp]
        public void Setup()
        {
            _userToProjectRepository = new Mock<IUserToProjectRepository>();
            _projectRepository = new Mock<IProjectRepository>();
            _userService = new Mock<IUserService>();
        }

        [Test]
        public void GetAccountByProjectId_ProjectDoesNotExist()
        {
            _projectRepository.Setup(p => p.GetProjectById(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var userToProjectService = new UserToProjectService(_userToProjectRepository.Object, _projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => userToProjectService.GetAccountByProjectId(1, It.IsAny<string>()));
        }

        [Test]
        public async Task GetAccountByProjectId_Success()
        {
            _projectRepository.Setup(p => p.GetProjectById(It.IsAny<long>()))
                .ReturnsAsync(new DbProject());

            _userToProjectRepository.Setup(u => u.GetAccountIdsByProjectId(It.IsAny<long>()))
                .ReturnsAsync(new List<long>());

            _userService.Setup(a => a.GetAccountById(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Account());

            var userToProjectService = new UserToProjectService(_userToProjectRepository.Object, _projectRepository.Object, _userService.Object);

            await userToProjectService.GetAccountByProjectId(1, It.IsAny<string>());

            _userToProjectRepository.Verify(u => u.GetAccountIdsByProjectId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public async Task GetProjectsByAccountId_Success()
        {
            _userToProjectRepository.Setup(u => u.GetProjectsByAccountId(It.IsAny<long>()))
                .ReturnsAsync(new List<DbProject>());

            _userService.Setup(a => a.GetAccountById(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Account());

            var userToProjectService = new UserToProjectService(_userToProjectRepository.Object, _projectRepository.Object, _userService.Object);

            await userToProjectService.GetProjectsByAccountId(1, It.IsAny<string>());

            _userToProjectRepository.Verify(u => u.GetProjectsByAccountId(It.IsAny<long>()), Times.Once);
        }
    }
}
