using ProjectTask.Domain.Mapper;
using ProjectTask.Domain.Models;
using ProjectTask.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ITaskCommentRepository _taskCommentRepository;

        public TaskCommentService(ITaskCommentRepository taskCommentRepository)
        {
            _taskCommentRepository = taskCommentRepository;
        }

        public async System.Threading.Tasks.Task CreateComment(string comment, long commenterId, long taskId)
        {
            if (string.IsNullOrEmpty(comment))
            {
                throw new ArgumentException("Please input comment.");
            }
            
            await _taskCommentRepository.CreateComment(comment, commenterId, taskId);
        }

        public async Task<TaskComment> GetCommentByCommentId(long commentId, string token)
        {
            var comment = await _taskCommentRepository.GetCommentByCommentId(commentId);

            if(comment == null)
            {
                throw new ArgumentException("Comment does not exist");
            }

            var coreComment = TaskCommentMapper.DbCommentToCoreComment(comment);

            return coreComment;
        }

        public async Task<List<TaskComment>> GetCommentsByTaskId(long taskId, int page)
        {
            var commentList = new List<Models.TaskComment>();

            var comments = await _taskCommentRepository.GetCommentsByTaskId(taskId, page);

            if (page <= 0)
            {
                throw new ArgumentException("Cannot display less than 1 page.");
            }

            foreach (var comment in comments)
            {
                var coreComment = TaskCommentMapper.DbCommentToCoreComment(comment);

                commentList.Add(coreComment);
            }

            return commentList;
        }

        public async System.Threading.Tasks.Task UpdateComment(long commentId, string newComment)
        {
            var comment = await _taskCommentRepository.GetCommentByCommentId(commentId);

            if (comment == null)
            {
                throw new ArgumentException("Comment does not exist");
            }

            if (string.IsNullOrEmpty(newComment))
            {
                throw new ArgumentException("Please input comment");
            }

            await _taskCommentRepository.UpdateComment(commentId, newComment);
        }
    }
}
