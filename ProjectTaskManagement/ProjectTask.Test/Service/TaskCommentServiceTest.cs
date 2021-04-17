using Moq;
using NUnit.Framework;
using ProjectTask.Domain.Services;
using ProjectTask.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DbComment = ProjectTask.Infrastructure.Repositories.Entities.TaskComment;

namespace ProjectTask.Test.Service
{
    [TestFixture]
    public class TaskCommentServiceTest
    {
        private Mock<ITaskCommentRepository> _taskCommentRepository;

        [SetUp]
        public void Setup()
        {
            _taskCommentRepository = new Mock<ITaskCommentRepository>();
        }

        [Test]
        public void CreateComment_CommentEmpty()
        {
            _taskCommentRepository.Setup(c => c.CreateComment(It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var taskCommentService = new TaskCommentService(_taskCommentRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskCommentService.CreateComment("", 1));
        }

        [Test]
        public void CreateComment_CommentNull()
        {
            _taskCommentRepository.Setup(c => c.CreateComment(It.IsAny<string>(), It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var taskCommentService = new TaskCommentService(_taskCommentRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskCommentService.CreateComment(null, 1));
        }

        [Test]
        public async Task CreateComment_Success()
        {
            _taskCommentRepository.Setup(c => c.CreateComment(It.IsAny<string>(), It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var taskCommentService = new TaskCommentService(_taskCommentRepository.Object);

            await taskCommentService.CreateComment("Comment", 1);

            _taskCommentRepository.Verify(c => c.CreateComment(It.IsAny<string>(), It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void GetCommentByCommentId_CommentDoesNotExist()
        {
            _taskCommentRepository.Setup(c => c.GetCommentByCommentId(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            var taskCommentService = new TaskCommentService(_taskCommentRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskCommentService.GetCommentByCommentId(1, It.IsAny<string>()));
        }

        [Test]
        public async Task GetCommentByCommentId_Success()
        {
            _taskCommentRepository.Setup(c => c.GetCommentByCommentId(It.IsAny<long>()))
                .ReturnsAsync(new DbComment());

            var taskCommentService = new TaskCommentService(_taskCommentRepository.Object);

            await taskCommentService.GetCommentByCommentId(1, It.IsAny<string>());

            _taskCommentRepository.Verify(c => c.GetCommentByCommentId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void UpdateComment_CommentDoesNotExist()
        {
            _taskCommentRepository.Setup(c => c.GetCommentByCommentId(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException());

            _taskCommentRepository.Setup(c => c.UpdateComment(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var taskCommentService = new TaskCommentService(_taskCommentRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskCommentService.UpdateComment(1, It.IsAny<string>()));
        }

        [Test]
        public void UpdateComment_CommentEmpty()
        {
            _taskCommentRepository.Setup(c => c.GetCommentByCommentId(It.IsAny<long>()))
                .ReturnsAsync(new DbComment());

            _taskCommentRepository.Setup(c => c.UpdateComment(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var taskCommentService = new TaskCommentService(_taskCommentRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskCommentService.UpdateComment(1, ""));
        }

        [Test]
        public void UpdateComment_CommentNull()
        {
            _taskCommentRepository.Setup(c => c.GetCommentByCommentId(It.IsAny<long>()))
                .ReturnsAsync(new DbComment());

            _taskCommentRepository.Setup(c => c.UpdateComment(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var taskCommentService = new TaskCommentService(_taskCommentRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => taskCommentService.UpdateComment(1, null));
        }

        [Test]
        public async Task UpdateComment_Success()
        {
            _taskCommentRepository.Setup(c => c.GetCommentByCommentId(It.IsAny<long>()))
                .ReturnsAsync(new DbComment());

            _taskCommentRepository.Setup(c => c.UpdateComment(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var taskCommentService = new TaskCommentService(_taskCommentRepository.Object);

            await taskCommentService.UpdateComment(1, "New Comment");

            _taskCommentRepository.Verify(c => c.UpdateComment(It.IsAny<long>(), It.IsAny<string>()), Times.Once);
        }
    }
}
