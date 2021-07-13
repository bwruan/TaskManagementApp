using Moq;
using NUnit.Framework;
using ProjectTask.Domain.Services;
using ProjectTask.Infrastructure.ProjectManagement;
using ProjectTask.Infrastructure.ProjectManagement.Models;
using ProjectTask.Infrastructure.Repositories;
using ProjectTask.Infrastructure.UserManagement;
using System;
using System.Threading.Tasks;
using DbTask = ProjectTask.Infrastructure.Repositories.Entities.Task;
using ServiceAccount = ProjectTask.Infrastructure.UserManagement.Models.Account;
using DbAccount = ProjectTask.Infrastructure.Repositories.Entities.Account;

namespace ProjectTask.Test.Service
{
    [TestFixture]
    public class TaskServiceTest
    {
        private Mock<ITaskRepository> _taskRepository;
        private Mock<IProjectService> _projectService;
        private Mock<IUserService> _userService;

        [SetUp]
        public void Setup()
        {
            _taskRepository = new Mock<ITaskRepository>();
            _projectService = new Mock<IProjectService>();
            _userService = new Mock<IUserService>();
        }

        [Test]
        public void CreateTask_NameEmpty()
        {
            _taskRepository.Setup(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ArgumentException());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.CreateTask("", "Description", 1, 1, DateTime.Parse("6/28/2021"), It.IsAny<string>()));
        }

        [Test]
        public void CreateTask_NameNull()
        {
            _taskRepository.Setup(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ArgumentException());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.CreateTask(null, "Description", 1, 1, DateTime.Parse("6/28/2021"), It.IsAny<string>()));
        }

        [Test]
        public void CreateTask_DescriptionEmpty()
        {
            _taskRepository.Setup(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ArgumentException());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.CreateTask("Task1", "", 1, 1, DateTime.Parse("6/28/2021"), It.IsAny<string>()));
        }

        [Test]
        public void CreateTask_DescriptionNull()
        {
            _taskRepository.Setup(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ArgumentException());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.CreateTask("Task1", null, 1, 1, DateTime.Parse("6/28/2021"), It.IsAny<string>()));
        }

        [Test]
        public void CreateTask_ProjectIdError()
        {
            _taskRepository.Setup(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ArgumentException());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.CreateTask("Task1", "Description", 0, 1, DateTime.Parse("6/28/2021"), It.IsAny<string>()));
        }

        [Test]
        public void CreateTask_TaskeeIdError()
        {
            _taskRepository.Setup(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ArgumentException());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.CreateTask("Task1", "Description", 1, 0, DateTime.Parse("6/28/2021"), It.IsAny<string>()));
        }

        [Test]
        public async Task CreateTask_Success()
        {
            _taskRepository.Setup(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .ReturnsAsync(1);

            _userService.Setup(u => u.GetAccountById(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new ServiceAccount());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            await taskService.CreateTask("Task1", "Description", 1, 1, DateTime.Parse("6/28/2021"), It.IsAny<string>());

            _taskRepository.Verify(t => t.CreateTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Test]
        public void GetTaskByName_TaskDoesNotExist()
        {
            _taskRepository.Setup(t => t.GetTaskByName(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.GetTaskByName("Task1", It.IsAny<string>()));
        }

        [Test]
        public async Task GetTaskByName_Success()
        {
            _taskRepository.Setup(t => t.GetTaskByName(It.IsAny<string>()))
                .ReturnsAsync(new DbTask() { Taskee = new DbAccount()});

            _projectService.Setup(p => p.GetProjectById(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Project());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            await taskService.GetTaskByName("Task1", It.IsAny<string>());

            _taskRepository.Verify(t => t.GetTaskByName(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetTaskByTaskId_TaskDoesNotExist()
        {
            _taskRepository.Setup(t => t.GetTaskByTaskId(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.GetTaskByTaskId(1, It.IsAny<string>()));
        }

        [Test]
        public async Task GetTaskByTaskId_Success()
        {
            _taskRepository.Setup(t => t.GetTaskByTaskId(It.IsAny<long>()))
                .ReturnsAsync(new DbTask() { Taskee = new DbAccount() });

            _projectService.Setup(p => p.GetProjectById(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Project());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            await taskService.GetTaskByTaskId(1, It.IsAny<string>());

            _taskRepository.Verify(t => t.GetTaskByTaskId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void MarkComplete_Fail()
        {
            _taskRepository.Setup(t => t.GetTaskByTaskId(It.IsAny<long>()))
                .ReturnsAsync(new DbTask() { TaskId = 1, IsCompleted = true });

            _taskRepository.Setup(t => t.MarkComplete(It.IsAny<long>(), It.IsAny<bool>()))
                .ReturnsAsync(new DateTime());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.MarkComplete(1));
        }

        [Test]
        public async Task MarkComplete_Success()
        {
            _taskRepository.Setup(t => t.GetTaskByTaskId(It.IsAny<long>()))
                .ReturnsAsync(new DbTask() { TaskId = 1, IsCompleted = false});
            
            _taskRepository.Setup(t => t.MarkComplete(It.IsAny<long>(), It.IsAny<bool>()))
                .ReturnsAsync(new DateTime());

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            await taskService.MarkComplete(1);

            _taskRepository.Verify(t => t.MarkComplete(It.IsAny<long>(), It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void UpdateTask_NameEmpty()
        {
            _taskRepository.Setup(t => t.GetTaskByTaskId(It.IsAny<long>()))
                .ReturnsAsync(new DbTask());

            _taskRepository.Setup(t => t.UpdateTask(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.UpdateTask(1, "", "NewDescription", 2, DateTime.Parse("6/1/2021")));
        }

        [Test]
        public void UpdateTask_NameNull()
        {
            _taskRepository.Setup(t => t.GetTaskByTaskId(It.IsAny<long>()))
                .ReturnsAsync(new DbTask());

            _taskRepository.Setup(t => t.UpdateTask(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.UpdateTask(1, null, "NewDescription", 2, DateTime.Parse("6/1/2021")));
        }

        [Test]
        public void UpdateTask_DescriptionEmpty()
        {
            _taskRepository.Setup(t => t.GetTaskByTaskId(It.IsAny<long>()))
                .ReturnsAsync(new DbTask());

            _taskRepository.Setup(t => t.UpdateTask(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.UpdateTask(1, "NewName", "", 2, DateTime.Parse("6/1/2021")));
        }

        [Test]
        public void UpdateTask_DescriptionNull()
        {
            _taskRepository.Setup(t => t.GetTaskByTaskId(It.IsAny<long>()))
                .ReturnsAsync(new DbTask());

            _taskRepository.Setup(t => t.UpdateTask(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.UpdateTask(1, "NewName", null, 2, DateTime.Parse("6/1/2021")));
        }

        [Test]
        public void UpdateTask_TaskeeIdError()
        {
            _taskRepository.Setup(t => t.GetTaskByTaskId(It.IsAny<long>()))
                .ReturnsAsync(new DbTask());

            _taskRepository.Setup(t => t.UpdateTask(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskService.UpdateTask(1, "NewName", "NewDescription", 0, DateTime.Parse("6/1/2021")));
        }

        [Test]
        public async Task UpdateTask_Success()
        {
            _taskRepository.Setup(t => t.GetTaskByTaskId(It.IsAny<long>()))
                .ReturnsAsync(new DbTask());

            _taskRepository.Setup(t => t.UpdateTask(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);

            var taskService = new TaskService(_taskRepository.Object, _projectService.Object, _userService.Object);

            await taskService.UpdateTask(1, "NewName", "NewDescription", 2, DateTime.Now);

            _taskRepository.Verify(t => t.UpdateTask(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}
