using Moq;
using NUnit.Framework;
using ProjectTask.Domain.Services;
using ProjectTask.Infrastructure.Repositories;
using ProjectTask.Infrastructure.UserManagement;
using ProjectTask.Infrastructure.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DbTask = ProjectTask.Infrastructure.Repositories.Entities.Task;

namespace ProjectTask.Test.Service
{
    [TestFixture]
    public class UserToTaskServiceTest
    {
        private Mock<IUserToTaskRepository> _userToTaskRepository;
        private Mock<IUserService> _userService;

        [SetUp]
        public void Setup()
        {
            _userToTaskRepository = new Mock<IUserToTaskRepository>();
            _userService = new Mock<IUserService>();
        }

        [Test]
        public void GetTasksByAccountId_NoTaskForAccount()
        {
            _userService.Setup(u => u.GetAccountById(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Account());

            _userToTaskRepository.Setup(t => t.GetTasksByAccountId(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var userToTaskService = new UserToTaskService(_userToTaskRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => userToTaskService.GetTasksByAccountId(1, It.IsAny<string>()));
        }

        [Test]
        public async Task GetTasksByAccountId_Success()
        {
            _userService.Setup(u => u.GetAccountById(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Account());

            _userToTaskRepository.Setup(t => t.GetTasksByAccountId(It.IsAny<long>()))
                .ReturnsAsync(new List<DbTask>());

            var userToTaskService = new UserToTaskService(_userToTaskRepository.Object, _userService.Object);

            await userToTaskService.GetTasksByAccountId(1, It.IsAny<string>());

            _userToTaskRepository.Verify(t => t.GetTasksByAccountId(It.IsAny<long>()), Times.Once);
        }
    }
}
