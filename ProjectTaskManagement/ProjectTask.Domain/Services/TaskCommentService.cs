using ProjectTask.Domain.Mapper;
using ProjectTask.Domain.Models;
using ProjectTask.Infrastructure.Repositories;
using ProjectTask.Infrastructure.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ITaskCommentRepository _taskCommentRepository;
        private readonly IUserService _userService;

        public TaskCommentService(ITaskCommentRepository taskCommentRepository, IUserService userService)
        {
            _taskCommentRepository = taskCommentRepository;
            _userService = userService;
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

        public async Task<List<TaskComment>> GetCommentsByTaskId(long taskId, int page, string token)
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
                var account = await _userService.GetAccountById(comment.CommenterId, token);

                coreComment.Commenter = TaskCommentMapper.UserAccountToCoreAccount(account);

                commentList.Add(coreComment);
            }

            return commentList;
        }

        public async Task<decimal> GetLastPageOfCommentsList(long taskId)
        {
            return await _taskCommentRepository.GetLastPageOfCommentsList(taskId);
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
