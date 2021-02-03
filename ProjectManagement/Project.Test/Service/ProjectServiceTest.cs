using Moq;
using NUnit.Framework;
using Project.Domain.Services;
using Project.Infrastructure.Repository;
using Project.Infrastructure.UserManagement;
using Project.Infrastructure.UserManagement.Models;
using System;
using System.Threading.Tasks;
using DbProject = Project.Infrastructure.Repository.Entities.Project;

namespace Project.Test.Service
{
    [TestFixture]
    public class ProjectServiceTest
    {
        private Mock<IProjectRepository> _projectRepository;
        private Mock<IUserService> _userService;

        [SetUp]
        public void Setup()
        {
            _projectRepository = new Mock<IProjectRepository>();
            _userService = new Mock<IUserService>();
        }

        [Test]
        public void CreateProject_NameEmpty()
        {
            _projectRepository.Setup(p => p.CreateProject(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.CreateProject("", "Description", 1));
        }

        [Test]
        public void CreateProject_NameNull()
        {
            _projectRepository.Setup(p => p.CreateProject(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.CreateProject(null, "Description", 1));
        }

        [Test]
        public void CreateProject_DescriptionEmpty()
        {
            _projectRepository.Setup(p => p.CreateProject(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.CreateProject("Project1", "", 1));
        }

        [Test]
        public void CreateProject_DescriptionNull()
        {
            _projectRepository.Setup(p => p.CreateProject(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.CreateProject("Project1", null, 1));
        }

        [Test]
        public void CreateProject_OwnerIdError()
        {
            _projectRepository.Setup(p => p.CreateProject(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.CreateProject("Project1", "Description", 0));
        }

        [Test]
        public async Task CreateProject_Success()
        {
            _projectRepository.Setup(p => p.CreateProject(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            await projectService.CreateProject("Project1", "Description", 1);

            _projectRepository.Verify(p => p.CreateProject(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void GetProjectById_ProjectDoesNotExist()
        {
            _projectRepository.Setup(p => p.GetProjectById(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.GetProjectById(1, It.IsAny<string>()));
        }

        //this didnt pass
        [Test]
        public async Task GetProjectById_Success()
        {
            _projectRepository.Setup(p => p.GetProjectById(It.IsAny<long>()))
                .ReturnsAsync(new DbProject());

            _userService.Setup(a => a.GetAccountById(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Account());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            //this returns a project right? you should verify that the returned project is not null and verify properties too if you set any
             await projectService.GetProjectById(1, It.IsAny<string>());

            _projectRepository.Verify(p => p.GetProjectById(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void GetProjectByName_ProjectDoesNotExist()
        {
            _projectRepository.Setup(p => p.GetProjectByName(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.GetProjectByName("Project1", It.IsAny<string>()));
        }

        //this also didnt pass - you have to run ur tests to make sure they pass
        [Test]
        public async Task GetProjectByName_Success()
        {
            _projectRepository.Setup(p => p.GetProjectByName(It.IsAny<string>()))
                .ReturnsAsync(new DbProject());

            _userService.Setup(a => a.GetAccountById(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Account());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);
            //this returns a project right? you should verify that the returned project is not null and verify properties too if you set any
            await projectService.GetProjectByName("Project1", It.IsAny<string>());

            _projectRepository.Verify(p => p.GetProjectByName(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UpdateProject_ProjectDoesNotExist()
        {
            _projectRepository.Setup(p => p.UpdateProject(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.UpdateProject(1, "NewName", "NewDescription", 2));
        }

        [Test]
        public void UpdateProject_NameEmpty()
        {
            _projectRepository.Setup(p => p.UpdateProject(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.UpdateProject(1, "", "NewDescription", 2));
        }

        [Test]
        public void UpdateProject_NameNull()
        {
            _projectRepository.Setup(p => p.UpdateProject(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.UpdateProject(1, null, "NewDescription", 2));
        }

        [Test]
        public void UpdateProject_DescriptionEmpty()
        {
            _projectRepository.Setup(p => p.UpdateProject(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.UpdateProject(1, "NewName", "", 2));
        }

        [Test]
        public void UpdateProject_DescriptionNull()
        {
            _projectRepository.Setup(p => p.UpdateProject(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.UpdateProject(1, "NewName", null, 2));
        }

        [Test]
        public void UpdateProject_NewOwnerIdError()
        {
            _projectRepository.Setup(p => p.UpdateProject(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => projectService.UpdateProject(1, "NewName", "NewDescription", 0));
        }

        [Test]
        public async Task UpdateProject_Success()
        {
            _projectRepository.Setup(p => p.GetProjectById(It.Is<long>(a => a.Equals(1))))
                .ReturnsAsync(new DbProject());

            _projectRepository.Setup(p => p.UpdateProject(It.Is<long>(a => a.Equals(1)), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var projectService = new ProjectService(_projectRepository.Object, _userService.Object);

            await projectService.UpdateProject(1, "NewName", "NewDescription", 2);

            _projectRepository.Verify(p => p.UpdateProject(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()), Times.Once);
        }
    }
}
